// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_OverdriveShift
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_OverdriveShift : PassiveAbilityBase
  {
    private bool threshold;

    public override void OnDie()
    {
      if (!(this.owner.customBook.ClassInfo.GetCharacterSkin() == "Dave_player"))
        return;
      this.owner.view.deadEvent = new BattleUnitView.DeadEvent(AftermathUtilityExtensions.ExplodeOnDeath);
      base.OnDie();
    }

    public override void OnTakeDamageByAttack(BattleDiceBehavior atkDice, int dmg)
    {
      base.OnTakeDamageByAttack(atkDice, dmg);
      if ((double) (this.owner.MaxHp / 4) <= (double) this.owner.hp || this.threshold)
        return;
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new PassiveAbility_Aftermath_OverdriveShift.BattleUnitBuf_Aftermath_WELCUMTOBOTTMGEARMATES());
      this.threshold = true;
      AftermathCollectionInitializer.aftermathMapHandler.SetEnemyTheme("dememenic_keter3eurobeat.mp3", false);
    }

    public class BattleUnitBuf_Aftermath_WELCUMTOBOTTMGEARMATES : BattleUnitBuf
    {
      private int RENDFROMEXISTENCE = 4;

      public override void OnRoundEnd()
      {
        AftermathCollectionInitializer.aftermathMapHandler.EnforceTheme();
        base.OnRoundEnd();
        this._owner.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Quickness, 3, this._owner);
        --this.RENDFROMEXISTENCE;
        if (this.RENDFROMEXISTENCE > 0)
          return;
        this._owner.Die();
        AftermathCollectionInitializer.aftermathMapHandler.UnEnforceTheme();
      }

      public override void BeforeRollDice(BattleDiceBehavior behavior)
      {
        base.BeforeRollDice(behavior);
        behavior.ApplyDiceStatBonus(new DiceStatBonus()
        {
          power = 2
        });
      }

      public override string keywordId => "Aftermath_TopGear";

      public override string keywordIconId => "Aftermath_TopGear";
    }
  }
}
