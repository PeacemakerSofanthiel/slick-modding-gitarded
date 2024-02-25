// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_ShroomLegacy
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_ShroomLegacy : PassiveAbilityBase
  {
    private int delay;

    public override void OnRoundEnd()
    {
      --this.delay;
      if (this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.CheckForKeyword("Venom_Keyword"))).Count <= 5 || this.delay > 0)
        return;
      this.owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60129));
      this.delay = 3;
    }
  }
}
