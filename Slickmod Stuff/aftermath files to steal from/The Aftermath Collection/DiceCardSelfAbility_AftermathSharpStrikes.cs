// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathSharpStrikes
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathSharpStrikes : DiceCardSelfAbilityBase
  {
    public static string Desc = "Can only be used this Scene";

    public override void OnRoundStart_inHand(BattleUnitModel unit, BattleDiceCardModel self)
    {
      if (!(unit.customBook.ClassInfo.Name == "A Yellow Ties Officer") && !(unit.customBook.ClassInfo.Name == "A Yellow Ties Officer's Page"))
        return;
      unit.view.charAppearance.SetAltMotion(ActionDetail.Hit, ActionDetail.Hit2);
      unit.view.charAppearance.SetAltMotion(ActionDetail.Slash, ActionDetail.Slash2);
      unit.view.charAppearance.SetAltMotion(ActionDetail.Penetrate, ActionDetail.Penetrate2);
      unit.view.charAppearance.SetAltMotion(ActionDetail.Default, ActionDetail.S4);
      unit.view.charAppearance.SetAltMotion(ActionDetail.Standing, ActionDetail.S4);
      unit.view.charAppearance.SetAltMotion(ActionDetail.Move, ActionDetail.Special);
      unit.view.charAppearance.SetAltMotion(ActionDetail.Guard, ActionDetail.S5);
      unit.view.charAppearance.SetAltMotion(ActionDetail.Evade, ActionDetail.S6);
      unit.view.charAppearance.SetAltMotion(ActionDetail.Damaged, ActionDetail.S7);
    }

    public override void OnRoundEnd(BattleUnitModel unit, BattleDiceCardModel self)
    {
      base.OnRoundEnd(unit, self);
      if (!(unit.customBook.ClassInfo.Name == "A Yellow Ties Officer") && !(unit.customBook.ClassInfo.Name == "A Yellow Ties Officer's Page"))
        return;
      unit.view.charAppearance.RemoveAltMotion(ActionDetail.Hit);
      unit.view.charAppearance.RemoveAltMotion(ActionDetail.Slash);
      unit.view.charAppearance.RemoveAltMotion(ActionDetail.Penetrate);
      unit.view.charAppearance.RemoveAltMotion(ActionDetail.Default);
      unit.view.charAppearance.RemoveAltMotion(ActionDetail.Move);
      unit.view.charAppearance.RemoveAltMotion(ActionDetail.Guard);
      unit.view.charAppearance.RemoveAltMotion(ActionDetail.Evade);
      unit.view.charAppearance.RemoveAltMotion(ActionDetail.Damaged);
      unit.view.charAppearance.RemoveAltMotion(ActionDetail.Standing);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Random_Keyword" };
    }
  }
}
