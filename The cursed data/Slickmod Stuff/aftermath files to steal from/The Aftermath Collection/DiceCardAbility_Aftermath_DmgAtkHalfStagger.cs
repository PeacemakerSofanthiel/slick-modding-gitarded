// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_DmgAtkHalfStagger
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_DmgAtkHalfStagger : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Deal damage equal to half of user’s current Stagger Resist";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      target?.TakeDamage(this.owner.breakDetail.breakGauge / 2, DamageType.Card_Ability, this.owner);
    }
  }
}
