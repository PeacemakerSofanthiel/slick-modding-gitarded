// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.BehaviourAction_Aftermath_EventHorizon
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class BehaviourAction_Aftermath_EventHorizon : BehaviourActionBase
  {
    public override FarAreaEffect SetFarAreaAtkEffect(BattleUnitModel self)
    {
      this._self = self;
      FarAreaEffect_Aftermath_EventHorizon aftermathEventHorizon = new GameObject().AddComponent<FarAreaEffect_Aftermath_EventHorizon>();
      aftermathEventHorizon.Init(self);
      return (FarAreaEffect) aftermathEventHorizon;
    }
  }
}
