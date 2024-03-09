// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_EvanPrescript1
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_EvanPrescript1 : DiceCardSelfAbilityBase
  {
    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      if (targetUnit != null && targetUnit.faction == unit.faction)
      {
        List<BattleDiceCardModel> all = targetUnit.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.GetCost() >= 1));
        if (all.Count > 0)
          RandomUtil.SelectOne<BattleDiceCardModel>(all).AddBuf((BattleDiceCardBuf) new DiceCardSelfAbility_Aftermath_EvanPrescript1.CostDecreasePermaBuf());
        SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
      }
      if (targetUnit.faction != unit.faction)
      {
        List<BattleDiceCardModel> all = unit.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.GetCost() >= 1));
        if (all.Count > 0)
          RandomUtil.SelectOne<BattleDiceCardModel>(all).AddBuf((BattleDiceCardBuf) new DiceCardSelfAbility_Aftermath_EvanPrescript1.CostDecreasePermaBuf());
        SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
      }
      SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
      foreach (int targetId in PassiveAbility_Aftermath_Evan5.targetIds)
        unit.personalEgoDetail.RemoveCard(new LorId(AftermathCollectionInitializer.packageId, targetId));
    }

    public override bool IsValidTarget(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      return targetUnit.faction == unit.faction ? targetUnit != unit : !BattleObjectManager.instance.GetAliveList(unit.faction).Exists((Predicate<BattleUnitModel>) (x => x != unit));
    }

    public override bool IsTargetableAllUnit() => true;

    public class CostDecreasePermaBuf : BattleDiceCardBuf
    {
      public override DiceCardBufType bufType => DiceCardBufType.CostDecrease;

      public CostDecreasePermaBuf() => this._stack = 1;

      public void Add() => ++this._stack;

      public override void OnUseCard(
        BattleUnitModel owner,
        BattlePlayingCardDataInUnitModel playingCard)
      {
        this.Destroy();
      }

      public override int GetCost(int oldCost) => oldCost - this.Stack;
    }
  }
}
