// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_NorincoShank
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_NorincoShank : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Win] Deal 2 damage to target [On Clash Lose] Deal 2 stagger damage to self; exhaust this page";

    public override void OnWinParrying()
    {
      this.behavior.card.target?.TakeDamage(2, DamageType.Card_Ability, this.owner);
    }

    public override void OnLoseParrying()
    {
      this.owner.breakDetail.TakeBreakDamage(2, DamageType.Card_Ability, this.owner);
      this.behavior.card.card.exhaust = true;
    }
  }
}
