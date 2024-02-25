// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathRandomize2
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathRandomize2 : DiceCardAbilityBase
  {
    public static string Desc = "This die's type is randomized [On Hit] Inflict 2 ??? next Scene";

    public override void BeforeRollDice()
    {
      base.BeforeRollDice();
      this.behavior.behaviourInCard.Detail = RandomUtil.SelectOne<BehaviourDetail>(BehaviourDetail.Slash, BehaviourDetail.Penetrate, BehaviourDetail.Hit);
    }

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
          this.behavior.behaviourInCard.EffectRes = "Liu1_Z";
          break;
        case KeywordBuf.Paralysis:
          this.behavior.behaviourInCard.EffectRes = "Bada_Z";
          break;
        case KeywordBuf.Bleeding:
          this.behavior.behaviourInCard.EffectRes = "Sword_Z";
          break;
        case KeywordBuf.Vulnerable:
          this.behavior.behaviourInCard.EffectRes = "SevenAssociation_Z";
          break;
        default:
          this.behavior.behaviourInCard.EffectRes = "Usett_Z";
          break;
      }
      target.bufListDetail.AddKeywordBufByCard(bufType, 2, this.owner);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Random_Keyword" };
    }
  }
}
