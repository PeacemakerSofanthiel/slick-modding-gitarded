// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_EvanPrescript4
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_EvanPrescript4 : DiceCardSelfAbilityBase
  {
    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      if (targetUnit != null && targetUnit.faction == unit.faction)
      {
        targetUnit.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_Aftermath_EvanPrescript4.BattleUnitBuf_EvanPrescriptBuf4());
        SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
      }
      if (targetUnit.faction != unit.faction)
      {
        unit.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_Aftermath_EvanPrescript4.BattleUnitBuf_EvanPrescriptBuf4());
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

    public class BattleUnitBuf_EvanPrescriptBuf4 : BattleUnitBuf
    {
      public override void OnRoundEnd() => this.Destroy();

      public override void BeforeRollDice(BattleDiceBehavior behavior)
      {
        if (!this.IsAttackDice(behavior.Detail) || behavior.abilityList.Find((Predicate<DiceCardAbilityBase>) (x => x is DiceCardSelfAbility_Aftermath_EvanPrescript4.BattleUnitBuf_EvanPrescriptBuf4.DiceCardAbility_Messenger5BufCard)) != null)
          return;
        behavior.AddAbility((DiceCardAbilityBase) new DiceCardSelfAbility_Aftermath_EvanPrescript4.BattleUnitBuf_EvanPrescriptBuf4.DiceCardAbility_Messenger5BufCard());
      }

      public class DiceCardAbility_Messenger5BufCard : DiceCardAbilityBase
      {
        public override void OnRollDice()
        {
          if (this.behavior.DiceVanillaValue != this.behavior.GetDiceMin() && this.behavior.DiceVanillaValue != this.behavior.GetDiceMax())
            return;
          this.behavior.ApplyDiceStatBonus(new DiceStatBonus()
          {
            power = 2,
            min = 1,
            max = -1
          });
        }
      }
    }
  }
}
