// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_Aftermath_NoCombat
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_Aftermath_NoCombat : BattleUnitBuf
  {
    public override bool IsCardChoosable(BattleDiceCardModel card)
    {
      return card.GetBehaviourList().Count <= 0;
    }

    public override void OnAfterRollSpeedDice()
    {
      base.OnAfterRollSpeedDice();
      List<BattleUnitModel> actionableEnemyList = Singleton<StageController>.Instance.GetActionableEnemyList();
      if (this._owner.faction != Faction.Player)
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
                  if (subTargets.Exists((Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == this._owner)))
                    subTargets.RemoveAll((Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == this._owner));
                  else if (cardDataInUnitModel.target == this._owner)
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
                  if (subTargets.Exists((Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == this._owner)))
                    subTargets.RemoveAll((Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == this._owner));
                  if (cardDataInUnitModel.target == this._owner)
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
                  else if (cardDataInUnitModel.earlyTarget == this._owner)
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
          Debug.LogError((object) "AftermathCollection: failed to redirect cards (Non-combatant)");
        }
      }
      SingletonBehavior<BattleManagerUI>.Instance.ui_TargetArrow.UpdateTargetList();
    }

    public override string keywordId => "AftermathBuf_NoCombat";
  }
}
