﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_StoredGoods
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_StoredGoods : PassiveAbilityBase
  {
    public override void Init(BattleUnitModel self)
    {
      base.Init(self);
      BattleUnitBufListDetail bufListDetail = self.bufListDetail;
      BattleUnitBuf_Aftermath_StoredChems buf = new BattleUnitBuf_Aftermath_StoredChems();
      buf.stack = 7;
      bufListDetail.AddBuf((BattleUnitBuf) buf);
    }
  }
}