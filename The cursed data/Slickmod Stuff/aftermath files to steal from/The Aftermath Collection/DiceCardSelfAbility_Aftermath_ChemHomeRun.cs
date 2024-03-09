// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_ChemHomeRun
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_ChemHomeRun : DiceCardSelfAbilityBase
  {
    public static string Desc = "Only one copy of this page can be held at a time\nExhausts when used or discarded; [On Exhaust] Gain 2 Strength; become Staggered at the end of the Scene";

    public override bool BeforeAddToHand(BattleUnitModel unit, BattleDiceCardModel self)
    {
      return unit.allyCardDetail.GetHand().Find((Predicate<BattleDiceCardModel>) (x => x.GetID() == self.GetID())) == null;
    }

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
      unit.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 2, unit);
      unit.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_Aftermath_ChemHomeRun.BattleUnitBuf_ChemHomeRunSTAGGERYOURSELF());
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Strength_Keyword" };
    }

    public class BattleUnitBuf_ChemHomeRunSTAGGERYOURSELF : BattleUnitBuf
    {
      public override void OnRoundEnd()
      {
        this._owner.breakDetail.TakeBreakDamage(this._owner.breakDetail.breakGauge, DamageType.Card_Ability);
        this.Destroy();
      }
    }
  }
}
