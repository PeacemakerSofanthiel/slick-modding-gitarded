// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_InflictAdditionalErosion
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_InflictAdditionalErosion : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] When inflicting Erosion using Combat Pages this Scene, inflict +1 additional stack";

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_Aftermath_InflictAdditionalErosion.BattlUnBuf_Aftermath_InflictAddDecay());
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Decay_Keyword" };
    }

    public class BattlUnBuf_Aftermath_InflictAddDecay : BattleUnitBuf
    {
      public override int OnGiveKeywordBufByCard(
        BattleUnitBuf cardBuf,
        int stack,
        BattleUnitModel target)
      {
        return cardBuf.bufType == KeywordBuf.Decay ? 1 : 0;
      }

      public override void OnRoundEnd() => this.Destroy();
    }
  }
}
