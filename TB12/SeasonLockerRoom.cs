// Decompiled with JetBrains decompiler
// Type: TB12.SeasonLockerRoom
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Analytics;
using DDL.UniformData;
using FootballVR;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Vars;

namespace TB12
{
  public class SeasonLockerRoom : MonoBehaviour
  {
    private static SeasonLockerRoom _instance = (SeasonLockerRoom) null;
    private static readonly Vector3 AVATAR_POS_RENDER_FOCUS = new Vector3(-1.3039f, 0.0f, 1.872f);
    private static readonly Vector3 AVATAR_POS_OUTSIDE_OF_RENDER = SeasonLockerRoom.AVATAR_POS_RENDER_FOCUS + new Vector3(500f, 0.0f, 0.0f);
    private UniformStore _uniformStore;
    [SerializeField]
    private PlayerUniformsStore _playerUniformsStore;
    [SerializeField]
    private UniformLogoStore _customUniformStore;
    [Space]
    [SerializeField]
    private AssetReference _avatarMaleRef;
    [SerializeField]
    private AssetReference _avatarFemaleRef;
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private Image _teamImage;
    [SerializeField]
    private GameObject _customizationItems;
    [Space(10f)]
    [SerializeField]
    private GameObject[] _interactiveObjects;
    [SerializeField]
    private Transform _trophyRoomPlayerPivot;
    private UniformCapture.Info _avatarInfo;
    private AvatarGraphics _previewAvatarMale;
    private AvatarGraphics _previewAvatarFemale;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public static SeasonLockerRoom Instance
    {
      get
      {
        if ((UnityEngine.Object) SeasonLockerRoom._instance == (UnityEngine.Object) null)
          SeasonLockerRoom._instance = UnityEngine.Object.FindObjectOfType<SeasonLockerRoom>();
        return SeasonLockerRoom._instance;
      }
    }

    private PlayerProfile _playerProfile => SaveManager.GetPlayerProfile();

    private void Awake()
    {
      SeasonLockerRoom._instance = this;
      this._uniformStore = SaveManager.GetUniformStore();
      this.AwakeAsync().ConfigureAwait(false);
    }

    private async Task AwakeAsync()
    {
      SeasonLockerRoom seasonLockerRoom = this;
      seasonLockerRoom._playerProfile.Initialize();
      VRState.LocomotionEnabled.SetValue(true);
      await seasonLockerRoom.CreateAvatarMale();
      await seasonLockerRoom.CreateAvatarFemale();
      ETeamUniformId teamUniformId = TeamDataCache.ToTeamUniformId(SeasonModeManager.self.seasonModeData.UserTeamIndex);
      seasonLockerRoom._linksHandler.SetLinks(new List<EventHandle>()
      {
        seasonLockerRoom._playerProfile.Customization.BodyType.Link<EBodyType>(new Action<EBodyType>(seasonLockerRoom.HandleBodyType)),
        (EventHandle) UIHandle.Link(seasonLockerRoom._nameText, seasonLockerRoom._playerProfile.Customization.LastName)
      });
      seasonLockerRoom._playerProfile.RefreshGloves(teamUniformId);
      if (!seasonLockerRoom._customizationItems.activeSelf)
      {
        seasonLockerRoom._previewAvatarFemale.transform.localPosition = SeasonLockerRoom.AVATAR_POS_OUTSIDE_OF_RENDER;
        seasonLockerRoom._previewAvatarMale.transform.localPosition = SeasonLockerRoom.AVATAR_POS_OUTSIDE_OF_RENDER;
      }
      seasonLockerRoom._playerProfile.Customization.HomeUniform.ForceValue(true);
      if (!seasonLockerRoom._playerProfile.IsLamarVO() || PersistentSingleton<SaveManager>.Instance.gameSettings._didCompleteTutorial)
        return;
      AppSounds.PlayVO(EVOTypes.kLamarLockerRoomIntro);
      VRState.InterationWithUI.SetValue(false);
      seasonLockerRoom._playerProfile.SetIsLamarVO(false);
      PersistentSingleton<SaveManager>.Instance.gameSettings._didCompleteTutorial = true;
      AnalyticEvents.Record<FtueCompletedArgs>(new FtueCompletedArgs());
      AppEvents.SaveGameSettings.Trigger();
      await Task.Delay(TimeSpan.FromSeconds(1.0));
      AppSounds.UpdateBGMVolume.Trigger(0.0f);
      await Task.Delay(TimeSpan.FromSeconds(10.819999694824219));
      AppSounds.PlayVO(EVOTypes.kLamarLockerRoomLocker);
      await Task.Delay(TimeSpan.FromSeconds(14.380000114440918));
      AppSounds.PlayVO(EVOTypes.kLamarLockerRoomArcade);
      await Task.Delay(TimeSpan.FromSeconds(15.63));
      AppSounds.PlayVO(EVOTypes.kLamarLockerRoomTrophy);
      await Task.Delay(TimeSpan.FromSeconds(9.6999998092651367));
      VRState.InterationWithUI.SetValue(true);
      seasonLockerRoom.StartCoroutine(AppSounds.FadeBGM(true, 2f));
    }

    private void RefreshNumberTexts(int value) => this.RefreshUniformTexts(this._playerProfile.Customization.LastName.Value, value);

    private void RefreshLastNameTexts(string value) => this.RefreshUniformTexts(value, this._playerProfile.Customization.UniformNumber.Value);

