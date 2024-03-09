// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathEqualizerAmulet
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathEqualizerAmulet : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] Target is not influenced by Power gain/loss for this Scene";

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      if (this.card.target == null)
        return;
      this.card.target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.NullifyPower, 1, this.owner);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Benito_Only_Keyword",
          "NullifyPower",
          "bstart_Keyword"
        };
      }
    }
  }
}
