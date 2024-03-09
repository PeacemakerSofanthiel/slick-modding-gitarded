// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathMutuallyAssuredIgnitionHunan
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathMutuallyAssuredIgnitionHunan : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] All of this character's dice gain '[On Clash Lose] Inflict 1 Burn to each other' this Scene";

    public override void OnStartBattle()
    {
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_OnClashLoseInflictBurn());
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "HunanBruceOnlyPage_Keyword",
          "Burn_Keyword"
        };
      }
    }
  }
}
