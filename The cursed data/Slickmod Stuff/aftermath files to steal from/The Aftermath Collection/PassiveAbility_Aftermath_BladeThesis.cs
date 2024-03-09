// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_BladeThesis
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_BladeThesis : PassiveAbilityBase
  {
    private int _blunt;

    public override void OnRollDice(BattleDiceBehavior behavior)
    {
      base.OnRollDice(behavior);
      bool flag = this.owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>();
      if (behavior.Detail == BehaviourDetail.Slash && !flag)
      {
        behavior.behaviourInCard.Detail = BehaviourDetail.Hit;
        behavior.behaviourInCard.MotionDetail = MotionDetail.H;
        behavior.behaviourInCard.EffectRes = "Sword_H";
      }
      if (behavior.Detail != BehaviourDetail.Guard || flag)
        return;
      BattleDiceBehavior targetDice = behavior.TargetDice;
      if (targetDice == null || targetDice.Type != BehaviourType.Atk)
        return;
      PassiveAbility_Aftermath_BladeThesis.ActivateBladeThesis(this.owner);
    }

    public override void OnRoundEnd()
    {
      base.OnRoundEnd();
      this._blunt = 0;
    }

    public override void OnSucceedAttack(BattleDiceBehavior behavior)
    {
      base.OnSucceedAttack(behavior);
      bool flag = this.owner.bufListDetail.HasBuf<PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive>();
      if (this.owner.currentDiceAction.target == null || behavior.Detail != BehaviourDetail.Hit || this._blunt >= 2 || flag)
        return;
      ++this._blunt;
      this.owner.currentDiceAction.target.bufListDetail.AddKeywordBufByEtc(KeywordBuf.Paralysis, 1, this.owner);
      this.owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase) this);
    }

    public static void ActivateBladeThesis(BattleUnitModel feller)
    {
      feller.bufListDetail.AddBuf((BattleUnitBuf) new PassiveAbility_Aftermath_BladeThesis.BattleUnitBuf_BladeThesisActive());
      AftermathCollectionInitializer.PlaySound(AftermathCollectionInitializer.aftermathMapHandler.GetAudioClip("activateBT.mp3"), feller.view.transform, 2f);
      feller.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase) new PassiveAbility_Aftermath_BladeThesis());
      if (!(feller.customBook.ClassInfo.Name == "Silvio") && !(feller.customBook.ClassInfo.Name == "Silvio's Page"))
        return;
      feller.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.Hit2);
      feller.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.Slash2);
      feller.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.Penetrate2);
    }

    public class BattleUnitBuf_BladeThesisActive : BattleUnitBuf
    {
      public override void BeforeRollDice(BattleDiceBehavior behavior)
      {
        base.BeforeRollDice(behavior);
        if (behavior.Detail == BehaviourDetail.Hit)
        {
          behavior.behaviourInCard.Detail = BehaviourDetail.Slash;
          behavior.behaviourInCard.MotionDetail = MotionDetail.J;
          behavior.behaviourInCard.EffectRes = "Sword_J";
        }
        if (behavior.Detail != BehaviourDetail.Slash)
          return;
        behavior.ApplyDiceStatBonus(new DiceStatBonus()
        {
          power = 2
        });
        this._owner.battleCardResultLog?.SetPassiveAbility((PassiveAbilityBase) new PassiveAbility_Aftermath_BladeThesis());
      }

      public override void OnRoundEnd()
      {
        base.OnRoundEnd();
        if (this._owner.customBook.ClassInfo.Name == "Silvio" || this._owner.customBook.ClassInfo.Name == "Silvio's Page" || this._owner.customBook.ClassInfo.Name == "Silvio II")
        {
          this._owner.view.charAppearance.RemoveAltMotion(ActionDetail.Hit);
          this._owner.view.charAppearance.RemoveAltMotion(ActionDetail.Slash);
          this._owner.view.charAppearance.RemoveAltMotion(ActionDetail.Penetrate);
        }
        this.Destroy();
      }
    }
  }
}
