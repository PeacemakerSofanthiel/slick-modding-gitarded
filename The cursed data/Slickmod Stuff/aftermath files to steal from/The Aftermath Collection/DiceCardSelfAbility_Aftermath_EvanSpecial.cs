// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_EvanSpecial
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_EvanSpecial : DiceCardSelfAbilityBase
  {
    public override void OnStartBattle()
    {
      BattlePlayingCardDataInUnitModel cardDataInUnitModel = this.card.target.cardSlotDetail.cardAry[this.card.targetSlotOrder];
      if (cardDataInUnitModel == null || cardDataInUnitModel.card.GetSpec().Ranged == CardRange.FarArea || cardDataInUnitModel.card.GetSpec().Ranged == CardRange.FarAreaEach)
        return;
      cardDataInUnitModel.target = this.owner;
      cardDataInUnitModel.targetSlotOrder = this.card.slotOrder;
    }

    public override bool OnChooseCard(BattleUnitModel owner)
    {
      return owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null;
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Aftermath_onlypage_evan_Keyword",
          "Aftermath_EvanEnmity_Keyword"
        };
      }
    }
  }
}
