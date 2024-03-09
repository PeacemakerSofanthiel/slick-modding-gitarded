// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_ChemRebound
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_ChemRebound : DiceCardSelfAbilityBase
  {
    public static string Desc = "Exhausts when used or discarded; [On Exhaust] Gain 1 Rebound next Scene";

    public override void OnUseCard()
    {
      this.Activate(this.owner, this.card.card);
      base.OnUseCard();
    }

    public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
    {
      this.Activate(unit, self);
      base.OnDiscard(unit, self);
    }

    public void Activate(BattleUnitModel unit, BattleDiceCardModel self)
    {
      self.exhaust = true;
      BattleUnitBuf battleUnitBuf = unit.bufListDetail.GetReadyBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_ChemRebound));
      if (battleUnitBuf == null)
      {
        BattleUnitBufListDetail bufListDetail = unit.bufListDetail;
        BattleUnitBuf_Aftermath_ChemRebound buf = new BattleUnitBuf_Aftermath_ChemRebound();
        buf.stack = 1;
        bufListDetail.AddReadyBuf((BattleUnitBuf) buf);
      }
      else
        ++battleUnitBuf.stack;
    }

    public override string[] Keywords
    {
      get
      {
        return new string[1]
        {
          "Aftermath_ChemRebound_Keyword"
        };
      }
    }
  }
}
