// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_Ophe_2
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_Ophe_2 : PassiveAbilityBase
  {
    private bool first = true;

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
      if (!this.first)
        return;
      if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null)
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 4, this.owner);
      else
        this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Quickness, 2, this.owner);
      this.first = false;
    }

    public override void OnRoundEnd()
    {
      base.OnRoundEnd();
      this.first = true;
      if (this.owner.bufListDetail.GetKewordBufStack(KeywordBuf.Quickness) > 0)
        return;
      this.owner.cardSlotDetail.RecoverPlayPoint(1);
      this.owner.allyCardDetail.DrawCards(1);
    }
  }
}
