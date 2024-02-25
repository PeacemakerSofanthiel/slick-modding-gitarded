// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathRestUpBenitoPoggers
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathRestUpBenitoPoggers : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Play] Target restores 1 Light and draws 1 page";

    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      base.OnUseInstance(unit, self, targetUnit);
      targetUnit.allyCardDetail.DrawCards(1);
      targetUnit.cardSlotDetail.RecoverPlayPoint(1);
    }
  }
}
