// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathFreshMeat
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathFreshMeat : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] All offensive dice on this page gain '[On Hit] Recover 2 HP'";

    public override void OnUseCard()
    {
      this.card.ApplyDiceAbility(DiceMatch.AllAttackDice, (DiceCardAbilityBase) new DiceCardAbility_recoverHp2atk());
    }

    public override string[] Keywords
    {
      get => new string[1]{ "bstart_Keyword" };
    }
  }
}
