﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardPriority_Aftermath_HyperGuardDontFuckingUseWithoutOvercharge
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardPriority_Aftermath_HyperGuardDontFuckingUseWithoutOvercharge : 
    DiceCardPriorityBase
  {
    public override int GetPriorityBonus(BattleUnitModel owner)
    {
      BattleUnitBuf battleUnitBuf = owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Overcharge));
      return battleUnitBuf != null && battleUnitBuf.stack > 0 ? 25 : -10;
    }
  }
}