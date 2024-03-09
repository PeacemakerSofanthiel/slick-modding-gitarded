// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathFlamingBurdenFirst
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathFlamingBurdenFirst : DiceCardAbilityBase
  {
    private bool rolled;
    public static string Desc = "[On Clash Win] Recycle this die (Max. 1); Inflict 2 Burn to target and self";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      if (target == null)
        return;
      this.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, 2, this.owner);
      target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, 2, this.owner);
    }

    public override void AfterAction()
    {
      if (this.rolled)
        return;
      this.rolled = true;
      this.ActivateBonusAttackDice();
    }
  }
}
