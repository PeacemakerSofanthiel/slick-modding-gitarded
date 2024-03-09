// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_Thunderstruck1atk
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_Thunderstruck1atk : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Inflict 1 Thunderstruck next Scene";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      BattleUnitBuf battleUnitBuf = target.bufListDetail.GetReadyBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Thunderstruck));
      if (battleUnitBuf == null)
      {
        BattleUnitBufListDetail bufListDetail = target.bufListDetail;
        BattleUnitBuf_Aftermath_Thunderstruck buf = new BattleUnitBuf_Aftermath_Thunderstruck();
        buf.stack = 1;
        bufListDetail.AddReadyBuf((BattleUnitBuf) buf);
      }
      else
        ++battleUnitBuf.stack;
    }

    public override string[] Keywords
    {
      get
      {
        return new string[1]
        {
          "Aftermath_Thunderstruck_Keyword"
        };
      }
    }
  }
}
