// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_DiplomatExpert
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_DiplomatExpert : PassiveAbilityBase
  {
    public override void OnRoundStart()
    {
      base.OnRoundStart();
      List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(this.owner.faction);
      aliveList.Remove(this.owner);
      RandomUtil.SelectOne<BattleUnitModel>(aliveList).bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, this.owner);
      RandomUtil.SelectOne<BattleUnitModel>(BattleObjectManager.instance.GetAliveList_opponent(this.owner.faction)).bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Weak, 1, this.owner);
    }
  }
}
