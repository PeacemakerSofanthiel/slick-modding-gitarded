// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_RunningInThe90s
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_RunningInThe90s : PassiveAbilityBase
  {
    public override void OnWaveStart() => base.OnWaveStart();

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
      base.OnUseCard(curCard);
      this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 2, this.owner);
    }

    public override void OnRollDice(BattleDiceBehavior behavior)
    {
      base.OnRollDice(behavior);
      if (!(behavior.behaviourInCard.ActionScript == "") || !(this.owner.customBook.ClassInfo.GetCharacterSkin() == "Dave") && !(this.owner.customBook.ClassInfo.GetCharacterSkin() == "Dave_player"))
        return;
      behavior.behaviourInCard.ActionScript = "AftermathDaveDawnAction";
    }

    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
      base.OnSucceedAttack(behavior);
      BattleUnitBuf readyBuf = this.owner.bufListDetail.GetReadyBuf(KeywordBuf.Quickness);
      if (readyBuf == null)
        return;
      --readyBuf.stack;
      if (readyBuf.stack >= 1)
        return;
      readyBuf.Destroy();
    }
  }
}
