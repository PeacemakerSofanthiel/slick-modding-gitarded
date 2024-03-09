// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermathenergy3draw1
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermathenergy3draw1 : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Play; Target Ally] Target restores 3 Light\nIf starved, add 'Rise From The Ashes' to hand and exhaust this page instead";

    public override void OnUseInstance(
      BattleUnitModel unit,
      BattleDiceCardModel self,
      BattleUnitModel targetUnit)
    {
      base.OnUseInstance(unit, self, targetUnit);
      if (unit.passiveDetail.HasPassive<PassiveAbility_Aftermath_Aftermathstarvation>() || unit.passiveDetail.HasPassive<PassiveAbility_Aftermath_Aftermathstarvation2>())
      {
        unit.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60217));
        self.exhaust = true;
      }
      else
        targetUnit.cardSlotDetail.RecoverPlayPoint(3);
    }

    public override bool IsOnlyAllyUnit() => true;

    public override bool IsTargetableSelf() => true;

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "BreadBoys_Only_Keyword",
          "Energy_Keyword"
        };
      }
    }
  }
}
