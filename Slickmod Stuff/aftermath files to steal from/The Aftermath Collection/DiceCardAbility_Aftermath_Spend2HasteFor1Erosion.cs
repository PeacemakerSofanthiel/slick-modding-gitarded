// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_Spend2HasteFor1Erosion
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_Spend2HasteFor1Erosion : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] If in the 'Blade Unlocked' state, spend 2 Haste to inflict 1 Erosion next Scene";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      BattleUnitBuf activatedBuf = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
      if (activatedBuf == null || activatedBuf.stack < 2 || this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) == null)
        return;
      activatedBuf.stack -= 2;
      target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner);
    }
  }
}
