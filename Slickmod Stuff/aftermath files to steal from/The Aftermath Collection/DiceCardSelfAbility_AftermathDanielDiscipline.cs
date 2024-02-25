// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathDanielDiscipline
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathDanielDiscipline : DiceCardSelfAbilityBase
  {
    public static string Desc = "Dice on this page and the one clashing with it are unaffected by Power gain or loss";

    public override void OnUseCard() => this.card.ignorePower = true;

    public override void OnStartParrying()
    {
      BattleUnitModel target = this.card.target;
      if (target == null || target.currentDiceAction == null)
        return;
      target.currentDiceAction.ignorePower = true;
    }

    public override string[] Keywords
    {
      get
      {
        return new string[1]
        {
          "Discipline_onlypage_daniel_Keyword"
        };
      }
    }
  }
}
