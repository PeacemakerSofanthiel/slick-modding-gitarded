// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathVenomEX
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathVenomEX : DiceCardSelfAbilityBase
  {
    public static string Desc = "Exhausts when used or discarded, only one can be held at once; [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Feeble, 1 Disarm and 1 Bind next Scene (Max. 2)";

    public override void OnUseCard() => this.OnActivate(this.owner, this.card.card);

    public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
    {
      this.OnActivate(unit, self);
    }

    private void OnActivate(BattleUnitModel unit, BattleDiceCardModel self)
    {
      self.exhaust = true;
      if (unit.bufListDetail.HasBuf<DiceCardSelfAbility_AftermathVenomEX.BattleUnitBuf_VenomEX>())
        return;
      unit.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_AftermathVenomEX.BattleUnitBuf_VenomEX());
    }

    public override bool BeforeAddToHand(BattleUnitModel unit, BattleDiceCardModel self)
    {
      return unit.allyCardDetail.GetHand().Find((Predicate<BattleDiceCardModel>) (x => x.GetID() == self.GetID())) == null;
    }

    public override string[] Keywords
    {
      get
      {
        return new string[4]
        {
          "Weak_Keyword",
          "Disarm_Keyword",
          "Binding_Keyword",
          "Venom_Keyword"
        };
      }
    }

    public class BattleUnitBuf_VenomEX : BattleUnitBuf
    {
      private int stacks;

      public override void OnSuccessAttack(BattleDiceBehavior behavior)
      {
        BattleUnitModel target = behavior.card.target;
        if (target == null || behavior.card.card.GetSpec().Ranged != CardRange.Near || this.stacks >= 2)
          return;
        target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Weak, 1, this._owner);
        target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Disarm, 1, this._owner);
        target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, this._owner);
        ++this.stacks;
      }

      public override void OnRoundEnd() => this.Destroy();
    }
  }
}
