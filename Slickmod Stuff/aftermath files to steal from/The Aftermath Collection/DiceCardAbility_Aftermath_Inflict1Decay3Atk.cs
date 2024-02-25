// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_Inflict1Decay3Atk
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_Inflict1Decay3Atk : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Inflict 1 Erosion 3 times";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
      target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
      target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Decay_Keyword" };
    }
  }
}
