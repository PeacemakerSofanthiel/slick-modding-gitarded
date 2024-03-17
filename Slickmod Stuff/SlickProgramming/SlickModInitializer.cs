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
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_Cycle", out var SlickCycle) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out SlickCycle) && EnumExtender.TryAddName("SlickMod_Cycle", SlickCycle))
            {
                MyKeywordBufs.SlickMod_Cycle = SlickCycle;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_Cycle>();
            }

            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_DamageDown", out var SlickDamageDown) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out SlickDamageDown) && EnumExtender.TryAddName("SlickMod_DamageDown", SlickDamageDown))
            {
                MyKeywordBufs.SlickMod_DamageDown = SlickDamageDown;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_Cycle>();
            }

            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_InfernalOverheat", out var SlickOverheat) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out SlickOverheat) && EnumExtender.TryAddName("SlickMod_InfernalOverheat", SlickOverheat))
            {
                MyKeywordBufs.SlickMod_InfernalOverheat = SlickOverheat;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_InfernalOverheat>();
            }

            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_SparkSamsara", out var SlickSamsara) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out SlickSamsara) && EnumExtender.TryAddName("SlickMod_SparkSamsara", SlickSamsara))
            {
                MyKeywordBufs.SlickMod_SparkSamsara = SlickSamsara;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_SparkSamsara>();
            }

            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_Orb_Focus", out var SlickFocus) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out SlickFocus) && EnumExtender.TryAddName("SlickMod_Orb_Focus", SlickFocus))
            {
                MyKeywordBufs.SlickMod_Orb_Focus = SlickFocus;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_Orb_Focus>();
            }
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_Orb_Malice", out var SlickMalice) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out SlickMalice) && EnumExtender.TryAddName("SlickMod_Orb_Malice", SlickMalice))
            {
                MyKeywordBufs.SlickMod_Orb_Malice = SlickMalice;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_Orb_Malice>();
            }
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_SpareParts", out var SlickParts) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out SlickParts) && EnumExtender.TryAddName("SlickMod_SpareParts", SlickParts))
            {
                MyKeywordBufs.SlickMod_SpareParts = SlickParts;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_SpareParts>();
            }
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_BlackTieInk", out var uhhhwahatever) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out uhhhwahatever) && EnumExtender.TryAddName("SlickMod_BlackTieInk", uhhhwahatever))
            {
                MyKeywordBufs.SlickMod_BlackTieInk = uhhhwahatever;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_BlackTieInk>();
            }
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_DrownedDrowning", out var MewhenIdrown) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out MewhenIdrown) && EnumExtender.TryAddName("SlickMod_DrownedDrowning", MewhenIdrown))
            {
                MyKeywordBufs.SlickMod_DrownedDrowning = MewhenIdrown;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_DrownedDrowning>();
            }
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_DrownedOmegaCringe", out var OmegaCringe) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out OmegaCringe) && EnumExtender.TryAddName("SlickMod_DrownedOmegaCringe", OmegaCringe))
            {
                MyKeywordBufs.SlickMod_DrownedOmegaCringe = OmegaCringe;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_DrownedOmegaCringe>();
            }

        }
        //Exclusive Combat Page Patch
        public class Patchlist
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(BookModel), "SetXmlInfo")]
            public static void BookModel_SetXmlInfo(BookModel __instance, BookXmlInfo ____classInfo, ref List<DiceCardXmlInfo> ____onlyCards)
            {
                if (__instance.BookId.packageId == packageId)
                {
                    foreach (int id in ____classInfo.EquipEffect.OnlyCard)
                    {
                        DiceCardXmlInfo cardItem = ItemXmlDataList.instance.GetCardItem(new LorId(packageId, id), false);
                        ____onlyCards.Add(cardItem);
                        DiceCardXmlInfo b = ItemXmlDataList.instance.GetCardItem(id, false);
                        if (____onlyCards.Contains(b))
                        {
                            ____onlyCards.Remove(b);
                        }
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
        public static KeywordBuf SlickMod_DamageDown;
        public static KeywordBuf SlickMod_InfernalOverheat;
        public static KeywordBuf SlickMod_SparkSamsara;
        public static KeywordBuf SlickMod_SparkSpeedBreak;
        public static KeywordBuf SlickMod_Combo;
        public static KeywordBuf SlickMod_ComboFinisher;
        public static KeywordBuf SlickMod_GearedUp;
        public static KeywordBuf SlickMod_Dangerous;
        public static KeywordBuf SlickMod_Orb_Malice;
        public static KeywordBuf SlickMod_Orb_Focus;
        public static KeywordBuf SlickMod_SpareParts;
        public static KeywordBuf SlickMod_BlackTieInk;
        public static KeywordBuf SlickMod_DrownedDrowning;
        public static KeywordBuf SlickMod_DrownedOmegaCringe;
    }
}