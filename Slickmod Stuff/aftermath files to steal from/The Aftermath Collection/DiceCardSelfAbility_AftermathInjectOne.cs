// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathInjectOne
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathInjectOne : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Discard a random Venom from hand, take 1 damage and gain 1 Strength/Endurance/Haste next Scene (depending on type of Venom discarded)";

    public override void OnUseCard()
    {
      List<BattleDiceCardModel> all = this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.CheckForKeyword("Venom_Keyword")));
      if (all.Count <= 0)
        return;
      this.owner.TakeDamage(1, DamageType.Card_Ability);
      BattleDiceCardModel card = RandomUtil.SelectOne<BattleDiceCardModel>(all);
      this.owner.allyCardDetail.DiscardACardByAbility(card);
      if (card.CheckForKeyword("Binding_Keyword") && card.CheckForKeyword("Weak_Keyword") && card.CheckForKeyword("Disarm_Keyword"))
      {
        this.owner.allyCardDetail.DiscardACardByAbility(card);
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, this.owner);
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, this.owner);
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, this.owner);
      }
      else if (card.CheckForKeyword("Binding_Keyword"))
      {
        this.owner.allyCardDetail.DiscardACardByAbility(card);
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, this.owner);
      }
      else if (card.CheckForKeyword("Paralysis_Keyword"))
      {
        this.owner.allyCardDetail.DiscardACardByAbility(card);
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 1, this.owner);
      }
      else
      {
        if (!card.CheckForKeyword("Disarm_Keyword"))
          return;
        this.owner.allyCardDetail.DiscardACardByAbility(card);
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, this.owner);
      }
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
