// Decompiled with JetBrains decompiler
// Type: CanvasGroupFadeSwitch
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

[RequireComponent(typeof (CanvasGroup))]
public class CanvasGroupFadeSwitch : MonoBehaviour
{
  public bool m_autoPlay;
  public float m_autoPlayDelay = 8f;
  private float m_autoPlayTimer;
  private CanvasGroup m_thisCanvasGroup;
  private int m_currentSubCanvas;
  private bool m_isFading;

  [ExecuteInEditMode]
  private void OnEnable()
  {
    this.m_isFading = false;
    this.m_thisCanvasGroup = this.GetComponent<CanvasGroup>();
    this.enabled = (Object) this.m_thisCanvasGroup != (Object) null;
    this.FadeTo0();
  }

  private void Update()
  {
    if (!this.m_autoPlay)
      return;
    this.m_autoPlayTimer += Time.deltaTime;
    if ((double) this.m_autoPlayTimer < (double) this.m_autoPlayDelay)
      return;
    this.m_autoPlayTimer = 0.0f;
    this.FadeToCanvas(this.m_currentSubCanvas + 1, true);
  }

  [ExecuteInEditMode]
  public void FadeToCanvas(int a_canvasIndex, bool a_modulo = false) => this.StartCoroutine(this.FadeToCanvasRoutine(a_canvasIndex, a_modulo));

  [ExecuteInEditMode]
  private IEnumerator FadeToCanvasRoutine(int a_canvasIndex, bool a_modulo = false)
  {
    if (!this.m_isFading)
    {
      this.m_isFading = true;
      float fadeSpeed = 2f;
      this.m_thisCanvasGroup.alpha = 1f;
      while ((double) this.m_thisCanvasGroup.alpha > 0.0)
      {
        this.m_thisCanvasGroup.alpha -= fadeSpeed * Time.deltaTime;
        yield return (object) new WaitForEndOfFrame();
      }
      this.DisableAllSubCanvases();
      this.EnableCanvasAtIndex(a_canvasIndex, a_modulo);
      while ((double) this.m_thisCanvasGroup.alpha < 1.0)
      {
        this.m_thisCanvasGroup.alpha += fadeSpeed * Time.deltaTime;
        yield return (object) new WaitForEndOfFrame();
      }
      this.m_isFading = false;
    }
  }

  private void DisableAllSubCanvases()
  {
    int childCount = this.transform.childCount;
    for (int index = 0; index < childCount; ++index)
      this.transform.GetChild(index).gameObject.SetActive(false);
  }

  private void EnableCanvasAtIndex(int a_index, bool a_modulo = false)
  {
    int childCount = this.transform.childCount;
    int num = a_index;
    if (a_index < 0 || !a_modulo && a_index >= childCount)
      return;
    if (a_modulo)
      num = a_index % childCount;
    this.m_currentSubCanvas = num;
    this.transform.GetChild(this.m_currentSubCanvas).gameObject.SetActive(true);
  }

  [ContextMenu("FadeTo0")]
  public void FadeTo0() => this.FadeToCanvas(0);

  [ContextMenu("FadeTo1")]
  public void FadeTo1() => this.FadeToCanvas(1);

  [ContextMenu("FadeTo2")]
  public void FadeTo2() => this.FadeToCanvas(2);

  [ContextMenu("FadeTo3")]
  public void FadeTo3() => this.FadeToCanvas(3);

  [ContextMenu("FadeTo4")]
  public void FadeTo4() => this.FadeToCanvas(4);
}
