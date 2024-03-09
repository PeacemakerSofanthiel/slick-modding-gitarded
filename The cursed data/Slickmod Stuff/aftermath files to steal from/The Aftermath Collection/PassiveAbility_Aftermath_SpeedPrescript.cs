// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_SpeedPrescript
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_SpeedPrescript : PassiveAbilityBase
  {
    public override int SpeedDiceNumAdder()
    {
      return 1 + BattleObjectManager.instance.GetAliveList(this.owner.faction).FindAll((Predicate<BattleUnitModel>) (x => x != this.owner && x.bufListDetail.GetActivatedBuf(KeywordBuf.IndexRelease) != null)).Count;
    }
  }
}
