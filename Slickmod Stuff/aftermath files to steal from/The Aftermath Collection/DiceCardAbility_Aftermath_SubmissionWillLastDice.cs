// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_SubmissionWillLastDice
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_SubmissionWillLastDice : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Spend 2 Haste to gain 1 Haste next scene (stacks of Erosion on target) times";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      BattleUnitBuf activatedBuf1 = target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
      BattleUnitBuf activatedBuf2 = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
      if (activatedBuf1 == null || activatedBuf2 == null || activatedBuf2.stack < 2)
        return;
      activatedBuf2.stack -= 2;
      for (int index = 0; index < activatedBuf1.stack; ++index)
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 1, this.owner);
    }
  }
}
