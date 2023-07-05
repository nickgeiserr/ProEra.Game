// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.UIAnchoring
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using UnityEngine;

namespace FootballVR.UI
{
  public class UIAnchoring : PersistentSingleton<UIAnchoring>
  {
    [SerializeField]
    private Canvas _frontCanvas;
    [SerializeField]
    private Canvas _leftCanvas;
    [SerializeField]
    private Canvas _rightCanvas;
    [SerializeField]
    private Canvas _pauseMenuCanvas;
    private Transform _gamePlayerTx;

    public static Canvas FrontCanvas => PersistentSingleton<UIAnchoring>.Instance._frontCanvas;

    public static Canvas LeftCanvas => PersistentSingleton<UIAnchoring>.Instance._leftCanvas;

    public static Canvas RightCanvas => PersistentSingleton<UIAnchoring>.Instance._rightCanvas;

    public static Canvas PauseMenuCanvas => PersistentSingleton<UIAnchoring>.Instance._pauseMenuCanvas;

    public static Vector3 PlayerPosition => PersistentSingleton<UIAnchoring>.Instance._gamePlayerTx.position;

    protected override void Awake()
    {
      base.Awake();
      this._gamePlayerTx = this.transform.parent;
    }
  }
}
