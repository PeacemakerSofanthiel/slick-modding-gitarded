// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_winenergy1
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_winenergy1 : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Win] Restore 1 Light";

    public override string[] Keywords
    {
      get => new string[1]{ "Energy_Keyword" };
    }

    public override void OnWinParrying() => this.owner.cardSlotDetail.RecoverPlayPointByCard(1);
  }
}
