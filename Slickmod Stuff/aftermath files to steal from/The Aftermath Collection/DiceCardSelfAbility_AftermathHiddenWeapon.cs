// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathHiddenWeapon
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathHiddenWeapon : DiceCardSelfAbilityBase
  {
    public static string Desc = "[On Use] Add 'Sharp-Suited Strikes' to hand; gain 3 Damage Up next Scene";

    public override void OnUseCard()
    {
      base.OnUseCard();
      this.owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60126), true).AddBuf((BattleDiceCardBuf) new DiceCardSelfAbility_AftermathHiddenWeapon.BattleDiceCardBuf_HiddenWeapon());
      this.owner.bufListDetail.AddKeywordBufByCard(KeywordBuf.DmgUp, 3, this.owner);
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "HiddenWeapon_Keyword",
          "DrawCard_Keyword"
        };
      }
    }

    public class BattleDiceCardBuf_HiddenWeapon : BattleDiceCardBuf
    {
      public override void OnRoundStart()
      {
        base.OnRoundStart();
        this._card.temporary = true;
      }
    }
  }
}
