// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_THEBALLAD
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_THEBALLAD : DiceCardSelfAbilityBase
  {
    public static string Desc = "This page’s Cost is lowered by half the amount of Erosion on self\n[Start of Clash] Purge all Erosion from self and target; if at least 10 Erosion was purged from target, the first die on this page gains ‘[On Clash Win] Destroy all of target’s dice’; if at least 10 Erosion was purged from self, the first die on this page gains ‘[On Hit] Inflict Erosion equal to all stacks of Erosion purged by this page’";

    public override int GetCostAdder(BattleUnitModel unit, BattleDiceCardModel self)
    {
      return -(unit.bufListDetail.GetKewordBufAllStack(KeywordBuf.Decay) / 2);
    }

    public override void OnStartParrying()
    {
      base.OnStartParrying();
      BattleUnitModel target = this.card.target;
      if (target == null)
        return;
      BattleUnitBuf activatedBuf1 = target.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
      BattleUnitBuf activatedBuf2 = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
      if (activatedBuf1 != null && activatedBuf1.stack >= 10)
        this.card.ApplyDiceAbility(DiceMatch.NextDice, (DiceCardAbilityBase) new DiceCardAbility_teddyEgo());
      if (activatedBuf2 != null && activatedBuf2.stack >= 10)
        this.card.ApplyDiceAbility(DiceMatch.NextAttackDice, (DiceCardAbilityBase) new DiceCardSelfAbility_Aftermath_THEBALLAD.DiceCardAbility_Aftermath_HiddenEffectBALLAD(activatedBuf2.stack + (activatedBuf1 != null ? activatedBuf1.stack : 0)));
      activatedBuf1?.Destroy();
      activatedBuf2?.Destroy();
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Decay_Keyword" };
    }

    public class DiceCardAbility_Aftermath_HiddenEffectBALLAD : DiceCardAbilityBase
    {
      private int stacks;

      public DiceCardAbility_Aftermath_HiddenEffectBALLAD(int stonk) => this.stacks = stonk;

      public override void OnSucceedAttack(BattleUnitModel target)
      {
        base.OnSucceedAttack(target);
        target?.bufListDetail?.AddKeywordBufByCard(KeywordBuf.Decay, this.stacks, this.owner);
      }
    }
  }
}
