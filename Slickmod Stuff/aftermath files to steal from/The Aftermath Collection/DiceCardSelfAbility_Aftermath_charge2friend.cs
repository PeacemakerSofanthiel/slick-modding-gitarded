// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_charge2friend
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_charge2friend : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] Give 2 allies 4 Charge";

    public override string[] Keywords
    {
      get => new string[1]{ "WarpCharge" };
    }

    public override void OnStartBattle()
    {
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList(this.card.owner.faction))
        alive.bufListDetail.AddKeywordBufByCard(KeywordBuf.WarpCharge, 2, (BattleUnitModel) null);
    }
  }
}
