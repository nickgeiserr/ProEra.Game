// Decompiled with JetBrains decompiler
// Type: GUIManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
  public static GUIManager instance;
  private bool allowPlaySelect;

  private void Awake()
  {
    if (!((Object) GUIManager.instance == (Object) null))
      return;
    GUIManager.instance = this;
  }

  public void CallStart()
  {
    this.allowPlaySelect = true;
    Globals.GameOver.SetValue(false);
  }

  public void ReturnToPreviousMenu(int player = 1)
  {
  }

  public void ShowFeedbackForm()
  {
  }

  public void StartSecondHalf()
  {
  }

  public void EndGame()
  {
  }

  public void ShowPrePlayHomeWindows()
  {
  }

  public void HidePrePlayWindows()
  {
  }

  public void ShowParentPauseWindow()
  {
  }

  public void HideParentPauseWindow()
  {
  }

  public bool IsParentPauseWindowVisible() => false;

  public void SetAllowPlaySelect(bool allow) => this.allowPlaySelect = allow;

  public bool IsPlaySelectAllowed() => this.allowPlaySelect;

  public void SetPlaySelectDelay() => this.StartCoroutine(this.SetPlaySelectDelayCoroutine());

  private IEnumerator SetPlaySelectDelayCoroutine()
  {
    this.allowPlaySelect = false;
    yield return (object) null;
    this.allowPlaySelect = true;
  }

  public void ToggleCameraType()
  {
  }

  public void SelectUIItem(Selectable item) => ControllerManagerGame.self.SelectUIElement(item);

  private IEnumerator DelayedSelectButton(Selectable b)
  {
    yield return (object) null;
    this.SelectUIItem(b);
  }
}
