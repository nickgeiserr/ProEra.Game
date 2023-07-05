// Decompiled with JetBrains decompiler
// Type: TB12.UI.LevelItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class LevelItem : MonoBehaviour
  {
    [SerializeField]
    private TouchButton _button;
    [SerializeField]
    private GameObject _activeGo;
    [SerializeField]
    private GameObject _inactiveGo;
    [SerializeField]
    private TextMeshProUGUI _activeText;
    [SerializeField]
    private TextMeshProUGUI _inactiveText;
    [SerializeField]
    private GameObject _bg_not_full;
    [SerializeField]
    private GameObject _bg_full;
    [SerializeField]
    private GameObject _one_star;
    [SerializeField]
    private GameObject _two_star;
    [SerializeField]
    private GameObject _three_star;
    private int _id;

    public event Action<int> OnLevelSelected;

    private void OnEnable() => this._button.onClick += new Action(this.ButtonHandler);

    private void OnDisable() => this._button.onClick -= new Action(this.ButtonHandler);

    public void SetActive(bool value) => this.gameObject.SetActive(value);

    public void Setup(int challengeId, bool locked, string text, int stars)
    {
      this.set_active(!locked);
      this.Configure();
      this._id = challengeId;
      this._activeText.text = text;
      this._inactiveText.text = text;
      switch (stars)
      {
        case 1:
          this._bg_not_full.SetActive(true);
          this._one_star.SetActive(true);
          break;
        case 2:
          this._bg_not_full.SetActive(true);
          this._two_star.SetActive(true);
          break;
        case 3:
          this._bg_full.SetActive(true);
          this._three_star.SetActive(true);
          break;
      }
      this.SetActive(true);
    }

    private void Configure()
    {
      this._bg_not_full.SetActive(false);
      this._bg_full.SetActive(false);
      this._one_star.SetActive(false);
      this._two_star.SetActive(false);
      this._three_star.SetActive(false);
    }

    private void set_active(bool active)
    {
      this._activeGo.SetActive(active);
      this._inactiveGo.SetActive(!active);
    }

    private void ButtonHandler()
    {
      Action<int> onLevelSelected = this.OnLevelSelected;
      if (onLevelSelected == null)
        return;
      onLevelSelected(this._id);
    }
  }
}
