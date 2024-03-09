// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathLetItOut
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathLetItOut : DiceCardSelfAbilityBase
  {
    public static string Desc = "Only one copy of this page can be in hand at once";

    public override bool BeforeAddToHand(BattleUnitModel unit, BattleDiceCardModel self)
    {
      return !unit.allyCardDetail.GetHand().Contains(self);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Burn_Keyword",
          "LetItOutWin",
          "LetItOutLose"
        };
      }
    }
  }
}
