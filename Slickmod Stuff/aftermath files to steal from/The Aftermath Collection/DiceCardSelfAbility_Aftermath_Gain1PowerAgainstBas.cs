// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Gain1PowerAgainstBased
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Gain1PowerAgainstBased : DiceCardSelfAbilityBase
  {
    public static string Desc = "If target has Based, dice on this page gain +1 Power";

    public override void OnUseCard()
    {
      base.OnUseCard();
      this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus()
      {
        power = 1
      });
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Basic" };
    }
  }
}
