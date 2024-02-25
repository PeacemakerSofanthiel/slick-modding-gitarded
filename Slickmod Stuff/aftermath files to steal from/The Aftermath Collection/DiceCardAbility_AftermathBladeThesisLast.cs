// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathBladeThesisLast
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathBladeThesisLast : DiceCardAbilityBase
  {
    private AudioClip hitBTlast = AftermathCollectionInitializer.aftermathMapHandler.GetAudioClip("hitBTlast.mp3");
    public static string Desc = "[On Hit] Target's Speed is fixed at 1 next Scene";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      if (target == null)
        return;
      target.bufListDetail.AddKeywordBufByCard(KeywordBuf.DecreaseSpeedTo1, 1, this.owner);
      if (this.owner.battleCardResultLog == null || !this.owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>())
        return;
      this.behavior.behaviourInCard.EffectRes = "FX_PC_RolRang_Greatsword";
      this.owner.battleCardResultLog.SetPrintEffectEvent(new BattleCardBehaviourResult.BehaviourEvent(this.Effect));
    }

    private void Effect()
    {
      CameraFilterUtil.EarthQuake(0.08f, 0.05f, 50f, 0.5f);
      AftermathCollectionInitializer.PlaySound(this.hitBTlast, this.owner.view.transform, 2.15f);
    }
  }
}
