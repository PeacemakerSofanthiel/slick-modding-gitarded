// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_Aftermathdragonfist
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_Aftermathdragonfist : PassiveAbilityBase
  {
    private bool emot;

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      if (behavior.Type != BehaviourType.Atk)
        return;
      BattleUnitModel target = behavior.card.target;
      if (target == null || target.bufListDetail.GetKewordBufAllStack(KeywordBuf.Burn) <= 0)
        return;
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        power = 1
      });
    }

    public override int OnGiveKeywordBufByCard(
      BattleUnitBuf buf,
      int stack,
      BattleUnitModel target)
    {
      return buf.bufType == KeywordBuf.Burn ? 1 : 0;
    }

    public override void OnRoundStart()
    {
      if (this.emot || this.owner.emotionDetail.EmotionLevel < 4)
        return;
      this.emot = true;
      this.owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60217));
    }
  }
}
