// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_LevigationSecondDie
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_LevigationSecondDie : DiceCardAbilityBase
  {
    private bool freebie;
    public static string Desc = "[On Hit] Spend 1 Haste to roll this die twice";

    public override void AfterAction()
    {
      base.AfterAction();
      BattleUnitBuf activatedBuf = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
      if (activatedBuf.stack <= 0 && !this.freebie)
        return;
      this.ActivateBonusAttackDice();
      if (this.freebie)
        this.freebie = false;
      else
        --activatedBuf.stack;
      this.freebie = true;
    }
  }
}
