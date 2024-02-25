// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_OnlyUsableAt5Erosion
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_OnlyUsableAt5Erosion : DiceCardSelfAbilityBase
  {
    public static string Desc = "Only usable at 5+ Erosion\n[On Use] Purge 5 Erosion from self";

    public override bool OnChooseCard(BattleUnitModel owner)
    {
      BattleUnitBuf activatedBuf = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
      return activatedBuf != null && activatedBuf.stack >= 5;
    }

    public override void OnUseCard()
    {
      base.OnUseCard();
      this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay).stack -= 5;
      BattleUnitBuf activatedBuf = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
      if (activatedBuf == null || activatedBuf.stack >= 1)
        return;
      activatedBuf.Destroy();
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Decay_Keyword" };
    }
  }
}
