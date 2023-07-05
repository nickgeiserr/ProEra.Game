// Decompiled with JetBrains decompiler
// Type: ProEra.Game.FieldController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using Framework.Data;
using Framework.StateManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB12;
using TB12.AppStates;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProEra.Game
{
  public class FieldController : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] _legacyFieldRenderers;
    [SerializeField]
    private GameObject[] _officialFieldRenderers;
    [SerializeField]
    private GameObject[] _sidelinePersonnel;
    [SerializeField]
    private GameObject[] _sideMarkers;
    [SerializeField]
    private MeshRenderer _officialFieldRenderer;
    [SerializeField]
    private AssetReference _gameplayTabletRef;
    private GameObject gameTabletObject;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private GameGraphicsSettings _settings => ScriptableSingleton<GameGraphicsSettings>.Instance;

    private void Awake()
    {
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this._settings.LegacyField.Link<bool>(new Action<bool>(this.ApplyFieldType)),
        this._settings.SidelinePersonnel.Link<bool>((Action<bool>) (unused => this.UpdateGraphicsState()), false),
        this._settings.SidelineMarkers.Link<bool>((Action<bool>) (unused => this.UpdateGraphicsState()), false),
        WorldState.CrowdEnabled.Link<bool>((Action<bool>) (unused => this.UpdateGraphicsState()))
      });
      if (StateManager<EAppState, GameState>.IsMultiplayerCurrentState())
        return;
      this.CreateGameplayTablet().SafeFireAndForget();
    }

    private async Task CreateGameplayTablet() => this.gameTabletObject = await AddressablesData.instance.InstantiateAsync(this._gameplayTabletRef, Vector3.zero, Quaternion.Euler(Vector3.zero), (Transform) null);

    private void UpdateGraphicsState()
    {
      this.ApplySidelineMarkers((bool) WorldState.CrowdEnabled && (bool) this._settings.SidelineMarkers);
      this.ApplySidelinePersonnel((bool) WorldState.CrowdEnabled && (bool) this._settings.SidelinePersonnel);
      FieldController.CheckForSuperbowl((Renderer) this._officialFieldRenderer);
    }

    private void ApplySidelineMarkers(bool state)
    {
      if (this._sideMarkers == null)
        return;
      foreach (GameObject sideMarker in this._sideMarkers)
      {
        if ((UnityEngine.Object) sideMarker != (UnityEngine.Object) null)
          sideMarker.SetActive(state);
      }
    }

    private void ApplySidelinePersonnel(bool state)
    {
      if (this._sidelinePersonnel == null)
        return;
      foreach (GameObject gameObject in this._sidelinePersonnel)
        gameObject.SetActive(state);
    }

    private void OnDestroy()
    {
      this._linksHandler.Clear();
      if (!((UnityEngine.Object) this.gameTabletObject != (UnityEngine.Object) null))
        return;
      AddressablesData.DestroyGameObject(this.gameTabletObject);
    }

    private void ApplyFieldType(bool legacyField)
    {
      foreach (GameObject legacyFieldRenderer in this._legacyFieldRenderers)
      {
        if ((UnityEngine.Object) legacyFieldRenderer != (UnityEngine.Object) null)
          legacyFieldRenderer.SetActive(legacyField);
      }
      foreach (GameObject officialFieldRenderer in this._officialFieldRenderers)
      {
        if ((UnityEngine.Object) officialFieldRenderer != (UnityEngine.Object) null)
          officialFieldRenderer.SetActive(!legacyField);
      }
    }

    public static void CheckForSuperbowl(Renderer targetRenderer)
    {
      if ((UnityEngine.Object) targetRenderer == (UnityEngine.Object) null || AppState.SeasonMode.Value == ESeasonMode.kUnknown || SeasonModeManager.self.GetCurrentGameRound() != SeasonModeGameRound.SuperBowl)
        return;
      int id1 = Shader.PropertyToID("_EndZone01");
      int id2 = Shader.PropertyToID("_EndZone02");
      targetRenderer.sharedMaterial.SetTexture(id1, (Texture) TeamResourcesManager.GetFieldLogo(PersistentData.GetCompUniform()));
      targetRenderer.sharedMaterial.SetTexture(id2, (Texture) TeamResourcesManager.GetFieldLogo(PersistentData.GetUserUniform()));
    }
  }
}
