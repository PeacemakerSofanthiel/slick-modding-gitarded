// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_EvanPrescript3
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_EvanPrescript3 : DiceCardSelfAbilityBase
  {
    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      if (targetUnit != null && targetUnit.faction == unit.faction)
      {
        int v = (int) ((double) targetUnit.MaxHp * 0.10000000149011612);
        int num = (int) ((double) targetUnit.breakDetail.breakGauge * 0.10000000149011612);
        if (v > 8)
          v = 8;
        if (num > 8)
          num = 8;
        targetUnit.RecoverHP(v);
        targetUnit.breakDetail.RecoverBreak(num);
        SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
      }
      if (targetUnit.faction != unit.faction)
      {
        int v = (int) ((double) unit.MaxHp * 0.10000000149011612);
        int num = (int) ((double) unit.breakDetail.breakGauge * 0.10000000149011612);
        if (v > 8)
          v = 8;
        if (num > 8)
          num = 8;
        unit.RecoverHP(v);
        unit.breakDetail.RecoverBreak(num);
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
  }
}
