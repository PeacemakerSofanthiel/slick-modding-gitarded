﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_23Butcher
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_23Butcher : PassiveAbilityBase
  {
    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
      this.owner.breakDetail.RecoverBreak(1);
      this.owner.RecoverHP(1);
    }

    public override void OnRoundStart() => this.owner.RecoverHP(2);

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      if (behavior.Detail != BehaviourDetail.Penetrate)
        return;
      this.owner.ShowPassiveTypo((PassiveAbilityBase) this);
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        power = 1
      });
    }
  }
}
