// Decompiled with JetBrains decompiler
// Type: PlayCallWristband
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System;
using UnityEngine;

public class PlayCallWristband : MonoBehaviour
{
  [SerializeField]
  private PlayerProfile playerProfile;
  private bool _isOverlappingHand;
  private Renderer _renderer;
  private readonly int _shaderKeyColorID = Shader.PropertyToID("_Color");

  [SerializeField]
  public bool IsOverlappingHand
  {
    get => this._isOverlappingHand;
    set => this._isOverlappingHand = value;
  }

  private void Start()
  {
    this._renderer = (Renderer) this.gameObject.GetComponent<MeshRenderer>();
    this.WristbandColorOnOnValueChanged(this.playerProfile.Customization.WristbandColor.Value);
    this.playerProfile.Customization.WristbandColor.OnValueChanged += new Action<Color>(this.WristbandColorOnOnValueChanged);
  }

  private void WristbandColorOnOnValueChanged(Color obj)
  {
    if (!((UnityEngine.Object) this._renderer != (UnityEngine.Object) null))
      return;
    MaterialPropertyBlock properties = new MaterialPropertyBlock();
    this._renderer.GetPropertyBlock(properties);
    properties.SetColor(this._shaderKeyColorID, obj);
    this._renderer.SetPropertyBlock(properties);
  }

  private void OnDestroy() => this.playerProfile.Customization.WristbandColor.OnValueChanged -= new Action<Color>(this.WristbandColorOnOnValueChanged);

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer != LayerMask.NameToLayer("UserAvatar") || !other.name.Contains("hands"))
      return;
    this._isOverlappingHand = true;
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.layer != LayerMask.NameToLayer("UserAvatar") || !other.name.Contains("hands"))
      return;
    this._isOverlappingHand = false;
  }
}
