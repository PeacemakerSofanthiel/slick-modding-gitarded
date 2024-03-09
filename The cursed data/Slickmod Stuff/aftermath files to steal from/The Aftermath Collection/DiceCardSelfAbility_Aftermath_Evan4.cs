// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Evan4
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Evan4 : DiceCardSelfAbilityBase
  {
    public override string[] Keywords
    {
      get => new string[1]{ "OnlyOne_Keyword" };
    }

    public override void OnUseCard()
    {
      this.owner.allyCardDetail.DrawCards(1);
      if (!this.owner.allyCardDetail.IsHighlander())
        return;
      List<BattleDiceCardModel> all = this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.GetCost() >= 1));
      if (all.Count <= 0)
        return;
      RandomUtil.SelectOne<BattleDiceCardModel>(all).AddBuf((BattleDiceCardBuf) new DiceCardSelfAbility_Aftermath_Evan4.CostDecreasePermaBuf());
    }

    public class CostDecreasePermaBuf : BattleDiceCardBuf
    {
      public override DiceCardBufType bufType => DiceCardBufType.CostDecrease;

      public CostDecreasePermaBuf() => this._stack = 1;

      public void Add() => ++this._stack;

      public override void OnUseCard(
        BattleUnitModel owner,
        BattlePlayingCardDataInUnitModel playingCard)
      {
        this.Destroy();
      }

      public override int GetCost(int oldCost) => oldCost - this.Stack;
    }
  }
}
