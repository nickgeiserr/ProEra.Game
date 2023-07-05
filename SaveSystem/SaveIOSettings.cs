// Decompiled with JetBrains decompiler
// Type: SaveSystem.SaveIOSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
  [CreateAssetMenu(fileName = "SaveSettings", menuName = "ProEra/SaveSystem/Create Save Settings")]
  public class SaveIOSettings : ScriptableObject
  {
    [Header("How often would like to check for save data in the saving queue?")]
    public int SaveQueueHeartBeatInMSeconds = 1;
    [Header("How often would like to check for save data in the loading queue?")]
    public int LoadQueueHeartBeatInMSeconds = 1;

    public string GetFullSavePath(string fileName) => Path.Combine(SaveIO.DefaultFolderPath, fileName);

    public bool SaveExists(string fileName) => File.Exists(this.GetFullSavePath(fileName));
  }
}
