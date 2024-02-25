using System.IO;
using System.Collections.Generic;
using System.Collections;
using LOR_DiceSystem;
using HarmonyLib;
using Mod;
using UnityEngine;
using CustomMapUtility;
using System;
using UI;
using System.Linq;
using System.Reflection;
using Workshop;
using System.Reflection.Emit;
using BattleCharacterProfile;
using Sound;
using KeywordUtil;
using EnumExtenderV2;

namespace The_Aftermath_Collection
{
    #region -x- MOD INITIALIZER -x-
	public class AftermathCollectionInitializer : ModInitializer
	{
		public static string packageId => "calmmagma.theaftermathcollection";

		public static CustomMapHandler aftermathMapHandler;

		public static string Path => ModContentManager.Instance.GetModPath(packageId);

        public static Dictionary<string, AssetBundle> assetBundles = new Dictionary<string, AssetBundle>();

		// Initializer function
		public override void OnInitializeMod()
		{
			base.OnInitializeMod();
			Harmony.CreateAndPatchAll(typeof(AftermathCollectionPatches), "calmmagma.theaftermathcollection");
            aftermathMapHandler = CustomMapHandler.GetCMU("calmmagma.theaftermathcollection");
            DirectoryInfo folder = new DirectoryInfo(Path + "/Resource/AssetBundles");
            foreach (var file in folder.GetFiles())
            {
                assetBundles.Add(file.Name, AssetBundle.LoadFromFile(file.FullName));
            }
            //EnumExtender.TryAddName("Aftermath_Spore", AftermathBufs.Aftermath_Spore);
            //KeywordUtils.RegisterKeywordBuf<BattleUnitBuf_Aftermath_Spore>();
            //removes errors on startup
            Singleton<ModContentManager>.Instance.GetErrorLogs().RemoveAll((string log) => log.Contains("The same assembly name already exists."));
		}

		// Sound handling function
		public static void PlaySound(AudioClip audio, Transform transform, float VolumnControl = 1.5f, bool loop = false)
		{
			BattleEffectSound battleEffectSound = UnityEngine.Object.Instantiate<BattleEffectSound>(SingletonBehavior<BattleSoundManager>.Instance.effectSoundPrefab, transform);
			float volume = 1f;
			bool flag = SingletonBehavior<BattleSoundManager>.Instance != null;
			bool flag2 = flag;
			if (flag2)
			{
				volume = SingletonBehavior<BattleSoundManager>.Instance.VolumeFX * VolumnControl;
			}
			battleEffectSound.Init(audio, volume, loop);
		}
		
		// Thumbnail patcher function
		public static Texture2D ThumbnailPatcher(int keypageId)
		{
			Texture2D texture2D = new Texture2D(4, 4);
			switch (keypageId)
			{
				// --- SECRET FIGHTS ---
				// Color Chun
				case 100:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "The Vermillion Dragon" + "/ClothCustom/Icon.png"));
					return texture2D;


				// --- NORINCO GREENHORNS ---
				// Cassandra
				case 10101:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Cassandra" + "/ClothCustom/Icon.png"));
                    return texture2D;

				// Norinco Workshop Greenhorn
                case 10102:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Norinco Workshop Greenhorn" + "/ClothCustom/Icon.png"));
                    return texture2D;

                // --- C.B.L. I ---
				// Dave
                case 20101:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Dave" + "/ClothCustom/Icon.png"));
                    return texture2D;

                // Amanita Junkie
                case 20102:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Amanita Junkie" + "/ClothCustom/Icon.png"));
                    return texture2D;

                // --- ZWEI WESTERN SECTION ---
                // Liam
                case 50101:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Liam" + "/ClothCustom/Icon.png"));
					return texture2D;

				// Zwei Section 3 Fixer
				case 50102:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Zwei Section 3 Fixer" + "/ClothCustom/Icon.png"));
					return texture2D;

				// Tamora
				case 50103:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Tamora" + "/ClothCustom/Icon.png"));
					return texture2D;

				// Daniel
				case 50104:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Daniel" + "/ClothCustom/Icon.png"));
					return texture2D;



				// --- TIES & FAMILY ---
				// Silvio
				case 60101:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Silvio" + "/ClothCustom/Icon.png"));
					return texture2D;

				// Benito
				case 60102:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Benito_player" + "/ClothCustom/Icon.png"));
					return texture2D;

				// Amanita Family Member
				case 60103:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Amanita Family Member" + "/ClothCustom/Icon.png"));
					return texture2D;

				// Yellow Ties Officer
				case 60104:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Yellow Ties Officer" + "/ClothCustom/Icon.png"));
					return texture2D;



				// --- LIU REMNANTS ---
				// Yijun
				case 60201:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Yijun" + "/ClothCustom/Icon.png"));
					return texture2D;

				// Hunan
				case 60205:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Hunan" + "/ClothCustom/Icon.png"));
					return texture2D;
				
				// Bruce
				case 60204:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Bruce" + "/ClothCustom/Icon.png"));
					return texture2D;

				// Ex-Liu Fixer Section 1
				case 60202:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Ex-Liu Fixer 1" + "/ClothCustom/Icon.png"));
					return texture2D;

				// Ex-Liu Fixer Section 2
				case 60203:
					texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Ex-Liu Fixer 2" + "/ClothCustom/Icon.png"));
					return texture2D;

                // -- RETURN OF THE INDEX --
                // Elis
                case 60303:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Elis" + "/ClothCustom/Icon.png"));
                    return texture2D;


