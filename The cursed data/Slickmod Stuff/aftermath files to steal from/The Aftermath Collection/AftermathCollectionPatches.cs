// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.AftermathCollectionPatches
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using HarmonyLib;
using LOR_DiceSystem;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UI;
using UnityEngine;
using Workshop;

#nullable disable
namespace The_Aftermath_Collection
{
  [HarmonyPatch]
  public class AftermathCollectionPatches
  {
    [HarmonyPostfix]
    [HarmonyPatch(typeof (BookModel), "SetXmlInfo")]
    public static void BookModel_SetXmlInfo_Post(
      BookModel __instance,
      BookXmlInfo ____classInfo,
      ref List<DiceCardXmlInfo> ____onlyCards)
    {
      if (!(__instance.BookId.packageId == AftermathCollectionInitializer.packageId))
        return;
      foreach (int id in ____classInfo.EquipEffect.OnlyCard)
      {
        DiceCardXmlInfo cardItem = ItemXmlDataList.instance.GetCardItem(new LorId(AftermathCollectionInitializer.packageId, id));
        ____onlyCards.Add(cardItem);
      }
    }

    [HarmonyPatch(typeof (SdCharacterUtil), "CreateSkin")]
    [HarmonyPatch(typeof (UICharacterRenderer), "SetCharacter")]
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> SdCharacterUtil_CreateSkin_Transpiler(
      IEnumerable<CodeInstruction> instructions,
      ILGenerator ilgen)
    {
      MethodInfo operand1 = AccessTools.Method(typeof (WorkshopSkinDataSetter), "SetData", new System.Type[1]
      {
        typeof (WorkshopSkinData)
      });
      bool flag = false;
      operand2 = (LocalBuilder) null;
      List<CodeInstruction> skinTranspiler = new List<CodeInstruction>(instructions);
      for (int index1 = 0; index1 < skinTranspiler.Count; ++index1)
      {
        if (CodeInstructionExtensions.Is(skinTranspiler[index1], OpCodes.Callvirt, (MemberInfo) operand1))
        {
          if (!flag)
          {
            int index2;
            for (index2 = index1 + 1; index2 < skinTranspiler.Count; ++index2)
            {
              if (skinTranspiler[index2].Branches(out Label? _))
                index2 = skinTranspiler.Count;
              else if (skinTranspiler[index2].IsStloc() && skinTranspiler[index2].operand is LocalBuilder operand2 && operand2.LocalType == typeof (bool))
              {
                flag = true;
                break;
              }
            }
            if (index2 == skinTranspiler.Count)
              Debug.Log((object) "Failed to obtain LateInit flag for CreateSkin");
          }
          else
          {
            skinTranspiler.InsertRange(index1 + 1, (IEnumerable<CodeInstruction>) new CodeInstruction[2]
            {
              new CodeInstruction(OpCodes.Ldc_I4_1),
              new CodeInstruction(OpCodes.Stloc_S, (object) operand2)
            });
            break;
          }
        }
      }
      return (IEnumerable<CodeInstruction>) skinTranspiler;
    }

