// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.UnitUtil
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using BattleCharacterProfile;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public static class UnitUtil
  {
    public static BattleUnitModel CopyModdedUnit(
      this StageController instance,
      Faction faction,
      BattleUnitModel cloner,
      int index = -1,
      int height = -1,
      XmlVector2 position = null)
    {
      UnitBattleDataModel unitBattleData = new UnitBattleDataModel(instance.GetStageModel(), cloner.UnitData.unitData);
      if (faction > Faction.Enemy)
        AccessTools.Field(typeof (UnitDataModel), "_ownerSephirah").SetValue((object) unitBattleData.unitData, (object) instance.CurrentFloor);
      if (height != -1)
        unitBattleData.unitData.customizeData.height = height;
      BattleUnitModel defaultUnit = BattleObjectManager.CreateDefaultUnit(faction);
      UnitDataModel unitData = unitBattleData.unitData;
      if (index < 0)
      {
        IEnumerable<int> source = BattleObjectManager.instance.GetAliveList(faction).Select<BattleUnitModel, int>((Func<BattleUnitModel, int>) (y => y.index));
        int num = 0;
        while (index < 0)
        {
          if (!source.Contains<int>(num))
          {
            index = num;
            break;
          }
          ++num;
        }
      }
      defaultUnit.index = index;
      defaultUnit.grade = unitData.grade;
      if (faction == Faction.Enemy)
      {
        StageWaveModel currentWaveModel = instance.GetCurrentWaveModel();
        if (position != null)
        {
          defaultUnit.formation = new FormationPosition(new FormationPositionXmlData()
          {
            vector = position
          });
        }
        else
        {
          int i = Mathf.Min(defaultUnit.index, currentWaveModel.GetFormation().PostionList.Count - 1);
          if (i < defaultUnit.index)
            Debug.Log((object) "AftermathCollection: Index higher than available formation positions, summoning at highest value possible");
          BattleUnitModel battleUnitModel = defaultUnit;
          FormationPositionXmlData info = new FormationPositionXmlData();
          XmlVector2 xmlVector2 = new XmlVector2();
          Vector3Int pos = currentWaveModel.GetFormationPosition(i).Pos;
          xmlVector2.x = pos.x;
          pos = currentWaveModel.GetFormationPosition(i).Pos;
          xmlVector2.y = pos.y;
          info.vector = xmlVector2;
          FormationPosition formationPosition = new FormationPosition(info);
          battleUnitModel.formation = formationPosition;
        }
      }
      else
      {
        StageLibraryFloorModel floor = instance.GetStageModel().GetFloor(instance.CurrentFloor);
        if (position != null)
        {
          defaultUnit.formation = new FormationPosition(new FormationPositionXmlData()
          {
            vector = position
          });
        }
        else
        {
          int index1 = Mathf.Min(defaultUnit.index, floor.GetFormation().PostionList.Count - 1);
          if (index1 < defaultUnit.index)
            Debug.Log((object) "AftermathCollection: Index higher than available formation positions, summoning at highest value possible");
          BattleUnitModel battleUnitModel = defaultUnit;
          FormationPositionXmlData info = new FormationPositionXmlData();
          XmlVector2 xmlVector2 = new XmlVector2();
          Vector3Int pos = floor.GetFormationPosition(index1).Pos;
          xmlVector2.x = pos.x;
          pos = floor.GetFormationPosition(index1).Pos;
          xmlVector2.y = pos.y;
          info.vector = xmlVector2;
          FormationPosition formationPosition = new FormationPosition(info);
          battleUnitModel.formation = formationPosition;
        }
      }
      BattleUnitModel battleUnitModel1;
      if (unitBattleData.isDead)
      {
        battleUnitModel1 = defaultUnit;
      }
      else
      {
        defaultUnit.SetUnitData(unitBattleData);
        defaultUnit.OnCreated();
        BattleObjectManager.instance.RegisterUnit(defaultUnit);
        defaultUnit.passiveDetail.OnUnitCreated();
        UnitUtil.AddEmotionPassives(defaultUnit);
        defaultUnit.cardSlotDetail.RecoverPlayPoint(defaultUnit.cardSlotDetail.GetMaxPlayPoint());
        defaultUnit.OnWaveStart();
        UnitUtil.LevelUpEmotion(defaultUnit, 0);
        UnitUtil.InitializeCombatUI(defaultUnit);
        defaultUnit.history.data2 = 8;
        battleUnitModel1 = defaultUnit;
      }
      return battleUnitModel1;
    }

    public static BattleUnitModel AddModdedUnit(
      this StageController instance,
      Faction faction,
      LorId enemyUnitId,
      int index = -1,
      int height = -1,
      XmlVector2 position = null)
    {
      UnitBattleDataModel dataByEnemyUnitId = UnitBattleDataModel.CreateUnitBattleDataByEnemyUnitId(instance.GetStageModel(), enemyUnitId);
      if (faction > Faction.Enemy)
        AccessTools.Field(typeof (UnitDataModel), "_ownerSephirah").SetValue((object) dataByEnemyUnitId.unitData, (object) instance.CurrentFloor);
      if (height != -1)
        dataByEnemyUnitId.unitData.customizeData.height = height;
      BattleObjectManager.instance.UnregisterUnitByIndex(faction, index);
      BattleUnitModel defaultUnit = BattleObjectManager.CreateDefaultUnit(faction);
      UnitDataModel unitData = dataByEnemyUnitId.unitData;
      if (index < 0)
      {
        IEnumerable<int> source = BattleObjectManager.instance.GetAliveList(faction).Select<BattleUnitModel, int>((Func<BattleUnitModel, int>) (y => y.index));
        int num = 0;
        while (index < 0)
        {
          if (!source.Contains<int>(num))
          {
            index = num;
            break;
          }
          ++num;
        }
      }
      defaultUnit.index = index;
      defaultUnit.grade = unitData.grade;
      if (faction == Faction.Enemy)
      {
        StageWaveModel currentWaveModel = instance.GetCurrentWaveModel();
        if (position != null)
        {
          defaultUnit.formation = new FormationPosition(new FormationPositionXmlData()
          {
            vector = position
          });
        }
        else
        {
          int i = Mathf.Min(defaultUnit.index, currentWaveModel.GetFormation().PostionList.Count - 1);
          if (i < defaultUnit.index)
            Debug.Log((object) "AftermathCollection: Index higher than available formation positions, summoning at highest value possible");
          defaultUnit.formation = new FormationPosition(new FormationPositionXmlData()
          {
            vector = new XmlVector2()
            {
              x = currentWaveModel.GetFormationPosition(i).Pos.x,
              y = currentWaveModel.GetFormationPosition(i).Pos.y
            }
          });
        }
      }
      else
      {
        StageLibraryFloorModel floor = instance.GetStageModel().GetFloor(instance.CurrentFloor);
        if (position != null)
        {
          defaultUnit.formation = new FormationPosition(new FormationPositionXmlData()
          {
            vector = position
          });
        }
        else
        {
          int index1 = Mathf.Min(defaultUnit.index, floor.GetFormation().PostionList.Count - 1);
          if (index1 < defaultUnit.index)
            Debug.Log((object) "AftermathCollection: Index higher than available formation positions, summoning at highest value possible");
          defaultUnit.formation = new FormationPosition(new FormationPositionXmlData()
          {
            vector = new XmlVector2()
            {
              x = floor.GetFormationPosition(index1).Pos.x,
              y = floor.GetFormationPosition(index1).Pos.y
            }
          });
        }
      }
      BattleUnitModel battleUnitModel;
      if (dataByEnemyUnitId.isDead)
      {
        battleUnitModel = defaultUnit;
      }
      else
      {
        defaultUnit.SetUnitData(dataByEnemyUnitId);
        defaultUnit.OnCreated();
        BattleObjectManager.instance.RegisterUnit(defaultUnit);
        defaultUnit.passiveDetail.OnUnitCreated();
        UnitUtil.AddEmotionPassives(defaultUnit);
        defaultUnit.cardSlotDetail.RecoverPlayPoint(defaultUnit.cardSlotDetail.GetMaxPlayPoint());
        defaultUnit.OnWaveStart();
        defaultUnit.allyCardDetail.DrawCards(defaultUnit.UnitData.unitData.GetStartDraw());
        UnitUtil.LevelUpEmotion(defaultUnit, 0);
        UnitUtil.InitializeCombatUI(defaultUnit);
        defaultUnit.history.data2 = 7;
        battleUnitModel = defaultUnit;
      }
      return battleUnitModel;
    }

    public static BattleUnitModel AddModdedUnitPlayerSide(
      this StageController instance,
      UnitModel unit,
      string packageId,
      bool playerSide = true)
    {
      StageLibraryFloorModel floor = instance.GetStageModel().GetFloor(instance.CurrentFloor);
      UnitDataModel data = new UnitDataModel(new LorId(packageId, unit.Id), playerSide ? floor.Sephirah : SephirahType.None);
      data.SetCustomName(unit.Name);
      BattleUnitModel defaultUnit = BattleObjectManager.CreateDefaultUnit(playerSide ? Faction.Player : Faction.Enemy);
      defaultUnit.index = unit.Pos;
      defaultUnit.grade = data.grade;
      BattleUnitModel battleUnitModel = defaultUnit;
      FormationPosition formationPosition;
      if (unit.CustomPos == null)
        formationPosition = floor.GetFormationPosition(defaultUnit.index);
      else
        formationPosition = new FormationPosition(new FormationPositionXmlData()
        {
          vector = unit.CustomPos
        });
      battleUnitModel.formation = formationPosition;
      UnitBattleDataModel unitBattleData = new UnitBattleDataModel(instance.GetStageModel(), data);
      unitBattleData.Init();
      defaultUnit.SetUnitData(unitBattleData);
      defaultUnit.OnCreated();
      BattleObjectManager.instance.RegisterUnit(defaultUnit);
      defaultUnit.passiveDetail.OnUnitCreated();
      UnitUtil.LevelUpEmotion(defaultUnit, unit.EmotionLevel);
      if (unit.LockedEmotion)
        defaultUnit.emotionDetail.SetMaxEmotionLevel(unit.MaxEmotionLevel);
      defaultUnit.allyCardDetail.DrawCards(defaultUnit.UnitData.unitData.GetStartDraw());
      defaultUnit.cardSlotDetail.RecoverPlayPoint(defaultUnit.cardSlotDetail.GetMaxPlayPoint());
      if (unit.AddEmotionPassive)
        UnitUtil.AddEmotionPassives(defaultUnit);
      defaultUnit.OnWaveStart();
      UnitUtil.InitializeCombatUI(defaultUnit);
      defaultUnit.history.data2 = 7;
      return defaultUnit;
    }

    public static void RefreshCombatUI(bool forceReturn = false)
    {
      foreach ((BattleUnitModel, int) tuple in BattleObjectManager.instance.GetList().Select<BattleUnitModel, (BattleUnitModel, int)>((Func<BattleUnitModel, int, (BattleUnitModel, int)>) ((value, i) => (value, i))))
      {
        SingletonBehavior<UICharacterRenderer>.Instance.SetCharacter(tuple.Item1.UnitData.unitData, tuple.Item2, true);
        if (forceReturn)
          tuple.Item1.moveDetail.ReturnToFormationByBlink(true);
      }
      try
      {
        BattleObjectManager.instance.InitUI();
      }
      catch (IndexOutOfRangeException ex)
      {
      }
    }

    public static void LevelUpEmotion(BattleUnitModel unit, int value)
    {
      for (int index = 0; index < value; ++index)
      {
        unit.emotionDetail.LevelUp_Forcely(1);
        unit.emotionDetail.CheckLevelUp();
      }
      Singleton<StageController>.Instance.GetCurrentStageFloorModel().team.UpdateCoin();
    }

    public static void StoreBattleUnitModel(BattleUnitModel unit, string packageId)
    {
      List<BattleUnitModel> output;
      Singleton<StageController>.Instance.GetStageModel().GetStageStorageData<List<BattleUnitModel>>(packageId + "_BattleUnitModelStorage", out output);
      if (output != null && output.Count > 0)
      {
        output.Add(unit);
        Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData(packageId + "_BattleUnitModelStorage", (object) output);
      }
      else
      {
        List<BattleUnitModel> battleUnitModelList = new List<BattleUnitModel>()
        {
          unit
        };
        Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData(packageId + "_BattleUnitModelStorage", (object) battleUnitModelList);
      }
    }

    public static void StoreBattleUnitModel(List<BattleUnitModel> units, string packageId)
    {
      List<BattleUnitModel> output;
      Singleton<StageController>.Instance.GetStageModel().GetStageStorageData<List<BattleUnitModel>>(packageId + "_BattleUnitModelStorage", out output);
      if (output == null || output.Count <= 0)
        return;
      output.AddRange((IEnumerable<BattleUnitModel>) units);
      Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData(packageId + "_BattleUnitModelStorage", (object) output);
    }

    public static List<BattleUnitModel> GetStoredUnitModels(string packageId)
    {
      List<BattleUnitModel> output;
      Singleton<StageController>.Instance.GetStageModel().GetStageStorageData<List<BattleUnitModel>>(packageId + "_BattleUnitModelStorage", out output);
      return output != null && output.Count > 0 ? output : (List<BattleUnitModel>) null;
    }

    public static void ClearBattleUnitStorage(string packageId)
    {
      Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData(packageId + "_BattleUnitModelStorage", (object) null);
    }

    public static UnitUtil.UnitSpawnMethod GetUnitSpawnMethod(BattleUnitModel unit)
    {
      return (UnitUtil.UnitSpawnMethod) unit.history.data2;
    }

    private static void InitializeCombatUI(BattleUnitModel battleUnitModel)
    {
      try
      {
        if (!((UnityEngine.Object) SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.GetProfileUI(battleUnitModel) == (UnityEngine.Object) null))
          return;
        BattleCharacterProfileUI characterProfileUi = new BattleCharacterProfileUI();
        characterProfileUi.gameObject.SetActive(true);
        characterProfileUi.Initialize();
        characterProfileUi.SetUnitModel(battleUnitModel);
        if (battleUnitModel.faction == Faction.Player)
          SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.allyarray[battleUnitModel.index] = characterProfileUi;
        else
          SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.enemyarray[battleUnitModel.index] = characterProfileUi;
      }
      catch (Exception ex)
      {
        if (ex.Message == "")
          Debug.Log((object) ("AftermathCollection: successfully summoned " + battleUnitModel.UnitData.unitData.name + " at index " + battleUnitModel.index.ToString()));
        else
          Debug.Log((object) ("AftermathCollection - failed to initialize UI of summoned unit: " + ex.Message));
      }
    }

    private static void AddEmotionPassives(BattleUnitModel unit)
    {
      List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(Faction.Player);
      if (!aliveList.Any<BattleUnitModel>())
        return;
      foreach (BattleEmotionCardModel emotionCardModel in aliveList.FirstOrDefault<BattleUnitModel>().emotionDetail.PassiveList.Where<BattleEmotionCardModel>((Func<BattleEmotionCardModel, bool>) (x => x.XmlInfo.TargetType == EmotionTargetType.AllIncludingEnemy || x.XmlInfo.TargetType == EmotionTargetType.All)))
      {
        if ((unit.faction != Faction.Enemy ? 1 : (emotionCardModel.XmlInfo.TargetType > EmotionTargetType.All ? 1 : 0)) != 0)
          unit.emotionDetail.ApplyEmotionCard(emotionCardModel.XmlInfo);
      }
    }

    public enum UnitSpawnMethod
    {
      Default = 0,
      Summoned = 7,
      Cloned = 8,
    }
  }
}
