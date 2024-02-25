// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_opheliaCard
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  [UnusedAbility]
  public class DiceCardSelfAbility_Aftermath_opheliaCard : DiceCardSelfAbilityBase
  {
    public static string Desc = "Only usable in the 'Blade Unlocked' state";

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "onlypage_Ophe_Keyword",
          "Quickness_Keyword"
        };
      }
    }

    public override bool OnChooseCard(BattleUnitModel owner)
    {
      return owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null;
    }
  }
}
