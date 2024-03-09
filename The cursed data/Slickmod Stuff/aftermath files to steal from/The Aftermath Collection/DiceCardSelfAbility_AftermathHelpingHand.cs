// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardSelfAbility_AftermathHelpingHand
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardSelfAbility_AftermathHelpingHand : DiceCardSelfAbilityBase
  {
    public static string Desc = "[Combat Start] Halve the amount of status ailments on target (Rounded up)";

    public override void OnStartBattle()
    {
      base.OnStartBattle();
      if (this.card.target == null)
        return;
      foreach (BattleUnitBuf activatedBuf in this.card.target.bufListDetail.GetActivatedBufList())
      {
        if (activatedBuf.positiveType == BufPositiveType.Negative && activatedBuf.stack >= 2)
          activatedBuf.stack = (activatedBuf.stack + 1) / 2;
      }
    }

    public override string[] Keywords
    {
      get
      {
        return new string[2]
        {
          "Benito_Only_Keyword",
          "bstart_Keyword"
        };
      }
    }
  }
}
