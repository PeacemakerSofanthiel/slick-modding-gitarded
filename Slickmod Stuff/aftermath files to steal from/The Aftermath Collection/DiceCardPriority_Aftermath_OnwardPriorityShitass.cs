// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardPriority_Aftermath_OnwardPriorityShitass
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardPriority_Aftermath_OnwardPriorityShitass : DiceCardPriorityBase
  {
    public override int GetPriorityBonus(BattleUnitModel owner)
    {
      if (owner.emotionDetail.EmotionLevel >= 5)
        return 999;
      if (owner.emotionDetail.EmotionLevel >= 4)
        return -1;
      return owner.emotionDetail.EmotionLevel >= 3 ? -2 : -9999;
    }
  }
}