    [HarmonyPatch(typeof (WorkshopSkinDataSetter), "LateInit")]
    [HarmonyFinalizer]
    private static Exception WorkshopSkinDataSetter_LateInit_Finalizer(Exception __exception)
    {
      return __exception is NullReferenceException ? (Exception) null : __exception;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof (BookModel), "GetThumbSprite")]
    public static void BookModel_GetThumbSprite(BookModel __instance, ref Sprite __result)
    {
      if (!(__instance.BookId.packageId == AftermathCollectionInitializer.packageId) || !(__instance.ClassInfo.skinType == "Custom") && !AftermathCollectionInitializer.exceptions.Contains<int>(__instance.BookId.id))
        return;
      Texture2D texture = AftermathCollectionInitializer.ThumbnailPatcher(__instance.BookId.id);
      __result = Sprite.Create(texture, new Rect(0.0f, 0.0f, (float) texture.width, (float) texture.height), new Vector2(0.5f, 0.5f));
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof (BookXmlInfo), "GetThumbSprite")]
    public static void BookXmlInfo_GetThumbSprite(BookXmlInfo __instance, ref Sprite __result)
    {
      if (!(__instance.id.packageId == AftermathCollectionInitializer.packageId) || !(__instance.skinType == "Custom") && !AftermathCollectionInitializer.exceptions.Contains<int>(__instance.id.id))
        return;
      Texture2D texture = AftermathCollectionInitializer.ThumbnailPatcher(__instance.id.id);
      __result = Sprite.Create(texture, new Rect(0.0f, 0.0f, (float) texture.width, (float) texture.height), new Vector2(0.5f, 0.5f));
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof (UICharacterRenderer), "SetCharacter")]
    [HarmonyPriority(800)]
    public static void UICharacterRenderer_SetCharacter_Post(
      UICharacterRenderer __instance,
      UnitDataModel unit,
      int index)
    {
      try
      {
        if (index < 0 || index >= 12 || __instance.characterList == null || unit == null)
          return;
        UICharacter character = __instance.characterList[index];
        if (character == null || !(unit.CustomBookItem.BookId.packageId == AftermathCollectionInitializer.packageId))
          return;
        switch (unit.CustomBookItem.BookId.id)
        {
          case 20101:
            character.unitAppearance.CustomAppearance.transform.localScale = Vector3.one * 0.45f;
            character.unitAppearance.CustomAppearance.SetScaleFactor(0.45f);
            break;
          case 50103:
          case 60204:
            using (IEnumerator<CharacterMotion> enumerator = character.unitAppearance.CharacterMotions.Values.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SpriteSet spriteSet = enumerator.Current.motionSpriteSet.Find((Predicate<SpriteSet>) (x => x.sprType == CharacterAppearanceType.RearHair));
                if (spriteSet != null)
                  spriteSet.sprRenderer.enabled = false;
              }
              break;
            }
        }
      }
      catch
      {
      }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof (BattleUnitView), "ChangeScale")]
    public static void BattleUnitView_ChangeScale_Post(BattleUnitView __instance)
    {
      try
      {
        if (__instance.model == null || !(__instance.model.customBook.BookId.packageId == AftermathCollectionInitializer.packageId))
          return;
        switch (__instance.model.customBook.BookId.id)
        {
          case 20101:
            __instance.characterRotationCenter.localScale = Vector3.one * 2f;
            __instance.costUI.rectParent.transform.localScale = __instance.transform.localScale * 1.35f;
            __instance.speedDiceSetterUI.transform.localScale = __instance.transform.localScale * 1.65f;
            __instance.charAppearance.CustomAppearance.transform.localScale = Vector3.one * 0.45f;
            __instance.charAppearance.CustomAppearance.SetScaleFactor(0.45f);
            break;
          case 50103:
          case 60204:
            using (IEnumerator<CharacterMotion> enumerator = __instance.charAppearance.CharacterMotions.Values.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SpriteSet spriteSet = enumerator.Current.motionSpriteSet.Find((Predicate<SpriteSet>) (x => x.sprType == CharacterAppearanceType.RearHair));
                if (spriteSet != null)
                  spriteSet.sprRenderer.enabled = false;
              }
              break;
            }
          case 60402:
          case 42060402:
            __instance.characterRotationCenter.localScale = Vector3.one * 1.1f;
            __instance.costUI.rectParent.transform.localScale = __instance.transform.localScale * 0.95f;
            __instance.speedDiceSetterUI.transform.localScale = __instance.transform.localScale * 1.15f;
            break;
          case 42020102:
            __instance.characterRotationCenter.localScale = Vector3.one * 2f;
            __instance.costUI.rectParent.transform.localScale = __instance.transform.localScale * 1.35f;
            __instance.speedDiceSetterUI.transform.localScale = __instance.transform.localScale * 1.65f;
            break;
        }
      }
      catch
      {
      }
    }

    [HarmonyPriority(800)]
    [HarmonyPrefix]
    [HarmonyPatch(typeof (BattleUnitBuf_warpCharge), "OnAddBuf")]
    private static void GainOvercharge(BattleUnitBuf_warpCharge __instance, out int __state)
    {
      __state = __instance.stack;
    }

    [HarmonyPriority(0)]
    [HarmonyPostfix]
    [HarmonyPatch(typeof (BattleUnitBuf_warpCharge), "OnAddBuf")]
    private static void GainOvercharge(
      BattleUnitBuf_warpCharge __instance,
      BattleUnitModel ____owner,
      in int __state)
    {
      if (!____owner.passiveDetail.HasPassive<PassiveAbility_Aftermath_DMO_MobiusBattleSuit>())
        return;
      int addedStack = __state - __instance.stack;
      if (addedStack <= 0)
        return;
      if (!(____owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Overcharge)) is BattleUnitBuf_Aftermath_Overcharge buf))
      {
        buf = new BattleUnitBuf_Aftermath_Overcharge();
        ____owner.bufListDetail.AddBuf((BattleUnitBuf) buf);
      }
      buf.OnAddBuf(addedStack);
    }

    private static void OnAddBuff(
      BattleUnitBuf_warpCharge __instance,
      BattleUnitModel ____owner,
      int addedStack)
    {
      if (!____owner.passiveDetail.HasPassive<PassiveAbility_Aftermath_DMO_MobiusBattleSuit>())
        return;
      int num = 10;
      if (____owner.passiveDetail.HasPassive<PassiveAbility_250123>())
        num = 20;
      if (__instance.stack != num)
        return;
      if (____owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Overcharge)) is BattleUnitBuf_Aftermath_Overcharge aftermathOvercharge && ____owner != null)
      {
        aftermathOvercharge.OnAddBuf(addedStack);
      }
      else
      {
        BattleUnitBuf_Aftermath_Overcharge buf = new BattleUnitBuf_Aftermath_Overcharge();
        ____owner.bufListDetail.AddBuf((BattleUnitBuf) buf);
        buf.OnAddBuf(addedStack);
      }
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof (BattleUnitBuf_warpCharge), "UseStack")]
    private static bool UseCharge(
      BattleUnitBuf_warpCharge __instance,
      BattleUnitModel ____owner,
      int v,
      bool isCard,
      ref bool __result)
    {
      if (!____owner.passiveDetail.HasPassive<PassiveAbility_Aftermath_DMO_MobiusBattleSuit>())
        return true;
      BattleUnitBuf_Aftermath_Overcharge aftermathOvercharge = ____owner.bufListDetail.GetActivatedBufList().Find((Predicate<BattleUnitBuf>) (x => x is BattleUnitBuf_Aftermath_Overcharge)) as BattleUnitBuf_Aftermath_Overcharge;
      if (!(aftermathOvercharge != null & isCard) || ____owner == null || !aftermathOvercharge.UseStack(v))
        return true;
      __result = true;
      return false;
    }
  }
}
