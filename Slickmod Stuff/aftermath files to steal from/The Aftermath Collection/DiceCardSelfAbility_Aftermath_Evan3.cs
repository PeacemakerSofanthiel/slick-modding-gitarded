// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_Evan3
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_Evan3 : DiceCardSelfAbilityBase
  {
    public override void OnStartBattle()
    {
      if (this.owner.allyCardDetail.IsHighlander())
        this.owner.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_Aftermath_Evan3.BattleUnitBuf_buf2());
      else
        this.owner.bufListDetail.AddBuf((BattleUnitBuf) new DiceCardSelfAbility_Aftermath_Evan3.BattleUnitBuf_buf1());
    }

    public override string[] Keywords
    {
      get => new string[1]{ "OnlyOne_Keyword" };
    }

    public class BattleUnitBuf_buf1 : BattleUnitBuf
    {
      public override void OnSuccessAttack(BattleDiceBehavior behavior) => this._owner.RecoverHP(2);

      public override void OnRoundEnd() => this.Destroy();
    }

    public class BattleUnitBuf_buf2 : BattleUnitBuf
    {
      public override void OnSuccessAttack(BattleDiceBehavior behavior) => this._owner.RecoverHP(3);

      public override void OnRoundEnd() => this.Destroy();
    }
  }
}
