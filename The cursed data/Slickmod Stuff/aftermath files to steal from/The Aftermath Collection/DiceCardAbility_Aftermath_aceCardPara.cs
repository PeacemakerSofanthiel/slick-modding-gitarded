// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_aceCardPara
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_aceCardPara : DiceCardAbilityBase
  {
    public static string Desc = "[On Hit] Inflict 5 Paralysis next Scene";

    public override void OnSucceedAttack()
    {
      this.card.target?.bufListDetail.AddKeywordBufByCard(KeywordBuf.Paralysis, 5, this.owner);
    }
  }
}
