// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_MetalGearRisingFromTheAshes
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_MetalGearRisingFromTheAshes : BattleUnitBuf
  {
    public override string keywordId => "MetalGearRisingFromTheAshes";

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      for (int index = 0; index < this.stack; ++index)
      {
        if (this.IsDefenseDice(behavior.Detail))
          behavior.AddAbility((DiceCardAbilityBase) new DiceCardAbility_burn1atk());
        else
          behavior.AddAbility((DiceCardAbilityBase) new BattleUnitBuf_MetalGearRisingFromTheAshes.DiceCardAbility_Burn1OnClashWin());
      }
    }

    public class DiceCardAbility_Burn1OnClashWin : DiceCardAbilityBase
    {
      public override void OnWinParrying()
      {
        this.behavior.card?.target?.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 1, this.owner);
      }
    }
  }
}
