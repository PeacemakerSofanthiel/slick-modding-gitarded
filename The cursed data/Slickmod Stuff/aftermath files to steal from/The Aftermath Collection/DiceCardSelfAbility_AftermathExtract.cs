// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathExtract
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathExtract : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Restore 1 Light; add 1 random Venom to hand";

    public override void OnUseCard()
    {
      base.OnUseCard();
      this.owner.cardSlotDetail.RecoverPlayPointByCard(1);
      VenomCardModel.AddVenomToHand(this.owner);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Family_Only_Keyword",
          "Venom_Keyword",
          "Energy_Keyword"
        };
      }
    }
  }
}
