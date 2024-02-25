// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_Enemy_CausticPlating
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_Enemy_CausticPlating : PassiveAbilityBase
  {
    public override void OnRoundStart()
    {
      base.OnRoundStart();
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList())
      {
        BattleUnitBuf activatedBuf = alive.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
        if (activatedBuf != null && this.owner.emotionDetail.EmotionLevel > activatedBuf.stack)
          activatedBuf.stack = this.owner.emotionDetail.EmotionLevel;
        else
          alive.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Decay, this.owner.emotionDetail.EmotionLevel, this.owner);
      }
    }
  }
}
