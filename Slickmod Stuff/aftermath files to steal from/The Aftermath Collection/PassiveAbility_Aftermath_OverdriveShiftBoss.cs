// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_OverdriveShiftBoss
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_XML;
using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_OverdriveShiftBoss : PassiveAbilityBase
  {
    private bool threshold;
    private bool goTopGearGo;

    public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
    {
      if ((double) (this.owner.MaxHp / 4) > (double) this.owner.hp && !this.threshold)
      {
        this.owner.SetHp(this.owner.MaxHp / 4);
        this.threshold = true;
        this.goTopGearGo = true;
      }
      return this.goTopGearGo;
    }

    public override void OnRoundStart()
    {
      base.OnRoundStart();
      if (!this.owner.breakDetail.IsBreakLifeZero() && this.goTopGearGo)
      {
        this.goTopGearGo = false;
        this.owner.bufListDetail.AddBuf((BattleUnitBuf) new PassiveAbility_Aftermath_OverdriveShiftBoss.BattleUnitBuf_Aftermath_TOPGEAREALSHIT());
        AftermathCollectionInitializer.aftermathMapHandler.InitCustomMap<CBLOnePointTwoManager>("CBLOnePointTwo");
        if (this.owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42020102))
          this.owner.view.DisplayDlg(DialogType.SPECIAL_EVENT, "TOP_GEAR_" + Singleton<Random>.Instance.Next(2).ToString());
      }
      if (this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is PassiveAbility_Aftermath_OverdriveShiftBoss.BattleUnitBuf_Aftermath_TOPGEAREALSHIT)) != null)
        AftermathCollectionInitializer.aftermathMapHandler.EnforceMap(1);
      else
        AftermathCollectionInitializer.aftermathMapHandler.EnforceMap();
    }

    public override bool IsImmuneBreakDmg(DamageType type) => this.goTopGearGo;

    public class BattleUnitBuf_Aftermath_TOPGEAREALSHIT : BattleUnitBuf
    {
      public override void OnRoundEnd()
      {
        base.OnRoundEnd();
        this._owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 3, this._owner);
        this._owner.cardSlotDetail.RecoverPlayPoint(2);
        this._owner.allyCardDetail.DrawCards(2);
      }

      public override void OnDie()
      {
        this._owner.view.deadEvent = new BattleUnitView.DeadEvent(AftermathUtilityExtensions.ExplodeOnDeath);
        base.OnDie();
      }

      public override void BeforeRollDice(BattleDiceBehavior behavior)
      {
        base.BeforeRollDice(behavior);
        behavior.ApplyDiceStatBonus(new DiceStatBonus()
        {
          power = 2
        });
      }

      public override bool IsImmuneBreakDmg(DamageType type) => true;

      public override int MaxPlayPointAdder() => 1;

      public override int SpeedDiceNumAdder() => 2;

      public override string keywordId => "Aftermath_TopGearBoss";

      public override string keywordIconId => "Aftermath_TopGear";
    }
  }
}
