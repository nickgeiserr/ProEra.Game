// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.GameStates.LockerRoom.MainMenu.LeaderboardElement
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace ProEra.Game.Sources.GameStates.LockerRoom.MainMenu
{
  [RequireComponent(typeof (RectTransform))]
  public class LeaderboardElement : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _rankText;
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private TextMeshProUGUI _pointsText;
    [SerializeField]
    private TextMeshProUGUI _pointsLabelText;
    [SerializeField]
    private Color _highlightColor = new Color(33f / 256f, (float) byte.MaxValue / 256f, 147f / 256f);
    private Color _defaultColor;
    private RectTransform _rectTransform;
    private int _rank;
    private string _name;
    private float _points;
    private string _pointsLabel;
    private bool _isHighlighted;

    public RectTransform RectTransform => this._rectTransform;

    public bool IsHighlighted
    {
      get => this._isHighlighted;
      set
      {
        this._isHighlighted = value;
        this.BaseColor = this._isHighlighted ? this._highlightColor : this._defaultColor;
      }
    }

    public int Rank
    {
      get => this._rank;
      set
      {
        this._rank = value;
        this._rankText.text = value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    public string Name
    {
      get => this._name;
      set
      {
        this._name = value;
        this._nameText.text = value;
      }
    }

    public float Points
    {
      get => this._points;
      set
      {
        this._points = value;
        this._pointsText.text = value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    public string PointsLabel
    {
      get => this._pointsLabel;
      set
      {
        this._pointsLabel = value;
        this._pointsLabelText.text = value;
      }
    }

    private Color BaseColor
    {
      set
      {
        this._rankText.color = value;
        this._nameText.color = value;
        this._pointsText.color = value;
        this._pointsLabelText.color = value;
      }
    }

    private void Awake()
    {
      this._rectTransform = this.GetComponent<RectTransform>();
      this.ValidateInspectorBinding();
      this._defaultColor = this._nameText.color;
    }

    private void ValidateInspectorBinding()
    {
    }
  }
}
