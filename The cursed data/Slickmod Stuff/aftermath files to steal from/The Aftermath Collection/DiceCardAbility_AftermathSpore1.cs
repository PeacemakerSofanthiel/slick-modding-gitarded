// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathSpore1
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathSpore1 : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Inflict 1 Spore next Scene";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      this.OnSucceedAttack();
      BattleUnitBuf battleUnitBuf = target.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Spore));
      if (battleUnitBuf != null)
        ++battleUnitBuf.stack;
      else
        target.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_Aftermath_Spore(1));
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Spore_Keyword" };
    }
  }
}
