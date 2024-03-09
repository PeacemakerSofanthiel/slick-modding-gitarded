// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.FarAreaEffect_Aftermath_EventHorizon
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class FarAreaEffect_Aftermath_EventHorizon : FarAreaEffect
  {
    private float _elapsed;
    private SpriteRenderer _spr;
    private ActionDetail _beforeMotion;
    private bool flag;
    private bool flag2;
    private List<BattleFarAreaPlayManager.VictimInfo> _victimList;
    private GameObject gameObject;
    private AssetBundle assetBundle = AftermathCollectionInitializer.assetBundles["lightninghammer"];

    public override void Init(BattleUnitModel self, params object[] args)
    {
      base.Init(self, args);
      this.OnEffectStart();
      this._elapsed = 0.0f;
      Singleton<BattleFarAreaPlayManager>.Instance.SetActionDelay(0.0f);
      new List<BattleUnitModel>() { self }.AddRange((IEnumerable<BattleUnitModel>) BattleObjectManager.instance.GetAliveList(self.faction == Faction.Enemy ? Faction.Player : Faction.Enemy));
      this._beforeMotion = ActionDetail.Default;
      this._victimList = new List<BattleFarAreaPlayManager.VictimInfo>((IEnumerable<BattleFarAreaPlayManager.VictimInfo>) Singleton<BattleFarAreaPlayManager>.Instance.victims);
      float x = -5.5f;
      if ((double) this._self.view.WorldPosition.x > 0.0)
        x *= -3.5f;
      self.moveDetail.Move(new Vector3(x, 0.0f, -5f), 200f);
    }

    public override void Update()
    {
      if (this.state == FarAreaEffect.EffectState.Start)
      {
        if (!this._self.moveDetail.isArrived)
          return;
        this._self.view.charAppearance.ChangeMotion(ActionDetail.S1);
        this.state = FarAreaEffect.EffectState.GiveDamage;
      }
      else if (this.state == FarAreaEffect.EffectState.GiveDamage)
      {
        this._elapsed += Time.deltaTime;
        if (this.flag)
        {
          this.gameObject = Object.Instantiate<GameObject>(this.assetBundle.LoadAsset<GameObject>("LightningHammer_MaximumCrash"));
          this.gameObject.transform.parent = this._self.view.atkEffectRoot;
          this.gameObject.transform.localPosition = new Vector3(0.0f, -2f, -1f);
          if ((double) this._self.view.WorldPosition.x < 0.0)
            this.gameObject.transform.localEulerAngles = new Vector3(0.0f, 180f, 0.0f);
          this.gameObject.layer = LayerMask.NameToLayer("Effect");
          this.gameObject.SetActive(true);
          this._self.view.charAppearance.ChangeMotion(ActionDetail.S2);
          this.flag = false;
        }
        if ((double) this._elapsed < 3.0 || !this.flag2)
          return;
        TimeManager.Instance.SlowMotion(0.12f, 0.125f, true);
        this._elapsed = 0.0f;
        this.isRunning = false;
        this.state = FarAreaEffect.EffectState.End;
      }
      else if (this.state == FarAreaEffect.EffectState.End)
      {
        this._elapsed += Time.deltaTime;
        if ((double) this._elapsed <= 0.5)
          return;
        this._self.view.charAppearance.ChangeMotion(ActionDetail.Default);
        this.state = FarAreaEffect.EffectState.None;
        this._elapsed = 0.0f;
      }
      else
      {
        if (this.state != FarAreaEffect.EffectState.None || !this._self.view.FormationReturned)
          return;
        Object.Destroy((Object) this.gameObject);
      }
    }

    public FarAreaEffect_Aftermath_EventHorizon()
    {
      this.flag = true;
      this.flag2 = true;
    }

    private void EarthQuake()
    {
      BattleCamManager instance1 = SingletonBehavior<BattleCamManager>.Instance;
      CameraFilterPack_FX_EarthQuake r = ((Object) instance1 != (Object) null ? instance1.EffectCam.gameObject.AddComponent<CameraFilterPack_FX_EarthQuake>() : (CameraFilterPack_FX_EarthQuake) null) ?? (CameraFilterPack_FX_EarthQuake) null;
      if (!((Object) r != (Object) null))
        return;
      r.StartCoroutine(this.EarthQuakeRoutine(r));
      BattleCamManager instance2 = SingletonBehavior<BattleCamManager>.Instance;
      AutoScriptDestruct autoScriptDestruct = ((Object) instance2 != (Object) null ? instance2.EffectCam.gameObject.AddComponent<AutoScriptDestruct>() : (AutoScriptDestruct) null) ?? (AutoScriptDestruct) null;
      if (!((Object) autoScriptDestruct != (Object) null))
        return;
      autoScriptDestruct.targetScript = (MonoBehaviour) r;
      autoScriptDestruct.time = 1f;
    }

    private IEnumerator EarthQuakeRoutine(CameraFilterPack_FX_EarthQuake r)
    {
      float e = 0.0f;
      while ((double) e < 1.0)
      {
        e += Time.deltaTime;
        r.Speed = (float) (20.0 * (1.0 - (double) e));
        r.X = (float) (0.20000000298023224 * (1.0 - (double) e));
        r.Y = (float) (0.20000000298023224 * (1.0 - (double) e));
        yield return (object) null;
      }
    }
  }
}
