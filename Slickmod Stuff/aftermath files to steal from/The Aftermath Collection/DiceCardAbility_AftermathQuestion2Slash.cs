// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathQuestion2Slash
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathQuestion2Slash : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Inflict 2 ??? next Scene";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      if (target == null)
        return;
      KeywordBuf bufType = RandomUtil.SelectOne<KeywordBuf>(new List<KeywordBuf>()
      {
        KeywordBuf.Burn,
        KeywordBuf.Paralysis,
        KeywordBuf.Vulnerable,
        KeywordBuf.Bleeding
      });
      switch (bufType)
      {
        case KeywordBuf.Burn:
          this.behavior.behaviourInCard.EffectRes = "Liu1_J";
          break;
        case KeywordBuf.Paralysis:
          this.behavior.behaviourInCard.EffectRes = "Bada_J";
          break;
        case KeywordBuf.Bleeding:
          this.behavior.behaviourInCard.EffectRes = "Sword_J";
          break;
        case KeywordBuf.Vulnerable:
          this.behavior.behaviourInCard.EffectRes = "SevenAssociation_J";
          break;
        default:
          this.behavior.behaviourInCard.EffectRes = "Usett_J";
          break;
      }
      target.bufListDetail.AddKeywordBufByCard(bufType, 2, this.owner);
    }
  }
}
