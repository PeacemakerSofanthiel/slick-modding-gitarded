// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Instant4ChargeEnemy
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Instant4ChargeEnemy : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Play] Give all allies 4 Charge";

    public override string[] Keywords
    {
      get => new string[1]{ "WarpCharge" };
    }

    public override void OnRoundStart_inHand(BattleUnitModel unit, BattleDiceCardModel self)
    {
      base.OnRoundStart_inHand(unit, self);
      if (unit.faction > Faction.Enemy || unit.RollSpeedDice().FindAll((Predicate<SpeedDice>) (x => !x.breaked)).Count <= 0 || unit.IsBreakLifeZero() || unit.cardSlotDetail.PlayPoint < 3)
        return;
      unit.cardSlotDetail.LosePlayPoint(3);
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList(unit.faction))
        alive.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 4);
    }
  }
}
