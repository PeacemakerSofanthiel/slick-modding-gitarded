// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_Aftermath_PrimeTimeOfYourLime
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_Aftermath_PrimeTimeOfYourLime : BattleUnitBuf
  {
    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      base.BeforeRollDice(behavior);
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        power = 1
      });
      BattleUnitBuf_Aftermath_PrimeTimeOfYourLime.DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage sidedAndLessDamage = (BattleUnitBuf_Aftermath_PrimeTimeOfYourLime.DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage) behavior.abilityList.Find((Predicate<DiceCardAbilityBase>) (x => x is BattleUnitBuf_Aftermath_PrimeTimeOfYourLime.DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage));
      if (behavior.Type != BehaviourType.Atk || behavior.IsParrying() || sidedAndLessDamage != null)
        return;
      behavior.AddAbility((DiceCardAbilityBase) new BattleUnitBuf_Aftermath_PrimeTimeOfYourLime.DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage());
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        dmgRate = -50,
        power = 1
      });
    }

    public override string keywordIconId => "Aftermath_CitrusAuraEgo";

    public override string keywordId => "Aftermath_PrimeTimeOfYourLime";

    public class DiceCardAbility_Aftermath_RerollOnceOneSidedAndLessDamage : DiceCardAbilityBase
    {
      public bool hasRerolled;

      public override void OnRollDice()
      {
        base.OnRollDice();
        if (this.hasRerolled)
          return;
        this.ActivateBonusAttackDice();
        this.hasRerolled = true;
      }
    }
  }
}
