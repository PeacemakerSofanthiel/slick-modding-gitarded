// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathLiuOnward
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathLiuOnward : DiceCardSelfAbilityBase
  {
    public static string Desc = "Only usable at Emotion Level 3 or above; The Cost of this page equals the user's current Light at the start of the Scene [On Use] Set Emotion Level to 0; dice on this page gain Power equal to the amount of Emotion levels lost; Gains additional effects depending on the amount of Emotion Levels lost";

    public override int GetCostLast(BattleUnitModel unit, BattleDiceCardModel self, int oldCost)
    {
      return unit.cardSlotDetail.PlayPoint;
    }

    public override void OnUseCard()
    {
      this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus()
      {
        power = this.owner.emotionDetail.EmotionLevel
      });
      switch (this.owner.emotionDetail.EmotionLevel)
      {
        case 3:
          this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 2, this.owner);
          this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 2, this.owner);
          break;
        case 4:
          this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 3, this.owner);
          this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 1, this.owner);
          this.owner.allyCardDetail.DrawCards(1);
          break;
        default:
          if (this.owner.emotionDetail.EmotionLevel >= 5)
          {
            this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, 2, this.owner);
            this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Endurance, 2, this.owner);
            PassiveAbilityBase passive = this.owner.passiveDetail.PassiveList.Find((Predicate<PassiveAbilityBase>) (x => x is PassiveAbility_Aftermath_Aftermathstarvation));
            if (passive != null)
            {
              this.owner.passiveDetail.DestroyPassive(passive);
              this.owner.passiveDetail.AddPassive(new LorId(AftermathCollectionInitializer.packageId, 60209));
              break;
            }
            break;
          }
          break;
      }
      this.owner.emotionDetail.SetEmotionLevel(0);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[5]
        {
          "LiuRemnantsOnlyPage_Keyword",
          "OnwardEffect1",
          "OnwardEffect2",
          "OnwardEffect3",
          "Burn_Keyword"
        };
      }
    }
  }
}
