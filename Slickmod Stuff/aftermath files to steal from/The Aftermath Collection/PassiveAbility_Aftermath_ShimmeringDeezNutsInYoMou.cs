// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_ShimmeringDeezNutsInYoMouth
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_ShimmeringDeezNutsInYoMouth : PassiveAbilityBase
  {
    public List<LorId> deck = new List<LorId>();

    public override void OnWaveStart()
    {
      base.OnWaveStart();
      this.deck.AddRange(this.owner.allyCardDetail.GetAllDeck().FindAll((Predicate<BattleDiceCardModel>) (x => x != null)).Select<BattleDiceCardModel, LorId>((Func<BattleDiceCardModel, LorId>) (x => x.GetID())));
    }

    public override void OnRoundStart()
    {
      this.owner.allyCardDetail.ExhaustAllCards();
      this.owner.cardSlotDetail.RecoverPlayPoint(this.owner.cardSlotDetail.GetMaxPlayPoint());
      this.deck = this.deck.Shuffle<LorId>();
      int count = this.deck.Count;
      for (int index = 0; index < 6; ++index)
      {
        try
        {
          this.owner.allyCardDetail.AddNewCard(this.deck[count - 1]);
          --count;
        }
        catch (IndexOutOfRangeException ex)
        {
          break;
        }
      }
      foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(Faction.Player).FindAll((Predicate<BattleUnitModel>) (x => x.IsImmune(KeywordBuf.Decay))))
        BattleObjectManager.instance.UnregisterUnitByIndex(Faction.Player, battleUnitModel.index);
    }
  }
}
