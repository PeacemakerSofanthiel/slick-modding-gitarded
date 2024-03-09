// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_StealBasedFromTarget
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_StealBasedFromTarget : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Purge Based from target; gain Based equal to stacks purged";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      if (target == null)
        return;
      BattleUnitBuf battleUnitBuf1 = target.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Based));
      if (battleUnitBuf1 == null)
        return;
      BattleUnitBuf battleUnitBuf2 = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Based));
      if (battleUnitBuf2 == null)
      {
        BattleUnitBufListDetail bufListDetail = this.owner.bufListDetail;
        BattleUnitBuf_Aftermath_Based buf = new BattleUnitBuf_Aftermath_Based();
        buf.stack = battleUnitBuf1.stack;
        bufListDetail.AddBuf((BattleUnitBuf) buf);
      }
      else
        battleUnitBuf2.stack += battleUnitBuf1.stack;
      battleUnitBuf1.Destroy();
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Basic" };
    }
  }
}
