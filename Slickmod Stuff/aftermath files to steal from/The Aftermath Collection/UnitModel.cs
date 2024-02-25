// Decompiled with JetBrains decompiler
// Type: The_Aftermath_Collection.UnitModel
// Assembly: The Aftermath Collection, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 724F9DCF-E15F-490F-9329-9E641893ABA3
// Assembly location: C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\3017810301\Assemblies\The Aftermath Collection.dll

#nullable disable
namespace The_Aftermath_Collection
{
  public class UnitModel
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public int Pos { get; set; }

    public SephirahType Sephirah { get; set; }

    public bool LockedEmotion { get; set; }

    public int MaxEmotionLevel { get; set; }

    public int EmotionLevel { get; set; }

    public bool AddEmotionPassive { get; set; } = true;

    public bool OnWaveStart { get; set; }

    public XmlVector2 CustomPos { get; set; }
  }
}
