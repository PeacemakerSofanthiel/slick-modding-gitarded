// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathReduceDmgNatRollApplyAndDraw1
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathReduceDmgNatRollApplyAndDraw1 : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Draw 1 page; All offensive dice on this page gain '[On Clash Lose] Reduce incoming damage by the natural roll'";

    public override void OnUseCard()
    {
      base.OnUseCard();
      this.card.ForeachQueue(DiceMatch.AllAttackDice, (BattlePlayingCardDataInUnitModel.ForeachAction) (x => x.AddAbility((DiceCardAbilityBase) new DiceCardAbility_AftermathReduceDmgNatRoll())));
      this.owner.allyCardDetail.DrawCards(1);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Protection_Keyword" };
    }
  }
}
