// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathAdd3PowerLastDie
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathAdd3PowerLastDie : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Lose] Add +3 Power to last die";

    public override void OnLoseParrying()
    {
      this.owner.currentDiceAction?.AddDiceAdder(DiceMatch.LastDice, 3);
    }
  }
}
