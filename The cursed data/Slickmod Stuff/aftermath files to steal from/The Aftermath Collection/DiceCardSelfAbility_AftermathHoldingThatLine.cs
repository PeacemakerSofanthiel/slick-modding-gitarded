// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathHoldingThatLine
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathHoldingThatLine : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] Gain 1 Protection this Scene [On Use] At the start of the next Scene, draw all copies of 'Holding the Line' from the deck";

    public override void OnStartBattle()
    {
      this.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Protection, 1, this.owner);
    }

    public override void OnUseCard()
    {
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_AftermathHoldingThatLine.BattleUnitBuf_DrawHoldingLineBuf(this.card.card));
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "DrawCard_Keyword",
          "Protection_Keyword"
        };
      }
    }

    public class BattleUnitBuf_DrawHoldingLineBuf : BattleUnitBuf
    {
      public BattleDiceCardModel _except;

      public BattleUnitBuf_DrawHoldingLineBuf(BattleDiceCardModel card) => this._except = card;

      public override void OnRoundEndTheLast()
      {
        this._owner.allyCardDetail.DrawCardsAllSpecific(new LorId(AftermathCollectionInitializer.packageId, 60207), this._except);
        this.Destroy();
      }
    }
  }
}
