// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathBladeThesisFirst
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathBladeThesisFirst : DiceCardAbilityBase
  {
    private AudioClip hitBTfirst = AftermathCollectionInitializer.aftermathMapHandler.GetAudioClip("hitBTfirst.mp3");
    public static string Desc = "[On Clash Lose] Activate 'Blade Thesis'";

    public override void OnLoseParrying()
    {
      base.OnLoseParrying();
      if (this.owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>())
        return;
      PassiveAbility_Aftermath_BladeThesis.ActivateBladeThesis(this.owner);
    }

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      if (target == null || this.owner.battleCardResultLog == null || !this.owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>())
        return;
      this.owner.battleCardResultLog.SetPrintEffectEvent(new BattleCardBehaviourResult.BehaviourEvent(this.Effect));
    }

    private void Effect()
    {
      CameraFilterUtil.EarthQuake(0.06f, 0.04f, 40f, 0.4f);
      AftermathCollectionInitializer.PlaySound(this.hitBTfirst, this.owner.view.transform, 1.75f);
    }
  }
}
