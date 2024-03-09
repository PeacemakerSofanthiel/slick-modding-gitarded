// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathClashLoseBurn2Both
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathClashLoseBurn2Both : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Lose] Inflict 2 Burn to each other";

    public override void OnLoseParrying()
    {
      BattleUnitModel target = this.behavior.card.target;
      if (target == null)
        return;
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 2, this.owner);
      target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 2, this.owner);
    }
  }
}
