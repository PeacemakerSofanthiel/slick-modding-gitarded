// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Evan2
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Evan2 : DiceCardSelfAbilityBase
  {
    public override void OnUseCard()
    {
      this.owner.TakeDamage((int) ((double) this.owner.hp * 0.070000000298023224));
      this.owner.cardSlotDetail.RecoverPlayPoint(3);
      this.owner.allyCardDetail.DrawCards(1);
    }
  }
}
