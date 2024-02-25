// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Instant4Charge
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Instant4Charge : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Play] Give all allies 4 Charge; if the user has Overcharge, spend all of it to restore 1 light to all allies";

    public override string[] Keywords
    {
      get => new string[1]{ "WarpCharge" };
    }

    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      unit.cardSlotDetail.LosePlayPoint(3);
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList(unit.faction))
        alive.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.WarpCharge, 4);
      if (!unit.bufListDetail.HasBuf<BattleUnitBuf_Aftermath_Overcharge>())
        return;
      BattleUnitBuf_Aftermath_Overcharge aftermathOvercharge = unit.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Overcharge)) as BattleUnitBuf_Aftermath_Overcharge;
      if (aftermathOvercharge.stack <= 0)
        return;
      aftermathOvercharge.UseStack(aftermathOvercharge.stack);
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList(unit.faction))
        alive?.cardSlotDetail.RecoverPlayPointByCard(1);
    }
  }
}
