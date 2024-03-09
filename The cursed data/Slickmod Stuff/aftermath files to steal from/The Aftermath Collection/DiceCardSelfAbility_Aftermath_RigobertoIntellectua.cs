// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_RigobertoIntellectual
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_RigobertoIntellectual : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] If not Singleton, reduce the Cost of 1 page in hand for every 5 Erosion on self (Max. 3) by 1 for every 5 Erosion on self (Max. 2)";

    public override void OnUseCard()
    {
      base.OnUseCard();
      if (this.owner.allyCardDetail.IsHighlander())
        return;
      int val1 = this.owner.bufListDetail.GetKewordBufAllStack(KeywordBuf.Decay) / 5;
      for (int index = 0; index < Math.Min(val1, 3); ++index)
        this.owner.allyCardDetail.GetHand().SelectOneRandom<BattleDiceCardModel>().AddCost(-Math.Min(val1, 2));
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Decay_Keyword" };
    }
  }
}
