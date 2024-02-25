// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_warpCharge4Draw1
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_warpCharge4Draw1 : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Gain 4 Charge and draw 1 page";

    public override string[] Keywords
    {
      get => new string[1]{ "WarpCharge" };
    }

    public override void OnUseCard()
    {
      this.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 4);
      this.owner.allyCardDetail.DrawCards(1);
    }
  }
}
