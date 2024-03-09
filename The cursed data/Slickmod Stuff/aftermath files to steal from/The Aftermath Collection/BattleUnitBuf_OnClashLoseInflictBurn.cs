// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_OnClashLoseInflictBurn
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_OnClashLoseInflictBurn : BattleUnitBuf
  {
    public override void OnLoseParrying(BattleDiceBehavior behavior)
    {
      BattleUnitModel target = behavior.card.target;
      if (target == null)
        return;
      this._owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 1, this._owner);
      target.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 1, this._owner);
    }

    public override void OnRoundEnd() => this.Destroy();
  }
}
