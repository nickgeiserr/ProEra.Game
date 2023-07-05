// Decompiled with JetBrains decompiler
// Type: ReceiverRouteUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using UnityEngine;

public class ReceiverRouteUI : MonoBehaviour
{
  [SerializeField]
  private PlayerAI player;
  private TrailRenderer _tr;

  private void Awake()
  {
    this._tr = this.GetComponent<TrailRenderer>();
    this._tr.emitting = false;
    this.player.OnInPrePlayPosition += new Action<float[]>(this.DrawOffRoute);
  }

  public void DrawOffRoute(float[] path)
  {
    Debug.Log((object) nameof (DrawOffRoute));
    this._tr.emitting = false;
    if (!this.gameObject.activeSelf)
      return;
    this._tr.forceRenderingOff = false;
    this.StartCoroutine(this.ContinueDrawingOffRoute(path));
  }

  private IEnumerator ContinueDrawingOffRoute(float[] path)
  {
    ReceiverRouteUI receiverRouteUi = this;
    Vector3 vector3_1 = new Vector3(receiverRouteUi.player.GetPlayStartPosition().x, 0.0f, receiverRouteUi.player.GetPlayStartPosition().z);
    receiverRouteUi.transform.position = vector3_1;
    receiverRouteUi._tr.emitting = true;
    receiverRouteUi._tr.Clear();
    yield return (object) null;
    for (int i = 1; i + 1 < path.Length; i += 3)
    {
      Vector3 vector3_2 = new Vector3(MatchManager.instance.ballHashPosition + path[i], 0.0f, ProEra.Game.MatchState.BallOn.Value + path[i + 1] * (float) global::Game.OffensiveFieldDirection);
      receiverRouteUi.transform.position = vector3_2;
      yield return (object) null;
    }
  }
}
