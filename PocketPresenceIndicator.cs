// Decompiled with JetBrains decompiler
// Type: PocketPresenceIndicator
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System;
using UnityEngine;

public class PocketPresenceIndicator : MonoBehaviour
{
  [SerializeField]
  private Collider _collider;
  [SerializeField]
  private Material _material;
  [SerializeField]
  private float _materialStatusInPocket = 0.75f;
  [SerializeField]
  private float _materialStatusOutOfPocket;
  [SerializeField]
  private bool _inPocket;
  [SerializeField]
  private Material _secondPocketMaterial;
  private static readonly int HighLightColor = Shader.PropertyToID("_HighlightColor");
  private static readonly int InnerColor = Shader.PropertyToID("_InnerColor");
  public Action<bool> OnPocketStatusChanged;

  public bool InPocket => this._inPocket;

  private void Start() => this.Initialize();

  private void Update() => this.SetInPocket(this._collider.bounds.Contains(PersistentSingleton<GamePlayerController>.Instance.position));

  private void SetInPocket(bool inPocket, bool forceChange = false)
  {
    if (!(inPocket != this._inPocket | forceChange))
      return;
    this._inPocket = inPocket;
    if ((UnityEngine.Object) this._material != (UnityEngine.Object) null)
      this._material.SetFloat("_Status", inPocket ? this._materialStatusInPocket : this._materialStatusOutOfPocket);
    if ((UnityEngine.Object) this._secondPocketMaterial != (UnityEngine.Object) null)
      this._secondPocketMaterial.SetColor(PocketPresenceIndicator.InnerColor, inPocket ? Color.green : Color.red);
    Action<bool> pocketStatusChanged = this.OnPocketStatusChanged;
    if (pocketStatusChanged == null)
      return;
    pocketStatusChanged(inPocket);
  }

  public void Initialize() => this.SetInPocket(this._collider.bounds.Contains(PersistentSingleton<GamePlayerController>.Instance.position), true);
}
