// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.ChemsCardModel
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public static class ChemsCardModel
  {
    public static void AddChemToHand(BattleUnitModel bruh)
    {
      LorId cardId;
      if (bruh.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 20101))).Count > 0)
        cardId = RandomUtil.SelectOne<LorId>(new List<LorId>()
        {
          new LorId(AftermathCollectionInitializer.packageId, 20102),
          new LorId(AftermathCollectionInitializer.packageId, 20103),
          new LorId(AftermathCollectionInitializer.packageId, 20100)
        });
      else
        cardId = RandomUtil.SelectOne<LorId>(new List<LorId>()
        {
          new LorId(AftermathCollectionInitializer.packageId, 20101),
          new LorId(AftermathCollectionInitializer.packageId, 20102),
          new LorId(AftermathCollectionInitializer.packageId, 20103),
          new LorId(AftermathCollectionInitializer.packageId, 20100)
        });
      bruh.allyCardDetail.AddNewCard(cardId);
    }

    public static bool IsChemCard(BattleDiceCardModel card)
    {
      return card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 20101) || card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 20102) || card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 20103) || card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 20100);
    }
  }
}
