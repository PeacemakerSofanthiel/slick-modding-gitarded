// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_Aftermath_Thunderstruck
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_Aftermath_Thunderstruck : BattleUnitBuf
  {
    public override void BeforeRollDice(BattleDiceBehavior behavior)
    {
      if (this.stack > 10)
        this.stack = 10;
      if (Singleton<Random>.Instance.Next(0, 10) >= this.stack)
        return;
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        power = -2
      });
    }

    public override void OnRoundEnd() => this.Destroy();

    public override BufPositiveType positiveType => BufPositiveType.Negative;

    public override string keywordId => "Aftermath_Thunderstruck";

    public override string bufActivatedText
    {
      get
      {
        return Singleton<BattleEffectTextsXmlList>.Instance.GetEffectTextDesc(this.keywordId, (object) this.stack, (object) (this.stack * 10));
      }
    }
  }
}
