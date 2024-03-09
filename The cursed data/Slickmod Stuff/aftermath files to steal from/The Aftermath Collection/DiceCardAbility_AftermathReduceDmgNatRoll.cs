// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathReduceDmgNatRoll
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathReduceDmgNatRoll : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Lose] Reduce incoming damage by the natural roll";

    public override void BeforeRollDice()
    {
      base.BeforeRollDice();
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardAbility_AftermathReduceDmgNatRoll.BattleUnitBuf_DiceDamageReduction(this.behavior));
    }

    public class BattleUnitBuf_DiceDamageReduction : BattleUnitBuf
    {
      private readonly BattleDiceBehavior myBehavior;

      public BattleUnitBuf_DiceDamageReduction(BattleDiceBehavior behavior)
      {
        this.myBehavior = behavior;
      }

      public override int GetDamageReduction(BattleDiceBehavior behavior)
      {
        BattleDiceBehavior behavior1 = this.myBehavior;
        return behavior1 == null ? base.GetDamageReduction(behavior) : behavior1.DiceResultValue;
      }

      public override void AfterDiceAction(BattleDiceBehavior behavior) => this.Destroy();
    }
  }
}
