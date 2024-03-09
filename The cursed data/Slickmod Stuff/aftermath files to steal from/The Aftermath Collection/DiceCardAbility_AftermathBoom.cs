// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathBoom
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathBoom : DiceCardAbilityBase
  {
    private AudioClip boomBurn = AftermathCollectionInitializer.aftermathMapHandler.GetAudioClip("boomBurn.mp3");
    public static string Desc = "[On Hit] Inflict 5 Burn to target and 1 Burn to self this Scene";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      this.owner.battleCardResultLog.SetPrintEffectEvent(new BattleCardBehaviourResult.BehaviourEvent(this.Effect));
      target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 5, this.owner);
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, 1, this.owner);
    }

    private void Effect()
    {
      CameraFilterUtil.EarthQuake(0.08f, 0.04f, 55f, 0.5f);
      AftermathCollectionInitializer.PlaySound(this.boomBurn, this.owner.view.transform, 1.67f);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Burn_Keyword" };
    }
  }
}
