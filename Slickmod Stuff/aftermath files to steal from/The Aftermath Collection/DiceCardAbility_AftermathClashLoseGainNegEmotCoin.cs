// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathClashLoseGainNegEmotCoin
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathClashLoseGainNegEmotCoin : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Lose] Gain 1 Negative Emotion Coin";

    public override void OnLoseParrying()
    {
      this.owner.emotionDetail.CreateEmotionCoin(EmotionCoinType.Negative);
      SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(this.owner, EmotionCoinType.Negative, 1);
    }
  }
}
