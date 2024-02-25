// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Target3Enemies
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Target3Enemies : DiceCardSelfAbilityBase
  {
    public static string Desc = "This page is used against 3 enemies";

    public override void OnApplyCard()
    {
      this.card.subTargets.Clear();
      List<BattleUnitModel> aliveListOpponent = BattleObjectManager.instance.GetAliveList_opponent(this.owner.faction);
      int num = 0;
      Predicate<BattleUnitModel> match = (Predicate<BattleUnitModel>) (x => x != this.card.target);
      foreach (BattleUnitModel battleUnitModel in aliveListOpponent.FindAll(match))
      {
        this.card.subTargets.Add(new BattlePlayingCardDataInUnitModel.SubTarget()
        {
          target = battleUnitModel,
          targetSlotOrder = UnityEngine.Random.Range(0, battleUnitModel.speedDiceCount)
        });
        ++num;
        if (num >= 2)
          break;
      }
    }
  }
}
