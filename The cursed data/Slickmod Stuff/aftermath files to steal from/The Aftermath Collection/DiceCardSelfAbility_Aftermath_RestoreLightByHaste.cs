// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_RestoreLightByHaste
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_RestoreLightByHaste : DiceCardSelfAbilityBase
  {
    public static string Desc = "[After Use] If Singleton, restore 1 Light for every 2 Haste on self (Max. 2)";

    public override void OnEndBattle()
    {
      base.OnEndBattle();
      if (!this.owner.allyCardDetail.IsHighlander())
        return;
      int kewordBufStack = this.owner.bufListDetail.GetKewordBufStack(KeywordBuf.Quickness);
      for (int index = 0; index < kewordBufStack / 2 && index < 2; ++index)
        this.owner.cardSlotDetail.RecoverPlayPoint(1);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Energy_Keyword",
          "Quickness_Keyword"
        };
      }
    }
  }
}
