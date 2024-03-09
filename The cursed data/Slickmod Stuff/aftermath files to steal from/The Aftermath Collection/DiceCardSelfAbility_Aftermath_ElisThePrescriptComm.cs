// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_Aftermath_ElisThePrescriptCommandsIt
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_Aftermath_ElisThePrescriptCommandsIt : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] If Singleton, give 2 Type Dice Power-Up to all Singleton allies (The type is determined by the current Offensive type boosted by Grace of the Prescript)";
    private KeywordBuf bufType = KeywordBuf.SlashPowerUp;

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "OnlyOne_Keyword",
          "onlypage_Elis_Keyword"
        };
      }
    }

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      if (!this.owner.allyCardDetail.IsHighlander() || !(this.owner.passiveDetail.PassiveList.Find((Predicate<PassiveAbilityBase>) (x => x is PassiveAbility_240018)) is PassiveAbility_240018 passiveAbility240018))
        return;
      switch (passiveAbility240018.targetBehaviour)
      {
        case BehaviourDetail.Slash:
          this.bufType = KeywordBuf.SlashPowerUp;
          break;
        case BehaviourDetail.Penetrate:
          this.bufType = KeywordBuf.PenetratePowerUp;
          break;
        case BehaviourDetail.Hit:
          this.bufType = KeywordBuf.HitPowerUp;
          break;
      }
      foreach (BattleUnitModel battleUnitModel in BattleObjectManager.instance.GetAliveList(this.owner.faction).FindAll((Predicate<BattleUnitModel>) (x => x != this.owner && x.allyCardDetail.IsHighlander())))
      {
        BattleUnitBuf activatedBuf = battleUnitModel.bufListDetail.GetActivatedBuf(this.bufType);
        if (activatedBuf != null)
          activatedBuf.stack += 2;
        else
          battleUnitModel.bufListDetail.AddKeywordBufThisRoundByEtc(this.bufType, 2, this.owner);
      }
    }
  }
}
