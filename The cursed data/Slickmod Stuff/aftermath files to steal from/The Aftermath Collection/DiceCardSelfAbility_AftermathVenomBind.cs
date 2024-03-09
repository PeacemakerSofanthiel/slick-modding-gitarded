// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathVenomBind
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathVenomBind : DiceCardSelfAbilityBase
  {
    public static string Desc = "Exhausts when used or discarded; [On Exhaust] On hit with Melee Combat Pages this Scene, inflict 1 Bind next Scene";

    public override void OnUseCard() => this.OnActivate(this.owner, this.card.card);

    public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
    {
      this.OnActivate(unit, self);
    }

    private void OnActivate(BattleUnitModel unit, BattleDiceCardModel self)
    {
      self.exhaust = true;
      if (unit.bufListDetail.HasBuf<DiceCardSelfAbility_AftermathVenomBind.BattleUnitBuf_VenomBind>())
        return;
      unit.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_AftermathVenomBind.BattleUnitBuf_VenomBind());
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Binding_Keyword",
          "Venom_Keyword"
        };
      }
    }

    public class BattleUnitBuf_VenomBind : BattleUnitBuf
    {
      public override void OnSuccessAttack(BattleDiceBehavior behavior)
      {
        BattleUnitModel target = behavior.card.target;
        if (target == null || behavior.card.card.GetSpec().Ranged != CardRange.Near)
          return;
        target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 1, this._owner);
      }

      public override void OnRoundEnd() => this.Destroy();
    }
  }
}
