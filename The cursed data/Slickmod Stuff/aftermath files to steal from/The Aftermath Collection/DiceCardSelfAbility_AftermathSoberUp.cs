// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathSoberUp
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathSoberUp : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Discard up to 5 Venoms, draw 1 page for every 2 Venoms discarded, and an extra one if 5 Venoms were discarded";

    public override void OnUseCard()
    {
      base.OnUseCard();
      List<BattleDiceCardModel> all = this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.CheckForKeyword("Venom_Keyword")));
      int num = all.Count;
      if (num > 5)
        num = 5;
      foreach (BattleDiceCardModel card in all)
      {
        this.owner.allyCardDetail.DiscardACardByAbility(card);
        if (num == 5)
          this.owner.allyCardDetail.DrawCards(1);
        else if (num % 2 == 0)
          this.owner.allyCardDetail.DrawCards(1);
        --num;
      }
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Family_Only_Keyword",
          "Venom_Use_Keyword",
          "DrawCard_Keyword"
        };
      }
    }
  }
}
