// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_ColorChunOnlyPageKeyword
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_ColorChunOnlyPageKeyword : DiceCardSelfAbilityBase
  {
    public override int GetCostAdder(BattleUnitModel unit, BattleDiceCardModel self)
    {
      return unit.emotionDetail.EmotionLevel >= 3 ? -1 : 0;
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "ColorChunOnlyPage_Keyword",
          "Burn_Keyword"
        };
      }
    }
  }
}
