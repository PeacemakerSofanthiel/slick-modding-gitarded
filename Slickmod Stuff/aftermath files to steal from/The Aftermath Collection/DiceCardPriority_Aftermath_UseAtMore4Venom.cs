// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardPriority_Aftermath_UseAtMore4Venom
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardPriority_Aftermath_UseAtMore4Venom : DiceCardPriorityBase
  {
    public override int GetPriorityBonus(BattleUnitModel owner)
    {
      return owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.CheckForKeyword("Venom_Keyword"))).Count >= 4 && owner.cardSlotDetail.cardQueue.ToList<BattlePlayingCardDataInUnitModel>().FindAll((Predicate<BattlePlayingCardDataInUnitModel>) (x => x.card.CheckForKeyword("Venom_Use_Keyword"))).Count < 2 ? 8 : -9;
    }
  }
}
