// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_NoCombat
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_NoCombat : PassiveAbilityBase
  {
    public override bool isTargetable => false;

    public override bool isImmortal => true;

    public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
    {
      List<LorId> lorIdList1 = new List<LorId>();
      lorIdList1.Add(new LorId(AftermathCollectionInitializer.packageId, 60105));
      lorIdList1.Add(new LorId(AftermathCollectionInitializer.packageId, 60120));
      lorIdList1.Add(new LorId(AftermathCollectionInitializer.packageId, 60121));
      lorIdList1.Add(new LorId(AftermathCollectionInitializer.packageId, 60122));
      lorIdList1.Add(new LorId(AftermathCollectionInitializer.packageId, 60130));
      lorIdList1.Add(new LorId(AftermathCollectionInitializer.packageId, 60131));
      List<LorId> lorIdList2 = new List<LorId>()
      {
        new LorId(AftermathCollectionInitializer.packageId, 60118),
        new LorId(AftermathCollectionInitializer.packageId, 60119),
        new LorId(AftermathCollectionInitializer.packageId, 60123)
      };
      if (lorIdList1.Contains(card.GetID()))
      {
        List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(this.owner.faction);
        aliveList.Remove(this.owner);
        if (!(card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 60130)))
          return RandomUtil.SelectOne<BattleUnitModel>(aliveList);
        List<int> source = new List<int>();
        foreach (BattleUnitModel battleUnitModel in aliveList)
        {
          int num = 0;
          foreach (BattleUnitBuf battleUnitBuf in battleUnitModel.bufListDetail.GetActivatedBufList().FindAll((Predicate<BattleUnitBuf>) (x => x.positiveType == BufPositiveType.Negative)))
            num += battleUnitBuf.stack;
          source.Add(num);
        }
        return aliveList[source.IndexOf(source.Max())];
      }
      return lorIdList2.Contains(card.GetID()) ? RandomUtil.SelectOne<BattleUnitModel>(BattleObjectManager.instance.GetAliveList_opponent(this.owner.faction)) : base.ChangeAttackTarget(card, idx);
    }

    public override bool OnBreakGageZero() => true;

    public override int SpeedDiceNumAdder() => 2;

    public override bool TeamKill() => true;

    public override int GetSpeedDiceAdder(int speedDiceResult) => -99;

    public override void OnAfterRollSpeedDice()
    {
      base.OnAfterRollSpeedDice();
      foreach (Dice dice in this.owner.speedDiceResult)
        dice.value = 1;
    }

    public override void OnRoundEndTheLast()
    {
      base.OnRoundEndTheLast();
      if (!this.owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_NoCombat>())
        this.owner.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_NoCombat());
      if (BattleObjectManager.instance.GetAliveList(this.owner.faction).FindAll((Predicate<BattleUnitModel>) (x => !x.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_NoCombat>())).Count != 0)
        return;
      this.owner.Die();
    }

    public override void OnRoundEnd()
    {
      foreach (BattlePlayingCardDataInUnitModel card in this.owner.cardSlotDetail.cardQueue)
      {
        if (card.target.faction != this.owner.faction)
        {
          this.owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Negative);
          SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(this.owner, EmotionCoinType.Negative, 1);
        }
        else if (card.target.faction == this.owner.faction)
        {
          this.owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Positive);
          SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(this.owner, EmotionCoinType.Positive, 1);
        }
        else
        {
          EmotionCoinType coinType = RandomUtil.SelectOne<EmotionCoinType>(EmotionCoinType.Negative, EmotionCoinType.Positive);
          this.owner.emotionDetail.CreateEmotionCoin(coinType);
          SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(this.owner, coinType, 1);
        }
      }
      this.owner.allyCardDetail.DiscardInHand(this.owner.allyCardDetail.GetHand().Count);
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
      base.OnUseCard(curCard);
      if (curCard.card.GetSpec().Ranged != CardRange.Instance)
        return;
      BattleUnitModel target = curCard.target;
      if (target == null)
        return;
      if (target.faction != this.owner.faction)
      {
        this.owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Negative);
        SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(this.owner, EmotionCoinType.Negative, 1);
      }
      else if (target.faction == this.owner.faction)
      {
        this.owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Positive);
        SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(this.owner, EmotionCoinType.Positive, 1);
      }
      else
      {
        EmotionCoinType coinType = RandomUtil.SelectOne<EmotionCoinType>(EmotionCoinType.Negative, EmotionCoinType.Positive);
        this.owner.emotionDetail.CreateEmotionCoin(coinType);
        SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(this.owner, coinType, 1);
      }
    }

    public override void OnRoundStart()
    {
      this.owner.allyCardDetail.DrawCards(4 + this.owner.emotionDetail.EmotionLevel);
      this.owner.cardSlotDetail.RecoverPlayPoint(3);
      if (this.owner.faction != Faction.Enemy)
        return;
      foreach (BattleDiceCardModel card in this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.GetSpec().Ranged == CardRange.Instance)))
      {
        BattleUnitModel target = (BattleUnitModel) null;
        if (card.GetID().id == 60105 || card.GetID().id == 60121)
        {
          List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(this.owner.faction);
          aliveList.Sort((Comparison<BattleUnitModel>) ((x, y) => x.PlayPoint - y.PlayPoint));
          if (aliveList.Any<BattleUnitModel>())
            target = aliveList[0];
        }
        else if (card.GetID().id == 60117)
        {
          List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(this.owner.faction);
          aliveList.Sort((Comparison<BattleUnitModel>) ((x, y) => x.allyCardDetail.GetHand().Count - y.allyCardDetail.GetHand().Count));
          if (aliveList.Any<BattleUnitModel>())
            target = aliveList[0];
        }
        if (target != null)
          this.owner.cardSlotDetail.AddCard(card, target, 0);
      }
    }

    public override void OnWaveStart()
    {
      base.OnWaveStart();
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_NoCombat());
    }

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      foreach (BattlePlayingCardDataInUnitModel cardDataInUnitModel in this.owner.cardSlotDetail.cardAry)
      {
        if (cardDataInUnitModel != null && cardDataInUnitModel.GetOriginalDiceBehaviorList().Count > 0)
          cardDataInUnitModel.DestroyPlayingCard();
      }
      List<BattleUnitModel> actionableEnemyList = Singleton<StageController>.Instance.GetActionableEnemyList();
      if (this.owner.faction != Faction.Player)
        return;
      for (int index1 = 0; index1 < actionableEnemyList.Count; ++index1)
      {
        BattleUnitModel actor = actionableEnemyList[index1];
        if (actor.turnState != BattleUnitTurnState.BREAK)
          actor.turnState = BattleUnitTurnState.WAIT_CARD;
        try
        {
          for (int index2 = 0; index2 < actor.speedDiceResult.Count; ++index2)
          {
            if (!actor.speedDiceResult[index2].breaked && index2 < actor.cardSlotDetail.cardAry.Count)
            {
              BattlePlayingCardDataInUnitModel cardDataInUnitModel = actor.cardSlotDetail.cardAry[index2];
              if (cardDataInUnitModel != null && cardDataInUnitModel.card != null)
              {
                if (cardDataInUnitModel.card.GetSpec().Ranged == CardRange.FarArea || cardDataInUnitModel.card.GetSpec().Ranged == CardRange.FarAreaEach)
                {
                  List<BattlePlayingCardDataInUnitModel.SubTarget> subTargets = cardDataInUnitModel.subTargets;
                  if (subTargets.Exists((Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == this.owner)))
                    subTargets.RemoveAll((Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == this.owner));
                  else if (cardDataInUnitModel.target == this.owner)
                  {
                    if (cardDataInUnitModel.subTargets.Count > 0)
                    {
                      BattlePlayingCardDataInUnitModel.SubTarget subTarget = RandomUtil.SelectOne<BattlePlayingCardDataInUnitModel.SubTarget>(cardDataInUnitModel.subTargets);
                      cardDataInUnitModel.target = subTarget.target;
                      cardDataInUnitModel.targetSlotOrder = subTarget.targetSlotOrder;
                      cardDataInUnitModel.earlyTarget = subTarget.target;
                      cardDataInUnitModel.earlyTargetOrder = subTarget.targetSlotOrder;
                    }
                    else
                    {
                      actor.allyCardDetail.ReturnCardToHand(actor.cardSlotDetail.cardAry[index2].card);
                      actor.cardSlotDetail.cardAry[index2] = (BattlePlayingCardDataInUnitModel) null;
                    }
                  }
                }
                else
                {
                  List<BattlePlayingCardDataInUnitModel.SubTarget> subTargets = cardDataInUnitModel.subTargets;
                  if (subTargets.Exists((Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == this.owner)))
                    subTargets.RemoveAll((Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == this.owner));
                  if (cardDataInUnitModel.target == this.owner)
                  {
                    BattleUnitModel targetByCard = BattleObjectManager.instance.GetTargetByCard(actor, cardDataInUnitModel.card, index2, actor.TeamKill());
                    if (targetByCard != null)
                    {
                      int targetSlot = UnityEngine.Random.Range(0, targetByCard.speedDiceResult.Count);
                      int num = actor.ChangeTargetSlot(cardDataInUnitModel.card, targetByCard, index2, targetSlot, actor.TeamKill());
                      cardDataInUnitModel.target = targetByCard;
                      cardDataInUnitModel.targetSlotOrder = num;
                      cardDataInUnitModel.earlyTarget = targetByCard;
                      cardDataInUnitModel.earlyTargetOrder = num;
                    }
                    else
                    {
                      actor.allyCardDetail.ReturnCardToHand(actor.cardSlotDetail.cardAry[index2].card);
                      actor.cardSlotDetail.cardAry[index2] = (BattlePlayingCardDataInUnitModel) null;
                    }
                  }
                  else if (cardDataInUnitModel.earlyTarget == this.owner)
                  {
                    BattleUnitModel targetByCard = BattleObjectManager.instance.GetTargetByCard(actor, cardDataInUnitModel.card, index2, actor.TeamKill());
                    if (targetByCard != null)
                    {
                      int targetSlot = UnityEngine.Random.Range(0, targetByCard.speedDiceResult.Count);
                      int num = actor.ChangeTargetSlot(cardDataInUnitModel.card, targetByCard, index2, targetSlot, actor.TeamKill());
                      cardDataInUnitModel.earlyTarget = targetByCard;
                      cardDataInUnitModel.earlyTargetOrder = num;
                    }
                    else
                    {
                      cardDataInUnitModel.earlyTarget = cardDataInUnitModel.target;
                      cardDataInUnitModel.earlyTargetOrder = cardDataInUnitModel.targetSlotOrder;
                    }
                  }
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          Debug.LogError((object) "AftermathCollection: failed to redirect cards (Benito - Non-combatant)");
        }
      }
      SingletonBehavior<BattleManagerUI>.Instance.ui_TargetArrow.UpdateTargetList();
    }

    public override bool IsImmune(KeywordBuf buf)
    {
      return buf == KeywordBuf.Stun || buf == KeywordBuf.Seal || base.IsImmune(buf);
    }

    public override bool IsTargetable_theLast() => false;

    public override bool IsTargetable(BattleUnitModel attacker) => false;

    public override BattleDiceCardModel OnSelectCardAuto(
      BattleDiceCardModel origin,
      int currentDiceSlotIdx)
    {
      return origin.CreateDiceCardBehaviorList().Count > 0 ? (BattleDiceCardModel) null : base.OnSelectCardAuto(origin, currentDiceSlotIdx);
    }
  }
}
