// Decompiled with JetBrains decompiler
// Type: SeasonTabletCanvasManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

public class SeasonTabletCanvasManager : MonoBehaviour
{
  [SerializeField]
  private SeasonTabletCanvasManager.SSeasonTabletCanvas[] _canvases;
  [SerializeField]
  private GameObject _topButtons;

  public void ShowPage(SeasonTabletCanvasManager.ESeasonTabletCanvas type)
  {
    for (int index = 0; index < this._canvases.Length; ++index)
    {
      this._canvases[index].canvas.EnableFollow(false);
      if (this._canvases[index].type == type)
        this._canvases[index].canvas.EnableFollow(true);
    }
    this._topButtons.SetActive(true);
    if (type <= SeasonTabletCanvasManager.ESeasonTabletCanvas.Rankings)
      return;
    this._topButtons.SetActive(false);
  }

  public enum ESeasonTabletCanvas
  {
    Home,
    Roster,
    Stats,
    Rankings,
    PassingPopup,
    RushingPopup,
    ReceivingPopup,
    DefensivePopup,
    KickingPopup,
  }

  [Serializable]
  private struct SSeasonTabletCanvas
  {
    public SeasonTabletCanvasManager.ESeasonTabletCanvas type;
    public SeasonTabletCanvas canvas;
  }
}
