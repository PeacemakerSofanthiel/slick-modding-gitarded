// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathRiseFromTheAshesYijunPassive
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathRiseFromTheAshesYijunPassive : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Restore 4 Light and draw 4 pages; fully recover Stagger Resist. For the remainder of the Act, all offensive dice gain '[On Hit] Inflict 1 Burn', and all defensive dice gain ‘[On Clash Win] Inflict 1 Burn’";

    public override void OnUseCard()
    {
      this.owner.cardSlotDetail.RecoverPlayPoint(4);
      this.owner.allyCardDetail.DrawCards(4);
      this.owner.breakDetail.ResetBreakDefault();
      BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_MetalGearRisingFromTheAshes));
      if (battleUnitBuf == null)
        this.owner.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_MetalGearRisingFromTheAshes());
      else
        ++battleUnitBuf.stack;
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Energy_Keyword",
          "DrawCard_Keyword",
          "MetalGearRisingFromTheAshesKeyword"
        };
      }
    }
  }
}
