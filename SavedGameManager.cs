// Decompiled with JetBrains decompiler
// Type: SavedGameManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

public class SavedGameManager : MonoBehaviour
{
  public static SavedGameManager self;
  private string saveVersionFile = "SaveVersion";

  private string saveDataPath => SaveIO.DefaultFolderPath;
}
