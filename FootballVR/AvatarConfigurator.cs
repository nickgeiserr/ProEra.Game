// Decompiled with JetBrains decompiler
// Type: FootballVR.AvatarConfigurator
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FootballVR
{
  [RequireComponent(typeof (AvatarGraphics))]
  public class AvatarConfigurator : MonoBehaviour
  {
    [SerializeField]
    private string _name = "Jackson";
    [SerializeField]
    private Color _skinColor = Color.white;
    [SerializeField]
    private int _avatarNumber = 8;
    [SerializeField]
    private bool _showNumberAndName = true;
    [SerializeField]
    private ETeamUniformId _uniform = ETeamUniformId.Ravens;
    [SerializeField]
    private bool _useLod = true;
    [FormerlySerializedAs("_avatar")]
    [SerializeField]
    [HideInInspector]
    private AvatarGraphics _avatarGraphics;

    private void OnValidate()
    {
      if ((Object) this._avatarGraphics != (Object) null)
        return;
      this._avatarGraphics = this.GetComponent<AvatarGraphics>();
      if (!((Object) this._avatarGraphics == (Object) null))
        return;
      Debug.LogError((object) "In order for avatarConfigurator to work, you need to assign the '_avatarGraphics' reference, or place the script on the same object as the avatar");
    }

    private void Awake() => this.Apply();

    public void Apply(bool editMode = false)
    {
      this.OnValidate();
      if ((Object) this._avatarGraphics == (Object) null)
        return;
      UniformStore uniformStore = SaveManager.GetUniformStore();
      if ((Object) uniformStore == (Object) null)
      {
        Debug.LogError((object) "Uniform store not found");
      }
      else
      {
        if (this._avatarGraphics.Renderers == null || this._avatarGraphics.Renderers.Count == 0)
        {
          if (editMode)
          {
            SkinnedMeshRenderer[] componentsInChildren = this._avatarGraphics.GetComponentsInChildren<SkinnedMeshRenderer>();
            if (componentsInChildren == null || componentsInChildren.Length == 0)
            {
              Debug.LogError((object) ("Failed to find any renderers for " + this.gameObject.name + " AvatarConfigurator"));
              return;
            }
            this._avatarGraphics.SetRenderers(componentsInChildren);
          }
          else
            this._avatarGraphics.Initialize();
        }
        if (this._avatarGraphics.Renderers == null || this._avatarGraphics.Renderers.Count == 0)
        {
          Debug.LogError((object) "Avatar is missing renderers, nothing to configure..");
        }
        else
        {
          FootballWorld.UniformConfig uniformConfig = uniformStore.GetUniformConfig(this._uniform, ETeamUniformFlags.Home);
          Texture2D[] textsTexture = UniformCapture.GetTextsTexture(3, new List<int>()
          {
            this._avatarNumber
          }, new List<string>() { this._name.ToUpper() }, 2);
          UniformCapture.Info info = new UniformCapture.Info()
          {
            BaseMap = uniformConfig.BasemapAlternative,
            PlayerIndex = 0,
            TextsAtlas = (Texture[]) textsTexture
          };
          uniformStore.SetNamesAndNumbersVisibility(this._showNumberAndName && !editMode);
          this._avatarGraphics.avatarGraphicsData.uniformCaptureInfo.Value = info;
          this._avatarGraphics.SetLod(this._useLod);
        }
      }
    }
  }
}
