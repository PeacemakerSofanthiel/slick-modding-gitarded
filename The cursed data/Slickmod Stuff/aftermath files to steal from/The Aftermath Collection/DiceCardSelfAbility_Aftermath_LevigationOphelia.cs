﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_LevigationOphelia
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_LevigationOphelia : DiceCardSelfAbilityBase
  {
    public static string Desc = "Only usable in the 'Blade Unlocked' state\nIf Speed is lower than 8, destroy the first die on this page";

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "onlypage_Ophe_Keyword",
          "Quickness_Keyword"
        };
      }
    }

    public override bool OnChooseCard(BattleUnitModel owner)
    {
      return owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null;
    }

    public override void OnStartBattleAfterCreateBehaviour()
    {
      if (this.card.speedDiceResultValue >= 8)
        return;
      this.card.DestroyDice(DiceMatch.DiceByIdx(0));
    }
  }
}
