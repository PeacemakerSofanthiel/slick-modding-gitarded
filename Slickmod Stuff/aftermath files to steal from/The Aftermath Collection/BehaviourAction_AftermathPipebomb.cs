// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BehaviourAction_AftermathPipebomb
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BehaviourAction_AftermathPipebomb : BehaviourActionBase
  {
    public override List<RencounterManager.MovingAction> GetMovingAction(
      ref RencounterManager.ActionAfterBehaviour self,
      ref RencounterManager.ActionAfterBehaviour opponent)
    {
      List<RencounterManager.MovingAction> movingAction1 = new List<RencounterManager.MovingAction>();
      bool flag = false;
      if (opponent.behaviourResultData != null)
        flag = opponent.behaviourResultData.IsFarAtk();
      if (self.result == Result.Win && !flag)
      {
        RencounterManager.MovingAction movingAction2 = new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.Stop, delay: 0.65f);
        movingAction2.SetEffectTiming(EffectTiming.POST, EffectTiming.PRE, EffectTiming.POST);
        if (self.view.model.customBook.Name == "A Yellow Ties Officer" || self.view.model.customBook.Name == "A Yellow Ties Officer's Page")
          movingAction2.actionDetail = ActionDetail.S1;
        movingAction2.customEffectRes = "Liu1_withdmged_p_sp1";
        movingAction2.knockbackPower = 13f;
        movingAction1.Add(movingAction2);
        RencounterManager.MovingAction movingAction3 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback);
        movingAction3.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
        movingAction1.Add(movingAction3);
        opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, delay: 0.0f));
        opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, delay: 0.65f));
      }
      else
        movingAction1 = base.GetMovingAction(ref self, ref opponent);
      return movingAction1;
    }
  }
}
