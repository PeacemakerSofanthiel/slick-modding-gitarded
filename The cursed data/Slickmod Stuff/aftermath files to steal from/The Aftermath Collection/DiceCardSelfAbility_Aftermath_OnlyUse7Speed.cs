// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_OnlyUse7Speed
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_OnlyUse7Speed : DiceCardSelfAbilityBase
  {
    public static string Desc = "Can only be used at 7+ Speed";

    public override bool OnChooseCard(BattleUnitModel owner) => owner.GetCurrentSpeed() > 6;
  }
}
