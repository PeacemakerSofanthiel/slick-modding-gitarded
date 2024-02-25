// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathLetItOutDie
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathLetItOutDie : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash] Activate Inner Flame and purge all of its stacks";

    public override void OnLoseParrying()
    {
      BattleUnitBuf buf = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_InnerFlame));
      if (buf == null)
        return;
      this.owner.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, buf.stack, this.owner);
      this.owner.bufListDetail.RemoveBuf(buf);
      buf.Destroy();
    }

    public override void OnWinParrying()
    {
      BattleUnitBuf buf = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_InnerFlame));
      BattleUnitModel target = this.card.target;
      if (buf == null || target == null)
        return;
      target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, buf.stack, this.owner);
      this.owner.bufListDetail.RemoveBuf(buf);
      buf.Destroy();
    }
  }
}
