// Decompiled with JetBrains decompiler
// Type: TB12.HeadsetDetachTracker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using TB12.AppStates;
using UnityEngine;

namespace TB12
{
  public class HeadsetDetachTracker : MonoBehaviour
  {
    [SerializeField]
    private LobbyState _lobbyState;
    [SerializeField]
    private float _unmountDelay = 30f;
    private readonly RoutineHandle _routineHandle = new RoutineHandle();

    [ContextMenu("MOUNT")]
    private void MountedHandler()
    {
      this._routineHandle.Stop();
      Debug.Log((object) "[INFO]: Mounted");
      this._routineHandle.Run(this.MountedRoutine());
    }

    [ContextMenu("UNMOUNT")]
    private void UnmountedHandler() => this._routineHandle.Run(this.UnmountedRoutine());

    private IEnumerator MountedRoutine()
    {
      yield return (object) new WaitForSeconds(1f);
    }

    private IEnumerator UnmountedRoutine()
    {
      float step = this._unmountDelay / 4f;
      Debug.Log((object) "[INFO]: Unmountnig started...");
      for (int i = 0; i < 4; ++i)
      {
        yield return (object) new WaitForSeconds(step);
        Debug.Log((object) string.Format("[INFO]: Unmounting in: {0}", (object) (float) ((double) this._unmountDelay - (double) i * (double) step)));
      }
      Debug.Log((object) "[INFO]: Unmounting now");
      ActivationState.ResetToLobby.Trigger();
    }
  }
}
