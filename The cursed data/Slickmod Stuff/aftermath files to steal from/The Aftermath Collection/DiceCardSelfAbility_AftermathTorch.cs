﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathTorch
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathTorch : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] Restore 35 HP and deal 35 stagger damage to self";

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      this.owner.RecoverHP(35);
      this.owner.breakDetail.TakeBreakDamage(35, DamageType.Card_Ability);
      this.card.card.exhaust = true;
    }
  }
}