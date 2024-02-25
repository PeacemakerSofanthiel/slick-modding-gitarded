// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_FamilyFirstS
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_FamilyFirstS : PassiveAbilityBase
  {
    private bool triggered;

    public override void OnRoundStartAfter()
    {
      base.OnRoundStartAfter();
      if (this.owner.emotionDetail.EmotionLevel < 2 || this.triggered)
        return;
      this.owner.allyCardDetail.AddNewCardToDeck(new LorId(AftermathCollectionInitializer.packageId, 60114));
      this.owner.allyCardDetail.AddNewCardToDeck(new LorId(AftermathCollectionInitializer.packageId, 60114));
      this.triggered = true;
    }

    public override void OnRoundStart()
    {
      base.OnRoundStart();
      this.owner.allyCardDetail.DrawCards(5);
      foreach (BattleDiceCardModel battleDiceCardModel in this.owner.allyCardDetail.GetHand())
        battleDiceCardModel.SetCostToZero();
    }

    public override void OnWaveStart()
    {
      base.OnWaveStart();
      this.owner.formation.ChangePos(new Vector2Int(8, 0));
    }
  }
}
