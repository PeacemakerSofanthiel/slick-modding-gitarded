// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Evan9
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Evan9 : DiceCardSelfAbilityBase
  {
    public override string[] Keywords
    {
      get
      {
        return new string[1]
        {
          "Aftermath_onlypage_evan_Keyword"
        };
      }
    }

    public override bool OnChooseCard(BattleUnitModel owner)
    {
      return owner.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null;
    }

    public override void OnUseCard()
    {
      this.card.ApplyDiceStatBonus(DiceMatch.AllDice, new DiceStatBonus()
      {
        power = Mathf.Min(5, Mathf.RoundToInt((float) (((double) this.owner.MaxHp - (double) this.owner.hp) / 10.0)))
      });
    }
  }
}
