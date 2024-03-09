// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_SixHasteErode
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  [UnusedAbility]
  public class DiceCardAbility_Aftermath_SixHasteErode : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] If user has 6+ Haste, inflict 2 Erosion this Scene";

    public override void OnSucceedAttack()
    {
      if (!(this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness) is BattleUnitBuf_quickness activatedBuf1) || activatedBuf1.stack < 6)
        return;
      BattleUnitModel target = this.card.target;
      if (target == null)
        return;
      target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Decay, 2, this.owner);
      if (!(target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay) is BattleUnitBuf_Decay activatedBuf2))
        return;
      activatedBuf2.ChangeToYanDecay();
    }
  }
}
