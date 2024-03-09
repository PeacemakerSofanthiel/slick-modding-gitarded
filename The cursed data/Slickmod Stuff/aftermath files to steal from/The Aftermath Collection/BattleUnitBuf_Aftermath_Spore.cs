// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_Aftermath_Spore
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_Aftermath_Spore : BattleUnitBuf
  {
    public BattleUnitBuf_Aftermath_Spore(int value) => this.stack = value;

    public override void OnRoundStart()
    {
      base.OnRoundStart();
      this._owner.TakeDamage(this.stack, DamageType.Buf);
      this.stack /= 2;
      if (this.stack > 0)
        return;
      this.Destroy();
    }

    public override void OnRollDice(BattleDiceBehavior behavior)
    {
      base.OnRollDice(behavior);
      if (!this.IsAttackDice(behavior.Detail))
        return;
      this._owner.TakeDamage(this.stack, DamageType.Buf);
    }

    public override BufPositiveType positiveType => BufPositiveType.Negative;

    public override string keywordId => "Aftermath_Spore_Keyword";
  }
}
