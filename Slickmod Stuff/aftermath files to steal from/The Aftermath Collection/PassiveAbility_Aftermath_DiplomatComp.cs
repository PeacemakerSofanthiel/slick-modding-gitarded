// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_DiplomatComp
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_DiplomatComp : PassiveAbilityBase
  {
    public override int OnGiveKeywordBufByCard(
      BattleUnitBuf buf,
      int stack,
      BattleUnitModel target)
    {
      return new List<KeywordBuf>()
      {
        KeywordBuf.Burn,
        KeywordBuf.Paralysis,
        KeywordBuf.Vulnerable,
        KeywordBuf.Bleeding
      }.Contains(buf.bufType) ? 1 : 0;
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      base.BeforeRollDice(behavior);
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        dmg = -3,
        breakDmg = -3
      });
    }
  }
}
