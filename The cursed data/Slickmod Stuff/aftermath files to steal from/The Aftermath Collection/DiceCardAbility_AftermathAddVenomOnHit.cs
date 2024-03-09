// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_AftermathAddVenomOnHit
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_AftermathAddVenomOnHit : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Add 1 random Venom to hand";

    public override void OnSucceedAttack()
    {
      base.OnSucceedAttack();
      VenomCardModel.AddVenomToHand(this.owner);
    }

    public override string[] Keywords
    {
      get => new string[1]{ "Family_Only_Keyword" };
    }
  }
}
