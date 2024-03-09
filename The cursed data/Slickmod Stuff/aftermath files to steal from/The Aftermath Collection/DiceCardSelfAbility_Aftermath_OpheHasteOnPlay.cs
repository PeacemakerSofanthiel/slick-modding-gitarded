// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_OpheHasteOnPlay
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_OpheHasteOnPlay : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Play] Gain 1 Haste next Scene for every 2 unique Combat Pages in deck (Rounded up; up to 7)";

    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      base.OnUseInstance(unit, self, targetUnit);
      LorId[] lorIdArray = new LorId[0];
      foreach (BattleDiceCardModel battleDiceCardModel in unit.allyCardDetail.GetAllDeck())
      {
        if (!lorIdArray.Contains<LorId>(battleDiceCardModel.GetID()))
          ((IEnumerable<LorId>) lorIdArray).Append<LorId>(battleDiceCardModel.GetID());
      }
      unit.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, Mathf.Min((((IEnumerable<LorId>) lorIdArray).Count<LorId>() + 1) / 2, 7), unit);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Quickness_Keyword" };
    }
  }
}
