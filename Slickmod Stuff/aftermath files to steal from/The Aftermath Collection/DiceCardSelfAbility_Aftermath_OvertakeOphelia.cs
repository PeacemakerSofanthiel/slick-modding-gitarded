// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_OvertakeOphelia
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_OvertakeOphelia : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Gain 1 Haste next Scene; If Speed is 8 or higher, restore 1 Light and draw 1 page";

    public override void OnUseCard()
    {
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, this.owner);
      if (this.card.speedDiceResultValue < 8)
        return;
      this.owner.cardSlotDetail.RecoverPlayPoint(1);
      this.owner.allyCardDetail.DrawCards(1);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Energy_Keyword",
          "DrawCard_Keyword",
          "Quickness_Keyword"
        };
      }
    }
  }
}
