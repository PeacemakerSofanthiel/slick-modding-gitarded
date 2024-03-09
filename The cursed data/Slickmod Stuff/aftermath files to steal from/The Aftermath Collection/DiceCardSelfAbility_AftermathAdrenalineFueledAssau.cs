// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathAdrenalineFueledAssault
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathAdrenalineFueledAssault : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] At the end of the Scene, gain 1 Protection next Scene for every Positive Emotion Point gained this Scene; gain 1 Damage Up next Scene for every Negative Emotion Point gained this Scene";

    public override void OnUseCard()
    {
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_AftermathAdrenalineFueledAssault.BattleUnitBuf_GiveDmgOrProtSceneEnd());
    }

    public override string[] Keywords
    {
      get
      {
        return new string[4]
        {
          "Protection_Keyword",
          "Energy_Keyword",
          "Burn_Keyword",
          "LiuRemnantsOnlyPage_Keyword"
        };
      }
    }

    public class BattleUnitBuf_GiveDmgOrProtSceneEnd : BattleUnitBuf
    {
      public override void OnRoundEnd()
      {
        foreach (EmotionCoin allEmotionCoin in (IEnumerable<EmotionCoin>) this._owner.emotionDetail.AllEmotionCoins)
        {
          switch (allEmotionCoin.CoinType)
          {
            case EmotionCoinType.Positive:
              this._owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Protection, 1, this._owner);
              continue;
            case EmotionCoinType.Negative:
              this._owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.DmgUp, 1, this._owner);
              continue;
            default:
              continue;
          }
        }
        this.Destroy();
      }
    }
  }
}
