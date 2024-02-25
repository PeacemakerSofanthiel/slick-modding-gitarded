// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_AftermathVerDragon
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_AftermathVerDragon : PassiveAbilityBase
  {
    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
      BattleUnitModel target = behavior.card.target;
      if (target == null)
        return;
      int emotionLevel = this.owner.emotionDetail.EmotionLevel;
      if (emotionLevel >= 3)
      {
        target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 3, this.owner);
      }
      else
      {
        if (emotionLevel <= 0)
          return;
        target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, emotionLevel, this.owner);
      }
    }

    public override void OnRoundStart()
    {
      base.OnRoundStart();
      if (this.owner.faction != Faction.Enemy)
        return;
      this.owner.cardSlotDetail.RecoverPlayPoint(1);
      this.owner.allyCardDetail.DrawCards(2);
    }
  }
}
