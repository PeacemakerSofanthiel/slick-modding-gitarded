// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Draw3PagesAnd2Erosion
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Draw3PagesAnd2Erosion : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Draw 3 pages and inflict 2 Erosion to self";

    public override void OnUseCard()
    {
      base.OnUseCard();
      this.owner.allyCardDetail.DrawCards(3);
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Decay, 2, this.owner);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Aftermath_Decay_Keyword",
          "DrawCard_Keyword"
        };
      }
    }
  }
}
