// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_YouveDoneFuckedUp
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_YouveDoneFuckedUp : PassiveAbilityBase
  {
    private bool neverAgain;

    public override bool BeforeTakeBreakDamage(BattleUnitModel attacker, int dmg)
    {
      if ((double) this.owner.MaxHp * 0.5 <= (double) this.owner.hp - (double) dmg || this.neverAgain)
        return base.BeforeTakeDamage(attacker, dmg);
      this.owner.SetHp((int) Math.Truncate((double) this.owner.MaxHp * 0.5));
      return true;
    }

    public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
    {
      if ((double) this.owner.MaxHp * 0.5 <= (double) this.owner.hp - (double) dmg || this.neverAgain)
        return base.BeforeTakeDamage(attacker, dmg);
      this.owner.SetHp((int) Math.Truncate((double) this.owner.MaxHp * 0.5));
      return true;
    }

    public override void OnRoundEnd()
    {
      base.OnRoundEnd();
      if ((double) this.owner.MaxHp * 0.5 < (double) this.owner.hp || this.owner.IsBreakLifeZero())
        return;
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_CitrusAuraEgo());
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_PrimeTimeOfYourLime());
      this.owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 301));
      this.owner.breakDetail.ResetBreakDefault();
      ((PassiveAbility_Aftermath_ShimmeringDeezNutsInYoMouth) this.owner.passiveDetail.PassiveList.Find((Predicate<PassiveAbilityBase>) (x => x is PassiveAbility_Aftermath_ShimmeringDeezNutsInYoMouth))).deck.Add(new LorId(AftermathCollectionInitializer.packageId, 301));
      this.neverAgain = true;
    }
  }
}
