// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_hasterepeat3
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_hasterepeat3 : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Spend 1 Haste to reroll this die (Max. 3)";
    private int count;

    public override void OnSucceedAttack()
    {
      if (this.count >= 3 || !(this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness) is BattleUnitBuf_quickness activatedBuf) || activatedBuf.stack < 1)
        return;
      this.ActivateBonusAttackDice();
      ++this.count;
      --activatedBuf.stack;
      ++this.owner.UnitData.historyInUnit.trashDisposalReuse;
    }
  }
}
