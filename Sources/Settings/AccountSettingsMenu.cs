// Decompiled with JetBrains decompiler
// Type: Sources.Settings.AccountSettingsMenu
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Web;
using System;
using TMPro;
using UnityEngine;

namespace Sources.Settings
{
  public class AccountSettingsMenu : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _activeUserText;
    [SerializeField]
    private SettingsValueButtonUI _accountLinkButton;

    private void Awake() => this.ValidateInspectorBinding();

    private void OnEnable()
    {
      PlayerApi.DeleteUserSuccess += new System.Action(this.UpdatePageStatus);
      PlayerApi.DeleteUserFailure += new System.Action(this.UpdatePageStatus);
      this.UpdatePageStatus();
    }

    private void OnDisable()
    {
      PlayerApi.DeleteUserSuccess -= new System.Action(this.UpdatePageStatus);
      PlayerApi.DeleteUserFailure -= new System.Action(this.UpdatePageStatus);
    }

    private void ValidateInspectorBinding()
    {
    }

    private void UpdatePageStatus()
    {
      if (PersistentSingleton<PlayerApi>.Instance.IsLoggedIn)
        PersistentSingleton<PlayerApi>.Instance.GetDisplayName((Action<string>) (displayName => this._activeUserText.text = displayName));
      else
        this._activeUserText.text = string.Empty;
    }
  }
}
