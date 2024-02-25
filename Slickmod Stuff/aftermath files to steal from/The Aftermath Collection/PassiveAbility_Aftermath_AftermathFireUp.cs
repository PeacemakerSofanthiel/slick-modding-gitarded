// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_AftermathFireUp
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_AftermathFireUp : PassiveAbilityBase
  {
    private int _emotionLevel;

    public override void OnRoundStart()
    {
      if (this._emotionLevel == this.owner.emotionDetail.EmotionLevel)
        return;
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new PassiveAbility_Aftermath_AftermathFireUp.BattleUnitBuf_FireUpGaming());
      this._emotionLevel = this.owner.emotionDetail.EmotionLevel;
    }

    public class BattleUnitBuf_FireUpGaming : BattleUnitBuf
    {
      public override int OnGiveKeywordBufByCard(
        BattleUnitBuf cardBuf,
        int stack,
        BattleUnitModel target)
      {
        return cardBuf.bufType == KeywordBuf.Burn ? 1 : 0;
      }

      public override void OnRoundEnd()
      {
        this._owner.bufListDetail.RemoveBuf((BattleUnitBuf) this);
        this.Destroy();
      }
    }
  }
}
