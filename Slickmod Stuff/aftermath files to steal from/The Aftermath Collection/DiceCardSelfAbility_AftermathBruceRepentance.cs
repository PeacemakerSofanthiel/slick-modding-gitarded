// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathBruceRepentance
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathBruceRepentance : DiceCardSelfAbilityBase
  {
    public static string Desc = "This page's Cost is reduced by 1 for every 3 stacks of Burn on self\n[Combat Start] Give Burn Protection to all allies equal to half of user's Burn Protection";

    public override int GetCostAdder(BattleUnitModel unit, BattleDiceCardModel self)
    {
      BattleUnitBuf activatedBuf = unit.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
      return activatedBuf != null ? -(activatedBuf.stack / 3) : 0;
    }

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      BattleUnitBuf battleUnitBuf1 = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_AftermathBurnProtection));
      if (battleUnitBuf1 == null)
        return;
      foreach (BattleUnitModel owner in BattleObjectManager.instance.GetAliveList(this.owner.faction).SkipWhile<BattleUnitModel>((Func<BattleUnitModel, bool>) (x => x == this.owner)))
      {
        BattleUnitBuf battleUnitBuf2 = owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_AftermathBurnProtection));
        if (battleUnitBuf2 == null)
        {
          BattleUnitBufListDetail bufListDetail = owner.bufListDetail;
          BattleUnitBuf_AftermathBurnProtection buf = new BattleUnitBuf_AftermathBurnProtection();
          buf.stack = battleUnitBuf1.stack / 2;
          bufListDetail.AddBuf((BattleUnitBuf) buf);
        }
        else
        {
          battleUnitBuf2.stack += battleUnitBuf1.stack / 2;
          battleUnitBuf2.Init(owner);
        }
      }
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "BruceOnlyPage_Keyword",
          "Burn_Keyword",
          "Aftermath_BurnProtection"
        };
      }
    }
  }
}
