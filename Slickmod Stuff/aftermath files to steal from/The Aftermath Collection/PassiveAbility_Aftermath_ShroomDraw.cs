// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_ShroomDraw
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_ShroomDraw : PassiveAbilityBase
  {
    public override void OnRoundEnd()
    {
      base.OnRoundEnd();
      if (Singleton<StageController>.Instance.RoundTurn % 2 == 0)
        VenomCardModel.AddVenomToHand(this.owner);
      if (this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.CheckForKeyword("Venom_Keyword"))).Count < 8)
        return;
      foreach (BattleDiceCardModel card in this.owner.allyCardDetail.GetHand().FindAll((Predicate<BattleDiceCardModel>) (x => x.CheckForKeyword("Venom_Keyword"))))
        this.owner.allyCardDetail.DiscardACardByAbility(card);
      this.owner.allyCardDetail.DrawCards(4);
    }
  }
}
