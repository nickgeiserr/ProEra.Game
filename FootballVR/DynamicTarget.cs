// Decompiled with JetBrains decompiler
// Type: FootballVR.DynamicTarget
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using UnityEngine;

namespace FootballVR
{
  public class DynamicTarget : MonoBehaviour, IThrowTarget
  {
    [SerializeField]
    private ThrowManager _throwManager;
    [SerializeField]
    private PhotonView _photonView;
    [SerializeField]
    private float _height = 0.02f;
    [SerializeField]
    private Transform _targetTx;
    [SerializeField]
    private MeshRenderer[] _renderers;
    private bool _state;
    private readonly Lazy<MaterialPropertyBlock> _mpb = new Lazy<MaterialPropertyBlock>((Func<MaterialPropertyBlock>) (() => new MaterialPropertyBlock()));
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private bool _isPriority;

    public bool Active => this._state;

    public float height => this._height;

    private void Awake()
    {
      if ((UnityEngine.Object) this._photonView == (UnityEngine.Object) null || this._photonView.Owner == null)
      {
        this.gameObject.SetActive(false);
      }
      else
      {
        object obj;
        if (this._photonView.Owner.CustomProperties.TryGetValue((object) "dynamicTarget", out obj))
          this.gameObject.SetActive((bool) obj);
        else
          this.gameObject.SetActive(false);
      }
    }

    private void OnEnable() => this._throwManager.RegisterTarget((IThrowTarget) this);

    private void OnDisable() => this._throwManager.UnregisterTarget((IThrowTarget) this);

    public void SetPosition(Vector3 pos)
    {
      pos.y = this._height;
      this.transform.position = pos;
    }

    public void SetState(bool state)
    {
      if (this._state == state)
        return;
      this._photonView.RPC("SetDynamicTargetStateRPC", RpcTarget.All, (object) state);
      Photon.Realtime.Player owner = this._photonView.Owner;
      Hashtable customProperties = owner.CustomProperties;
      customProperties[(object) "dynamicTarget"] = (object) state;
      owner.SetCustomProperties(customProperties);
    }

    [PunRPC]
    public void SetDynamicTargetStateRPC(bool state)
    {
      this._state = state;
      this.gameObject.SetActive(state);
    }

    public Vector3 EvaluatePosition(float time) => this._targetTx.position;

    public Vector3 GetHitPosition(float time, Vector3 ballPos) => this._targetTx.position;

    public bool IsTargetValid(float timeOffset = 0.0f) => true;

    public bool TargetValidForAI => true;

    public bool ReceiveBall(EventData eventData) => false;

    public float minCatchTime => 0.0f;

    public string TargetName => nameof (DynamicTarget);

    public float hitRange => 0.1f;

    public bool IsPlayer() => false;

    public void SetColor(Color color)
    {
      color.a = 0.5f;
      MaterialPropertyBlock properties = this._mpb.Value;
      foreach (MeshRenderer renderer in this._renderers)
      {
        renderer.GetPropertyBlock(properties);
        properties.SetColor(DynamicTarget.BaseColor, color);
        renderer.SetPropertyBlock(properties);
      }
    }

    public void ResetState()
    {
      Photon.Realtime.Player owner = this._photonView.Owner;
      Hashtable customProperties = owner.CustomProperties;
      customProperties[(object) "dynamicTarget"] = (object) false;
      owner.SetCustomProperties(customProperties);
    }

    public void SetPriority(bool priority) => this._isPriority = priority;

    public bool IsPriorityTarget() => this._isPriority;

    public void GetReplayData(out ThrowReplayData data) => data = new ThrowReplayData();

    public Vector3 GetIdealThrowTarget() => Vector3.zero;

    public void DrawRange()
    {
    }
  }
}
