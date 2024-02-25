// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_Evan5
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_Evan5 : PassiveAbilityBase
  {
    public static readonly List<int> targetIds = new List<int>()
    {
      60320,
      60321,
      60322,
      60323
    };

    public override void OnRoundStart()
    {
      foreach (int targetId in PassiveAbility_Aftermath_Evan5.targetIds)
        this.owner.personalEgoDetail.RemoveCard(new LorId(AftermathCollectionInitializer.packageId, targetId));
      (this.owner.passiveDetail.PassiveList.Find((Predicate<PassiveAbilityBase>) (x => x is PassiveAbility_Aftermath_Evan5)) as PassiveAbility_Aftermath_Evan5).GivePrescriptPages(this.owner);
    }

    public void AddOne(List<int> list)
    {
      if (list.Count <= 0)
        return;
      int id = RandomUtil.SelectOne<int>(list);
      this.owner.personalEgoDetail.AddCard(new LorId(AftermathCollectionInitializer.packageId, id));
      list.Remove(id);
    }

    public void GivePrescriptPages(BattleUnitModel unit)
    {
      List<int> list = new List<int>((IEnumerable<int>) PassiveAbility_Aftermath_Evan5.targetIds);
      if (this.owner.emotionDetail.EmotionLevel >= 3)
      {
        this.AddOne(list);
        this.AddOne(list);
      }
      else
        this.AddOne(list);
    }
  }
}
