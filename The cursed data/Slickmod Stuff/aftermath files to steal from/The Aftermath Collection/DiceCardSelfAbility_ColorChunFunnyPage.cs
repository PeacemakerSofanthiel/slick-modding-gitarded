// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_ColorChunFunnyPage
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_ColorChunFunnyPage : DiceCardSelfAbilityBase
  {
    public static string Desc = "This page exhausts when discarded or used\nThis page's Cost is lowered by the number of other 'Flaming Blitz' in hand\n[Combat Start] Discard up to 2 other copies of ‘Flaming Blitz’ and add their dice to this page";

    public override void OnUseCard() => this.card.card.exhaust = true;

    public override void OnDiscard(BattleUnitModel unit, BattleDiceCardModel self)
    {
      self.exhaust = true;
    }

    public override void OnRoundStart_inHand(BattleUnitModel unit, BattleDiceCardModel self)
    {
      base.OnRoundStart_inHand(unit, self);
      self.AddCost(-unit.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 101) && x != self)).Count);
    }

    public override void OnStartBattleAfterCreateBehaviour()
    {
      List<BattleDiceCardModel> all = this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 101)));
      int num = 0;
      if (all.Count <= 0)
        return;
      foreach (BattleDiceCardModel card in all)
      {
        if (card != null)
        {
          foreach (BattleDiceBehavior diceCardBehavior in card.CreateDiceCardBehaviorList())
          {
            if (diceCardBehavior != null && num < 2)
            {
              this.card.AddDice(diceCardBehavior);
              this.owner.allyCardDetail.DiscardACardByAbility(card);
              ++num;
            }
          }
        }
      }
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "ColorChunOnlyPage_Keyword",
          "Burn_Keyword"
        };
      }
    }
  }
}
