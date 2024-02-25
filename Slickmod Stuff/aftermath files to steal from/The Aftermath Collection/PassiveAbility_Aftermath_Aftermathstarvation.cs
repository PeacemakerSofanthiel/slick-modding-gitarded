// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_Aftermathstarvation
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_Aftermathstarvation : PassiveAbilityBase
  {
    private int _lightLastScene;
    private bool h;

    public override void OnStartOneSidePlay(BattlePlayingCardDataInUnitModel card)
    {
      this.owner.battleCardResultLog?.SetEndCardActionEvent(new BattleCardBehaviourResult.BehaviourEvent(this.PrintEffect));
    }

    public override void OnStartParrying(BattlePlayingCardDataInUnitModel card)
    {
      this.owner.battleCardResultLog?.SetEndCardActionEvent(new BattleCardBehaviourResult.BehaviourEvent(this.PrintEffect));
    }

    private void PrintEffect()
    {
      this._lightLastScene = this.owner.PlayPoint;
      int count1 = this.owner.emotionDetail.PositiveCoins.Count;
      int count2 = this.owner.emotionDetail.NegativeCoins.Count;
      if (!this.owner.emotionDetail.CheckLevelUp())
        return;
      this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, count2 / 2, this.owner);
      this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, count1 / 2, this.owner);
      this.owner.cardSlotDetail.SetPlayPoint(this._lightLastScene + this.owner.cardSlotDetail.GetMaxPlayPoint() / 2);
    }

    public override void OnRoundEnd()
    {
      this._lightLastScene = this.owner.PlayPoint;
      if (this.owner.emotionDetail.AllEmotionCoins.Count < this.owner.emotionDetail.MaximumCoinNumber)
        return;
      this.h = true;
      int count = this.owner.emotionDetail.PositiveCoins.Count;
      this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, this.owner.emotionDetail.NegativeCoins.Count / 2, this.owner);
      this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, count / 2, this.owner);
    }

    public override void OnRoundEndTheLast()
    {
      if (!this.h)
        return;
      this.h = false;
      this.owner.cardSlotDetail.SetPlayPoint(this._lightLastScene + this.owner.cardSlotDetail.GetMaxPlayPoint() / 2);
    }
  }
}
