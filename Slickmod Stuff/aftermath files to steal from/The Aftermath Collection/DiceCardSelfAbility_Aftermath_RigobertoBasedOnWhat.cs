// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_RigobertoBasedOnWhatPlayer
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_RigobertoBasedOnWhatPlayer : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] If user is Based, restore 2 Light and draw 1 page; otherwise, spend 4 Erosion to become 2 Based";

    public override void OnUseCard()
    {
      base.OnUseCard();
      if (this.owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Based)) != null)
      {
        this.owner.cardSlotDetail.RecoverPlayPointByCard(2);
        this.owner.allyCardDetail.DrawCards(1);
      }
      else
      {
        BattleUnitBuf activatedBuf = this.owner.bufListDetail.GetActivatedBuf(KeywordBuf.Decay);
        if (activatedBuf == null || activatedBuf.stack <= 4)
          return;
        activatedBuf.stack -= 4;
        if (activatedBuf.stack < 1)
          activatedBuf.Destroy();
        BattleUnitBufListDetail bufListDetail = this.owner.bufListDetail;
        BattleUnitBuf_Aftermath_Based buf = new BattleUnitBuf_Aftermath_Based();
        buf.stack = 2;
        bufListDetail.AddBuf((BattleUnitBuf) buf);
      }
    }

    public override string[] Keywords
    {
      get
      {
        return new string[4]
        {
          "Energy_Keyword",
          "DrawCard_Keyword",
          "Aftermath_Decay_Keyword",
          "Aftermath_Basic"
        };
      }
    }
  }
}
