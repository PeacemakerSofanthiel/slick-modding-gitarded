// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_AftermathScorchBody
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_AftermathScorchBody : PassiveAbilityBase
  {
    public override void OnWinParrying(BattleDiceBehavior behavior)
    {
      BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_AftermathBurnProtection));
      if (battleUnitBuf != null)
      {
        ++battleUnitBuf.stack;
        battleUnitBuf.Init(this.owner);
      }
      else
      {
        BattleUnitBufListDetail bufListDetail = this.owner.bufListDetail;
        BattleUnitBuf_AftermathBurnProtection buf = new BattleUnitBuf_AftermathBurnProtection();
        buf.stack = 1;
        bufListDetail.AddBuf((BattleUnitBuf) buf);
      }
    }

    public override void OnWaveStart()
    {
      BattleUnitBufListDetail bufListDetail = this.owner.bufListDetail;
      BattleUnitBuf_AftermathBurnProtection buf = new BattleUnitBuf_AftermathBurnProtection();
      buf.stack = 5;
      bufListDetail.AddBuf((BattleUnitBuf) buf);
    }
  }
}
