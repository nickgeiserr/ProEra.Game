// Decompiled with JetBrains decompiler
// Type: ProEra.Game.UI.Screens.Shared.Settings.AccountLoginChecker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game.Sources.SeasonMode.SeasonTablet;
using UnityEngine;

namespace ProEra.Game.UI.Screens.Shared.Settings
{
  [RequireComponent(typeof (CanvasTabManager))]
  public class AccountLoginChecker : MonoBehaviour
  {
    private CanvasTabManager _canvasTabManager;

    private SaveKeycloakUserData _keycloakUserData => PersistentSingleton<SaveManager>.Instance.GetKeycloakUserData();

    private void Awake() => this._canvasTabManager = this.GetComponent<CanvasTabManager>();

    private void OnEnable() => this._canvasTabManager.SimulateTabPress(this._keycloakUserData.AuthToken == string.Empty ? 4 : 0);
  }
}
