// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathStackFunnyPage
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathStackFunnyPage : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] If 'Justice: Duty' is in hand, exhaust all of its copies, then add the dice of the discarded pages to this";

    public override void OnStartBattleAfterCreateBehaviour()
    {
      List<BattleDiceCardModel> all = this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 50105)));
      if (all.Count <= 0)
        return;
      DiceCardXmlInfo cardItem = ItemXmlDataList.instance.GetCardItem(new LorId(AftermathCollectionInitializer.packageId, 50105));
      BattleDiceBehavior diceBehavior = new BattleDiceBehavior()
      {
        behaviourInCard = cardItem.DiceBehaviourList[0].Copy()
      };
      foreach (BattleDiceCardModel card in all)
      {
        this.owner.cardSlotDetail.keepCard.AddDice(diceBehavior);
        this.owner.allyCardDetail.ExhaustCardInHand(card);
      }
    }

    public override string[] Keywords
    {
      get => new string[1]{ "bstart_Keyword" };
    }
  }
}
