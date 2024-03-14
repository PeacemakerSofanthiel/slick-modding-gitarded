﻿// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_Gain1StaggerProtectionOnHit
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_Gain1StaggerProtectionOnHit : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Gain 1 Stagger Protection next Scene";

    public override void OnSucceedAttack(BattleUnitModel target)
    {
      base.OnSucceedAttack(target);
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.BreakProtection, 1, this.owner);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "BreakProtection_Keyword" };
    }
  }
}