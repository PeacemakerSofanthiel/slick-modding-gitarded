// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_SpendChargeRecycle
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_SpendChargeRecycle : DiceCardAbilityBase
  {
    private int count;
    public static string Desc = "[On Hit] Spend 3 Charge to recycle this die (Up to 3 times). If user has Overcharge, inflict 2 Paralysis";

    public override void OnSucceedAttack()
    {
      if (this.count >= 3)
        return;
      if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) is BattleUnitBuf_warpCharge activatedBuf && activatedBuf.stack >= 3)
      {
        activatedBuf.UseStack(3, true);
        this.ActivateBonusAttackDice();
        ++this.count;
      }
      if (!this.owner.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
        return;
      this.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 2, this.owner);
    }
  }
}
