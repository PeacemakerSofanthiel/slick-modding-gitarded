// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathCamaraderie
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathCamaraderie : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] All of target's allies gain 1 Strength and 1 Endurance this Scene, restore 1 Light and 5 HP, and lose 6 stacks of random status ailments";

    public override void OnUseCard()
    {
      base.OnUseCard();
      if (this.card.target == null)
        return;
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList(this.card.target.faction))
      {
        int num = 6;
        foreach (BattleUnitBuf activatedBuf in alive.bufListDetail.GetActivatedBufList())
        {
          if (activatedBuf.positiveType == BufPositiveType.Negative && num > 0 && activatedBuf.stack >= 1)
          {
            int stack = activatedBuf.stack;
            if (num >= stack)
            {
              num -= stack;
              activatedBuf.Destroy();
            }
            else if (num < stack)
            {
              activatedBuf.stack -= num;
              num = 0;
            }
          }
          if (activatedBuf.stack < 0)
            activatedBuf.Destroy();
        }
        alive.RecoverHP(5);
        alive.cardSlotDetail.RecoverPlayPointByCard(1);
        alive.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Strength, 1);
        alive.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Endurance, 1);
      }
    }

    public override string[] Keywords
    {
      get
      {
        return new string[5]
        {
          "Benito_Only_Keyword",
          "Strength_Keyword",
          "Endurance_Keyword",
          "Energy_Keyword",
          "bstart_Keyword"
        };
      }
    }
  }
}
