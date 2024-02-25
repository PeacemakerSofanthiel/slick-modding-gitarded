// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_AftermathBurnProtection
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_AftermathBurnProtection : BattleUnitBuf
  {
    public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
    {
      if (keyword != KeywordBuf.Burn)
        return base.DmgFactor(dmg, type, keyword);
      return this.stack >= 10 ? 0.0f : (float) (1.0 - (double) this.stack / 10.0);
    }

    public override void Init(BattleUnitModel owner)
    {
      base.Init(owner);
      if (this.stack <= 11)
        return;
      this.stack = 11;
    }

    public override void OnRoundEndTheLast()
    {
      --this.stack;
      if (this.stack > 0)
        return;
      this.Destroy();
    }

    public override BufPositiveType positiveType => BufPositiveType.Positive;

    public override string keywordId => "Aftermath_BurnProtection";

    public override string bufActivatedText
    {
      get
      {
        return Singleton<BattleEffectTextsXmlList>.Instance.GetEffectTextDesc(this.keywordId, (object) this.stack, (object) (this.stack * 10));
      }
    }
  }
}
