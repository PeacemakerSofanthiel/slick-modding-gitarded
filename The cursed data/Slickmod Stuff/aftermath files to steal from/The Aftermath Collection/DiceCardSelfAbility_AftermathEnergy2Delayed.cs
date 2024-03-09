// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathEnergy2Delayed
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathEnergy2Delayed : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Restore 1 Light next Scene and the Scene after";

    public override void OnUseCard()
    {
      this.owner.bufListDetail.AddBuf((BattleUnitBuf) new BattleUnitBuf_DelayedLight2());
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Energy_Keyword" };
    }
  }
}
