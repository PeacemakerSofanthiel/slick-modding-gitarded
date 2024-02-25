// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_RapidNeutralizationProtocol
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_RapidNeutralizationProtocol : PassiveAbilityBase
  {
    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
      base.OnSucceedAttack(behavior);
      BattleUnitBuf activatedBuf = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
      if (activatedBuf == null)
        return;
      this.owner.RecoverHP(activatedBuf.stack / 2);
      this.owner.breakDetail.RecoverBreak(activatedBuf.stack / 2);
    }

    public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
    {
      base.OnTakeDamageByAttack(atkDice, dmg);
      BattleUnitBuf activatedBuf = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
      if (activatedBuf == null || atkDice.owner == null)
        return;
      atkDice.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Decay, activatedBuf.stack / 5, this.owner);
    }
  }
}
