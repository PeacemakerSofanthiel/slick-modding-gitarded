// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Evan6
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Evan6 : DiceCardSelfAbilityBase
  {
    public override void OnUseCard()
    {
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.Strength, Mathf.Min(3, Mathf.RoundToInt((float) (((double) this.owner.MaxHp - (double) this.owner.hp) / 15.0))), this.owner);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Strength_Keyword" };
    }
  }
}
