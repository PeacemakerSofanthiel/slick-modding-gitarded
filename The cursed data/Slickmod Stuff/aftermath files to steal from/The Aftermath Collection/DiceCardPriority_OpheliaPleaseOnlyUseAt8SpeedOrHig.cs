﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardPriority_OpheliaPleaseOnlyUseAt8SpeedOrHigher
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardPriority_OpheliaPleaseOnlyUseAt8SpeedOrHigher : DiceCardPriorityBase
  {
    public override int GetPriorityBonus(BattleUnitModel owner, int speed) => speed >= 8 ? 50 : -99;
  }
}