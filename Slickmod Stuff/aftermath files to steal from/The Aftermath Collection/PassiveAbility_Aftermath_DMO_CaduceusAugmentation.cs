// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_DMO_CaduceusAugmentation
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_DMO_CaduceusAugmentation : PassiveAbilityBase
  {
    private readonly List<BehaviourDetail> enduredResists = new List<BehaviourDetail>();
    private readonly BehaviourDetail[] possibleDetails = new BehaviourDetail[3]
    {
      BehaviourDetail.Slash,
      BehaviourDetail.Penetrate,
      BehaviourDetail.Hit
    };

    public override void OnRoundStart()
    {
      this.enduredResists.Clear();
      IOrderedEnumerable<BehaviourDetail> source = ((IEnumerable<BehaviourDetail>) this.possibleDetails).OrderBy<BehaviourDetail, float>((Func<BehaviourDetail, float>) (x => RandomUtil.valueForProb));
      if (this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.WarpCharge) is BattleUnitBuf_warpCharge activatedBuf && activatedBuf.UseStack(2, false))
      {
        this.enduredResists.AddRange(source.Take<BehaviourDetail>(2));
      }
      else
      {
        this.enduredResists.Add(source.First<BehaviourDetail>());
        this.owner.RecoverHP(2);
        this.owner.breakDetail.RecoverBreak(2);
      }
    }

    public override AtkResist GetResistHP(AtkResist origin, BehaviourDetail detail)
    {
      return !this.enduredResists.Contains(detail) ? base.GetResistHP(origin, detail) : AtkResist.Endure;
    }
  }
}
