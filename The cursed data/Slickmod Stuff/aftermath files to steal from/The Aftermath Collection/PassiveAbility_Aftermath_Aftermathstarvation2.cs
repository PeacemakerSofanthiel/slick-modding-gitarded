// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_Aftermathstarvation2
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_Aftermathstarvation2 : PassiveAbilityBase
  {
    private int _lightLastScene;
    private bool h;

    public override void OnLevelUpEmotion()
    {
      base.OnLevelUpEmotion();
      BattleDiceCardModel card = RandomUtil.SelectOne<BattleDiceCardModel>(this.owner.allyCardDetail.GetDeck());
      if (card == null)
        return;
      if (card.GetCost() > 0)
        card.AddCost(-1);
      this.owner.allyCardDetail.AddCardToHand(card);
    }

    public override void OnRollDice(BattleDiceBehavior behavior)
    {
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        power = this.owner.emotionDetail.EmotionLevel / 2
      });
    }

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
      if (!this.owner.emotionDetail.CheckLevelUp())
        return;
      this.owner.cardSlotDetail.SetPlayPoint(this._lightLastScene + this.owner.cardSlotDetail.GetMaxPlayPoint() / 2);
    }

    public override void OnRoundEnd()
    {
      this._lightLastScene = this.owner.PlayPoint;
      if (this.owner.emotionDetail.AllEmotionCoins.Count < this.owner.emotionDetail.MaximumCoinNumber)
        return;
      this.h = true;
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
