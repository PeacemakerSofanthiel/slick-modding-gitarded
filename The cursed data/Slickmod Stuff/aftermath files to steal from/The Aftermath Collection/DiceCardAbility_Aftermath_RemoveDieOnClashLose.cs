// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.DiceCardAbility_Aftermath_RemoveDieOnClashLose
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using System;
using System.Collections.Generic;

#nullable disable
namespace The_Aftermath_Collection
{
  public class DiceCardAbility_Aftermath_RemoveDieOnClashLose : DiceCardAbilityBase
  {
    public static string Desc = "[On Clash Lose] Remove this die for the rest of the Act";

    public override void OnLoseParrying()
    {
      base.OnLoseParrying();
      List<DiceBehaviour> diceBehaviourList = new List<DiceBehaviour>();
      foreach (DiceBehaviour diceBehaviour1 in this.card.card.XmlData.DiceBehaviourList.FindAll((Predicate<DiceBehaviour>) (x => x != this.behavior.behaviourInCard)))
      {
        DiceBehaviour diceBehaviour2 = diceBehaviour1.Copy();
        diceBehaviourList.Add(diceBehaviour2);
      }
      this.card.card.XmlData.DiceBehaviourList = diceBehaviourList;
    }
  }
}
