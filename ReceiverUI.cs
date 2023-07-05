// Decompiled with JetBrains decompiler
// Type: ReceiverUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using TB12;
using TMPro;
using UnityEngine;

public class ReceiverUI : MonoBehaviour
{
  [SerializeField]
  private Transform _lightPillar;
  [SerializeField]
  private Transform _avatar;
  [SerializeField]
  private TrailRenderer _routeTrail;
  [Header("Configuration Settings")]
  [SerializeField]
  private bool _useEligibilityText;
  private MeshRenderer _lightPillarRenderer;
  private Color[] _lightPillarColors;
  private TextMeshPro _lightPillarText;
  private List<SkinnedMeshRenderer> _avatarRenderers = new List<SkinnedMeshRenderer>();
  private float _lastEligibility;
  private Color _currentTextColor;
  private Color _currentRouteColor;
  private MaterialPropertyBlock _mpb;
  private const short OUTLINE_MAT_INDEX = 1;
  private static readonly int Status = Shader.PropertyToID("_Status");
  private static readonly int Fade = Shader.PropertyToID("_Fade");

  private void Start()
  {
    this._lightPillarRenderer = this._lightPillar.GetComponent<MeshRenderer>();
    this._lightPillarColors = new Color[3]
    {
      this._lightPillarRenderer.material.GetColor("_Color02"),
      this._lightPillarRenderer.material.GetColor("_Color01"),
      this._lightPillarRenderer.material.GetColor("_Color")
    };
    this._lightPillarText = this._lightPillar.GetChild(0).GetComponent<TextMeshPro>();
    if (!this._useEligibilityText)
      this._lightPillarText.gameObject.SetActive(false);
    for (int index = 0; index < this._avatarRenderers.Count; ++index)
    {
      SkinnedMeshRenderer component = this._avatar.GetChild(index).GetComponent<SkinnedMeshRenderer>();
      if ((Object) component != (Object) null)
        this._avatarRenderers.Add(component);
    }
    this._currentRouteColor = Color.black;
    this._mpb = new MaterialPropertyBlock();
  }

  public void UpdateEligibility(float eligibility)
  {
    if ((double) this._lastEligibility == (double) eligibility)
      return;
    this._lightPillarRenderer.GetPropertyBlock(this._mpb);
    this._mpb.SetFloat(ReceiverUI.Status, eligibility);
    this._lightPillarRenderer.SetPropertyBlock(this._mpb);
    if (this._useEligibilityText)
    {
      this._currentTextColor = (double) eligibility < 0.5 ? Color.Lerp(this._lightPillarColors[0], this._lightPillarColors[1], eligibility * 2f) : Color.Lerp(this._lightPillarColors[1], this._lightPillarColors[2], (float) (((double) eligibility - 0.5) * 2.0));
      this._lightPillarText.color = this._currentTextColor;
      this._lightPillarText.text = (eligibility * 100f).ToString("##0");
    }
    for (int index = 0; index < this._avatarRenderers.Count; ++index)
    {
      SkinnedMeshRenderer avatarRenderer = this._avatarRenderers[index];
      if (avatarRenderer.materials.Length > 1)
      {
        avatarRenderer.GetPropertyBlock(this._mpb, 1);
        this._mpb.SetFloat(ReceiverUI.Status, eligibility);
        this._mpb.SetFloat(ReceiverUI.Fade, 1f);
        avatarRenderer.SetPropertyBlock(this._mpb, 1);
      }
    }
    this._currentRouteColor.r = eligibility;
    this._routeTrail.startColor = this._currentRouteColor;
    this._routeTrail.endColor = this._currentRouteColor;
    this._lastEligibility = eligibility;
  }

  public void SetActiveUI(bool active)
  {
    this._lightPillar.gameObject.SetActive(active);
    for (int index = 0; index < this._avatarRenderers.Count; ++index)
    {
      SkinnedMeshRenderer avatarRenderer = this._avatarRenderers[index];
      if (avatarRenderer.materials.Length > 1)
      {
        avatarRenderer.GetPropertyBlock(this._mpb, 1);
        this._mpb.SetFloat(ReceiverUI.Fade, active ? 1f : 0.0f);
        avatarRenderer.SetPropertyBlock(this._mpb, 1);
      }
    }
    this._routeTrail.forceRenderingOff = !active || !(bool) DevControls.ReceiverRouteUI;
  }
}
