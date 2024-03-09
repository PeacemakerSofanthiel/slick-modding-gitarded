// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_AftermathBurnSpirit
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_AftermathBurnSpirit : PassiveAbilityBase
  {
    private int _THISGIRLISONFIIIIRE;

    public override int OnGiveKeywordBufByCard(
      BattleUnitBuf buf,
      int stack,
      BattleUnitModel target)
    {
      if (buf.bufType == KeywordBuf.Burn)
        this._THISGIRLISONFIIIIRE += stack;
      return base.OnGiveKeywordBufByCard(buf, stack, target);
    }

    public override void OnRoundStart()
    {
      this._THISGIRLISONFIIIIRE /= 5;
      if (this._THISGIRLISONFIIIIRE >= 2)
        this.owner.cardSlotDetail.RecoverPlayPoint(2);
      else
        this.owner.cardSlotDetail.RecoverPlayPoint(this._THISGIRLISONFIIIIRE);
      this._THISGIRLISONFIIIIRE = 0;
    }
  }
}
