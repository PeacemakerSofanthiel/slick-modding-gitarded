// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_ExhaustSelfOnClashLose
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_ExhaustSelfOnClashLose : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Lose] Exhaust this page";

    public override void OnLoseParrying()
    {
      base.OnLoseParrying();
      BattlePlayingCardDataInUnitModel card = this.behavior.card;
      if (card == null)
        return;
      card.card.exhaust = true;
    }
  }
}
