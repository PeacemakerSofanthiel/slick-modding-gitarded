// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_SpendChargeHP
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_SpendChargeHP : DiceCardSelfAbilityBase
  {
    public static string Desc = "Only usable at 10+ Overcharge\n[On Use] Spend all charge to recover 10 HP";

    public override void OnUseCard()
    {
      BattleUnitBuf_Aftermath_Overcharge aftermathOvercharge = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Overcharge)) as BattleUnitBuf_Aftermath_Overcharge;
      if (aftermathOvercharge.stack <= 0)
        return;
      aftermathOvercharge.UseStack(aftermathOvercharge.stack);
      this.card.owner.RecoverHP(10);
    }

    public override bool OnChooseCard(BattleUnitModel owner)
    {
      return owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Overcharge)) is BattleUnitBuf_Aftermath_Overcharge aftermathOvercharge && aftermathOvercharge.stack >= 10;
    }
  }
}
