// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_MeAndTheBoys
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_XML;
using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_MeAndTheBoys : PassiveAbilityBase
  {
    private int curThreshhold;
    private bool stahpBroPls;

    public override void OnWaveStart()
    {
      base.OnWaveStart();
      AftermathCollectionInitializer.aftermathMapHandler.InitCustomMap<CBLOneMapManager>("CBLOneMapManager");
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S10);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Damaged, ActionDetail.S10);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S10);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Guard, ActionDetail.S10);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Aim, ActionDetail.S10);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.S11);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.S11);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.S11);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Move, ActionDetail.S11);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Fire, ActionDetail.S11);
      this.owner.view.charAppearance.SetAltMotion(ActionDetail.Evade, ActionDetail.S12);
    }

    public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
    {
      if ((double) this.owner.MaxHp * 0.75 > (double) this.owner.hp - (double) dmg && this.curThreshhold < 1)
      {
        this.owner.SetHp((int) Math.Truncate((double) this.owner.MaxHp * 0.75));
        ++this.curThreshhold;
        this.stahpBroPls = true;
      }
      else if ((double) this.owner.MaxHp * 0.5 > (double) this.owner.hp - (double) dmg && this.curThreshhold < 2)
      {
        this.owner.SetHp((int) Math.Truncate((double) this.owner.MaxHp * 0.5));
        ++this.curThreshhold;
        this.stahpBroPls = true;
      }
      return this.stahpBroPls;
    }

    public override void OnRoundEnd()
    {
      this.owner.cardSlotDetail.RecoverPlayPoint(1);
      this.owner.allyCardDetail.DrawCards(1);
      base.OnRoundEnd();
      if (!this.stahpBroPls)
        return;
      this.stahpBroPls = false;
      switch (this.curThreshhold)
      {
        case 1:
          BattleUnitModel battleUnitModel1 = Singleton<StageController>.Instance.AddModdedUnit(Faction.Enemy, new LorId(AftermathCollectionInitializer.packageId, 10102), height: Singleton<Random>.Instance.Next(165, 176), position: new XmlVector2()
          {
            x = 18,
            y = 16
          });
          BattleUnitModel battleUnitModel2 = Singleton<StageController>.Instance.AddModdedUnit(Faction.Enemy, new LorId(AftermathCollectionInitializer.packageId, 10102), height: Singleton<Random>.Instance.Next(165, 176), position: new XmlVector2()
          {
            x = 17,
            y = -16
          });
          this.owner.view.charAppearance.RemoveAllAltMotion();
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S13);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Damaged, ActionDetail.S13);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S13);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Guard, ActionDetail.S13);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Aim, ActionDetail.S13);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.S14);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.S14);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.S14);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Move, ActionDetail.S14);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Fire, ActionDetail.S14);
          this.owner.view.charAppearance.SetAltMotion(ActionDetail.Evade, ActionDetail.S15);
          UnitUtil.RefreshCombatUI();
          battleUnitModel1.view.DisplayDlg(DialogType.SPECIAL_EVENT, "GREENHORNS_OUT_" + Singleton<Random>.Instance.Next(3).ToString());
          BattleUnitView view1 = battleUnitModel2.view;
          int num1 = Singleton<Random>.Instance.Next(3);
          string id1 = "GREENHORNS_OUT_" + num1.ToString();
          view1.DisplayDlg(DialogType.SPECIAL_EVENT, id1);
          if (!(this.owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42020102)))
            break;
          BattleUnitView view2 = this.owner.view;
          num1 = Singleton<Random>.Instance.Next(2);
          string id2 = "GREENHORNS_OUT_" + num1.ToString();
          view2.DisplayDlg(DialogType.SPECIAL_EVENT, id2);
          break;
        case 2:
          BattleUnitModel battleUnitModel3 = Singleton<StageController>.Instance.AddModdedUnit(Faction.Enemy, new LorId(AftermathCollectionInitializer.packageId, 20101), height: Singleton<Random>.Instance.Next(165, 176), position: new XmlVector2()
          {
            x = 8,
            y = 8
          });
          BattleUnitModel battleUnitModel4 = Singleton<StageController>.Instance.AddModdedUnit(Faction.Enemy, new LorId(AftermathCollectionInitializer.packageId, 20101), height: Singleton<Random>.Instance.Next(165, 176), position: new XmlVector2()
          {
            x = 8,
            y = -8
          });
          this.owner.view.charAppearance.RemoveAllAltMotion();
          UnitUtil.RefreshCombatUI();
          BattleUnitView view3 = battleUnitModel3.view;
          int num2 = Singleton<Random>.Instance.Next(3);
          string id3 = "JUNKIES_OUT_" + num2.ToString();
          view3.DisplayDlg(DialogType.SPECIAL_EVENT, id3);
          BattleUnitView view4 = battleUnitModel4.view;
          num2 = Singleton<Random>.Instance.Next(3);
          string id4 = "JUNKIES_OUT_" + num2.ToString();
          view4.DisplayDlg(DialogType.SPECIAL_EVENT, id4);
          if (!(this.owner.Book.BookId == new LorId(AftermathCollectionInitializer.packageId, 42020102)))
            break;
          this.owner.view.DisplayDlg(DialogType.SPECIAL_EVENT, "JUNKIES_OUT_" + Singleton<Random>.Instance.Next(2).ToString());
          break;
      }
    }

    public override bool IsImmuneDmg() => this.stahpBroPls;
  }
}
