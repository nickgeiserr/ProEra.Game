// Decompiled with JetBrains decompiler
// Type: TB12.PracticePlaysStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace TB12
{
  [CreateAssetMenu(menuName = "ProEra/Data/PracticePlaysStore", fileName = "PracticePlaysStore")]
  [AppStore]
  public class PracticePlaysStore : AxisPlaysStore
  {
    [SerializeField]
    private bool _hideSpecialTeams;
    private readonly List<BaseFormation> _baseFormations = new List<BaseFormation>();
    private readonly Dictionary<BaseFormation, List<FormationData>> _formations = new Dictionary<BaseFormation, List<FormationData>>();
    private readonly List<BaseFormation> _baseDefFormations = new List<BaseFormation>();
    private readonly Dictionary<BaseFormation, List<FormationData>> _defFormations = new Dictionary<BaseFormation, List<FormationData>>();
    private EPlaybookType currentPlaybookType;
    private List<FormationData> offPlays = new List<FormationData>();
    private List<FormationData> defPlays = new List<FormationData>();

    public override int BaseFormationsCount => this.currentPlaybookType == EPlaybookType.Defense ? this._baseDefFormations.Count : this._baseFormations.Count;

    public bool HideSpecialTeams
    {
      get => this._hideSpecialTeams;
      set => this._hideSpecialTeams = value;
    }

    public void Initialize(EPlaybookType playbookType) => this.LoadPlayBook(playbookType);

    public override void SetupPlayImage(SpriteRenderer playImage, PlayData playData)
    {
      FormationData formationData = PlaybookState.CurrentFormation.Value;
      string imageFileName = PlayImageUtil.GetImageFileName(playData);
      SpriteAtlas spriteAtlas;
      if (this.currentPlaybookType == EPlaybookType.Defense)
      {
        spriteAtlas = Plays.self.DefSpriteAtlasP1;
        Plays.self.GetCurDefPlaybookP1();
      }
      else
      {
        spriteAtlas = Plays.self.OffSpriteAtlasP1;
        Plays.self.GetCurOffPlaybookP1();
      }
      playImage.sprite = (Sprite) null;
      if ((Object) spriteAtlas == (Object) null)
        spriteAtlas = Resources.Load<SpriteAtlas>("ALL ATLAS");
      if (!((Object) spriteAtlas != (Object) null))
        return;
      Sprite sprite = spriteAtlas.GetSprite(imageFileName);
      if (!((Object) sprite != (Object) null))
        return;
      playImage.sprite = sprite;
    }

    public void LoadPlayBook(EPlaybookType playbookType)
    {
      this.currentPlaybookType = playbookType;
      switch (playbookType)
      {
        case EPlaybookType.Offense:
          this.offPlays = Plays.self.GetOffPlaybookForPlayer(Player.One);
          if (this._hideSpecialTeams)
          {
            for (int index = this.offPlays.Count - 1; index >= 0; --index)
            {
              if (this.offPlays[index].GetFormationType() == FormationType.OffSpecial)
                this.offPlays.RemoveAt(index);
            }
          }
          this.LoadPlays(this.offPlays);
          break;
        case EPlaybookType.Defense:
          this.defPlays = Plays.self.GetDefPlaybookForPlayer(Player.One);
          this.LoadPlays(this.defPlays);
          break;
        default:
          Debug.LogError((object) ("LoadPlaybook called an invalid playbook type= " + Common.EnumToString(playbookType.ToString())));
          break;
      }
    }

    public void LoadPlays(List<FormationData> plays)
    {
      this.PlayCount = plays.Count;
      List<BaseFormation> baseFormationList;
      Dictionary<BaseFormation, List<FormationData>> dictionary;
      if (this.currentPlaybookType == EPlaybookType.Defense)
      {
        baseFormationList = this._baseDefFormations;
        dictionary = this._defFormations;
      }
      else
      {
        baseFormationList = this._baseFormations;
        dictionary = this._formations;
      }
      baseFormationList.Clear();
      dictionary.Clear();
      for (int index = 0; index < this.PlayCount; ++index)
      {
        BaseFormation baseFormation = plays[index].GetFormationPositions().GetBaseFormation();
        List<FormationData> formationDataList;
        if (!dictionary.TryGetValue(baseFormation, out formationDataList))
        {
          dictionary[baseFormation] = formationDataList = new List<FormationData>();
          baseFormationList.Add(baseFormation);
        }
        formationDataList.Add(plays[index]);
      }
      if (dictionary.Keys.Count < 5)
        Debug.Log((object) "Offensive Playbook does not have the minimum number of base formations");
      PlaybookState.FormationBase.SetValue(baseFormationList[0]);
    }

    public override string GetBaseFormationName(int index) => Common.EnumToString((this.currentPlaybookType != EPlaybookType.Defense ? this._baseFormations : this._baseDefFormations)[index].ToString());

    public override FormationData GetFormationData(BaseFormation baseFormation, int formationIndex)
    {
      List<FormationData> formationDataList;
      if (!(this.currentPlaybookType != EPlaybookType.Defense ? this._formations : this._defFormations).TryGetValue(baseFormation, out formationDataList))
      {
        Debug.LogError((object) string.Format("No plays found under base formation {0}", (object) baseFormation));
        return (FormationData) null;
      }
      if (formationIndex >= 0 && formationIndex < formationDataList.Count)
        return formationDataList[formationIndex];
      Debug.LogError((object) string.Format("Invalid formation index {0}, group has {1} formations", (object) formationIndex, (object) formationDataList.Count));
      return (FormationData) null;
    }

    public override int GetFormationsCount(BaseFormation baseFormation)
    {
      List<FormationData> formationDataList;
      if ((this.currentPlaybookType != EPlaybookType.Defense ? this._formations : this._defFormations).TryGetValue(baseFormation, out formationDataList))
        return formationDataList.Count;
      Debug.LogError((object) string.Format("No plays found under base formation {0}", (object) baseFormation));
      return 0;
    }

    public override BaseFormation GetBaseFormation(int baseIndex)
    {
      List<BaseFormation> baseFormationList = this.currentPlaybookType != EPlaybookType.Defense ? this._baseFormations : this._baseDefFormations;
      if (baseIndex >= 0 && baseIndex < baseFormationList.Count)
        return baseFormationList[baseIndex];
      Debug.LogError((object) string.Format("Invalid index for base formation {0}, count is {1}", (object) baseIndex, (object) this._baseFormations.Count));
      return BaseFormation.None;
    }
  }
}
