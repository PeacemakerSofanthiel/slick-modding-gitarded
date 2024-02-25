// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_AftermathFireMind
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_AftermathFireMind : PassiveAbilityBase
  {
    private int _win;

    public override void OnRoundStart()
    {
      this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Strength, 1, this.owner);
    }

    public override void OnWinParrying(BattleDiceBehavior behavior)
    {
      ++this._win;
      if (this._win % 2 != 0)
        return;
      this.owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Burn, 3, this.owner);
    }

    public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
    {
      if (card.GetID().id == 60202 && card.GetID().packageId == AftermathCollectionInitializer.packageId)
      {
        List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(true);
        aliveList.Remove(this.owner);
        List<int> source = new List<int>();
        foreach (BattleUnitModel battleUnitModel in aliveList)
          source.Add(battleUnitModel.bufListDetail.GetKewordBufAllStack(KeywordBuf.Burn));
        if (aliveList[source.IndexOf(source.Max())] != null)
          return aliveList[source.IndexOf(source.Max())];
      }
      return base.ChangeAttackTarget(card, idx);
    }
  }
}
