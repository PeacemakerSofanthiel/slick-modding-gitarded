﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_StoredChems1pwBoost3NextDie
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_StoredChems1pwBoost3NextDie : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Win] Gain 1 Stored Chems; boost next die's max value by +3";

    public override void OnWinParrying()
    {
      base.OnWinParrying();
      BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_StoredChems));
      if (battleUnitBuf == null)
      {
        BattleUnitBufListDetail bufListDetail = this.owner.bufListDetail;
        BattleUnitBuf_Aftermath_StoredChems buf = new BattleUnitBuf_Aftermath_StoredChems();
        buf.stack = 1;
        bufListDetail.AddBuf((BattleUnitBuf) buf);
      }
      else
        ++battleUnitBuf.stack;
      this.behavior.card.ApplyDiceStatBonus(DiceMatch.NextDice, new DiceStatBonus()
      {
        max = 3
      });
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_StoredChems" };
    }
  }
}