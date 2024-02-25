// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathClashLoseDraw1Page
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathClashLoseDraw1Page : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Lose] Draw 1 page";

    public override void OnLoseParrying() => this.owner.allyCardDetail.DrawCards(1);
  }
}
