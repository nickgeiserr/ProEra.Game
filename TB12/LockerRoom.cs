// Decompiled with JetBrains decompiler
// Type: TB12.LockerRoom
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballWorld;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vars;

namespace TB12
{
  public class LockerRoom : MonoBehaviour
  {
    [SerializeField]
    private UniformLogoStore _customUniformStore;
    [SerializeField]
    private AvatarGraphics _previewAvatar;
    [SerializeField]
    private AvatarGraphics _previewAvatarFemale;
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private Image _teamImage;
    private PlayerProfile _playerProfile;
    private UniformStore _uniformStore;
    private readonly LinksHandler _linksHandler = new LinksHandler();

    private void Awake()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      this._playerProfile = SaveManager.GetPlayerProfile();
      this._playerProfile.Initialize();
      Transform transform = this.transform;
      PersistentSingleton<GamePlayerController>.Instance.SetPositionAndRotation(transform.position, transform.rotation);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this._playerProfile.Customization.NameColor.Link<Color>(new Action<Color>(this.HandleNameColorChanged)),
        this._playerProfile.Customization.BodyType.Link<EBodyType>(new Action<EBodyType>(this.HandleBodyType)),
        (EventHandle) UIHandle.Link(this._nameText, this._playerProfile.Customization.LastName)
      });
    }

    private void OnDestroy()
    {
      this._linksHandler.Clear();
      this._uniformStore = (UniformStore) null;
      this._playerProfile = (PlayerProfile) null;
    }

    private void HandleUniformChanged()
    {
      ETeamUniformId uniform = (ETeamUniformId) (Variable<ETeamUniformId>) this._playerProfile.Customization.Uniform;
      ETeamUniformFlags flag = this._playerProfile.Customization.HomeUniform.Value ? ETeamUniformFlags.Home : ETeamUniformFlags.Away;
      FootballWorld.UniformConfig uniformConfig = this._uniformStore.GetUniformConfig(uniform, flag);
      this._previewAvatar.avatarGraphicsData.baseMap.Value = uniformConfig.BasemapAlternative;
      this._previewAvatarFemale.avatarGraphicsData.baseMap.Value = uniformConfig.BasemapAlternative;
      UniformLogo uniformLogo = this._customUniformStore.GetUniformLogo(uniform);
      if (uniformLogo == null)
        return;
      this._teamImage.sprite = uniformLogo.teamLogo;
    }

    private void HandleNameColorChanged(Color color) => this._nameText.color = color;

    private void HandleBodyType(EBodyType type)
    {
      bool flag = type == EBodyType.Male;
      this._previewAvatar.gameObject.SetActive(flag);
      this._previewAvatarFemale.gameObject.SetActive(!flag);
      Transform transform = flag ? this._previewAvatar.transform : this._previewAvatarFemale.transform;
      transform.localPosition = new Vector3(0.0f, 0.0f, 2.941468f);
      transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 160f, 0.0f));
    }
  }
}
