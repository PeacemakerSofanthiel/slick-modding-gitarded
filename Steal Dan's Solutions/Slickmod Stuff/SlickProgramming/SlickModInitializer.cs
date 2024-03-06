using System;
using System.Collections.Generic;
using EnumExtenderV2;
using System.Reflection;
using HarmonyLib;
using KeywordUtil;
using LOR_DiceSystem;
using Mod;
using SlickRuinaMod;
using static Hat_Method.Hat_KeywordBuf;
using SlickProgramming;

namespace SlickRuinaMod
{
    public class SlickModInitializer : ModInitializer
    {
        // The funny
        public override void OnInitializeMod()
        {
            //More Exclusive
            Harmony.CreateAndPatchAll(typeof(Patchlist), base.GetType().Assembly.GetName().Name);

            // Stackable buff returns corresponding bufType
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_Cycle", out var newKeyword) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out newKeyword) && EnumExtender.TryAddName("SlickMod_Cycle", newKeyword))
            {
                MyKeywordBufs.SlickMod_Cycle = newKeyword;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_Cycle>();
            }

            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_InfernalOverheat", out var newKeyword2) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out newKeyword2) && EnumExtender.TryAddName("SlickMod_InfernalOverheat", newKeyword2))
            {
                MyKeywordBufs.SlickMod_InfernalOverheat = newKeyword2;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_InfernalOverheat>();
            }

            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_SparkSamsara", out var newKeyword3) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out newKeyword3) && EnumExtender.TryAddName("SlickMod_SparkSamsara", newKeyword2))
            {
                MyKeywordBufs.SlickMod_SparkSamsara = newKeyword3;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_SparkSamsara>();
            }
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_GearedUp", out var newKeyword4) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out newKeyword4) && EnumExtender.TryAddName("SlickMod_GearedUp", newKeyword4))
            {
                MyKeywordBufs.SlickMod_GearedUp = newKeyword4;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_GearedUp>();
            }
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_Dangerous", out var newKeyword5) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out newKeyword5) && EnumExtender.TryAddName("SlickMod_Dangerous", newKeyword5))
            {
                MyKeywordBufs.SlickMod_Dangerous = newKeyword5;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_Dangerous>();
            }

            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_Orb_Focus", out var a) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out a) && EnumExtender.TryAddName("SlickMod_Orb_Focus", a))
            {
                MyKeywordBufs.SlickMod_Orb_Focus = a;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_Orb_Focus>();
            }
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_Orb_Malice", out var b) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out b) && EnumExtender.TryAddName("SlickMod_Orb_Malice", b))
            {
                MyKeywordBufs.SlickMod_Orb_Focus = b;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_Orb_Malice>();
            }
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_BlackTieInk", out var uhhhwahatever) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out uhhhwahatever) && EnumExtender.TryAddName("SlickMod_BlackTieInk", uhhhwahatever))
            {
                MyKeywordBufs.SlickMod_BlackTieInk = uhhhwahatever;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_BlackTieInk>();
            }

        }
        //Exclusive Combat Page Patch
        public class Patchlist
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(BookModel), "SetXmlInfo")]
            public static void BookModel_SetXmlInfo(BookModel __instance, BookXmlInfo ____classInfo, ref List<DiceCardXmlInfo> ____onlyCards)
            {
                if (__instance.BookId.packageId == SlickModInitializer.packageId)
                {
                    foreach (int id in ____classInfo.EquipEffect.OnlyCard)
                    {
                        DiceCardXmlInfo cardItem = ItemXmlDataList.instance.GetCardItem(new LorId(SlickModInitializer.packageId, id), false);
                        ____onlyCards.Add(cardItem);
                    }
                }
            }
        }
        public static string language;
        public static string path;
        public const string packageId = "SlickMod";
    }
    
    // Container
    public static class MyKeywordBufs
    {
        public static KeywordBuf SlickMod_Cycle;
        public static KeywordBuf SlickMod_FlowState;
        public static KeywordBuf SlickMod_InfernalOverheat;
        public static KeywordBuf SlickMod_SparkSamsara;
        public static KeywordBuf SlickMod_SparkSpeedBreak;
        public static KeywordBuf SlickMod_Combo;
        public static KeywordBuf SlickMod_ComboFinisher;
        public static KeywordBuf SlickMod_GearedUp;
        public static KeywordBuf SlickMod_Dangerous;
        public static KeywordBuf SlickMod_Orb_Malice;
        public static KeywordBuf SlickMod_Orb_Focus;
        public static KeywordBuf SlickMod_BlackTieInk;
    }
}