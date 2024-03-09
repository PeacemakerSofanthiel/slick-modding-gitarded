using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace The_Aftermath_Collection 
{

    // PIPEBOMB FUNNI (IRREFUSABLE OFFER SECOND DIE)
    public class BehaviourAction_AftermathPipebomb : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            List<RencounterManager.MovingAction> list = new List<RencounterManager.MovingAction>();
            bool flag = false;
            if (opponent.behaviourResultData != null)
            {
                flag = opponent.behaviourResultData.IsFarAtk();
            }
            if (self.result == Result.Win && !flag)
            {
                RencounterManager.MovingAction movingAction = new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.Stop, 1f, true, 0.65f, 1f);
                movingAction.SetEffectTiming(EffectTiming.POST, EffectTiming.PRE, EffectTiming.POST);

                if (self.view.model.customBook.Name == "A Yellow Ties Officer" || self.view.model.customBook.Name == "A Yellow Ties Officer's Page")
                {
                    movingAction.actionDetail = ActionDetail.S1;
                }

                movingAction.customEffectRes = "Liu1_withdmged_p_sp1";
                movingAction.knockbackPower = 13f;
                list.Add(movingAction);
                RencounterManager.MovingAction movingAction2 = new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback);
                movingAction2.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
                list.Add(movingAction2);

                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, true, 0f, 1f));
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Knockback, 1f, true, 0.65f, 1f));

            }
            else
            {
                list = base.GetMovingAction(ref self, ref opponent);
            }
            return list;
        }
    }

    // Raging Storm: Hope
    public class BehaviourAction_AftermathRagingStormHope : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            try
            {
                List<RencounterManager.MovingAction> list = new List<RencounterManager.MovingAction>();
                bool flag = false;
                if (opponent.behaviourResultData != null)
                {
                    flag = opponent.behaviourResultData.IsFarAtk();
                }
                if (self.result == Result.Win && self.data.actionType == ActionType.Atk && !flag)
                {
                    RencounterManager.MovingAction movingAction1 = new RencounterManager.MovingAction(ActionDetail.Evade, CharMoveState.MoveBack, 6f, true, 0.1f, 0.8f);
                    movingAction1.SetEffectTiming(EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
                    list.Add(movingAction1);

                    RencounterManager.MovingAction movingAction2 = new RencounterManager.MovingAction(ActionDetail.S1, CharMoveState.Stop, 0.6f, true, 1f, 1f);
                    movingAction2.customEffectRes = "FX_Mon_Bayyard_SCharge";
                    movingAction2.SetEffectTiming(EffectTiming.PRE, EffectTiming.NOT_PRINT, EffectTiming.NOT_PRINT);
                    list.Add(movingAction2);

                    RencounterManager.MovingAction movingAction3 = new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.MoveForward, 27.5f, false, 1f, 1.1f);
                    movingAction3.customEffectRes = "XiaoEgo_S5";
                    movingAction3.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
                    list.Add(movingAction3);

                    if (opponent.infoList.Count > 0)
                    {
                        opponent.infoList.Clear();
                    }
                    opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Guard, CharMoveState.Stop, 0f, true, 0.1f, 1f));
                    opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 0f, false, 2.2f, 1f));
                }
                else
                {
                    list = base.GetMovingAction(ref self, ref opponent);
                }
                return list;
            } catch
            {
                return base.GetMovingAction(ref self, ref opponent); // added due to being strangely finnicky and causing softlocks
            }
        }
    }

    // vehicular manslaughter
    public class BehaviourAction_AftermathDaveDawnAction : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            List<RencounterManager.MovingAction> list = new List<RencounterManager.MovingAction>();
            bool flag = false;
            if (opponent.behaviourResultData != null)
            {
                flag = opponent.behaviourResultData.IsFarAtk();
            }
            if (self.result == Result.Win && self.data.actionType == ActionType.Atk && !flag)
            {
                RencounterManager.MovingAction movingAction = new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.MoveForward, 25f, false, 0.5f, 1.5f);
                movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
                if (self.view.model.bufListDetail.GetActivatedBufList().Find(x => x is PassiveAbility_Aftermath_OverdriveShift.BattleUnitBuf_Aftermath_WELCUMTOBOTTMGEARMATES || x is PassiveAbility_Aftermath_OverdriveShiftBoss.BattleUnitBuf_Aftermath_TOPGEAREALSHIT) != null)
                {
                    movingAction.speed += 0.5f;
                }
                list.Add(movingAction);
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.KnockDown, 1f, false, 0.5f, 1f));
            }
            else
            {
                list = base.GetMovingAction(ref self, ref opponent);
            }
            return list;
        }
    }

    // Levigation
    public class BehaviourAction_AftermathLevigationDawnAction : BehaviourActionBase
    {
        public override List<RencounterManager.MovingAction> GetMovingAction(ref RencounterManager.ActionAfterBehaviour self, ref RencounterManager.ActionAfterBehaviour opponent)
        {
            List<RencounterManager.MovingAction> list = new List<RencounterManager.MovingAction>();
            bool flag = false;
            if (opponent.behaviourResultData != null)
            {
                flag = opponent.behaviourResultData.IsFarAtk();
            }
            if (self.result == Result.Win && self.data.actionType == ActionType.Atk && !flag)
            {
                RencounterManager.MovingAction movingAction = new RencounterManager.MovingAction(ActionDetail.Penetrate, CharMoveState.MoveForward, 25f, false, 0f, 2.25f);
                movingAction.SetEffectTiming(EffectTiming.PRE, EffectTiming.PRE, EffectTiming.PRE);
                list.Add(movingAction);
                opponent.infoList.Add(new RencounterManager.MovingAction(ActionDetail.Damaged, CharMoveState.Stop, 1f, false, 0f, 2.25f));
            }
            else
            {
                list = base.GetMovingAction(ref self, ref opponent);
            }
            return list;
        }
    }

    // Event Horizon from Mobius Office
    public class FarAreaEffect_Aftermath_EventHorizon : FarAreaEffect
    {
        private float _elapsed;

        private SpriteRenderer _spr;

        private ActionDetail _beforeMotion;

        private bool flag;

        private bool flag2;

        private List<BattleFarAreaPlayManager.VictimInfo> _victimList;

        private new GameObject gameObject;

        private AssetBundle assetBundle = AftermathCollectionInitializer.assetBundles["lightninghammer"];

        public override void Init(BattleUnitModel self, params object[] args)
        {
            base.Init(self, args);
            this.OnEffectStart();
            this._elapsed = 0f;
            Singleton<BattleFarAreaPlayManager>.Instance.SetActionDelay(0f, 0.5f);
            new List<BattleUnitModel>
            {
                self
            }.AddRange(BattleObjectManager.instance.GetAliveList((self.faction == Faction.Enemy) ? Faction.Player : Faction.Enemy));
            this._beforeMotion = ActionDetail.Default;
            this._victimList = new List<BattleFarAreaPlayManager.VictimInfo>(Singleton<BattleFarAreaPlayManager>.Instance.victims);
            float x = -5.5f;
            if (this._self.view.WorldPosition.x > 0f)
            {
                x *= -3.5f;
            }
            self.moveDetail.Move(new Vector3(x, 0f, -5f), 200f, true, false); //change this vector and speed if your need
        }

        public override void Update()
        {
            if (this.state == FarAreaEffect.EffectState.Start) // move to centre phase
            {
                if (this._self.moveDetail.isArrived)
                {
                    this._self.view.charAppearance.ChangeMotion(ActionDetail.S1); //change ready motion
                    this.state = FarAreaEffect.EffectState.GiveDamage;
                    return;
                }
            }
            else if (this.state == FarAreaEffect.EffectState.GiveDamage) // damage phase
            {
                this._elapsed += Time.deltaTime;
                if (this.flag)
                {
                    this.gameObject = Instantiate(assetBundle.LoadAsset<GameObject>("LightningHammer_MaximumCrash"));
                    this.gameObject.transform.parent = this._self.view.atkEffectRoot;
                    this.gameObject.transform.localPosition = new Vector3(0, -2, -1);  //change this vector if your need , now vfx is O,O,O base on your character(In theory, it should be)
                    if (this._self.view.WorldPosition.x < 0f)
                    {
                        this.gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                    }
                    this.gameObject.layer = LayerMask.NameToLayer("Effect");
                    this.gameObject.SetActive(true);
                    this._self.view.charAppearance.ChangeMotion(ActionDetail.S2); // change attacking motion
                    this.flag = false;
                }
                if (this._elapsed >= 3f && this.flag2) // change 3f to your time ,it is Time from appearance to fall
                {
                    TimeManager.Instance.SlowMotion(0.12f, 0.125f, true); // some time slow function
                    this._elapsed = 0f;
                    this.isRunning = false;
                    this.state = FarAreaEffect.EffectState.End;
                    return;
                }
            }
            else if (this.state == FarAreaEffect.EffectState.End)  //end phase
            {
                this._elapsed += Time.deltaTime;
                if (this._elapsed > 0.5f) //change this time if your need ,Generally, it is not necessary
                {
                    this._self.view.charAppearance.ChangeMotion(ActionDetail.Default); // change motion to default
                    this.state = FarAreaEffect.EffectState.None;
                    this._elapsed = 0f;
                    return;
                }
            }
            else if (this.state == FarAreaEffect.EffectState.None && this._self.view.FormationReturned)
            {
                UnityEngine.Object.Destroy(base.gameObject);
            }
        }

        public FarAreaEffect_Aftermath_EventHorizon()
        {
            this.flag = true;
            this.flag2 = true;
        }

        private void EarthQuake() // screen jitter
        {
            BattleCamManager instance = SingletonBehavior<BattleCamManager>.Instance;
            CameraFilterPack_FX_EarthQuake cameraFilterPack_FX_EarthQuake = ((instance != null) ? instance.EffectCam.gameObject.AddComponent<CameraFilterPack_FX_EarthQuake>() : null) ?? null;
            if (cameraFilterPack_FX_EarthQuake != null)
            {
                cameraFilterPack_FX_EarthQuake.StartCoroutine(this.EarthQuakeRoutine(cameraFilterPack_FX_EarthQuake));
                BattleCamManager instance2 = SingletonBehavior<BattleCamManager>.Instance;
                AutoScriptDestruct autoScriptDestruct = ((instance2 != null) ? instance2.EffectCam.gameObject.AddComponent<AutoScriptDestruct>() : null) ?? null;
                if (autoScriptDestruct != null)
                {
                    autoScriptDestruct.targetScript = cameraFilterPack_FX_EarthQuake;
                    autoScriptDestruct.time = 1f; // time
                }
            }
        }

        private IEnumerator EarthQuakeRoutine(CameraFilterPack_FX_EarthQuake r)
        {
            float e = 0f;
            while (e < 1f) // 1f is time ,remeber to change all 1f
            {
                e += Time.deltaTime;
                r.Speed = 20f * (1f - e); // speed
                r.X = 0.2f * (1f - e); // X - axis strength
                r.Y = 0.2f * (1f - e); // Y - axis strength
                yield return null;
            }
            yield break;
        }
    }

    public class BehaviourAction_Aftermath_EventHorizon : BehaviourActionBase
    {
        // Token: 0x0600011C RID: 284 RVA: 0x00007958 File Offset: 0x00005B58
        public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
        {
            this._self = self;
            FarAreaEffect_Aftermath_EventHorizon FarAreaEffect_EventHorizon = new GameObject().AddComponent<FarAreaEffect_Aftermath_EventHorizon>();
            FarAreaEffect_EventHorizon.Init(self, Array.Empty<object>());
            return FarAreaEffect_EventHorizon;
        }
    }

}