// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathBladeThesis
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathBladeThesis : DiceCardSelfAbilityBase
  {
    public static string Desc = "If Blade Thesis is active, dice on this page roll their maximum value; if not, dice on this page roll their minimum value.";

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      base.BeforeRollDice(behavior);
      if (this.owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>())
        behavior.behaviourInCard.Min = behavior.GetDiceVanillaMax();
      else
        behavior.behaviourInCard.Dice = behavior.GetDiceVanillaMin();
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Silvio_Only_Keyword" };
    }
  }
}
