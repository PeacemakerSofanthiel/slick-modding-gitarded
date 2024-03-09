// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_NoDmgStaggerSelf
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_NoDmgStaggerSelf : DiceCardSelfAbilityBase
  {
    public static string Desc = "Dice on this page deal no physical damage [On Use] Stagger self at the end of the Scene";

    public override void OnStartBattleAfterCreateBehaviour()
    {
      base.OnStartBattleAfterCreateBehaviour();
      this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus()
      {
        dmg = -99999
      });
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_Aftermath_NoDmgStaggerSelf.BattleUnitBuf_StaggerSelfSceneEnd());
    }

    private class BattleUnitBuf_StaggerSelfSceneEnd : BattleUnitBuf
    {
      public override void OnRoundEnd()
      {
        base.OnRoundEnd();
        this._owner.breakDetail.TakeBreakDamage(this._owner.breakDetail.GetDefaultBreakGauge());
        this.Destroy();
      }
    }
  }
}
