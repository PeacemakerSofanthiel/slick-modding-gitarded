// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_ExhaustIfNoDice
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_ExhaustIfNoDice : DiceCardSelfAbilityBase
  {
    public static string Desc = "If there are no dice left on this page, exhaust it at the start of the Scene";

    public override void OnRoundStart_inHand(BattleUnitModel unit, BattleDiceCardModel self)
    {
      base.OnRoundStart_inHand(unit, self);
      if (self.XmlData.DiceBehaviourList.Count > 0)
        return;
      unit.allyCardDetail.ExhaustACard(self);
      self.exhaust = true;
    }
  }
}
