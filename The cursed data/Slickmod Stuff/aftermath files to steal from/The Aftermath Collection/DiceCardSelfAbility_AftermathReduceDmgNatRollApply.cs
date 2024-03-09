// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathReduceDmgNatRollApply2Ally1Protection
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathReduceDmgNatRollApply2Ally1Protection : 
    DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] Give 1 Protection to two random allies [On Use] All offensive dice on this page gain '[On Clash Lose] Reduce incoming damage by the natural roll'";

    public override void OnUseCard()
    {
      base.OnUseCard();
      this.card.ForeachQueue(DiceMatch.AllAttackDice, (BattlePlayingCardDataInUnitModel.ForeachAction) (x => x.AddAbility((DiceCardAbilityBase) new DiceCardAbility_AftermathReduceDmgNatRoll())));
    }

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList_random(this.owner.faction, 2))
        battleUnitModel.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Protection, 1, this.owner);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Protection_Keyword" };
    }
  }
}
