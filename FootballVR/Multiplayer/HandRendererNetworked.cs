// Decompiled with JetBrains decompiler
// Type: FootballVR.Multiplayer.HandRendererNetworked
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Photon.Pun;
using UnityEngine;

namespace FootballVR.Multiplayer
{
  public class HandRendererNetworked : HandRenderer, IPunObservable
  {
    [SerializeField]
    private PhotonView _photonView;
    private bool _localView;

    private void Awake() => this._localView = this._photonView.IsMine;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
      if (stream.IsWriting)
      {
        stream.SendNext((object) this._flex);
        stream.SendNext((object) this._pointBlend);
        stream.SendNext((object) this._thumbsUpBlend);
        stream.SendNext((object) this._pinch);
      }
      else
      {
        this._flex = (float) stream.ReceiveNext();
        this._pointBlend = (float) stream.ReceiveNext();
        this._thumbsUpBlend = (float) stream.ReceiveNext();
        this._pinch = (float) stream.ReceiveNext();
      }
    }

    protected override void Update()
    {
      if (this._localView)
        base.Update();
      else
        this.UpdateAnimStates();
    }
  }
}
