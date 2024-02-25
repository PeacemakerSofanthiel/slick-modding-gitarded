using System;
using System.Collections.Generic;
using UnityEngine;

namespace SlickRuinaMod
{
    // Fast Slash Action Script
    public class BehaviourAction_SlickMod_QuickAttack_J : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            bool flag = false;
            bool flag2 = opponent.behaviourResultData != null;
            if (flag2)
            {
                flag = opponent.behaviourResultData.IsFarAtk();
            }
            List<RencounterManager.MovingAction> list = new List<RencounterManager.MovingAction>();
            bool flag3 = self.result == Result.Win && self.data.actionType == ActionType.Atk && !flag;
            if (flag3)
            {
                int resultDiceValue = self.behaviourResultData.resultDiceValue;
                float dstRatio = Mathf.Clamp(0.1f * (float)resultDiceValue, 0.5f, 12f);
                RencounterManager.MovingAction movingAction = new RencounterManager.MovingAction(ActionDetail.Slash, CharMoveState.MoveForward, dstRatio, true, 0.025f, 2f);
                movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
                list.Add(movingAction);
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, dstRatio, true, 0.025f, 2f));
            }
            else
            {
                list = base.GetMovingAction(ref self, ref opponent);
            }
            return list;
        }
    }

    // Fast Pierce Action Script
    public class BehaviourAction_SlickMod_QuickAttack_Z : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            bool flag = false;
            bool flag2 = opponent.behaviourResultData != null;
            if (flag2)
            {
                flag = opponent.behaviourResultData.IsFarAtk();
            }
            List<RencounterManager.MovingAction> list = new List<RencounterManager.MovingAction>();
            bool flag3 = self.result == Result.Win && self.data.actionType == ActionType.Atk && !flag;
            if (flag3)
            {
                int resultDiceValue = self.behaviourResultData.resultDiceValue;
                float dstRatio = Mathf.Clamp(0.1f * (float)resultDiceValue, 0.5f, 12f);
                RencounterManager.MovingAction movingAction = new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.MoveForward, dstRatio, true, 0.025f, 2f);
                movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
                list.Add(movingAction);
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, dstRatio, true, 0.025f, 2f));
            }
            else
            {
                list = base.GetMovingAction(ref self, ref opponent);
            }
            return list;
        }
    }

    // Fast Blunt Action Script
    public class BehaviourAction_SlickMod_QuickAttack_H : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            bool flag = false;
            bool flag2 = opponent.behaviourResultData != null;
            if (flag2)
            {
                flag = opponent.behaviourResultData.IsFarAtk();
            }
            List<RencounterManager.MovingAction> list = new List<RencounterManager.MovingAction>();
            bool flag3 = self.result == Result.Win && self.data.actionType == ActionType.Atk && !flag;
            if (flag3)
            {
                int resultDiceValue = self.behaviourResultData.resultDiceValue;
                float dstRatio = Mathf.Clamp(0.1f * (float)resultDiceValue, 0.5f, 12f);
                RencounterManager.MovingAction movingAction = new RencounterManager.MovingAction(ActionDetail.Hit, CharMoveState.MoveForward, dstRatio, true, 0.025f, 2f);
                movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
                list.Add(movingAction);
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, dstRatio, true, 0.025f, 2f));
            }
            else
            {
                list = base.GetMovingAction(ref self, ref opponent);
            }
            return list;
        }
    }

    // Fast Fire Action Script
    public class BehaviourAction_SlickMod_QuickAttack_F : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            List<RencounterManager.MovingAction> list = new List<RencounterManager.MovingAction>();
            bool flag = self.result == Result.Win && self.data.actionType == ActionType.Atk;
            if (flag)
            {
                int resultDiceValue = self.behaviourResultData.resultDiceValue;
                float dstRatio = Mathf.Clamp(0.1f * (float)resultDiceValue, 0.5f, 12f);
                RencounterManager.MovingAction movingAction = new RencounterManager.MovingAction(ActionDetail.Fire, CharMoveState.Stop, dstRatio, true, 0.025f, 2f);
                movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
                list.Add(movingAction);
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, dstRatio, true, 0.025f, 2f));
            }
            else
            {
                list = base.GetMovingAction(ref self, ref opponent);
            }
            return list;
        }
    }
}