    private void RefreshUniformTexts(string lastName, int number)
    {
      UniformCapture.ClearAllCachedTextures();
      Texture2D[] textsTexture = UniformCapture.GetTextsTexture((int) TeamDataCache.ToTeamUniformId(SeasonModeManager.self.seasonModeData.UserTeamIndex), new List<int>()
      {
        number
      }, new List<string>() { lastName }, 2);
      if (this._avatarInfo == null)
        this._avatarInfo = new UniformCapture.Info();
      this._avatarInfo.TextsAtlas = (Texture[]) textsTexture;
      this._avatarInfo.PlayerIndex = 0;
    }

    public void SetActiveCustomizationItems(bool active, string data = "")
    {
      if ((UnityEngine.Object) this._customizationItems != (UnityEngine.Object) null && this._customizationItems.activeSelf != active)
        this._customizationItems.SetActive(active);
      if (active)
      {
        this.HandleBodyType(this._playerProfile.Customization.BodyType.Value);
        this._nameText.text = this._playerProfile.Customization.LastName.Value;
        if (string.IsNullOrEmpty(data))
          return;
        if ((UnityEngine.Object) this._previewAvatarMale != (UnityEngine.Object) null)
          this._previewAvatarMale.ConfigAvatar(data);
        if (!((UnityEngine.Object) this._previewAvatarFemale != (UnityEngine.Object) null))
          return;
        this._previewAvatarFemale.ConfigAvatar(data);
      }
      else
      {
        this._previewAvatarFemale.transform.localPosition = SeasonLockerRoom.AVATAR_POS_OUTSIDE_OF_RENDER;
        this._previewAvatarMale.transform.localPosition = SeasonLockerRoom.AVATAR_POS_OUTSIDE_OF_RENDER;
      }
    }

    private void OnDestroy()
    {
      VRState.LocomotionEnabled.SetValue(false);
      this._linksHandler.Clear();
      foreach (GameObject interactiveObject in this._interactiveObjects)
      {
        if ((UnityEngine.Object) interactiveObject != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) interactiveObject);
      }
      if ((UnityEngine.Object) this._previewAvatarFemale != (UnityEngine.Object) null)
        AddressablesData.DestroyGameObject(this._previewAvatarFemale.gameObject);
      if ((UnityEngine.Object) this._previewAvatarMale != (UnityEngine.Object) null)
        AddressablesData.DestroyGameObject(this._previewAvatarMale.gameObject);
      this._uniformStore = (UniformStore) null;
    }

    private void HandleUniformChanged()
    {
      ETeamUniformId uniform = (ETeamUniformId) (Variable<ETeamUniformId>) this._playerProfile.Customization.Uniform;
      ETeamUniformFlags flag = this._playerProfile.Customization.HomeUniform.Value ? ETeamUniformFlags.Home : ETeamUniformFlags.Away;
      this.RefreshAvatarUniform(this._uniformStore.GetUniformConfig(uniform, flag).BasemapAlternative);
      UniformLogo uniformLogo = this._customUniformStore.GetUniformLogo(uniform);
      if (uniformLogo == null)
        return;
      this._teamImage.sprite = uniformLogo.teamLogo;
    }

    private void RefreshAvatarUniform(Texture2D value)
    {
      this._avatarInfo.BaseMap = value;
      if ((UnityEngine.Object) this._previewAvatarMale != (UnityEngine.Object) null)
        this._previewAvatarMale.ApplyUniformData(this._avatarInfo, true);
      if (!((UnityEngine.Object) this._previewAvatarFemale != (UnityEngine.Object) null))
        return;
      this._previewAvatarFemale.ApplyUniformData(this._avatarInfo, true);
    }

    private void HandleBodyType(EBodyType type)
    {
      bool flag = type == EBodyType.Male;
      if ((UnityEngine.Object) this._previewAvatarFemale == (UnityEngine.Object) null || (UnityEngine.Object) this._previewAvatarMale == (UnityEngine.Object) null)
        return;
      if (flag)
      {
        this._previewAvatarFemale.transform.localPosition = SeasonLockerRoom.AVATAR_POS_OUTSIDE_OF_RENDER;
        this._previewAvatarMale.transform.localPosition = SeasonLockerRoom.AVATAR_POS_RENDER_FOCUS;
      }
      else
      {
        this._previewAvatarMale.transform.localPosition = SeasonLockerRoom.AVATAR_POS_OUTSIDE_OF_RENDER;
        this._previewAvatarFemale.transform.localPosition = SeasonLockerRoom.AVATAR_POS_RENDER_FOCUS;
      }
    }

    private async Task CreateAvatarMale()
    {
      if (!((UnityEngine.Object) this._previewAvatarMale == (UnityEngine.Object) null))
        return;
      GameObject gameObject = await AddressablesData.instance.InstantiateAsync(this._avatarMaleRef, SeasonLockerRoom.AVATAR_POS_OUTSIDE_OF_RENDER, Quaternion.Euler(new Vector3(0.0f, 203f, 0.0f)), (Transform) null);
      if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
        return;
      this._previewAvatarMale = gameObject.GetComponent<AvatarGraphics>();
    }

    private async Task CreateAvatarFemale()
    {
      if (!((UnityEngine.Object) this._previewAvatarFemale == (UnityEngine.Object) null))
        return;
      GameObject gameObject = await AddressablesData.instance.InstantiateAsync(this._avatarFemaleRef, SeasonLockerRoom.AVATAR_POS_OUTSIDE_OF_RENDER, Quaternion.Euler(new Vector3(0.0f, 203f, 0.0f)), (Transform) null);
      if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
        return;
      this._previewAvatarFemale = gameObject.GetComponent<AvatarGraphics>();
    }

    public Transform GetTrophyRoomPlayerPivot() => this._trophyRoomPlayerPivot;
  }
}
