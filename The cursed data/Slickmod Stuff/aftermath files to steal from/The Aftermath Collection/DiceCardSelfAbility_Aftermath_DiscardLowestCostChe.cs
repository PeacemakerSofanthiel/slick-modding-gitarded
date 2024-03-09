// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_DiscardLowestCostChem
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_DiscardLowestCostChem : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Discard a Chem in with the lowest Cost";

    public override void OnUseCard()
    {
      base.OnUseCard();
      List<BattleDiceCardModel> all = this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => ChemsCardModel.IsChemCard(x)));
      if (!all.Any<BattleDiceCardModel>())
        return;
      all.SortByCost();
      this.owner.allyCardDetail.DiscardACardByAbility(all.First<BattleDiceCardModel>());
    }
  }
}
