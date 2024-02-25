// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathFunnyPage
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathFunnyPage : DiceCardAbilityBase
  {
    public static string Desc = "This die and the dice targetting it cannot be destroyed; [On Hit] Inflict 3 Bleed";

    public override bool IsImmuneDestory => true;

    public override void BeforeRollDice_Target(BattleDiceBehavior targetDice)
    {
      base.BeforeRollDice_Target(targetDice);
      targetDice.card.ApplyDiceAbility(DiceMatch.AllDice, (DiceCardAbilityBase) new DiceCardAbility_AftermathFunnyPage.DiceCardAbility_noBreak());
    }

    public override void OnSucceedAttack()
    {
      this.behavior.card?.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Bleeding, 3, this.owner);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Bleed_Keyword" };
    }

    private class DiceCardAbility_noBreak : DiceCardAbilityBase
    {
      public override bool IsImmuneDestory => true;
    }
  }
}
