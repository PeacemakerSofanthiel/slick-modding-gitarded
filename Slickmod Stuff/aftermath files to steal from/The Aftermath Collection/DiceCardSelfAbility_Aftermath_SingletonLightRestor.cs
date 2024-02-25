// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_SingletonLightRestoreOnHaste
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_SingletonLightRestoreOnHaste : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] If Singleton, restore 1 Light for every 2 Haste on self (Max. 2)";

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "Energy_Keyword",
          "OnlyOne_Keyword",
          "Quickness_Keyword"
        };
      }
    }

    public override void OnUseCard()
    {
      if (!this.owner.allyCardDetail.IsHighlander())
        return;
      BattleUnitBuf activatedBuf = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness);
      if (activatedBuf == null)
        return;
      this.owner.cardSlotDetail.RecoverPlayPoint(Mathf.Min(activatedBuf.stack / 2, 2));
    }
  }
}
