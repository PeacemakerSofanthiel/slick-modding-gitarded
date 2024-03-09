// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_GuidedAssault
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_GuidedAssault : PassiveAbilityBase
  {
    public override void OnStartBattle()
    {
      base.OnStartBattle();
      BattlePlayingCardDataInUnitModel cardDataInUnitModel = this.owner.cardSlotDetail.cardQueue.Last<BattlePlayingCardDataInUnitModel>();
      if (cardDataInUnitModel == null)
        return;
      List<BattleDiceBehavior> all = cardDataInUnitModel.GetDiceBehaviorList().FindAll((Predicate<BattleDiceBehavior>) (x => this.IsAttackDice(x.Detail)));
      if (all.Count <= 1)
        return;
      BattleDiceBehavior battleDiceBehavior1 = all.First<BattleDiceBehavior>((Func<BattleDiceBehavior, bool>) (x => this.IsAttackDice(x.Detail)));
      foreach (BattleDiceBehavior battleDiceBehavior2 in all)
        battleDiceBehavior2.behaviourInCard.Detail = battleDiceBehavior1.Detail;
    }
  }
}
