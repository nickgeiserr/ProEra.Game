// Decompiled with JetBrains decompiler
// Type: UDB.LoadingOutTransitionHelper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  [RequireComponent(typeof (Transition))]
  public class LoadingOutTransitionHelper : CachedMonoBehaviour, ITransitionHelper
  {
    public bool needsFirstUpdate;
    private Transition _transition;

    private Transition transition
    {
      get
      {
        if ((Object) this._transition == (Object) null)
          this._transition = this.GetComponent<Transition>();
        return this._transition;
      }
    }

    private void OnEnable() => NotificationCenter.AddListener("AddOutLoadingTransitions", new Callback(this.AddOutLoadingTransitions));

    private void OnDisable() => NotificationCenter.RemoveListener("AddOutLoadingTransitions", new Callback(this.AddOutLoadingTransitions));

    public void AddOutLoadingTransitions()
    {
      if (!((Object) this.transition != (Object) null))
        return;
      SceneTransitionPlayer.AddAsOutSceneTransition(this.transition);
    }

    void ITransitionHelper.Prepare() => SceneTransitionPlayer.Attach(this.transform);

    void ITransitionHelper.Play()
    {
    }

    bool ITransitionHelper.Linger() => true;

    bool ITransitionHelper.NeedsFirstUpdate() => this.needsFirstUpdate;
  }
}
