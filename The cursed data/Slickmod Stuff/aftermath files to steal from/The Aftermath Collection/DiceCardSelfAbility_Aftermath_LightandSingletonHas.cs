// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_LightandSingletonHaste
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  [UnusedAbility]
  public class DiceCardSelfAbility_Aftermath_LightandSingletonHaste : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Restore 1 Light. If Singleton, gain 1 Haste next Scene";

    public override void OnUseCard()
    {
      this.owner.cardSlotDetail.RecoverPlayPoint(1);
      if (!this.owner.allyCardDetail.IsHighlander())
        return;
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, this.owner);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Energy_Keyword",
          "OnlyOne_Keyword",
          "Quickness_Keyword"
        };
      }
    }
  }
}
