// Decompiled with JetBrains decompiler
// Type: FootballVR.WorldPopupText
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace FootballVR
{
  public class WorldPopupText : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private bool _destroyOnComplete;
    [SerializeField]
    private bool _isNetworked;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();

    public event Action<WorldPopupText> OnComplete;

    private void Start()
    {
      if (!this._destroyOnComplete)
        return;
      this.OnComplete += (Action<WorldPopupText>) (popup =>
      {
        if (this._isNetworked & PhotonView.Get(this.gameObject).IsMine)
          PhotonNetwork.Destroy(this.gameObject);
        else
          UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      });
    }

    private void OnDisable() => this._routineHandle.Stop();

    private void OnDestroy() => this._routineHandle.Stop();

    public void Display(string msg, float duration = 1f, float fadeOut = 0.75f)
    {
      this._text.alpha = 1f;
      this._text.text = msg;
      this._text.gameObject.SetActive(true);
      this._routineHandle.Run(this.WaitAndHideRoutine(duration, fadeOut));
    }

    private IEnumerator WaitAndHideRoutine(float delay, float fadeOut)
    {
      yield return (object) new WaitForSeconds(delay);
      this.Hide(fadeOut);
    }

    public void Hide(float fadeOut) => this._routineHandle.Run(this.FadeOutRoutine(fadeOut));

    private IEnumerator FadeOutRoutine(float dur)
    {
      WorldPopupText worldPopupText = this;
      float start = Time.time;
      float end = start + dur;
      float time;
      while ((double) (time = Time.time) < (double) end)
      {
        float num = Mathf.InverseLerp(end, start, time);
        worldPopupText._text.alpha = num;
        yield return (object) null;
      }
      Action<WorldPopupText> onComplete = worldPopupText.OnComplete;
      if (onComplete != null)
        onComplete(worldPopupText);
      worldPopupText.gameObject.SetActive(false);
    }
  }
}
