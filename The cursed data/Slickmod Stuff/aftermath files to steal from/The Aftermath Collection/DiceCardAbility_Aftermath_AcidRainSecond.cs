// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_AcidRainSecond
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_AcidRainSecond : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] If target has 4 or more Erosion, inflict 1 Erosion to all enemies";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      if (target == null || target.bufListDetail.GetKewordBufAllStack(KeywordBuf.Decay) < 4)
        return;
      BattleObjectManager.instance.GetAliveList(target.faction).ForEach((Action<BattleUnitModel>) (x => x.bufListDetail.AddKeywordBufByCard(KeywordBuf.Decay, 1, this.owner)));
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_Decay_Keyword" };
    }
  }
}