                // -- MOBIUS OFFICE --
                // Mobius Office Enforcer
                case 60401:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "A Mobius Office Fixer" + "/ClothCustom/Icon.png"));
                    return texture2D;

                // Mobius Office Charger
                case 60402:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Mobius Office Charger" + "/ClothCustom/Icon.png"));
                    return texture2D;

                // Lance
                case 60403:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Lance" + "/ClothCustom/Icon.png"));
                    return texture2D;

                // Ace
                case 60404:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Ace" + "/ClothCustom/Icon.png"));
                    return texture2D;

                // Mobius Office Commander
                case 60405:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Mobius Office Commander" + "/ClothCustom/Icon.png"));
                    return texture2D;

                // Mobius Office Charger, Page 2
                case 60406:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "Mobius Office Charger 2" + "/ClothCustom/Icon.png"));
                    return texture2D;

                // Mobius Office Enforcer, Page 2
                case 60407:
                    texture2D.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/" + "A Mobius Office Fixer 2" + "/ClothCustom/Icon.png"));
                    return texture2D;




                // ----- DEFAULT -----
                default:
					return texture2D;
			}
		}

        public static readonly int[] exceptions =
		{
			100
		};

    }
    #endregion


    #region -x- HARMONY SHENANIGANS -x-
    [HarmonyPatch]
    public class AftermathCollectionPatches
    {
        /// <summary>
        /// Simple patch that makes exclusive combat pages work
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(BookModel), nameof(BookModel.SetXmlInfo))]
        public static void BookModel_SetXmlInfo_Post(BookModel __instance, BookXmlInfo ____classInfo, ref List<DiceCardXmlInfo> ____onlyCards)
        {
            if (__instance.BookId.packageId == AftermathCollectionInitializer.packageId)
            {
                foreach (int id in ____classInfo.EquipEffect.OnlyCard)
                {
                    DiceCardXmlInfo cardItem = ItemXmlDataList.instance.GetCardItem(new LorId(AftermathCollectionInitializer.packageId, id), false);
                    ____onlyCards.Add(cardItem);
                }
            }
        }

        /*
        /// <summary>
        /// Hat's front sprite patch, for displaying sprites above other stuff
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(CharacterAppearance), nameof(CharacterAppearance.ChangeLayer))]
        public static void CharacterAppearance_ChangeLayer_Postfix(CharacterAppearance __instance, string layerName, Dictionary<ActionDetail, CharacterMotion> ____characterMotionDic)
        {
            foreach (KeyValuePair<ActionDetail, CharacterMotion> keyValuePair in ____characterMotionDic)
            {
                List<SpriteSet> motionSpriteSet = keyValuePair.Value.motionSpriteSet;
                foreach (SpriteSet spriteSet in motionSpriteSet)
                {
                    if (spriteSet.sprRenderer.name != null)
                    {
                        bool flag = spriteSet.sprRenderer.name == "Customize_Renderer_Front";
                        if (flag)
                        {
                            spriteSet.sprRenderer.sortingOrder = 1100;
                        }
                    }
                }
            }
        }
        */
        
        /// <summary>
        /// Cyam's front sprite patch, for forcing front sprites to show above everything else
        /// <br></br>
        /// Apparently less "forceful" than Hat's patch due to not messing with the layers
        /// <br></br>
        /// // CalmMagma: Also, apparently all of this is just to change ONE FUCKING BOOLEAN FROM <see langword="false"/> TO <see langword="true"/>? What the fuck?
        /// </summary>
        [HarmonyPatch(typeof(SdCharacterUtil), nameof(SdCharacterUtil.CreateSkin))]
        [HarmonyPatch(typeof(UICharacterRenderer), nameof(UICharacterRenderer.SetCharacter))]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> SdCharacterUtil_CreateSkin_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilgen)
        {
            var setDataMethod = AccessTools.Method(typeof(WorkshopSkinDataSetter), nameof(WorkshopSkinDataSetter.SetData), new Type[] { typeof(WorkshopSkinData) });
            bool obtainedFlag = false;
            LocalBuilder local = null;
            var codes = new List<CodeInstruction>(instructions);
            for (var i = 0; i < codes.Count; i++)
            {
                if (codes[i].Is(OpCodes.Callvirt, setDataMethod))
                {
                    if (!obtainedFlag)
                    {
                        int j;
                        for (j = i + 1; j < codes.Count; j++)
                        {
                            if (codes[j].Branches(out Label? _))
                            {
                                j = codes.Count;
                            }
                            else
                            {
                                if (codes[j].IsStloc())
                                {
                                    local = codes[j].operand as LocalBuilder;
                                    if (local != null && local.LocalType == typeof(bool))
                                    {
                                        obtainedFlag = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (j == codes.Count)
                        {
                            Debug.Log("Failed to obtain LateInit flag for CreateSkin");
                        }
                    }
                    else
                    {
                        codes.InsertRange(i + 1, new CodeInstruction[]
                        {
                            new CodeInstruction(OpCodes.Ldc_I4_1),
                            new CodeInstruction(OpCodes.Stloc_S, local)
                        });
                        break;
                    }
                }
            }
            return codes;
        }

        /// <summary>
        /// Cyam's front sprite patch- this is to prevent the game from shitting itself
        /// </summary>
        [HarmonyPatch(typeof(Workshop.WorkshopSkinDataSetter), nameof(Workshop.WorkshopSkinDataSetter.LateInit))]
        [HarmonyFinalizer]
        static Exception WorkshopSkinDataSetter_LateInit_Finalizer(Exception __exception)
        {
            if (__exception is NullReferenceException)
            {
                return null;
            }
            return __exception;
        }
        

        /// <summary>
        /// Makes keypage appearance previews work
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(BookModel), nameof(BookModel.GetThumbSprite))]
        public static void BookModel_GetThumbSprite(BookModel __instance, ref Sprite __result)
        {
            if (__instance.BookId.packageId == AftermathCollectionInitializer.packageId && (__instance.ClassInfo.skinType == "Custom" || AftermathCollectionInitializer.exceptions.Contains(__instance.BookId.id)))
            {
                Texture2D texture2D = AftermathCollectionInitializer.ThumbnailPatcher(__instance.BookId.id);
                __result = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
            }
        }

        /// <summary>
        /// Makes appearance projection previews work
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(BookXmlInfo), nameof(BookXmlInfo.GetThumbSprite))]
        public static void BookXmlInfo_GetThumbSprite(BookXmlInfo __instance, ref Sprite __result)
        {
            if (__instance.workshopID == AftermathCollectionInitializer.packageId && (__instance.skinType == "Custom" || AftermathCollectionInitializer.exceptions.Contains(__instance.id.id)))
            {
                Texture2D texture2D = AftermathCollectionInitializer.ThumbnailPatcher(__instance.id.id);
                __result = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
            } 
        }

        /// <summary>
        /// Makes skin parameters work for UI elements (such as skin previews/chracter portraits)
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(UICharacterRenderer), nameof(UICharacterRenderer.SetCharacter))]
        [HarmonyPriority(Priority.First)]
        public static void UICharacterRenderer_SetCharacter_Post(UICharacterRenderer __instance, UnitDataModel unit, int index)
        {
            try
            {
                if (index >= 0 && index < 12 && __instance.characterList != null && unit != null)
                {
                    UICharacter uicharacter = __instance.characterList[index];
                    if (uicharacter != null)
                    {
                        if (unit.CustomBookItem.BookId.packageId == AftermathCollectionInitializer.packageId)
                        {
                            switch (unit.CustomBookItem.BookId.id)
                            {
                                /*
                                case 20101: // dave player side
                                    uicharacter.unitAppearance.CustomAppearance.transform.localScale = Vector3.one * 0.45f;
                                    uicharacter.unitAppearance.CustomAppearance.SetScaleFactor(0.45f);
                                    break; */

                                case 50103: // tamora player side (hood)
                                case 60204: // bruce player side (bandage)
                                    foreach (var motion in uicharacter.unitAppearance.CustomAppearance.allSpriteList)
                                    {
                                        if (motion != null && motion.name.StartsWith("Customized_BackHair"))
                                        {
                                            motion.enabled = false;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch // (Exception e)
            {
                //Debug.Log(AftermathCollectionInitializer.packageId + " - failed to alter skin properties for UI:\n" + e.Message);
            }
        }

        /// <summary>
        /// Makes skin parameters work for in-battle/SD sprites
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(BattleUnitView), nameof(BattleUnitView.ChangeScale))]
        public static void BattleUnitView_ChangeScale_Post(BattleUnitView __instance)
        {
            try
            {
                if (__instance.model != null)
                {
                    if (__instance.model.customBook.BookId.packageId == AftermathCollectionInitializer.packageId)
                    {
                        switch (__instance.model.customBook.BookId.id)
                        {
                            /*
                            case 20101: // dave player side
                                //__instance.characterRotationCenter.localScale = Vector3.one * 2f;
                                //__instance.costUI.rectParent.transform.localScale = __instance.characterRotationCenter.localScale * 1.35f;
                                //__instance.speedDiceSetterUI.transform.localScale = __instance.characterRotationCenter.localScale * 1.65f;
                                __instance.charAppearance.CustomAppearance.transform.localScale = Vector3.one * 0.45f;
                                __instance.charAppearance.CustomAppearance.SetScaleFactor(0.45f);
                                break;

                            case 42020102: // dave enemy side
                                //__instance.characterRotationCenter.localScale = Vector3.one * 2f;
                                __instance.costUI.rectParent.transform.localScale = __instance.characterRotationCenter.localScale * 1.35f;
                                __instance.speedDiceSetterUI.transform.localScale = __instance.characterRotationCenter.localScale * 1.65f;
                                break;
                                */

                            /*
                            case 60402:
                            case 42060402: // mobius office charger 
                                __instance.characterRotationCenter.localScale = Vector3.one * 1.1f;
                                __instance.costUI.rectParent.transform.localScale = __instance.characterRotationCenter.localScale * 0.95f;
                                __instance.speedDiceSetterUI.transform.localScale = __instance.characterRotationCenter.localScale * 1.15f;
                                break;
                                */

                            case 50103: // tamora player side (hood)
                            case 60204: // bruce player side (bandage)
                                foreach (var motion in __instance.charAppearance.CustomAppearance.allSpriteList)
                                {
                                    if (motion != null && motion.name.StartsWith("Customized_BackHair"))
                                    {
                                        motion.enabled = false;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            catch // (Exception e)
            {
               // Debug.Log(AftermathCollectionInitializer.packageId + " - failed to alter skin properties:\n" + e.Message);
            }
        }

        /// <summary>
        /// Necessary to let Benito gain Emotion Coins from On Play pages.
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(BattlePlayingCardSlotDetail), nameof(BattlePlayingCardSlotDetail.OnApplyCard))]
        public static void BattlePlayingCardSlotDetail_OnApplyCard_Post(BattlePlayingCardSlotDetail __instance, BattleDiceCardModel card, bool __result)
        {
            if (__result && __instance._self.passiveDetail.HasPassive<PassiveAbility_Aftermath_NoCombat>() && card.GetSpec().Ranged == CardRange.Instance)
            {
                BattleUnitModel owner = card.owner;
                if (owner != null)
                {
                    EmotionCoinType bruh = RandomUtil.SelectOne(EmotionCoinType.Negative, EmotionCoinType.Positive);
                    owner.emotionDetail.CreateEmotionCoin(bruh);
                    SingletonBehavior<BattleManagerUI>.Instance.ui_battleEmotionCoinUI.OnAcquireCoin(owner, bruh, 1);
                }                
            }
        }

        /// <summary>
        /// These handle making Overcharge possible because vanilla Charge is basically untouchable without Harmony.
        /// </summary>
        #region - MOBIUS OFFICE PATCHES -
        [HarmonyPriority(Priority.First), HarmonyPrefix]
        [HarmonyPatch(typeof(BattleUnitBuf_warpCharge), nameof(BattleUnitBuf_warpCharge.OnAddBuf))]
        private static void GainOvercharge(BattleUnitBuf_warpCharge __instance, out int __state) => __state = __instance.stack + 0;
        [HarmonyPriority(Priority.Last), HarmonyPostfix]
        [HarmonyPatch(typeof(BattleUnitBuf_warpCharge), nameof(BattleUnitBuf_warpCharge.OnAddBuf))]
        private static void GainOvercharge(BattleUnitBuf_warpCharge __instance, BattleUnitModel ____owner, in int __state)
        {
            if (!____owner.passiveDetail.HasPassive<PassiveAbility_Aftermath_DMO_MobiusBattleSuit>()) return;
            int over = __state - __instance.stack;
            if (over <= 0) return;
            if (!(____owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Overcharge) is BattleUnitBuf_Aftermath_Overcharge buf))
            {
                buf = new BattleUnitBuf_Aftermath_Overcharge();
                ____owner.bufListDetail.AddBuf(buf);
            }
            buf.OnAddBuf(over);
        }

        // [HarmonyPostfix, HarmonyPatch(typeof(BattleUnitBuf_warpCharge), nameof(BattleUnitBuf_warpCharge.OnAddBuf))]
        private static void OnAddBuff(BattleUnitBuf_warpCharge __instance, BattleUnitModel ____owner, int addedStack)
        {
            if (!____owner.passiveDetail.HasPassive<PassiveAbility_Aftermath_DMO_MobiusBattleSuit>())
            {
                return;
            }
            int num = 10;
            if (____owner.passiveDetail.HasPassive<PassiveAbility_250123>())
            {
                num = 20;
            }
            if (__instance.stack == num)
            {
                var buf = ____owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Overcharge) as BattleUnitBuf_Aftermath_Overcharge;
                if (buf != null && ____owner != null)
                {
                    buf.OnAddBuf(addedStack);
                }
                else
                {
                    buf = new BattleUnitBuf_Aftermath_Overcharge();
                    ____owner.bufListDetail.AddBuf(buf);
                    buf.OnAddBuf(addedStack);

                }
            }
        }
        [HarmonyPrefix, HarmonyPatch(typeof(BattleUnitBuf_warpCharge), nameof(BattleUnitBuf_warpCharge.UseStack))]
        private static bool UseCharge(BattleUnitBuf_warpCharge __instance, BattleUnitModel ____owner, int v, bool isCard, ref bool __result)
        {
            if (!____owner.passiveDetail.HasPassive<PassiveAbility_Aftermath_DMO_MobiusBattleSuit>())
            {
                return true;
            }
            var buf = ____owner.bufListDetail.GetActivatedBufList().Find(x => x is BattleUnitBuf_Aftermath_Overcharge) as BattleUnitBuf_Aftermath_Overcharge;
            if (buf != null && isCard && ____owner != null && buf.UseStack(v))
            {
                //Up to you if you want to return true when using overcharge.
                __result = true;
                return false;
            }
            return true;
        }
        #endregion

    }
    #endregion


    #region -x- STAGE MANAGERS -x-

    public class BreadBoysMapManager : CustomMapManager
	{
		protected override string[] CustomBGMs
		{
			get
			{
				return new string[] { "emotion0bread.mp3", "emotion2bread.mp3" };
			}
		}
	}

    public class CBLOneMapManager : CustomMapManager
    {
        protected override string[] CustomBGMs
        {
            get
            {
                return new string[] { "Keter Battle Theme 1.wav", "Keter Battle Theme 2.wav" };
            }
        }
    }

    public class CBLOnePointTwoManager : CustomMapManager
    {
        protected override string[] CustomBGMs
        {
            get
            {
                return new string[] { "dememenic_keter3eurobeat.ogg" };
            }
        }
    }


    #endregion


    #region -x- VARIED UTILITIES -x-

    /// <summary>
    /// A simplified BattleUnitModel object for more easily cloning BattleUnitModels.
    /// </summary>
    public class UnitModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Pos { get; set; }

        public SephirahType Sephirah { get; set; }

        public bool LockedEmotion { get; set; }

        public int MaxEmotionLevel { get; set; } = 0;

        public int EmotionLevel { get; set; }

        public bool AddEmotionPassive { get; set; } = true;

        public bool OnWaveStart { get; set; }

        public XmlVector2 CustomPos { get; set; }
    }

    /// <summary>
    /// Initially taken from CWR (with permission from undefined) before being improved upon 
    /// <br></br>by fixing some UI bugs and other unrelated jank and by adding some shortcuts for
    /// <br></br>storing BattleUnitModels easily.
    /// </summary>
    public static class UnitUtil
	{
        public enum UnitSpawnMethod
        {
             Default,
             Summoned = 7,
             Cloned = 8
        }

        /// <summary>
        /// Copies a <see cref="BattleUnitModel"/> into a simplified <see cref="UnitModel"/> for cloning purposes.
        /// <br></br>Useful for adding controllable units with <see cref="AddModdedUnitPlayerSide"/>.
        /// </summary>
        public static UnitModel Copy(this BattleUnitModel model) => new UnitModel() 
        {  
            Id = model.id,
            Name = model.UnitData.unitData.name,
            Pos = model.index,
            Sephirah = model.UnitData.unitData.OwnerSephirah,
            LockedEmotion = false,
            MaxEmotionLevel = model.emotionDetail.MaximumEmotionLevel,
            EmotionLevel = model.emotionDetail.EmotionLevel,
            OnWaveStart = true,
            CustomPos = new XmlVector2 { x = model.formation.Pos.x, y = model.formation.Pos.y }
        }; 

        /// <summary>
        /// Clones an existing BattleUnitModel and summons them.
        /// </summary>
        /// <returns>The cloned <see cref="BattleUnitModel"/>.</returns>
        public static BattleUnitModel CopyModdedUnit(this StageController instance, Faction faction, BattleUnitModel cloner, int index = -1, int height = -1, XmlVector2 position = null)
        {
            UnitBattleDataModel unitBattleDataModel = new UnitBattleDataModel(instance.GetStageModel(), cloner.UnitData.unitData);
            if (faction > Faction.Enemy)
            {
                FieldInfo fieldInfo = AccessTools.Field(typeof(UnitDataModel), "_ownerSephirah");
                fieldInfo.SetValue(unitBattleDataModel.unitData, instance.CurrentFloor);
            }
            if (height != -1)
            {
                unitBattleDataModel.unitData.customizeData.height = height;
            }
            BattleUnitModel battleUnitModel = BattleObjectManager.CreateDefaultUnit(faction);
            UnitDataModel unitData = unitBattleDataModel.unitData;
            if (index < 0)
            {
                IEnumerable<int> source = from y in BattleObjectManager.instance.GetAliveList(faction)
                                          select y.index;
                int num = 0;
                while (index < 0)
                {
                    if (!source.Contains(num))
                    {
                        index = num;
                        break;
                    }
                    num++;
                }
            }
            battleUnitModel.index = index;
            battleUnitModel.grade = unitData.grade;
            if (faction == Faction.Enemy)
            {
                StageWaveModel currentWaveModel = instance.GetCurrentWaveModel();
                if (position != null)
                {
                    battleUnitModel.formation = new FormationPosition(new FormationPositionXmlData
                    {
                        vector = position
                    });
                }
                else
                {
                    int num2 = Mathf.Min(battleUnitModel.index, currentWaveModel.GetFormation().PostionList.Count - 1);
                    if (num2 < battleUnitModel.index)
                    {
                        Debug.Log("UnitUtil: Index higher than available formation positions, summoning at highest value possible");
                    }
                    battleUnitModel.formation = new FormationPosition(new FormationPositionXmlData
                    {
                        vector = new XmlVector2
                        {
                            x = currentWaveModel.GetFormationPosition(num2).Pos.x,
                            y = currentWaveModel.GetFormationPosition(num2).Pos.y
                        }
                    });
                }
            }
            else
            {
                StageLibraryFloorModel floor = instance.GetStageModel().GetFloor(instance.CurrentFloor);
                if (position != null)
                {
                    battleUnitModel.formation = new FormationPosition(new FormationPositionXmlData
                    {
                        vector = position
                    });
                }
                else
                {
                    int num3 = Mathf.Min(battleUnitModel.index, floor.GetFormation().PostionList.Count - 1);
                    if (num3 < battleUnitModel.index)
                    {
                        Debug.Log("UnitUtil: Index higher than available formation positions, summoning at highest value possible");
                    }
                    battleUnitModel.formation = new FormationPosition(new FormationPositionXmlData
                    {
                        vector = new XmlVector2
                        {
                            x = floor.GetFormationPosition(num3).Pos.x,
                            y = floor.GetFormationPosition(num3).Pos.y
                        }
                    });
                }
            }
            BattleUnitModel result;
            if (unitBattleDataModel.isDead)
            {
                result = battleUnitModel;
            }
            else
            {
                battleUnitModel.SetUnitData(unitBattleDataModel);
                battleUnitModel.OnCreated();
                BattleObjectManager.instance.RegisterUnit(battleUnitModel);
                battleUnitModel.passiveDetail.OnUnitCreated();
                UnitUtil.AddEmotionPassives(battleUnitModel);
                battleUnitModel.cardSlotDetail.RecoverPlayPoint(battleUnitModel.cardSlotDetail.GetMaxPlayPoint());
                battleUnitModel.OnWaveStart();
                UnitUtil.LevelUpEmotion(battleUnitModel, 0);
                UnitUtil.InitializeCombatUI(battleUnitModel);
                battleUnitModel.history.data2 = (int)UnitSpawnMethod.Cloned;
                result = battleUnitModel;
            }
            return result;
        }

        /// <summary>
        /// Summons a new unit.
        /// </summary>
        /// <returns>The summoned <see cref="BattleUnitModel"/>.</returns>
        public static BattleUnitModel AddModdedUnit(this StageController instance, Faction faction, LorId enemyUnitId, int index = -1, int height = -1, XmlVector2 position = null)
        {
            UnitBattleDataModel unitBattleDataModel = UnitBattleDataModel.CreateUnitBattleDataByEnemyUnitId(instance.GetStageModel(), enemyUnitId);
            if (faction > Faction.Enemy)
            {
                FieldInfo fieldInfo = AccessTools.Field(typeof(UnitDataModel), "_ownerSephirah");
                fieldInfo.SetValue(unitBattleDataModel.unitData, instance.CurrentFloor);
            }
            if (height != -1)
            {
                unitBattleDataModel.unitData.customizeData.height = height;
            }
            BattleObjectManager.instance.UnregisterUnitByIndex(faction, index);
            BattleUnitModel battleUnitModel = BattleObjectManager.CreateDefaultUnit(faction);
            UnitDataModel unitData = unitBattleDataModel.unitData;
            if (index < 0)
            {
                IEnumerable<int> source = from y in BattleObjectManager.instance.GetAliveList(faction)
                                          select y.index;
                int num = 0;
                while (index < 0)
                {
                    if (!source.Contains(num))
                    {
                        index = num;
                        break;
                    }
                    num++;
                }
            }
            battleUnitModel.index = index;
            battleUnitModel.grade = unitData.grade;
            if (faction == Faction.Enemy)
            {
                StageWaveModel currentWaveModel = instance.GetCurrentWaveModel();
                if (position != null)
                {
                    battleUnitModel.formation = new FormationPosition(new FormationPositionXmlData
                    {
                        vector = position
                    });
                }
                else
                {
                    int num2 = Mathf.Min(battleUnitModel.index, currentWaveModel.GetFormation().PostionList.Count - 1);
                    if (num2 < battleUnitModel.index)
                    {
                        Debug.Log("UnitUtil: Index higher than available formation positions, summoning at highest value possible");
                    }
                    battleUnitModel.formation = new FormationPosition(new FormationPositionXmlData
                    {
                        vector = new XmlVector2
                        {
                            x = currentWaveModel.GetFormationPosition(num2).Pos.x,
                            y = currentWaveModel.GetFormationPosition(num2).Pos.y
                        }
                    });
                }
            }
            else
            {
                StageLibraryFloorModel floor = instance.GetStageModel().GetFloor(instance.CurrentFloor);
                if (position != null)
                {
                    battleUnitModel.formation = new FormationPosition(new FormationPositionXmlData
                    {
                        vector = position
                    });
                }
                else
                {
                    int num3 = Mathf.Min(battleUnitModel.index, floor.GetFormation().PostionList.Count - 1);
                    if (num3 < battleUnitModel.index)
                    {
                        Debug.Log("UnitUtil: Index higher than available formation positions, summoning at highest value possible");
                    }
                    battleUnitModel.formation = new FormationPosition(new FormationPositionXmlData
                    {
                        vector = new XmlVector2
                        {
                            x = floor.GetFormationPosition(num3).Pos.x,
                            y = floor.GetFormationPosition(num3).Pos.y
                        }
                    });
                }
            }
            BattleUnitModel result;
            if (unitBattleDataModel.isDead)
            {
                result = battleUnitModel;
            }
            else
            {
                battleUnitModel.SetUnitData(unitBattleDataModel);
                battleUnitModel.OnCreated();
                BattleObjectManager.instance.RegisterUnit(battleUnitModel);
                battleUnitModel.passiveDetail.OnUnitCreated();
                UnitUtil.AddEmotionPassives(battleUnitModel);
                battleUnitModel.cardSlotDetail.RecoverPlayPoint(battleUnitModel.cardSlotDetail.GetMaxPlayPoint());
                battleUnitModel.OnWaveStart();
                battleUnitModel.allyCardDetail.DrawCards(battleUnitModel.UnitData.unitData.GetStartDraw());
                UnitUtil.LevelUpEmotion(battleUnitModel, 0);
                UnitUtil.InitializeCombatUI(battleUnitModel);
                battleUnitModel.history.data2 = (int)UnitSpawnMethod.Summoned;
                result = battleUnitModel;
            }
            return result;
        }

        /// <summary>
        /// Summons an unit that can be controlled by the player.
        /// </summary>
        /// <returns>The summoned <see cref="BattleUnitModel"/>.</returns>
        public static BattleUnitModel AddModdedUnitPlayerSide(this StageController instance, UnitModel unit, string packageId, bool playerSide = true)
        {
            StageLibraryFloorModel floor = instance.GetStageModel().GetFloor(instance.CurrentFloor);
            UnitDataModel unitDataModel = new UnitDataModel(new LorId(packageId, unit.Id), playerSide ? floor.Sephirah : SephirahType.None, false);
            unitDataModel.SetCustomName(unit.Name);
            BattleUnitModel battleUnitModel = BattleObjectManager.CreateDefaultUnit(playerSide ? Faction.Player : Faction.Enemy);
            int index = unit.Pos;
	        if (index < 0)
            {
                IEnumerable<int> source = from y in BattleObjectManager.instance.GetAliveList()
                                          select y.index;
                int num = 0;
                while (index < 0)
                {
                    if (!source.Contains(num))
                    {
                        index = num;
                        break;
                    }
                    num++;
                }
            }
            battleUnitModel.index = index;
            battleUnitModel.grade = unitDataModel.grade;
            battleUnitModel.formation = ((unit.CustomPos != null) ? new FormationPosition(new FormationPositionXmlData
            {
                vector = unit.CustomPos
            }) : floor.GetFormationPosition(battleUnitModel.index));
            UnitBattleDataModel unitBattleDataModel = new UnitBattleDataModel(instance.GetStageModel(), unitDataModel);
            unitBattleDataModel.Init();
            battleUnitModel.SetUnitData(unitBattleDataModel);
            battleUnitModel.OnCreated();
            BattleObjectManager.instance.RegisterUnit(battleUnitModel);
            battleUnitModel.passiveDetail.OnUnitCreated();
            UnitUtil.LevelUpEmotion(battleUnitModel, unit.EmotionLevel); 
            if (unit.LockedEmotion)
            {
                battleUnitModel.emotionDetail.SetMaxEmotionLevel(unit.MaxEmotionLevel);
            }
            battleUnitModel.allyCardDetail.DrawCards(battleUnitModel.UnitData.unitData.GetStartDraw());
            battleUnitModel.cardSlotDetail.RecoverPlayPoint(battleUnitModel.cardSlotDetail.GetMaxPlayPoint());
            if (unit.AddEmotionPassive)
            {
                UnitUtil.AddEmotionPassives(battleUnitModel);
            }
            battleUnitModel.OnWaveStart();
            UnitUtil.InitializeCombatUI(battleUnitModel);
            battleUnitModel.history.data2 = (int)UnitSpawnMethod.Summoned;
            return battleUnitModel;
        }

        /// <summary>
        /// Refreshes the UI after summoning <see cref="BattleUnitModel"/>s.
        /// <br></br>(If you're summoning just one or several units at once, call this after you're done summoning units to update the UI.)
        /// </summary>
        /// <param name="forceReturn">Forces the units to return to their assigned position in the formation.</param>
        public static void RefreshCombatUI(bool forceReturn = false)
        {
            foreach (ValueTuple<BattleUnitModel, int> valueTuple in BattleObjectManager.instance.GetList().Select((BattleUnitModel value, int i) => new ValueTuple<BattleUnitModel, int>(value, i)))
            {                
                SingletonBehavior<UICharacterRenderer>.Instance.SetCharacter(valueTuple.Item1.UnitData.unitData, valueTuple.Item2, true, false);
                if (forceReturn)
                {
                    valueTuple.Item1.moveDetail.ReturnToFormationByBlink(true);
                }
            }
            try
            {
                BattleObjectManager.instance.InitUI();
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        /// <summary>
        /// Levels up an unit's emotion level.
        /// </summary>
        /// <param name="unit">Which unit should level up.</param>
        /// <param name="value">How much they should level up.</param>
        public static void LevelUpEmotion(BattleUnitModel unit, int value)
        {
            for (int i = 0; i < value; i++)
            {
                unit.emotionDetail.LevelUp_Forcely(1);
                unit.emotionDetail.CheckLevelUp();
            }
            Singleton<StageController>.Instance.GetCurrentStageFloorModel().team.UpdateCoin();
        }

        /// <summary>
        /// Stores a single unit in Battle Unit Storage.
        /// </summary>
        /// <param name="unit">The unit to be stored.</param>
        /// <param name="packageId">The packageId of the mod.</param>
        public static void StoreBattleUnitModel(BattleUnitModel unit, string packageId)
        {
            Singleton<StageController>.Instance.GetStageModel().GetStageStorageData<List<BattleUnitModel>>(packageId + "_BattleUnitModelStorage", out var output);
            if (output != null && output.Count > 0)
            {
                output.Add(unit);
                Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData(packageId + "_BattleUnitModelStorage", output);
            } else
            {
                var list = new List<BattleUnitModel> { unit };
                Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData(packageId + "_BattleUnitModelStorage", list);
            }

        }

        /// <summary>
        /// Stores multiple units in Battle Unit Storage.
        /// </summary>
        /// <param name="units">The units to be stored.</param>
        /// <param name="packageId">The packageId of the mod.</param>
        public static void StoreBattleUnitModel(List<BattleUnitModel> units, string packageId)
        {
            Singleton<StageController>.Instance.GetStageModel().GetStageStorageData<List<BattleUnitModel>>(packageId + "_BattleUnitModelStorage", out var output);
            if (output != null && output.Count > 0)
            {
                output.AddRange(units);
                Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData(packageId + "_BattleUnitModelStorage", output);
            } else return;      
        }

        /// <param name="packageId">The packageId of the mod.</param>
        /// <returns>A list of <see cref="BattleUnitModel"/>s containing the stored units in Battle Unit Storage.</returns>
        public static List<BattleUnitModel> GetStoredUnitModels(string packageId)
        {
            Singleton<StageController>.Instance.GetStageModel().GetStageStorageData<List<BattleUnitModel>>(packageId + "_BattleUnitModelStorage", out var output);
            if (output != null && output.Count > 0) return output;
            return null;
        }

        /// <summary>
        /// Clears out Battle Unit Storage.
        /// </summary>
        /// <param name="packageId">The packageId of the mod.</param>
        public static void ClearBattleUnitStorage(string packageId)
        {
            Singleton<StageController>.Instance.GetStageModel().SetStageStorgeData(packageId + "_BattleUnitModelStorage", null);
        }

        /// <summary>
        /// Returns the method through which an unit was summoned which can range between <see cref="UnitSpawnMethod.Default"/>, <see cref="UnitSpawnMethod.Summoned"/> or <see cref="UnitSpawnMethod.Cloned"/>.
        /// </summary>
        /// <param name="unit">The unit to check.</param>
        /// <returns>A <see cref="UnitSpawnMethod"/> containing the spawn method of how the unit was spawned.<b><br></br>Only works with units summoned by <see cref="UnitUtil"/>.</b></returns>
        public static UnitSpawnMethod GetUnitSpawnMethod(BattleUnitModel unit)
        {
            return (UnitSpawnMethod)unit.history.data2;
        }

        private static void InitializeCombatUI(BattleUnitModel battleUnitModel)
        {
            try
            {
                BattleCharacterProfileUI battleUI = SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.GetProfileUI(battleUnitModel);
                if (battleUI == null)
                {
                    battleUI = new BattleCharacterProfileUI();
                    battleUI.gameObject.SetActive(true);
                    battleUI.Initialize();
                    battleUI.SetUnitModel(battleUnitModel);
                    if (battleUnitModel.faction == Faction.Player)
                    {
                        SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.allyarray[battleUnitModel.index] = battleUI;
                    }
                    else
                    {
                        SingletonBehavior<BattleManagerUI>.Instance.ui_unitListInfoSummary.enemyarray[battleUnitModel.index] = battleUI;
                    }

                }
            }
            catch (Exception e)
            {
                if (e.Message == "")
                    Debug.Log("UnitUtil: successfully summoned " + battleUnitModel.UnitData.unitData.name + " at index " + battleUnitModel.index);
                else
                    Debug.Log("UnitUtil - failed to initialize UI of summoned unit: " + e.Message);
            }
        }

        private static void AddEmotionPassives(BattleUnitModel unit)
        {
            List<BattleUnitModel> aliveList = BattleObjectManager.instance.GetAliveList(Faction.Player);
            if (aliveList.Any<BattleUnitModel>())
            {
                foreach (BattleEmotionCardModel battleEmotionCardModel in from x in aliveList.FirstOrDefault<BattleUnitModel>().emotionDetail.PassiveList
                                                                          where x.XmlInfo.TargetType == EmotionTargetType.AllIncludingEnemy || x.XmlInfo.TargetType == EmotionTargetType.All
                                                                          select x)
                {
                    bool flag2 = unit.faction != Faction.Enemy || battleEmotionCardModel.XmlInfo.TargetType > EmotionTargetType.All;
                    if (flag2)
                    {
                        unit.emotionDetail.ApplyEmotionCard(battleEmotionCardModel.XmlInfo, false);
                    }
                }
            }
        }
    }

    public static class AftermathUtilityExtensions
    {
        /// <summary>
        /// Set <u><see cref="BattleUnitView.deadEvent"/></u> to this in order to make an unit explode on death.
        /// </summary>
        public static void ExplodeOnDeath(BattleUnitView view)
        {
            if (view.model.UnitData.floorBattleData.param3 != 113413411)
            {
                SoundEffectPlayer soundEffectPlayer = SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Creature/MatchGirl_Explosion");
                if (soundEffectPlayer != null)
                {
                    soundEffectPlayer.SetGlobalPosition(view.WorldPosition);
                }
                var effect = SingletonBehavior<DiceEffectManager>.Instance.CreateCreatureEffect("1/MatchGirl_Footfall", 1f, view, null, 2f);
                effect.transform.localScale *= 3.5f;
                effect.AttachEffectLayer();
                view.model.UnitData.floorBattleData.param3 = 113413411;
                view.StartDeadEffect(false);
                view.model._deadSceneBlock = true;
            }
        }

        /// <summary>
        /// Returns a random element from a given list.
        /// </summary>
        public static T SelectOneRandom<T>(this IList<T> list)
        {
            return list[Singleton<System.Random>.Instance.Next(list.Count)];
        }

        /// <summary>
        /// Returns several random elements from a given list.
        /// </summary>
        /// <param name="count">The number of random elements to be returned</param>
        /// <returns>A new list containing randomly selected elements from the original list. The same element may be picked multiple times.</returns>
        public static List<T> SelectManyRandom<T>(this IList<T> list, int count)
        {
            List<T> list2 = new List<T>();
            for (int x = 0; x < count; x++)
            {
                list2.Append(list[Singleton<System.Random>.Instance.Next(list.Count)]);
            }
            return list2;
        }

        /// <summary>
        /// Sorts a list of <see cref="BattleDiceCardModel"/>s (Combat Pages) by their current cost, in crescent order.
        /// </summary>
        /// <param name="list">The list to be sorted.</param>
        public static void SortByCost(this List<BattleDiceCardModel> list)
        {
            list.Sort((BattleDiceCardModel x, BattleDiceCardModel y) => x.CurCost - y.CurCost);
        }

        /// <summary>
        /// Shuffles a list.
        /// </summary>
        public static List<T> Shuffle<T>(this IList<T> list)
        {
            return list.OrderBy(x => Singleton<System.Random>.Instance.Next()).ToList();
        }

        /// <summary>
        /// Checks to see if a card or one of its dice posses a given keyword.
        /// </summary>
        /// <param name="card">The card to be tested.</param>
        /// <param name="keyword">The keyword to test the card for.</param>
        /// <returns>Returns <see langword="true"/> if the <paramref name="card"/> or any of its dice have the given <paramref name="keyword"/>. Returns <see langword="false"/> otherwise.</returns>
        public static bool CheckForKeyword(this BattleDiceCardModel card, string keyword)
        {
            if (card != null)
            {
                DiceCardXmlInfo xmlData = card.XmlData;
                if (xmlData == null)
                {
                    return false;
                }
                if (xmlData.Keywords.Contains(keyword))
                {
                    return true;
                }
                List<string> abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
                for (int i = 0; i < abilityKeywords.Count; i++)
                {
                    if (abilityKeywords[i] == keyword)
                    {
                        return true;
                    }
                }
                foreach (DiceBehaviour diceBehaviour in card.GetBehaviourList())
                {
                    List<string> abilityKeywords_byScript = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(diceBehaviour.Script);
                    for (int j = 0; j < abilityKeywords_byScript.Count; j++)
                    {
                        if (abilityKeywords_byScript[j] == keyword)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Adds a buf <u>this Scene</u> to a character's buf list. 
        /// </summary>
        /// <param name="stacks">How many stacks of the buf to add.</param>
        /// <param name="args">Arguments to be fed into the BattleUnitBuf's constructor.<br></br>(In case a new instance needs to be made AND the buf has a custom constructor. Otherwise, leave empty.)</param>
        /// <typeparam name="T">Class type of the BattleUnitBuf.</typeparam>
        public static void AddBuf<T>(this BattleUnitBufListDetail bufList, int stacks, params object[] args)
        {
            BattleUnitBuf buf = bufList.GetActivatedBufList().Find(x => x is T);
            if (buf != null && !buf.IsDestroyed() && bufList.CanAddBuf(buf))
                buf.stack += stacks;
            else
            {
                BattleUnitBuf buf2 = Activator.CreateInstance(typeof(T), args) as BattleUnitBuf;
                buf2.stack = stacks;
                bufList.AddBuf(buf2);
            }
        }

        /// <summary>
        /// Adds a buf <u>next Scene</u> to a character's buf list. 
        /// </summary>
        /// <param name="stacks">How many stacks of the buf to add.</param>
        /// <param name="args">Arguments to be fed into the BattleUnitBuf's constructor.<br></br>(In case a new instance needs to be made AND the buf has a custom constructor. Otherwise, leave empty.)</param>
        /// <typeparam name="T">Class type of the BattleUnitBuf.</typeparam>
        public static void AddReadyBuf<T>(this BattleUnitBufListDetail bufList, int stacks, params object[] args)
        {
            BattleUnitBuf buf = bufList.GetReadyBufList().Find(x => x is T);
            if (buf != null && !buf.IsDestroyed())
                buf.stack += stacks;
            else
            {
                BattleUnitBuf buf2 = Activator.CreateInstance(typeof(T), args) as BattleUnitBuf;
                buf2.stack = stacks;
                bufList.AddReadyBuf(buf2);
            }
        }

        /// <summary>
        /// Adds a buf <u>the Scene after the next</u> to a character's buf list. 
        /// </summary>
        /// <param name="stacks">How many stacks of the buf to add.</param>
        /// <param name="args">Arguments to be fed into the BattleUnitBuf's constructor.<br></br>(In case a new instance needs to be made AND the buf has a custom constructor. Otherwise, leave empty.)</param>
        /// <typeparam name="T">Class type of the BattleUnitBuf.</typeparam>
        public static void AddReadyReadyBuf<T>(this BattleUnitBufListDetail bufList, int stacks, params object[] args)
        {
            BattleUnitBuf buf = bufList.GetReadyReadyBufList().Find(x => x is T);
            if (buf != null && !buf.IsDestroyed())
                buf.stack += stacks;
            else
            {
                BattleUnitBuf buf2 = Activator.CreateInstance(typeof(T), args) as BattleUnitBuf;
                buf2.stack = stacks;
                bufList.AddReadyReadyBuf(buf2);
            }
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

    public static class VenomCardModel
    {
        // Add 1 venom to hand function
        public static void AddVenomToHand(BattleUnitModel bruh)
        {
            LorId card;

            if (bruh.allyCardDetail.GetHand().FindAll(x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 60109)).Count > 0)
            { // This if/else prevents the character from not getting any venom in case there's already a Venom EX in hand.
                card = RandomUtil.SelectOne<LorId>(new List<LorId> {
                    new LorId(AftermathCollectionInitializer.packageId, 60102),
                    new LorId(AftermathCollectionInitializer.packageId, 60103),
                    new LorId(AftermathCollectionInitializer.packageId, 60104)
                });
            }
            else
            {
                card = RandomUtil.SelectOne<LorId>(new List<LorId>{
                    new LorId(AftermathCollectionInitializer.packageId, 60102),
                    new LorId(AftermathCollectionInitializer.packageId, 60103),
                    new LorId(AftermathCollectionInitializer.packageId, 60104),
                    new LorId(AftermathCollectionInitializer.packageId, 60109)
                });
            }

            bruh.allyCardDetail.AddNewCard(card);
        }

        public static bool IsVenom(BattleDiceCardModel card)
        {
            return card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 60102) || card.GetID() ==
                new LorId(AftermathCollectionInitializer.packageId, 60103) || card.GetID() ==
                new LorId(AftermathCollectionInitializer.packageId, 60104) || card.GetID() ==
                new LorId(AftermathCollectionInitializer.packageId, 60109);
        }
    }

    public static class ChemsCardModel
    {
        // Add 1 chem to hand function
        public static void AddChemToHand(BattleUnitModel bruh)
        {
            LorId card;

            if (bruh.allyCardDetail.GetHand().FindAll(x => x.GetID() == new LorId(AftermathCollectionInitializer.packageId, 20101)).Count > 0)
            { // This if/else prevents the character from not getting any venom in case there's already a Home Run in hand.
                card = RandomUtil.SelectOne<LorId>(new List<LorId> {
                    new LorId(AftermathCollectionInitializer.packageId, 20102),
                    new LorId(AftermathCollectionInitializer.packageId, 20103),
                    new LorId(AftermathCollectionInitializer.packageId, 20100)
                });
            }
            else
            {
                card = RandomUtil.SelectOne<LorId>(new List<LorId>{
                    new LorId(AftermathCollectionInitializer.packageId, 20101),
                    new LorId(AftermathCollectionInitializer.packageId, 20102),
                    new LorId(AftermathCollectionInitializer.packageId, 20103),
                    new LorId(AftermathCollectionInitializer.packageId, 20100)
                });
            }

            bruh.allyCardDetail.AddNewCard(card);
        }

        public static bool IsChemCard(BattleDiceCardModel card)
        {
            return card.GetID() == new LorId(AftermathCollectionInitializer.packageId, 20101) || card.GetID() ==
                new LorId(AftermathCollectionInitializer.packageId, 20102) || card.GetID() ==
                new LorId(AftermathCollectionInitializer.packageId, 20103) || card.GetID() ==
                new LorId(AftermathCollectionInitializer.packageId, 20100);
        }
    }

    /// <summary>
    /// This utility was made by StartUp for the Da'at Floor Mod in order to create On Scroll effects.<br></br>
    /// It has been used with due permission from both StartUp himself and the current coordinator<br></br>
    /// of the Da'at Floor Mod (at the time of writing, Dememenic).<br></br><br></br>
    /// If you plan on taking this for yourself, please credit StartUp for ScrollAbilityUtil, and keep this summary tag here.
    /// <br></br>Oh yeah, and as a bonus, you're gonna need to reference UnityEngine.InputLegacyModule beside the regular CoreModule
    /// <br></br>in order for this to work.
    /// </summary>
    internal static class ScrollAbilityUtil
    {
        internal static ScrollAbilityManager GetScrollAbilityManager(this BattleUnitModel model)
            => model.view.GetComponent<ScrollAbilityManager>() ?? model.view.gameObject.AddComponent<ScrollAbilityManager>();

        internal static void AddScrollAbility(this BattleUnitModel model, BattleDiceCardModel card, ScrollAbilityBase ability)
            => model.GetScrollAbilityManager().AddAbility(card, ability);

        internal static void AddScrollAbility<T>(this BattleUnitModel model, BattleDiceCardModel card) where T : ScrollAbilityBase, new()
            => model.AddScrollAbility(card, new T());
    }

    public class ScrollAbilityManager : MonoBehaviour
    {
        private bool scrolled;
        private BattleUnitModel owner;
        private Dictionary<BattleDiceCardModel, ScrollAbilityBase> _dict = new Dictionary<BattleDiceCardModel, ScrollAbilityBase>();
        
        private void Awake() 
            => owner = gameObject.GetComponent<BattleUnitView>().model;
        
        private void Update()
        {
            if (scrolled) scrolled = !(BattleCamManager.Instance.scrollable = true);

            if (StageController.Instance.Phase != StageController.StagePhase.ApplyLibrarianCardPhase) return;

            var handUI = BattleManagerUI.Instance.ui_unitCardsInHand;
            if (handUI.SelectedModel != owner) return;

            var uiCard = handUI.GetSelectedCard();
            if (uiCard == null) return;

            var card = uiCard.CardModel;
            if (card == null || !_dict.ContainsKey(card)) return;

            scrolled = BattleCamManager.Instance.scrollable;
            BattleCamManager.Instance.scrollable = false;

            var scroll = Input.mouseScrollDelta.y;
            if (Mathf.Abs(scroll) < float.Epsilon) return;

            if (scroll > 0)
                _dict[card].OnScrollUp(owner, card);
            else
                _dict[card].OnScrollDown(owner, card);

            uiCard.SetCard(card);
            ((Singleton<StageController>.Instance.AllyFormationDirection == Direction.RIGHT)
                ? SingletonBehavior<BattleManagerUI>.Instance.ui_unitInformationPlayer
                : SingletonBehavior<BattleManagerUI>.Instance.ui_unitInformation).ShowPreviewCard(card, true);
            uiCard.KeywordListUI.Activate();
        }


        public void AddAbility(BattleDiceCardModel card, ScrollAbilityBase ability, bool overrideIfExist = false)
        {
            if (card is null) throw new ArgumentNullException(nameof(card));
            if (ability is null) throw new ArgumentNullException(nameof(ability));
            if (_dict.ContainsKey(card) && !overrideIfExist) return;
            _dict[card] = ability;
        }
    }

    public class ScrollAbilityBase
    {
        public virtual void OnScrollUp(BattleUnitModel unit, BattleDiceCardModel self) { }
        public virtual void OnScrollDown(BattleUnitModel unit, BattleDiceCardModel self) { }
    }
    

    /// <summary>
    /// This ability went unused for one reason one another- likely due to being reworked at some point in development.
    /// </summary>
    public class UnusedAbility : Attribute { }

    #endregion
}
