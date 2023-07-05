// Decompiled with JetBrains decompiler
// Type: FootballVR.LoadingEnvironment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.Playables;

namespace FootballVR
{
  public class LoadingEnvironment : TransitionEnvironment
  {
    [SerializeField]
    private MeshRenderer _renderer;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private PlayableDirector _playableDirector;

    private void Awake() => this.SetVisibility(false);

    public void SetVisibility(bool visible)
    {
      this._renderer.enabled = visible;
      this._canvas.gameObject.SetActive(visible);
      if (visible)
        this._playableDirector.Play();
      else
        this._playableDirector.Stop();
    }
  }
}
