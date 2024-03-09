// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_DMO_CaduceusProcedure
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_DMO_CaduceusProcedure : PassiveAbilityBase
  {
    public static string Desc = "At the start of each Scene, recover 3 HP for every 5 stacks of Charge.";

    public override void OnRoundStart()
    {
      base.OnRoundStart();
      this.owner.RecoverHP(3 * (this.owner.bufListDetail.GetKewordBufStack(KeywordBuf.WarpCharge) / 5));
    }
  }
}
