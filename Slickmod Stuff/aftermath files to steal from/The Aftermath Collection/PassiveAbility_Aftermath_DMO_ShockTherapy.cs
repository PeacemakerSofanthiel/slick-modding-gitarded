// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_DMO_ShockTherapy
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_DMO_ShockTherapy : PassiveAbilityBase
  {
    public static string Desc = "At the start of each Scene, give 1 Charge to 2 random other allies.";

    public override void OnRoundStart()
    {
      List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(this.owner.faction);
      for (int index = 2; aliveList.Count > 0 && index > 0; --index)
      {
        BattleUnitModel battleUnitModel = RandomUtil.SelectOne<BattleUnitModel>(aliveList);
        aliveList.Remove(battleUnitModel);
        battleUnitModel.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.WarpCharge, 1);
      }
    }
  }
}
