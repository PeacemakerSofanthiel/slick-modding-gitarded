// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.AftermathCollectionInitializer
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

using CustomMapUtility;
using HarmonyLib;
using Mod;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
namespace The_Aftermath_Collection
{
  public class AftermathCollectionInitializer : ModInitializer
  {
    public static readonly string packageId = "calmmagma.theaftermathcollection";
    public static CustomMapHandler aftermathMapHandler;
    public static Dictionary<string, AssetBundle> assetBundles = new Dictionary<string, AssetBundle>();
    public static readonly int[] exceptions = new int[1]
    {
      100
    };

    public static string Path
    {
      get
      {
        return Singleton<ModContentManager>.Instance.GetModPath(AftermathCollectionInitializer.packageId);
      }
    }

    public override void OnInitializeMod()
    {
      base.OnInitializeMod();
      Harmony.CreateAndPatchAll(typeof (AftermathCollectionPatches), "calmmagma.theaftermathcollection");
      AftermathCollectionInitializer.aftermathMapHandler = CustomMapHandler.GetCMU("calmmagma.theaftermathcollection");
      foreach (FileInfo file in new DirectoryInfo(AftermathCollectionInitializer.Path + "/Resource/AssetBundles").GetFiles())
        AftermathCollectionInitializer.assetBundles.Add(file.Name, AssetBundle.LoadFromFile(file.FullName));
      Singleton<ModContentManager>.Instance.GetErrorLogs().RemoveAll((Predicate<string>) (log => log.Contains("The same assembly name already exists.")));
    }

    public static void PlaySound(
      AudioClip audio,
      Transform transform,
      float VolumnControl = 1.5f,
      bool loop = false)
    {
      BattleEffectSound battleEffectSound = UnityEngine.Object.Instantiate<BattleEffectSound>(SingletonBehavior<BattleSoundManager>.Instance.effectSoundPrefab, transform);
      float num1 = 1f;
      if ((UnityEngine.Object) SingletonBehavior<BattleSoundManager>.Instance != (UnityEngine.Object) null)
        num1 = SingletonBehavior<BattleSoundManager>.Instance.VolumeFX * VolumnControl;
      AudioClip ac = audio;
      double volume = (double) num1;
      int num2 = loop ? 1 : 0;
      battleEffectSound.Init(ac, (float) volume, num2 != 0);
    }

    public static Texture2D ThumbnailPatcher(int keypageId)
    {
      Texture2D tex = new Texture2D(4, 4);
      switch (keypageId)
      {
        case 100:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/The Vermillion Dragon/ClothCustom/Icon.png"));
          return tex;
        case 10101:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Cassandra/ClothCustom/Icon.png"));
          return tex;
        case 10102:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Norinco Workshop Greenhorn/ClothCustom/Icon.png"));
          return tex;
        case 20101:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Dave/ClothCustom/Icon.png"));
          return tex;
        case 20102:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Amanita Junkie/ClothCustom/Icon.png"));
          return tex;
        case 50101:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Liam/ClothCustom/Icon.png"));
          return tex;
        case 50102:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Zwei Section 3 Fixer/ClothCustom/Icon.png"));
          return tex;
        case 50103:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Tamora/ClothCustom/Icon.png"));
          return tex;
        case 50104:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Daniel/ClothCustom/Icon.png"));
          return tex;
        case 60101:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Silvio/ClothCustom/Icon.png"));
          return tex;
        case 60102:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Benito_player/ClothCustom/Icon.png"));
          return tex;
        case 60103:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Amanita Family Member/ClothCustom/Icon.png"));
          return tex;
        case 60104:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Yellow Ties Officer/ClothCustom/Icon.png"));
          return tex;
        case 60201:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Yijun/ClothCustom/Icon.png"));
          return tex;
        case 60202:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Ex-Liu Fixer 1/ClothCustom/Icon.png"));
          return tex;
        case 60203:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Ex-Liu Fixer 2/ClothCustom/Icon.png"));
          return tex;
        case 60204:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Bruce/ClothCustom/Icon.png"));
          return tex;
        case 60205:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Hunan/ClothCustom/Icon.png"));
          return tex;
        case 60303:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Elis/ClothCustom/Icon.png"));
          return tex;
        case 60401:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Mobius Office Fixer/ClothCustom/Icon.png"));
          return tex;
        case 60402:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Mobius Office Charger/ClothCustom/Icon.png"));
          return tex;
        case 60403:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Lance/ClothCustom/Icon.png"));
          return tex;
        case 60404:
          tex.LoadImage(File.ReadAllBytes(AftermathCollectionInitializer.Path + "/Resource/CharacterSkin/Ace/ClothCustom/Icon.png"));
          return tex;
        default:
          return tex;
      }
    }
  }
}
