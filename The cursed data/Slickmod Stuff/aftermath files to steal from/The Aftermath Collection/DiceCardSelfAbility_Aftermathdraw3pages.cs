// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermathdraw3pages
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermathdraw3pages : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Play] Target draws 3 pages";

    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      base.OnUseInstance(unit, self, targetUnit);
      targetUnit.allyCardDetail.DrawCards(3);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Benito_Only_Keyword",
          "DrawCard_Keyword"
        };
      }
    }
  }
}
