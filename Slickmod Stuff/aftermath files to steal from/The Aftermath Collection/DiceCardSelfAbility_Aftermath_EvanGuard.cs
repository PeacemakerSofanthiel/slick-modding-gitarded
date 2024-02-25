// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_EvanGuard
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_EvanGuard : DiceCardSelfAbilityBase
  {
    public override bool OnChooseCard(BattleUnitModel owner)
    {
      return BattleObjectManager.instance.GetAliveList(this.owner.faction).Count > 1;
    }

    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      if (targetUnit == null)
        return;
      unit.bufListDetail.AddReadyBuf((BattleUnitBuf) new DiceCardSelfAbility_Aftermath_EvanGuard.BattleUnitBuf_EvanSelfBuf());
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList())
        alive.bufListDetail.AddReadyBuf((BattleUnitBuf) new DiceCardSelfAbility_Aftermath_EvanGuard.BattleUnitBuf_EvanTarget());
      SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.UpdateCharacterProfileAll();
    }

    public class BattleUnitBuf_EvanSelfBuf : BattleUnitBuf
    {
      public override string keywordId => "Aftermath_EvanProvoke";

      public override float DmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
      {
        return 0.65f;
      }

      public override float BreakDmgFactor(int dmg, DamageType type = DamageType.ETC, KeywordBuf keyword = KeywordBuf.None)
      {
        return 0.65f;
      }

      public override void OnRoundEnd() => this.Destroy();
    }

    public class BattleUnitBuf_EvanTarget : BattleUnitBuf
    {
      public override BattleUnitModel ChangeAttackTarget(BattleDiceCardModel card, int idx)
      {
        return BattleObjectManager.instance.GetAliveList().Find((Predicate<BattleUnitModel>) (x => x.bufListDetail.HasBuf<DiceCardSelfAbility_Aftermath_EvanGuard.BattleUnitBuf_EvanSelfBuf>()));
      }

      public override void OnRoundEnd() => this.Destroy();
    }
  }
}
