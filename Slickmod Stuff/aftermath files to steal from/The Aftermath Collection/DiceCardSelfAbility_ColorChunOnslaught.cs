// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_ColorChunOnslaught
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_ColorChunOnslaught : DiceCardSelfAbilityBase
  {
    private int burn;
    public static string Desc = "If 3 or more Burn was inflicted with this page, reduce the Cost of all 'Frontal Onslaughts' by 1 [On Use] Draw 1 page";

    public override void OnUseCard()
    {
      this.owner.allyCardDetail.DrawCards(1);
      BattleUnitModel target = this.card.target;
      if (target == null)
        return;
      BattleUnitBuf activatedBuf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
      if (activatedBuf == null)
        return;
      this.burn = activatedBuf.stack;
    }

    public override void OnEndBattle()
    {
      BattleUnitModel target = this.card.target;
      if (target == null)
        return;
      BattleUnitBuf activatedBuf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
      if (activatedBuf == null || activatedBuf.stack < this.burn + 3)
        return;
      foreach (BattleDiceCardModel battleDiceCardModel in this.owner.allyCardDetail.GetAllDeck().FindAll((Predicate<BattleDiceCardModel>) (x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 104))))
        battleDiceCardModel.AddCost(-1);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "ColorChunOnlyPage_Keyword",
          "Burn_Keyword",
          "DrawCard_Keyword"
        };
      }
    }
  }
}
