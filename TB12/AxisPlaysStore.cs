// Decompiled with JetBrains decompiler
// Type: TB12.AxisPlaysStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace TB12
{
  [CreateAssetMenu(menuName = "ProEra/Data/AxisPlaysStore", fileName = "AxisPlaysStore")]
  [AppStore]
  public class AxisPlaysStore : ScriptableObject
  {
    private readonly List<BaseFormation> _baseFormations = new List<BaseFormation>();
    private readonly Dictionary<BaseFormation, List<FormationData>> _formations = new Dictionary<BaseFormation, List<FormationData>>();
    private string _PlayImagesPath = "Assets/ProEra.Game/UI/PlayImages";
    private string _ResourcesPath = "Assets/ProEra.Game/UI/PlayImages/Resources";
    public SpriteAtlas SpriteAtlas;

    public string PlayImagesPath => this._PlayImagesPath;

    public string ResourcesPath => this._ResourcesPath;

    public virtual int PlayCount { get; protected set; }

    public virtual int BaseFormationsCount => this._baseFormations.Count;

    public virtual void Initialize(bool isAudiblePlays = false)
    {
      this.SpriteAtlas = (SpriteAtlas) null;
      List<FormationData> playbookForPlayer = Plays.self.GetOffPlaybookForPlayer(Player.One);
      if (playbookForPlayer == null)
      {
        Debug.LogError((object) "Plays.self.GetOffPlaybookForPlayer(Player.One);\nNo plays found for player one!");
      }
      else
      {
        this.PlayCount = playbookForPlayer.Count;
        for (int index = 0; index < this.PlayCount; ++index)
        {
          if (playbookForPlayer[index] == null)
          {
            Debug.LogError((object) string.Format("Play {0} is null for GetOffPlaybookForPlayer(Player.One).", (object) index));
          }
          else
          {
            BaseFormation baseFormation = playbookForPlayer[index].GetFormationPositions().GetBaseFormation();
            List<FormationData> formationDataList;
            if (!this._formations.TryGetValue(baseFormation, out formationDataList))
            {
              this._formations[baseFormation] = formationDataList = new List<FormationData>();
              this._baseFormations.Add(baseFormation);
            }
            formationDataList.Add(playbookForPlayer[index]);
          }
        }
      }
      if (this._formations.Keys.Count < 5)
        Debug.Log((object) "Offensive Playbook does not have the minimum number of base formations");
      if (isAudiblePlays)
        return;
      PlaybookState.FormationBase.SetValue(this._baseFormations[0]);
    }

    public virtual void SetupPlayImage(SpriteRenderer PlayImage, PlayData playData)
    {
      if (!(bool) PlaybookState.OnOffense)
        Plays.self.GetCurDefPlaybookP1();
      else
        Plays.self.GetCurOffPlaybookP1();
      FormationData formationData = PlaybookState.CurrentFormation.Value;
      string imageFileName = PlayImageUtil.GetImageFileName(playData);
      this.LoadAtlas("ALL ATLAS");
      if (!((Object) this.SpriteAtlas != (Object) null))
        return;
      Sprite sprite = this.SpriteAtlas.GetSprite(imageFileName);
      if (!((Object) sprite != (Object) null))
        return;
      PlayImage.sprite = sprite;
    }

    public Sprite GetPlayImage(PlayData playData)
    {
      if (!(bool) PlaybookState.OnOffense)
        Plays.self.GetCurDefPlaybookP1();
      else
        Plays.self.GetCurOffPlaybookP1();
      if (playData.GetFormation() == null)
        return (Sprite) null;
      string imageFileName = PlayImageUtil.GetImageFileName(playData);
      this.LoadAtlas("ALL ATLAS");
      if ((Object) this.SpriteAtlas != (Object) null)
      {
        Sprite sprite = this.SpriteAtlas.GetSprite(imageFileName);
        if ((Object) sprite != (Object) null)
          return sprite;
      }
      return (Sprite) null;
    }

    public void LoadAtlas(string AtlasName)
    {
      if ((Object) this.SpriteAtlas != (Object) null)
      {
        if (!(this.SpriteAtlas.name != AtlasName))
          return;
        this.SpriteAtlas = Resources.Load<SpriteAtlas>(AtlasName);
      }
      else
        this.SpriteAtlas = Resources.Load<SpriteAtlas>(AtlasName);
    }

    public virtual string GetBaseFormationName(int index) => Common.EnumToString(this._baseFormations[index].ToString());

    public virtual FormationData GetFormationData(BaseFormation baseFormation, int formationIndex)
    {
      List<FormationData> formationDataList;
      if (!this._formations.TryGetValue(baseFormation, out formationDataList))
      {
        Debug.LogError((object) string.Format("No plays found under base formation {0}", (object) baseFormation));
        return (FormationData) null;
      }
      if (formationIndex >= 0 && formationIndex < formationDataList.Count)
        return formationDataList[formationIndex];
      Debug.LogError((object) string.Format("Invalid formation index {0}, group has {1} formations", (object) formationIndex, (object) formationDataList.Count));
      return (FormationData) null;
    }

    public virtual int GetFormationsCount(BaseFormation baseFormation)
    {
      List<FormationData> formationDataList;
      if (this._formations.TryGetValue(baseFormation, out formationDataList))
        return formationDataList.Count;
      Debug.LogError((object) string.Format("No plays found under base formation {0}", (object) baseFormation));
      return 0;
    }

    public virtual BaseFormation GetBaseFormation(int baseIndex)
    {
      if (baseIndex >= 0 && baseIndex < this._baseFormations.Count)
        return this._baseFormations[baseIndex];
      Debug.LogError((object) string.Format("Invalid index for base formation {0}, count is {1}", (object) baseIndex, (object) this._baseFormations.Count));
      return BaseFormation.None;
    }
  }
}
