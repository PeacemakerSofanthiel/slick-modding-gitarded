// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_TheLimeLime
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_TheLimeLime : PassiveAbilityBase
  {
    private bool isitthreyet;

    public override void OnWaveStart()
    {
      base.OnWaveStart();
      if (LibraryModel.Instance.GetEquipedBookList().FindAll((Predicate<BookModel>) (x => x != null && x.BookId == new LorId(AftermathCollectionInitializer.packageId, 300))).Count <= 1)
        return;
      this.owner.view.deadEvent = new BattleUnitView.DeadEvent(AftermathUtilityExtensions.ExplodeOnDeath);
      this.owner.Die();
    }

    public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
    {
      base.OnUseCard(curCard);
      if (curCard.card.GetRarity() <= Rarity.Common && !curCard.card.XmlData.optionList.Contains(CardOption.EgoPersonal) && !curCard.card.XmlData.optionList.Contains(CardOption.EGO))
        return;
      this.owner.view.deadEvent = new BattleUnitView.DeadEvent(AftermathUtilityExtensions.ExplodeOnDeath);
      this.owner.Die();
    }

    public override void OnLevelUpEmotion()
    {
      base.OnLevelUpEmotion();
      if (this.owner.emotionDetail.EmotionLevel < 4 || this.isitthreyet)
        return;
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_CitrusAuraEgo());
      this.owner.personalEgoDetail.AddCard(new LorId(AftermathCollectionInitializer.packageId, 301));
      this.owner.personalEgoDetail.AddCard(new LorId(AftermathCollectionInitializer.packageId, 302));
      this.isitthreyet = true;
    }
  }
}
