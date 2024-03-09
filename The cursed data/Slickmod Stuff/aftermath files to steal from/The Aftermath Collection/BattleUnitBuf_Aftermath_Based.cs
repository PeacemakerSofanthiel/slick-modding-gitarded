// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_Aftermath_Based
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_Aftermath_Based : BattleUnitBuf
  {
    public override bool IsImmuneDmg(DamageType type, KeywordBuf keyword = KeywordBuf.None)
    {
      return keyword == KeywordBuf.Decay || base.IsImmuneDmg(type, keyword);
    }

    public override float BreakDmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
    {
      return keyword == KeywordBuf.Decay ? -999f : this.DmgFactor(dmg, type, keyword);
    }

    public override void OnRoundEnd()
    {
      base.OnRoundEnd();
      --this.stack;
      if (this.stack >= 1)
        return;
      this.Destroy();
    }

    public override string keywordId => "Aftermath_Basic";
  }
}
