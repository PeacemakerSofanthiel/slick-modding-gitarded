﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathEmotionCrashItDoGoDown
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathEmotionCrashItDoGoDown : DiceCardSelfAbilityBase
  {
    public static string Desc = "Can only be used at Emotion Level 3 and above [On Use] Reduce Emotion Level by 1; Draw 3 pages";

    public override void OnUseCard()
    {
      this.owner.allyCardDetail.DrawCards(3);
      this.owner.emotionDetail.SetEmotionLevel(this.owner.emotionDetail.EmotionLevel - 1);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "DrawCard_Keyword",
          "LiuRemnantsOnlyPage_Keyword"
        };
      }
    }
  }
}