// Decompiled with JetBrains decompiler
// Type: PlaybookUIMobileManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

public class PlaybookUIMobileManager : MonoBehaviour
{
  public RectTransform playbookRecTransform;
  public bool overrideHeight;
  public float height = 400f;
  public bool overrideWidth;
  public float width = 1000f;
  public GameObject controls;
  private Vector2 sizeDelta;
  public Scrollbar offensiveScrollbar;
  public Scrollbar defensiveScrollbar;
  public Scrollbar formationScrollbar;
  public PlaybookManager playbookManager;

  private void OnEnable()
  {
    if (!this.overrideHeight)
      return;
    this.sizeDelta = this.playbookRecTransform.sizeDelta;
    if (this.overrideHeight && this.controls.activeInHierarchy)
      this.sizeDelta.y = this.height;
    if (this.overrideWidth)
      this.sizeDelta.x = this.width;
    this.playbookRecTransform.sizeDelta = this.sizeDelta;
  }

  public void ButtonUp()
  {
    if (this.offensiveScrollbar.gameObject.activeInHierarchy)
      this.playbookManager.ScrollPlaySelectUp();
    if (this.defensiveScrollbar.gameObject.activeInHierarchy)
      this.playbookManager.ScrollPlaySelectUp();
    if (!this.formationScrollbar.gameObject.activeInHierarchy)
      return;
    this.formationScrollbar.value += this.playbookManager.GetFormationScrollAmount();
  }

  public void ButtonDown()
  {
    if (this.offensiveScrollbar.gameObject.activeInHierarchy)
      this.playbookManager.ScrollPlaySelectDown();
    if (this.defensiveScrollbar.gameObject.activeInHierarchy)
      this.playbookManager.ScrollPlaySelectDown();
    if (!this.formationScrollbar.gameObject.activeInHierarchy)
      return;
    this.formationScrollbar.value -= this.playbookManager.GetFormationScrollAmount();
  }
}
