﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_DelayedLight2
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_DelayedLight2 : BattleUnitBuf
  {
    private int scene = 2;

    public override void OnRoundStart()
    {
      if (this.scene <= 0)
      {
        this.Destroy();
      }
      else
      {
        --this.scene;
        this._owner.cardSlotDetail.RecoverPlayPoint(1);
      }
    }
  }
}
