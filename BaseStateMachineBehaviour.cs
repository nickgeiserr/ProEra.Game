// Decompiled with JetBrains decompiler
// Type: BaseStateMachineBehaviour
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using MxM;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachineBehaviour : StateMachineBehaviour
{
  protected PlayerIdentity _player;
  protected PlayerTelegraphy _telegraphy;
  protected StateMachine _stateMachine;
  private bool _isInited;
  private MxMAnimator _mxmAnimator;
  public static Transform gazeTargetsParent;
  private GazeController _gazeController;
  private static Dictionary<GazeController, Transform> _gazeTargetDictionary = new Dictionary<GazeController, Transform>();

  protected MxMAnimator MxmAnimator
  {
    get
    {
      if ((Object) this._mxmAnimator == (Object) null)
        this._mxmAnimator = this._telegraphy.GetComponent<MxMAnimator>();
      return this._mxmAnimator;
    }
  }

  protected GazeController gazeController
  {
    get
    {
      if ((Object) this._gazeController == (Object) null)
        this._gazeController = this._telegraphy.GetComponent<GazeController>();
      return this._gazeController;
    }
  }

  protected Gameboard gameboard => ScriptableSingleton<Gameboard>.Instance;

  protected Transform GazeTarget
  {
    get
    {
      Transform transform;
      if (!BaseStateMachineBehaviour._gazeTargetDictionary.TryGetValue(this.gazeController, out transform))
      {
        transform = new GameObject("GazeTargetGO").transform;
        transform.SetParent(BaseStateMachineBehaviour.gazeTargetsParent);
        BaseStateMachineBehaviour._gazeTargetDictionary.Add(this.gazeController, transform);
      }
      return transform;
    }
  }

  protected string AddRequireTagPrefix(string tagName) => "RequireTag_" + tagName;

  private void Initialize(Animator animator)
  {
    this._stateMachine = animator.transform.parent.GetComponent<StateMachine>();
    this._player = animator.transform.parent.GetComponent<PlayerIdentity>();
    this._telegraphy = animator.transform.parent.GetComponent<PlayerTelegraphy>();
    this._isInited = true;
  }

  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (!this._isInited)
      this.Initialize(animator);
    this._stateMachine.InvokeStateChangeEvent((StateMachineBehaviour) this);
  }

  public override void OnStateUpdate(
    Animator animator,
    AnimatorStateInfo stateInfo,
    int layerIndex)
  {
    this._stateMachine.UpdateValues(this._telegraphy);
  }

  public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    if (this._isInited)
      return;
    this.Initialize(animator);
  }
}
