// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Spend1ChemDraw1
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Spend1ChemDraw1 : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Spend 1 Stored Chems to add 1 random Chem to hand";

    public override void OnUseCard()
    {
      base.OnUseCard();
      BattleUnitBuf battleUnitBuf = this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_StoredChems));
      if (battleUnitBuf == null)
        return;
      if (battleUnitBuf.stack > 0)
      {
        --battleUnitBuf.stack;
        ChemsCardModel.AddChemToHand(this.owner);
      }
      if (battleUnitBuf.stack >= 1)
        return;
      battleUnitBuf.Destroy();
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Aftermath_StoredChems" };
    }
  }
}
