// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BehaviourAction_AftermathDaveDawnAction
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BehaviourAction_AftermathDaveDawnAction : BehaviourActionBase
  {
    public override List<RencounterManager.MovingAction> GetMovingAction(
      ref RencounterManager.ActionAfterBehaviour self,
      ref RencounterManager.ActionAfterBehaviour opponent)
    {
      List<RencounterManager.MovingAction> movingAction1 = new List<RencounterManager.MovingAction>();
      bool flag = false;
      if (opponent.behaviourResultData != null)
        flag = opponent.behaviourResultData.IsFarAtk();
      if (self.result == Result.Win && self.data.actionType == ActionType.Atk && !flag)
      {
        RencounterManager.MovingAction movingAction2 = new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.MoveForward, 25f, false, 0.5f, 1.5f);
        movingAction2.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
        if (self.view.model.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is PassiveAbility_Aftermath_OverdriveShift.BattleUnitBuf_Aftermath_WELCUMTOBOTTMGEARMATES || x is PassiveAbility_Aftermath_OverdriveShiftBoss.BattleUnitBuf_Aftermath_TOPGEAREALSHIT)) != null)
          movingAction2.speed += 0.5f;
        movingAction1.Add(movingAction2);
        opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.KnockDown, updateDir: false, delay: 0.5f));
      }
      else
        movingAction1 = base.GetMovingAction(ref self, ref opponent);
      return movingAction1;
    }
  }
}
