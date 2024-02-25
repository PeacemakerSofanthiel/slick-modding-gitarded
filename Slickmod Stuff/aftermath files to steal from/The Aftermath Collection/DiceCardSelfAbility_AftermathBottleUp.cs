// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathBottleUp
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathBottleUp : DiceCardSelfAbilityBase
  {
    public static string Desc = "This page can target allies [On Use] Purge all stacks of Burn from self and target; gain stacks of ‘Inner Flame’ equal to Burn purged this way";

    public override bool IsTargetableAllUnit() => true;

    public override void OnUseCard()
    {
      int num = 0;
      BattleUnitModel target = this.card.target;
      if (target != null)
      {
        BattleUnitBuf battleUnitBuf = target.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x.bufType == KeywordBuf.Burn));
        if (battleUnitBuf != null)
        {
          num += battleUnitBuf.stack;
          target.bufListDetail.RemoveBufAll(KeywordBuf.Burn);
        }
      }
      BattleUnitBuf battleUnitBuf1 = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x.bufType == KeywordBuf.Burn));
      if (battleUnitBuf1 != null)
      {
        num += battleUnitBuf1.stack;
        this.owner.bufListDetail.RemoveBufAll(KeywordBuf.Burn);
      }
      if (num <= 0)
        return;
      BattleUnitBuf battleUnitBuf2 = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_InnerFlame));
      if (battleUnitBuf2 != null)
      {
        battleUnitBuf2.stack += num;
      }
      else
      {
        BattleUnitBufListDetail bufListDetail = this.owner.bufListDetail;
        BattleUnitBuf_Aftermath_InnerFlame buf = new BattleUnitBuf_Aftermath_InnerFlame();
        buf.stack = num;
        bufListDetail.AddBuf((BattleUnitBuf) buf);
      }
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "HunanOnlyPage_Keyword",
          "Burn_Keyword"
        };
      }
    }
  }
}
