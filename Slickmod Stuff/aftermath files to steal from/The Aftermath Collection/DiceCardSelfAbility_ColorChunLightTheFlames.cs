// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_ColorChunLightTheFlames
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_ColorChunLightTheFlames : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] When inflicting Burn using Combat Pages this Scene, inflict 1 additional stacks; add a copy of 'Flaming Blitz' to hand";

    public override void OnStartBattle()
    {
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_ColorChunLightTheFlames.BattleUnitBuf_AftermathAddAnotherBurnStackPls());
      this.owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 101));
    }

    public override string[] Keywords
    {
      get
      {
        return new string[3]
        {
          "ColorChunOnlyPage_Keyword",
          "Burn_Keyword",
          "DrawCard_Keyword"
        };
      }
    }

    public class BattleUnitBuf_AftermathAddAnotherBurnStackPls : BattleUnitBuf
    {
      public override int OnGiveKeywordBufByCard(
        BattleUnitBuf cardBuf,
        int stack,
        BattleUnitModel target)
      {
        return cardBuf is BattleUnitBuf_burn ? 1 : base.OnGiveKeywordBufByCard(cardBuf, stack, target);
      }
    }
  }
}
