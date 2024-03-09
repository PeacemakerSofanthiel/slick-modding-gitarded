// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_RigobertoSublimeStrike
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_RigobertoSublimeStrike : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Gain 1 Endurance next Scene; If target has Erosion, gain 1 Strength next Scene";

    public override void OnUseCard()
    {
      base.OnUseCard();
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, this.owner);
      if (this.card.target == null || this.card.target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) == null)
        return;
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, this.owner);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Endurance_Keyword",
          "Strength_Keyword",
          "Aftermath_Decay_Keyword"
        };
      }
    }
  }
}
