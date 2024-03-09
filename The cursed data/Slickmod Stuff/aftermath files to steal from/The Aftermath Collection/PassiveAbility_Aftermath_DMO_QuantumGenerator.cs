// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_DMO_QuantumGenerator
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_DMO_QuantumGenerator : PassiveAbilityBase
  {
    public static string Desc = "At the start of each Scene, gain 3 Charge. When hit, if the character has Overcharge, inflict 1-3 Paralysis to the attacker.";

    public override void OnRoundStart()
    {
      this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 3, this.owner);
    }

    public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
    {
      if (!this.owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
        return;
      this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase) this);
      int stack = RandomUtil.Range(1, 3);
      atkDice.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Paralysis, stack, this.owner);
    }
  }
}
