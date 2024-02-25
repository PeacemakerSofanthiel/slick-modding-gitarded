// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_EvanPrescript2
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_EvanPrescript2 : DiceCardSelfAbilityBase
  {
    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      if (targetUnit != null && targetUnit.faction == unit.faction)
      {
        targetUnit.allyCardDetail.DiscardInHand(1);
        targetUnit.allyCardDetail.DrawCards(2);
      }
      if (targetUnit.faction != unit.faction)
      {
        unit.allyCardDetail.DiscardInHand(1);
        unit.allyCardDetail.DrawCards(2);
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

    public override string[] Keywords
    {
      get => new string[1]{ "DrawCard_Keyword" };
    }
  }
}
