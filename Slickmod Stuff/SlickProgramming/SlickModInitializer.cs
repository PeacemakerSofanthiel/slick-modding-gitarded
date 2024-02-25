using System;
using System.Collections.Generic;
using System.IO;
using EnumExtenderV2;
using HarmonyLib;
using KeywordUtil;
using Mod;
using SlickRuinaMod;
using UnityEngine;
using static Hat_Method.Hat_KeywordBuf;

namespace SlickRuinaMod
{
    public class SlickModInitializer : ModInitializer
    {
        // The funny
        public override void OnInitializeMod()
        {        
            // Stackable buff returns corresponding bufType
            if (EnumExtender.TryGetValueOf<KeywordBuf>("SlickMod_Cycle", out var newKeyword) ||
                EnumExtender.TryFindUnnamedValue(default(KeywordBuf), null, false, out newKeyword) && EnumExtender.TryAddName("SlickMod_Cycle", newKeyword))
            {
                MyKeywordBufs.SlickMod_Cycle = newKeyword;
                KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_SlickMod_Cycle>();
            }
        }
    }
    
    // Container
    public static class MyKeywordBufs
    {
        public static KeywordBuf SlickMod_Cycle;
        public static KeywordBuf SlickMod_FlowState;
    }
}