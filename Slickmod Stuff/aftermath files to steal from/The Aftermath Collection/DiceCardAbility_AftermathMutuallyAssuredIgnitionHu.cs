// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathMutuallyAssuredIgnitionHunanDie
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathMutuallyAssuredIgnitionHunanDie : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Inflict 1 + (Personal Emotion Level) amount of Burn to each other";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      if (target == null)
        return;
      int stack = 1 + this.owner.emotionDetail.EmotionLevel;
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, stack, this.owner);
      target.bufListDetail.AddKeywordBufByCard(KeywordBuf.Burn, stack, this.owner);
    }
  }
}
