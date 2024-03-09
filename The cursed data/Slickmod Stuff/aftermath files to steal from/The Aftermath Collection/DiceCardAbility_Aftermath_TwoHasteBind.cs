// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_TwoHasteBind
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  [UnusedAbility]
  public class DiceCardAbility_Aftermath_TwoHasteBind : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] If user has 2+ Haste, inflict 2 Bind next Scene";

    public override void OnSucceedAttack()
    {
      if (!(this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Quickness) is BattleUnitBuf_quickness activatedBuf) || activatedBuf.stack < 2)
        return;
      this.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Binding, 2, this.owner);
    }
  }
}
