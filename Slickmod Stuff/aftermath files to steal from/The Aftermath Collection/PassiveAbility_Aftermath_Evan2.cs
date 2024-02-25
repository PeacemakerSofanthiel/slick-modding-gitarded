// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_Evan2
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_Evan2 : PassiveAbilityBase
  {
    public override void OnWaveStart()
    {
      this.owner.personalEgoDetail.AddCard(new LorId(AftermathCollectionInitializer.packageId, 60319));
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      base.BeforeRollDice(behavior);
      int stacks = 0;
      BattleUnitModel unitModel = this.owner;
      Predicate<BattlePlayingCardDataInUnitModel.SubTarget> match = (Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == unitModel);
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList())
      {
        foreach (BattlePlayingCardDataInUnitModel cardDataInUnitModel in alive.cardSlotDetail.cardAry)
        {
          bool flag = false;
          if (cardDataInUnitModel != null && cardDataInUnitModel.card != null)
          {
            if (cardDataInUnitModel.subTargets.Count > 0 && cardDataInUnitModel.subTargets.Exists(match))
              flag = true;
            if (cardDataInUnitModel.target == unitModel | flag)
              ++stacks;
          }
        }
      }
      if (stacks > 5)
        stacks = 5;
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new PassiveAbility_Aftermath_Evan2.BattleUnitBuf_EvanProtectionJuggernaut(stacks));
    }

    public override void OnStartBattle()
    {
      int num1 = 0;
      BattleUnitModel unitModel = this.owner;
      Predicate<BattlePlayingCardDataInUnitModel.SubTarget> match = (Predicate<BattlePlayingCardDataInUnitModel.SubTarget>) (x => x.target == unitModel);
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList())
      {
        foreach (BattlePlayingCardDataInUnitModel cardDataInUnitModel in alive.cardSlotDetail.cardAry)
        {
          bool flag = false;
          if (cardDataInUnitModel != null && cardDataInUnitModel.card != null)
          {
            if (cardDataInUnitModel.subTargets.Count > 0 && cardDataInUnitModel.subTargets.Exists(match))
              flag = true;
            if (cardDataInUnitModel.target == unitModel | flag)
              ++num1;
          }
        }
      }
      if (num1 < 5)
        return;
      DiceCardXmlInfo cardItem = ItemXmlDataList.instance.GetCardItem(new LorId(AftermathCollectionInitializer.packageId, 60318));
      DiceBehaviour diceBehaviour1 = new DiceBehaviour();
      List<BattleDiceBehavior> behaviourList = new List<BattleDiceBehavior>();
      int num2 = 0;
      foreach (DiceBehaviour diceBehaviour2 in cardItem.DiceBehaviourList)
      {
        BattleDiceBehavior battleDiceBehavior = new BattleDiceBehavior();
        battleDiceBehavior.behaviourInCard = diceBehaviour2.Copy();
        battleDiceBehavior.SetIndex(num2++);
        behaviourList.Add(battleDiceBehavior);
      }
      this.owner.cardSlotDetail.keepCard.AddBehaviours(cardItem, behaviourList);
    }

    public override void OnRoundStart()
    {
      List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(this.owner.faction);
      aliveList.Remove(this.owner);
      BattleUnitModel battleUnitModel1 = (BattleUnitModel) null;
      if ((aliveList.Count <= 0 ? 0 : (this.owner.emotionDetail.EmotionLevel >= 3 ? 1 : 0)) == 0)
        return;
      foreach (BattleUnitModel battleUnitModel2 in aliveList)
      {
        if (battleUnitModel1 == null)
          battleUnitModel1 = battleUnitModel2;
        else if ((double) battleUnitModel2.hp == (double) battleUnitModel1.hp)
        {
          if (RandomUtil.SelectOne<int>(new List<int>()
          {
            0,
            1
          }) == 1)
            battleUnitModel1 = battleUnitModel2;
        }
        else if ((double) battleUnitModel2.hp < (double) battleUnitModel1.hp)
          battleUnitModel1 = battleUnitModel2;
      }
      battleUnitModel1?.bufListDetail.AddBuf((BattleUnitBuf) new PassiveAbility_Aftermath_Evan2.BattleUnitBuf_EvanBodyGuard());
    }

    public class BattleUnitBuf_EvanBodyGuard : BattleUnitBuf
    {
      public override bool Hide => true;

      public override bool IsTargetable(BattleUnitModel attacker)
      {
        foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList(this._owner.faction))
        {
          if ((alive == this._owner ? 0 : (alive.IsTargetable(attacker) ? 1 : 0)) != 0)
            return false;
        }
        return base.IsTargetable(attacker);
      }

      public override void OnRoundEnd()
      {
        base.OnRoundEnd();
        this.Destroy();
      }
    }

    public class BattleUnitBuf_EvanProtectionJuggernaut : BattleUnitBuf
    {
      public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
      {
        return (float) (1.0 - (double) this.stack / 100.0 * 5.0);
      }

      public BattleUnitBuf_EvanProtectionJuggernaut(int stacks) => this.stack = stacks;

      public override void OnRoundEnd() => this.Destroy();
    }
  }
}
