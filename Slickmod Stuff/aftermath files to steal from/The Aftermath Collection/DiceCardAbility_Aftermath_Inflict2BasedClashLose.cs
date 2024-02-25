// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_Inflict2BasedClashLose
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_Inflict2BasedClashLose : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Lose] Give 2 Based to target";

    public override void OnLoseParrying()
    {
      base.OnLoseParrying();
      BattleUnitModel target = this.behavior.card.target;
      if (target == null)
        return;
      BattleUnitBuf battleUnitBuf = target.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Based));
      if (battleUnitBuf == null)
      {
        BattleUnitBufListDetail bufListDetail = target.bufListDetail;
        BattleUnitBuf_Aftermath_Based buf = new BattleUnitBuf_Aftermath_Based();
        buf.stack = 2;
        bufListDetail.AddBuf((BattleUnitBuf) buf);
      }
      else
        battleUnitBuf.stack += 2;
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Basic" };
    }
  }
}
