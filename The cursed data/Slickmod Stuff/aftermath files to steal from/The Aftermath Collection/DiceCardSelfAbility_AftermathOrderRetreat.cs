// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathOrderRetreat
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathOrderRetreat : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] Give target an Evade die (4~8)";

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      if (this.card.target == null)
        return;
      BattleDiceCardModel playingCard = BattleDiceCardModel.CreatePlayingCard(ItemXmlDataList.instance.GetCardItem(new LorId(AftermathCollectionInitializer.packageId, 60128)));
      BattleDiceBehavior diceCardBehavior = playingCard.CreateDiceCardBehaviorList()[0];
      this.card.target.cardSlotDetail.keepCard.AddBehaviour(playingCard, diceCardBehavior);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Benito_Only_Keyword",
          "bstart_Keyword"
        };
      }
    }
  }
}
