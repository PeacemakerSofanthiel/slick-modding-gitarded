// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_Evan1
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_Evan1 : PassiveAbilityBase
  {
    private List<LorId> _cardIdList = new List<LorId>();

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
      LorId id = curCard.card.GetID();
      if (!this._cardIdList.Contains(id))
        this._cardIdList.Add(id);
      if (this._cardIdList.Count < 4)
        return;
      this._cardIdList.Clear();
      int v = (int) ((double) this.owner.MaxHp * 0.10000000149011612);
      if (v > 8)
        v = 8;
      this.owner.RecoverHP(v);
    }

    public override void OnRoundStart()
    {
      if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) == null)
        return;
      BattleUnitBuf_Aftermath_EvanShield.GainBuf(this.owner, 1);
    }
  }
}
