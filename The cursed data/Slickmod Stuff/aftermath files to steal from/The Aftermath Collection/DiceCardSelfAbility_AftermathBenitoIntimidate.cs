// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathBenitoIntimidate
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathBenitoIntimidate : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] Inflict 2 Disarm on target's team this Scene";

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList(this.card.target.faction))
        alive.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Disarm, 2, this.owner);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Benito_Only_Keyword",
          "Disarm_Keyword",
          "bstart_Keyword"
        };
      }
    }
  }
}
