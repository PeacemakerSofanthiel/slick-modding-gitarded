// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_Enemy_AcidicSpill
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_Enemy_AcidicSpill : PassiveAbilityBase
  {
    private bool first;

    public override void OnRoundStart()
    {
      base.OnRoundStart();
      this.first = true;
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
      if (this.first)
      {
        curCard.ApplyDiceAbility(DiceMatch.AllAttackDice, (DiceCardAbilityBase) new DiceCardAbility_Aftermath_Erosion2AtkBoth());
        curCard.ApplyDiceStatBonus(DiceMatch.AllAttackDice, new DiceStatBonus()
        {
          power = 2
        });
        this.first = false;
      }
      base.OnUseCard(curCard);
    }
  }
}
