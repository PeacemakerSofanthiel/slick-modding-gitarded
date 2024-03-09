// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_MinMaxAgainstErosion
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_MinMaxAgainstErosion : DiceCardAbilityBase
  {
    public static string Desc = "If target has Erosion, boost this die's min and max values by +1";

    public override void BeforeRollDice()
    {
      base.BeforeRollDice();
      if (this.behavior.card.target == null || this.behavior.card.target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) == null)
        return;
      this.behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        min = 1,
        max = 1
      });
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Decay_Keyword" };
    }
  }
}
