// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_AftermathFighterBeyond
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_AftermathFighterBeyond : PassiveAbilityBase
  {
    private int str;
    private int end;

    public override void OnLevelUpEmotion()
    {
      this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, this.owner);
      this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Endurance, 1, this.owner);
      ++this.str;
      ++this.end;
    }

    public override void OnRoundStart()
    {
      if (this.owner.emotionDetail.EmotionLevel >= 5)
      {
        if (this.str < 2)
          this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 2 - this.str, this.owner);
        if (this.end >= 2)
          return;
        this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 2 - this.end, this.owner);
      }
      else
      {
        if (this.owner.emotionDetail.EmotionLevel < 3)
          return;
        if (this.str < 1)
          this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, this.owner);
        if (this.end >= 1)
          return;
        this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Endurance, 1, this.owner);
      }
    }

    public override void OnRoundEnd()
    {
      this.end = 0;
      this.str = 0;
    }
  }
}
