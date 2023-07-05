// Decompiled with JetBrains decompiler
// Type: LandingSpotScript
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class LandingSpotScript : MonoBehaviour
{
  public Transform trans;

  private void Start()
  {
  }

  public void ShowPassLocation(Vector3 pos)
  {
    this.trans.position = new Vector3(pos.x, 0.06f, pos.z);
    MatchManager.instance.playersManager.ballLandingSpotGO.SetActive(true);
  }

  public void Hide() => MatchManager.instance.playersManager.ballLandingSpotGO.SetActive(false);
}
