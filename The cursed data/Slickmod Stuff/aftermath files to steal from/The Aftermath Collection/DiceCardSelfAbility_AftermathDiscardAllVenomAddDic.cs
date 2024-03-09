// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathDiscardAllVenomAddDice
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathDiscardAllVenomAddDice : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Change this page's range to Ranged, remove all dice from this page, discard up to 4 Venoms and add their dice to this page";

    public override void OnUseCard()
    {
      base.OnUseCard();
      this.card.card.XmlData.Spec.Ranged = CardRange.Far;
      List<BattleDiceCardModel> all = this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.CheckForKeyword("Venom_Keyword")));
      this.card.DestroyDice(DiceMatch.AllDice);
      if (all.Count <= 0)
        return;
      for (int index = all.Count < 4 ? all.Count : 4; index > 0; --index)
      {
        BattleDiceCardModel card = all[index - 1];
        BattleDiceBehavior diceCardBehavior = card.CreateDiceCardBehaviorList()[0];
        this.owner.allyCardDetail.DiscardACardByAbility(card);
        diceCardBehavior.behaviourInCard.MotionDetail = MotionDetail.F;
        this.card.AddDice(diceCardBehavior);
      }
    }

    public override void OnEnterCardPhase(BattleUnitModel unit, BattleDiceCardModel self)
    {
      base.OnEnterCardPhase(unit, self);
      self.XmlData.Spec.Ranged = CardRange.Near;
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Family_Only_Keyword",
          "Venom_Use_Keyword"
        };
      }
    }
  }
}
