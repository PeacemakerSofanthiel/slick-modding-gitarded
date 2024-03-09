// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_ColorChunInnerFlames
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_ColorChunInnerFlames : DiceCardSelfAbilityBase
  {
    private int stacks;
    public static string Desc = "";

    public override void OnStartBattle()
    {
      BattleUnitModel target = this.card.target;
      if (target == null)
        return;
      BattleUnitBuf activatedBuf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
      if (activatedBuf == null)
        return;
      this.stacks = activatedBuf.stack;
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      this.stacks /= 6;
      if (this.stacks < 1)
        return;
      this.card.ApplyDiceAbility(DiceMatch.NextAttackDice, (DiceCardAbilityBase) new DiceCardSelfAbility_ColorChunInnerFlames.DiceCardAbility_AftermathhiddenEconomyOne(this.stacks));
      this.card.ApplyDiceAbility(DiceMatch.NextDefenseDice, (DiceCardAbilityBase) new DiceCardSelfAbility_ColorChunInnerFlames.DiceCardAbility_AftermathhiddenEconomyTwo(this.stacks));
    }

    public override string[] Keywords
    {
      get
      {
        return new string[4]
        {
          "ColorChunOnlyPage_Keyword",
          "Burn_Keyword",
          "DrawCard_Keyword",
          "Energy_Keyword"
        };
      }
    }

    public class DiceCardAbility_AftermathhiddenEconomyOne : DiceCardAbilityBase
    {
      private int stonks;

      public DiceCardAbility_AftermathhiddenEconomyOne(int stacks) => this.stonks = stacks;

      public override void OnRollDice()
      {
        if (!this.behavior.IsParrying())
          return;
        this.owner.cardSlotDetail.RecoverPlayPoint(this.stonks);
      }
    }

    public class DiceCardAbility_AftermathhiddenEconomyTwo : DiceCardAbilityBase
    {
      private int stonks;

      public DiceCardAbility_AftermathhiddenEconomyTwo(int stacks) => this.stonks = stacks;

      public override void OnWinParrying()
      {
        base.OnWinParrying();
        this.owner.allyCardDetail.DrawCards(this.stonks);
      }

      public override void OnDrawParrying()
      {
        base.OnDrawParrying();
        this.owner.allyCardDetail.DrawCards(this.stonks);
      }

      public override void OnLoseParrying()
      {
        BattleDiceBehavior targetDice = this.behavior.TargetDice;
        if (targetDice == null || this.IsAttackDice(targetDice.Detail))
          return;
        this.owner.allyCardDetail.DrawCards(this.stonks);
      }
    }
  }
}
