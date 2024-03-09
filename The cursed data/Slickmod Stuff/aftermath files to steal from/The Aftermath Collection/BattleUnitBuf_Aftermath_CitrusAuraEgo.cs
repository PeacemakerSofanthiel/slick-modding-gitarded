// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_Aftermath_CitrusAuraEgo
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_Aftermath_CitrusAuraEgo : BattleUnitBuf
  {
    public override void OnRoundStart()
    {
      base.OnRoundStart();
      foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList().FindAll((Predicate<BattleUnitModel>) (x => !x.bufListDetail.GetActivatedBufList().Any<BattleUnitBuf>((Func<BattleUnitBuf, bool>) (y => y is BattleUnitBuf_Aftermath_CitrusAuraEgo.BattleUnitBuf_Aftermath_CitrusAuraEgoHidden)))))
        battleUnitModel.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_CitrusAuraEgo.BattleUnitBuf_Aftermath_CitrusAuraEgoHidden());
    }

    public override string keywordId => "Aftermath_CitrusAuraEgo";

    public class BattleUnitBuf_Aftermath_CitrusAuraEgoHidden : BattleUnitBuf
    {
      public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
      {
        return keyword == KeywordBuf.Decay ? 2f : base.DmgFactor(dmg, type, keyword);
      }

      public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
      {
        base.OnTakeDamageByAttack(atkDice, dmg);
        BattleUnitBuf activatedBuf = this._owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
        if (activatedBuf == null)
          return;
        activatedBuf.stack /= 2;
      }

      public override void OnRoundEndTheLast()
      {
        BattleUnitBuf activatedBuf = this._owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
        if (activatedBuf != null)
          activatedBuf.stack /= 2;
        base.OnRoundEndTheLast();
      }
    }
  }
}
