// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BehaviourAction_AftermathRagingStormHope
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BehaviourAction_AftermathRagingStormHope : BehaviourActionBase
  {
    public override List<RencounterManager.MovingAction> GetMovingAction(
      ref RencounterManager.ActionAfterBehaviour self,
      ref RencounterManager.ActionAfterBehaviour opponent)
    {
      try
      {
        List<RencounterManager.MovingAction> movingAction1 = new List<RencounterManager.MovingAction>();
        bool flag = false;
        if (opponent.behaviourResultData != null)
          flag = opponent.behaviourResultData.IsFarAtk();
        if (self.result == Result.Win && self.data.actionType == ActionType.Atk && !flag)
        {
          RencounterManager.MovingAction movingAction2 = new RencounterManager.MovingAction(ActionDetail.Evade, CharMoveState.MoveBack, 6f, delay: 0.1f, speed: 0.8f);
          movingAction2.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
          movingAction1.Add(movingAction2);
          RencounterManager.MovingAction movingAction3 = new RencounterManager.MovingAction(ActionDetail.S1, CharMoveState.Stop, 0.6f, delay: 1f);
          movingAction3.customEffectRes = "FX_Mon_Bayyard_SCharge";
          movingAction3.SetEffectTiming(EffectTiming.PRE, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
          movingAction1.Add(movingAction3);
          RencounterManager.MovingAction movingAction4 = new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.MoveForward, 27.5f, false, 1f, 1.1f);
          movingAction4.customEffectRes = "XiaoEgo_S5";
          movingAction4.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
          movingAction1.Add(movingAction4);
          if (opponent.infoList.Count > 0)
            opponent.infoList.Clear();
          opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Guard, CharMoveState.Stop, 0.0f, delay: 0.1f));
          opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 0.0f, false, 2.2f));
        }
        else
          movingAction1 = base.GetMovingAction(ref self, ref opponent);
        return movingAction1;
      }
      catch
      {
        return base.GetMovingAction(ref self, ref opponent);
      }
    }
  }
}
