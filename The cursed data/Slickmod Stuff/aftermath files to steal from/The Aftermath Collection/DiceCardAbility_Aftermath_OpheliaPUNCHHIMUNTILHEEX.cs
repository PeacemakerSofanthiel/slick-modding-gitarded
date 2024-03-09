// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_OpheliaPUNCHHIMUNTILHEEXPLODES
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_OpheliaPUNCHHIMUNTILHEEXPLODES : DiceCardAbilityBase
  {
    private int count;
    public static string Desc = "[On Hit] Spend 1 Haste to recycle this die; if this die has been recycled at least once, inflict 1 Erosion next Scene";

    public override void OnSucceedAttack()
    {
      base.OnSucceedAttack();
      BattleUnitBuf activatedBuf = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
      if (activatedBuf == null)
        return;
      if (activatedBuf.stack >= 1)
      {
        --activatedBuf.stack;
        ++this.count;
        this.ActivateBonusAttackDice();
        if (this.count <= 1)
          return;
        this.behavior.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
      }
      else
        activatedBuf.Destroy();
    }
  }
}
