﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_ColorChunZeroBurn7
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_ColorChunZeroBurn7 : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Inflict 1 Burn 7 times; lose 7 stacks of Burn";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      if (target == null)
        return;
      for (int index = 0; index < 7; ++index)
        target.bufListDetail.AddKeywordBufThisRoundByCard(KeywordBuf.Burn, 1, this.owner);
      BattleUnitBuf activatedBuf = target.bufListDetail.GetActivatedBuf(KeywordBuf.Burn);
      if (activatedBuf.stack >= 7)
        activatedBuf.stack -= 7;
      else
        activatedBuf.Destroy();
    }
  }
}