// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BattleUnitBuf_Aftermath_ChemRebound
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BattleUnitBuf_Aftermath_ChemRebound : BattleUnitBuf
  {
    public override int MaxPlayPointAdder() => this.stack;

    public override void OnRollDice(BattleDiceBehavior behavior)
    {
      base.OnRollDice(behavior);
      behavior.ApplyDiceStatBonus(new DiceStatBonus()
      {
        dmg = -Singleton<Random>.Instance.Next(1, this.stack + 3)
      });
    }

    public override string keywordId => "Aftermath_ChemRebound";

    public override string bufActivatedText
    {
      get
      {
        return Singleton<BattleEffectTextsXmlList>.Instance.GetEffectTextDesc(this.keywordId, (object) this.stack, (object) (this.stack + 2));
      }
    }
  }
}
