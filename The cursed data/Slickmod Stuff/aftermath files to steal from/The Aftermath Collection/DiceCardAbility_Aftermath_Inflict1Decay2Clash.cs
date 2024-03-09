// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_Inflict1Decay2Clash
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_Inflict1Decay2Clash : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash] Inflict 1 Erosion 2 times";

    public override void OnWinParrying()
    {
      base.OnWinParrying();
      BattleUnitModel target = this.behavior.card.target;
      target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
      target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
    }

    public override void OnLoseParrying()
    {
      base.OnLoseParrying();
      BattleUnitModel target = this.behavior.card.target;
      target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
      target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
    }

    public override void OnDrawParrying()
    {
      base.OnDrawParrying();
      BattleUnitModel target = this.behavior.card.target;
      target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
      target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Decay_Keyword" };
    }
  }
}
