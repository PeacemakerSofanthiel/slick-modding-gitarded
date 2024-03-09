// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardPriority_Aftermath_Camaraderie
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardPriority_Aftermath_Camaraderie : DiceCardPriorityBase
  {
    public override int GetPriorityBonus(BattleUnitModel owner)
    {
      int num = 0;
      foreach (BattleUnitModel alive in BattleObjectManager.instance.GetAliveList(owner.faction))
      {
        foreach (BattleUnitBuf activatedBuf in alive.bufListDetail.GetActivatedBufList())
        {
          if (activatedBuf.positiveType == BufPositiveType.Negative)
            num += activatedBuf.stack;
        }
      }
      return num >= 20 ? 3 : -1;
    }
  }
}
