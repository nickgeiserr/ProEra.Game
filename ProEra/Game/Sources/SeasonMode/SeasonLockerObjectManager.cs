// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.SeasonMode.SeasonLockerObjectManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace ProEra.Game.Sources.SeasonMode
{
  public class SeasonLockerObjectManager : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] _activeSeasonObjects;
    [SerializeField]
    private GameObject[] _endSeasonObjects;

    private void Start() => SeasonModeManager.self.OnInitComplete += new System.Action(this.Init);

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) SeasonModeManager.self != (UnityEngine.Object) null))
        return;
      SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
    }

    private void Init()
    {
      if (SeasonModeManager.self.SeasonOverForUser)
        this.ShowEndSeasonProps();
      else
        this.ShowMidSeasonProps();
    }

    private void ShowMidSeasonProps()
    {
      foreach (GameObject endSeasonObject in this._endSeasonObjects)
      {
        if ((UnityEngine.Object) endSeasonObject != (UnityEngine.Object) null)
          endSeasonObject.SetActive(false);
      }
      foreach (GameObject activeSeasonObject in this._activeSeasonObjects)
      {
        if ((UnityEngine.Object) activeSeasonObject != (UnityEngine.Object) null)
          activeSeasonObject.SetActive(true);
      }
    }

    private void ShowEndSeasonProps()
    {
      foreach (GameObject activeSeasonObject in this._activeSeasonObjects)
      {
        if ((UnityEngine.Object) activeSeasonObject != (UnityEngine.Object) null)
          activeSeasonObject.SetActive(false);
      }
      foreach (GameObject endSeasonObject in this._endSeasonObjects)
      {
        if ((UnityEngine.Object) endSeasonObject != (UnityEngine.Object) null)
          endSeasonObject.SetActive(true);
      }
    }
  }
}
