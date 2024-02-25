// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.AftermathUtilityExtensions
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using LOR_DiceSystem;
using Sound;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace The_Aftermath_Collection
{
  public static class AftermathUtilityExtensions
  {
    public static void ExplodeOnDeath(BattleUnitView view)
    {
      if (view.model.UnitData.floorBattleData.param3 == 113413411)
        return;
      SoundEffectPlayer soundEffectPlayer = SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Creature/MatchGirl_Explosion");
      if ((UnityEngine.Object) soundEffectPlayer != (UnityEngine.Object) null)
        soundEffectPlayer.SetGlobalPosition(view.WorldPosition);
      Battle.CreatureEffect.CreatureEffect creatureEffect = SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect("1/MatchGirl_Footfall", 1f, view, (BattleUnitView) null, 2f);
      creatureEffect.transform.localScale *= 3.5f;
      creatureEffect.AttachEffectLayer();
      view.model.UnitData.floorBattleData.param3 = 113413411;
      view.StartDeadEffect(false);
      view.model._deadSceneBlock = true;
    }

    public static T SelectOneRandom<T>(this IList<T> list)
    {
      return list[Singleton<System.Random>.Instance.Next(list.Count)];
    }

    public static List<T> SelectManyRandom<T>(this IList<T> list, int count)
    {
      List<T> source = new List<T>();
      for (int index = 0; index < count; ++index)
        source.Append<T>(list[Singleton<System.Random>.Instance.Next(list.Count)]);
      return source;
    }

    public static void SortByCost(this List<BattleDiceCardModel> list)
    {
      list.Sort((Comparison<BattleDiceCardModel>) ((x, y) => x.CurCost - y.CurCost));
    }

    public static List<T> Shuffle<T>(this IList<T> list)
    {
      return list.OrderBy<T, int>((Func<T, int>) (x => Singleton<System.Random>.Instance.Next())).ToList<T>();
    }

    public static bool CheckForKeyword(this BattleDiceCardModel card, string keyword)
    {
      if (card == null)
        return false;
      DiceCardXmlInfo xmlData = card.XmlData;
      if (xmlData == null)
        return false;
      if (xmlData.Keywords.Contains(keyword))
        return true;
      List<string> abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
      for (int index = 0; index < abilityKeywords.Count; ++index)
      {
        if (abilityKeywords[index] == keyword)
          return true;
      }
      foreach (DiceBehaviour behaviour in card.GetBehaviourList())
      {
        List<string> keywordsByScript = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(behaviour.Script);
        for (int index = 0; index < keywordsByScript.Count; ++index)
        {
          if (keywordsByScript[index] == keyword)
            return true;
        }
      }
      return false;
    }

    public static void RemoveAllAltMotion(this CharacterAppearance charAppearance)
    {
      charAppearance.RemoveAltMotion(ActionDetail.Default);
      charAppearance.RemoveAltMotion(ActionDetail.Damaged);
      charAppearance.RemoveAltMotion(ActionDetail.Standing);
      charAppearance.RemoveAltMotion(ActionDetail.Guard);
      charAppearance.RemoveAltMotion(ActionDetail.Aim);
      charAppearance.RemoveAltMotion(ActionDetail.Hit);
      charAppearance.RemoveAltMotion(ActionDetail.Slash);
      charAppearance.RemoveAltMotion(ActionDetail.Penetrate);
      charAppearance.RemoveAltMotion(ActionDetail.Move);
      charAppearance.RemoveAltMotion(ActionDetail.Fire);
      charAppearance.RemoveAltMotion(ActionDetail.Evade);
    }
  }
}
