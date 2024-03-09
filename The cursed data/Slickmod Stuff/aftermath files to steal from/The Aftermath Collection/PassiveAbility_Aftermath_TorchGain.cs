// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.PassiveAbility_Aftermath_TorchGain
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class PassiveAbility_Aftermath_TorchGain : PassiveAbilityBase
  {
    public override void OnRoundEnd()
    {
      base.OnRoundEnd();
      if (Singleton<StageController>.Instance.RoundTurn % 3 != 0)
        return;
      this.owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60127));
    }

    public override void OnWaveStart()
    {
      base.OnWaveStart();
      this.owner.allyCardDetail.AddNewCard(new LorId(AftermathCollectionInitializer.packageId, 60127)).SetCostToZero();
      AftermathCollectionInitializer.aftermathMapHandler.InitCustomMap<BreadBoysMapManager>("BreadBoysLastStand");
    }

    public override void OnRoundStart()
    {
      base.OnRoundStart();
      AftermathCollectionInitializer.aftermathMapHandler.EnforceMap();
    }
  }
}
