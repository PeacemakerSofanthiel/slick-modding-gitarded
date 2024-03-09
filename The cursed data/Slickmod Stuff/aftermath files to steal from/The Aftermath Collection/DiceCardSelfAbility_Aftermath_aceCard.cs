// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_aceCard
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_aceCard : DiceCardSelfAbilityBase
  {
    public static string Desc = "Only usable at 10+ Overcharge";

    public override string[] Keywords
    {
      get
      {
        return new string[1]
        {
          "Aftermath_Dem_Overcharge_Keyword"
        };
      }
    }

    public override bool OnChooseCard(BattleUnitModel owner)
    {
      return owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Overcharge)) is BattleUnitBuf_Aftermath_Overcharge aftermathOvercharge && aftermathOvercharge.stack >= 10;
    }
  }
}
