// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_NorincoDesperation
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_NorincoDesperation : PassiveAbilityBase
  {
    public override void OnRoundEnd()
    {
      if (this.owner.cardSlotDetail.cardQueue.ToList<BattlePlayingCardDataInUnitModel>().Find((Predicate<BattlePlayingCardDataInUnitModel>) (x => x.card.exhaust)) == null)
        return;
      this.owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Strength, 1, this.owner);
    }
  }
}
