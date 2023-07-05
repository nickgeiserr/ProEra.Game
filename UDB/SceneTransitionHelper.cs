// Decompiled with JetBrains decompiler
// Type: UDB.SceneTransitionHelper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  [RequireComponent(typeof (Transition))]
  public class SceneTransitionHelper : CachedMonoBehaviour, ITransitionHelper
  {
    public TransitionType transitionType;
    public bool needsFirstUpdate;
    private Transition _transition;
    private string _sceneName;
    private bool addedIn;
    private bool addedOut;

    private Transition transition
    {
      get
      {
        if ((Object) this._transition == (Object) null)
          this._transition = this.GetComponent<Transition>();
        return this._transition;
      }
    }

    private string sceneName
    {
      get
      {
        if (this._sceneName == null)
          this._sceneName = this.gameObject.scene.name;
        return this._sceneName;
      }
    }

    private void Awake()
    {
      if (!((Object) this.transition != (Object) null) || this.transitionType != TransitionType.In || this.addedIn)
        return;
      this.transition.ActivateRender(true);
      SceneTransitionPlayer.AddAsInSceneTransition(this.transition);
      SceneTransitionPlayer.WaitInSceneTransition();
      this.addedIn = true;
    }

    private void OnEnable()
    {
      NotificationCenter.AddListener("AddInSceneTransitions", new Callback(this.AddInSceneTransitions));
      NotificationCenter.AddListener("AddOutSceneTransitions", new Callback(this.AddOutSceneTransitions));
    }

    private void OnDisable()
    {
      NotificationCenter.RemoveListener("AddInSceneTransitions", new Callback(this.AddInSceneTransitions));
      NotificationCenter.RemoveListener("AddOutSceneTransitions", new Callback(this.AddOutSceneTransitions));
    }

    public void AddInSceneTransitions()
    {
      if (!((Object) this.transition != (Object) null) || this.transitionType != TransitionType.Out || this.addedIn || SceneRegistry.ValueForKey(SceneManager.activeSceneName) != SceneRegistry.ValueForKey(this.sceneName))
        return;
      SceneTransitionPlayer.AddAsInSceneTransition(this.transition);
      this.addedIn = true;
    }

    public void AddOutSceneTransitions()
    {
      if (!((Object) this.transition != (Object) null) || this.transitionType != TransitionType.Out || this.addedOut || SceneRegistry.ValueForKey(SceneManager.activeSceneName) != SceneRegistry.ValueForKey(this.sceneName))
        return;
      SceneTransitionPlayer.AddAsOutSceneTransition(this.transition);
      this.addedOut = true;
    }

    void ITransitionHelper.Prepare() => SceneTransitionPlayer.Attach(this.transform);

    void ITransitionHelper.Play()
    {
    }

    bool ITransitionHelper.Linger() => true;

    bool ITransitionHelper.NeedsFirstUpdate() => this.needsFirstUpdate;
  }
}
