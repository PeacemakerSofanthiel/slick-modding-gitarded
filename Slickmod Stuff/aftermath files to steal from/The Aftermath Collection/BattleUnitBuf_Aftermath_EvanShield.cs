// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_Aftermath_EvanShield
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_Aftermath_EvanShield : BattleUnitBuf
  {
    public override BufPositiveType positiveType => BufPositiveType.Positive;

    public override string keywordId => "Aftermath_EvanShield";

    public BattleUnitBuf_Aftermath_EvanShield(BattleUnitModel model)
    {
      this._owner = model;
      this.stack = 0;
    }

    public static void GainBuf(BattleUnitModel model, int add)
    {
      if (!(model.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_EvanShield && !x.IsDestroyed())) is BattleUnitBuf_Aftermath_EvanShield aftermathEvanShield1))
      {
        BattleUnitBuf_Aftermath_EvanShield aftermathEvanShield = new BattleUnitBuf_Aftermath_EvanShield(model);
        model.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_EvanShield(model));
        (model.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_EvanShield)) as BattleUnitBuf_Aftermath_EvanShield).Add(add);
      }
      else
        aftermathEvanShield1.Add(add);
    }

    public static void GainReadyBuf(BattleUnitModel model, int add)
    {
      if (!(model.bufListDetail.GetReadyBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_EvanShield && !x.IsDestroyed())) is BattleUnitBuf_Aftermath_EvanShield aftermathEvanShield1))
      {
        BattleUnitBuf_Aftermath_EvanShield aftermathEvanShield = new BattleUnitBuf_Aftermath_EvanShield(model);
        model.bufListDetail.AddReadyBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_EvanShield(model));
        (model.bufListDetail.GetReadyBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_EvanShield)) as BattleUnitBuf_Aftermath_EvanShield).Add(add);
      }
      else
        aftermathEvanShield1.Add(add);
    }

    public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
    {
      if ((double) this.stack < 1.0)
        return base.DmgFactor(dmg, type, keyword);
      int v = (int) ((double) this._owner.MaxHp * 0.10000000149011612);
      if (v > 8)
        v = 8;
      this._owner.RecoverHP(v);
      this.Add(-1);
      return 0.25f;
    }

    public void Add(int add)
    {
      this.stack += add;
      if (this.stack < 1)
        this.Destroy();
      if (this.stack < 3)
        return;
      this.stack = 3;
    }

    public override void AfterDiceAction(BattleDiceBehavior behavior)
    {
      if (!this.IsDestroyed())
        return;
      this._owner.bufListDetail.RemoveBuf((BattleUnitBuf) this);
    }

    public override void OnRoundEnd()
    {
      if (this.IsDestroyed())
      {
        this._owner.bufListDetail.RemoveBuf((BattleUnitBuf) this);
      }
      else
      {
        BattleUnitBuf battleUnitBuf = this._owner.bufListDetail.GetReadyBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_EvanShield));
        if (battleUnitBuf == null || battleUnitBuf.stack <= 0)
          return;
        battleUnitBuf.Destroy();
      }
    }
  }
}
