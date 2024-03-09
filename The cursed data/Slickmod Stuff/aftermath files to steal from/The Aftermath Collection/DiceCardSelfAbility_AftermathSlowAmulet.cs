// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathSlowAmulet
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathSlowAmulet : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] Inflict 5 Bind to target next Scene and 3 Bind the Scene after";

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      if (this.card.target == null)
        return;
      this.card.target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 5, this.owner);
      this.card.target.bufListDetail.AddKeywordBufNextNextByCard(KeywordBuf.Binding, 3, this.owner);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Benito_Only_Keyword",
          "Binding_Keyword",
          "bstart_Keyword"
        };
      }
    }
  }
}
