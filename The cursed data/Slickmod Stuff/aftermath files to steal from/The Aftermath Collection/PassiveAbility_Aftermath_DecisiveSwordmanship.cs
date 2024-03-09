// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_DecisiveSwordmanship
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_DecisiveSwordmanship : PassiveAbilityBase
  {
    private bool activated;

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      this.activated = false;
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
      if (curCard.GetOriginalDiceBehaviorList().Count != 1 || curCard.GetOriginalDiceBehaviorList().FindAll((Predicate<DiceBehaviour>) (x => x.Detail == BehaviourDetail.Slash)).Count != 1)
        return;
      this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase) this);
      curCard.emotionMultiplier = 2;
      curCard.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus()
      {
        power = 1
      });
      if (this.activated)
        return;
      curCard.ApplyDiceAbility(DiceMatch.AllDice, (DiceCardAbilityBase) new DiceCardAbility_oswald_destroy1dice());
      this.activated = true;
    }

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      if (behavior.Detail != BehaviourDetail.Slash)
        return;
      this.owner.ShowPassiveTypo((PassiveAbilityBase) this);
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        power = 1
      });
    }
  }
}
