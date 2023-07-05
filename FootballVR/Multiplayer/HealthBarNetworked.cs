// Decompiled with JetBrains decompiler
// Type: FootballVR.Multiplayer.HealthBarNetworked
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.Multiplayer
{
  public class HealthBarNetworked : MonoBehaviour, IPunObservable
  {
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private Slider _healthLossSlider;
    [SerializeField]
    private Image _healthFillImage;
    [SerializeField]
    private Image _healthLossFillImage;
    [SerializeField]
    private float _healthLossDrainRate = 1f;
    [SerializeField]
    private bool _isFrontHealthBar;
    private PhotonView _photonView;
    [Header("DEBUG")]
    [SerializeField]
    private float _currentHealth = 100f;
    [SerializeField]
    private float _maxHealth = 100f;
    [SerializeField]
    private bool _displayHealthBar;
    private readonly LinksHandler _linksHandler = new LinksHandler();
    private readonly RoutineHandle _routineHandle = new RoutineHandle();

    private void Awake()
    {
      this._photonView = PhotonView.Get(this.gameObject);
      this._linksHandler.AddLink(MultiplayerEvents.DisplayHealthBar.Link<bool>((Action<bool>) (display => this._displayHealthBar = display)));
      this._linksHandler.AddLink(MultiplayerEvents.SetHealth.Link<int, float>(new Action<int, float>(this.SetHealthBar)));
      this._linksHandler.AddLink(MultiplayerEvents.UpdateHealth.Link<int, float>(new Action<int, float>(this.UpdateHealthBar)));
    }

    public void SetHealthBar(int playerID, float maxHealth = 10f)
    {
      if (this._photonView.OwnerActorNr != playerID)
        return;
      this._healthSlider.maxValue = maxHealth;
      this._healthLossSlider.maxValue = maxHealth;
      this._healthSlider.value = maxHealth;
      this._healthLossSlider.value = maxHealth;
      this._maxHealth = maxHealth;
      this._currentHealth = this._maxHealth;
    }

    public void UpdateHealthBar(int playerID, float targetHealth)
    {
      if (this._photonView.OwnerActorNr != playerID || (double) targetHealth >= (double) this._currentHealth || !this._displayHealthBar)
        return;
      this._routineHandle.Stop();
      this._routineHandle.Run(this.DrainHealth(targetHealth));
    }

    private IEnumerator DrainHealth(float targetHealth)
    {
      this.DisplayHealth(true);
      this._healthSlider.value = targetHealth;
      while ((double) this._currentHealth >= (double) targetHealth)
      {
        this._healthLossSlider.value = this._currentHealth;
        this._currentHealth -= 0.005f * this._healthLossDrainRate;
        yield return (object) null;
      }
      this.DisplayHealth(false);
    }

    private void DisplayHealth(bool state)
    {
      Debug.Log((object) ("Setting health bar display to : " + state.ToString()));
      if (this._photonView.IsMine)
      {
        if (!this._isFrontHealthBar)
          state = false;
      }
      else if (this._isFrontHealthBar)
        state = false;
      if ((bool) (UnityEngine.Object) this._healthLossFillImage)
        this._healthLossFillImage.enabled = state;
      if (!(bool) (UnityEngine.Object) this._healthFillImage)
        return;
      this._healthFillImage.enabled = state;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
  }
}
