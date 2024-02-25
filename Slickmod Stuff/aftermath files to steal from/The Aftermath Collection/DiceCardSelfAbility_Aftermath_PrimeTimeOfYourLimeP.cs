// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_PrimeTimeOfYourLimePlayer
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_PrimeTimeOfYourLimePlayer : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Play] This Scene, all dice gain +1 Dice Power; if an attack is one-sided, deal 50% less damage and Stagger damage, gain +1 additional Power and reroll the die once";

    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      base.OnUseInstance(unit, self, targetUnit);
      unit.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_PrimeTimeOfYourLimeOneScene());
      SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfile(unit, unit.faction, unit.hp, unit.breakDetail.breakGauge, unit.bufListDetail.GetBufUIDataList());
    }
  }
}
