// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Evan1
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Evan1 : DiceCardSelfAbilityBase
  {
    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "IndexReleaseCard3_Keyword",
          "DrawCard_Keyword",
          "Energy_Keyword"
        };
      }
    }

    public override void OnUseCard()
    {
      this.owner.allyCardDetail.DrawCards(1);
      this.owner.allyCardDetail.AddNewCard(605010);
      this.card.card.exhaust = true;
    }
  }
}
