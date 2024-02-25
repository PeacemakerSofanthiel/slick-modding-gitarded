// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_lanceCardRecycle
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_lanceCardRecycle : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Spend 4 Charge to recycle this die [On Clash Lose] Deal 8 damaget to self and target";

    public override void OnSucceedAttack()
    {
      if (!(this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) is BattleUnitBuf_warpCharge activatedBuf) || activatedBuf.stack < 4)
        return;
      activatedBuf.UseStack(4, true);
      this.ActivateBonusAttackDice();
    }

    public override void OnLoseParrying()
    {
      BattleUnitModel target = this.card.target;
      if (target == null)
        return;
      target.TakeDamage(8, DamageType.Card_Ability, this.owner);
      this.owner.TakeDamage(8, DamageType.Card_Ability, this.owner);
    }
  }
}
