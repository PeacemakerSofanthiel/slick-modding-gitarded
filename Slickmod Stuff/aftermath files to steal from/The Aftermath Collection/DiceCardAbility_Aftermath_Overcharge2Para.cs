// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_Overcharge2Para
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_Overcharge2Para : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Win] If user has Overcharge, inflict 2 Paralysis";

    public override void OnWinParrying()
    {
      if (!this.owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
        return;
      this.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 2, this.owner);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Paralysis_Keyword" };
    }
  }
}
