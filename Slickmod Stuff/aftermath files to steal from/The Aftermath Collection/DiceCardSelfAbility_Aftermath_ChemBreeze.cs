// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_ChemBreeze
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_ChemBreeze : DiceCardSelfAbilityBase
  {
    public static string Desc = "Exhausts when used or discarded; [On Exhaust] Gain 2 Haste next Scene; inflict 1 Paralysis to self next Scene";

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
      unit.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 2, unit);
      unit.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 1, unit);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Quickness_Keyword",
          "Paralysis_Keyword"
        };
      }
    }
  }
}
