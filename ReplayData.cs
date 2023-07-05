// Decompiled with JetBrains decompiler
// Type: ReplayData
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using UnityEngine;

[Serializable]
public class ReplayData
{
  public string version;
  public ThrowReplayData throwData;
  public GameReplayData gameData;
  public Vector3 rawThrowVector;
  public Vector3 cameraForward;

  public void PrintData()
  {
    if (!ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value || this.throwData == null || this.gameData == null)
      return;
    this.version = "0.3.2";
    this.cameraForward = PersistentSingleton<PlayerCamera>.Instance.transform.forward;
    Debug.LogError((object) JsonUtility.ToJson((object) this));
  }

  public static ReplayData LoadData(string data) => JsonUtility.FromJson<ReplayData>(data);
}
