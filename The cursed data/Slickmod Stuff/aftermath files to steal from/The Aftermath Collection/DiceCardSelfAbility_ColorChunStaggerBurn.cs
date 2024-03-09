// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_ColorChunStaggerBurn
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_ColorChunStaggerBurn : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Offensive dice on this page deal bonus stagger damage equivalent to half of the amount of Burn on target";

    public override void OnUseCard()
    {
      BattleUnitModel target = this.card.target;
      if (target == null)
        return;
      int num = target.bufListDetail.GetKewordBufAllStack(KeywordBuf.Burn) / 2;
      if (num <= 0)
        return;
      this.card.ApplyDiceStatBonus(DiceMatch.AllAttackDice, new DiceStatBonus()
      {
        breakDmg = num
      });
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "ColorChunOnlyPage_Keyword",
          "Burn_Keyword"
        };
      }
    }
  }
}
