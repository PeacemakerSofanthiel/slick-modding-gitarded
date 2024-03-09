// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BehaviourAction_AftermathLevigationDawnAction
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BehaviourAction_AftermathLevigationDawnAction : BehaviourActionBase
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
        RencounterManager.MovingAction movingAction2 = new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.MoveForward, 25f, false, 0.0f, 2.25f);
        movingAction2.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
        movingAction1.Add(movingAction2);
        opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, updateDir: false, delay: 0.0f, speed: 2.25f));
      }
      else
        movingAction1 = base.GetMovingAction(ref self, ref opponent);
      return movingAction1;
    }
  }
}
