// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathHaste2EmotionPlus
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathHaste2EmotionPlus : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Gain 1 Haste next Scene; At Emotion Level 2 or less, gain 1 additional Haste";

    public override void OnUseCard()
    {
      if (this.owner.emotionDetail.EmotionLevel <= 2)
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 2, this.owner);
      else
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, this.owner);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Quickness_Keyword",
          "Burn_Keyword"
        };
      }
    }
  }
}
