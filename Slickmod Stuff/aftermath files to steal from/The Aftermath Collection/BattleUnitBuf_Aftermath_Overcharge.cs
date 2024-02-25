// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_Aftermath_Overcharge
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_Aftermath_Overcharge : BattleUnitBuf
  {
    public const int maxValue = 10;

    public BattleUnitBuf_Aftermath_Overcharge(int value) => this.stack = value;

    public override string keywordIconId => "Aftermath_Overcharge";

    public override string keywordId => "Aftermath_Dem_Overcharge";

    public BattleUnitBuf_Aftermath_Overcharge() => this.stack = 0;

    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      if (behavior == null || behavior.Type != BehaviourType.Atk)
        return;
      if (Random.Range(0, 100) >= 50)
        this._owner.TakeDamage(this.stack, DamageType.Buf);
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        dmg = this.stack
      });
    }

    public override void OnAddBuf(int addedStack)
    {
      if (addedStack > 0)
        this.stack += addedStack;
      this.stack = Mathf.Clamp(this.stack, 0, 10);
    }

    public override void OnRoundEnd()
    {
      if (this.stack > 0)
        return;
      this.Destroy();
    }

    public bool UseStack(int v)
    {
      if (this.stack < v)
        return false;
      this.stack -= v;
      return true;
    }
  }
}
