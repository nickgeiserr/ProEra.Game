// Decompiled with JetBrains decompiler
// Type: TB12.AudiblePlaysStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  [CreateAssetMenu(menuName = "ProEra/Data/AudiblePlaysStore", fileName = "AudiblePlaysStore")]
  [AppStore]
  public class AudiblePlaysStore : AxisPlaysStore
  {
    private BaseFormation _baseFormation;
    private FormationData _formation;
    private List<PlayData> audiblePlays;

    public override int BaseFormationsCount => 1;

    public override void Initialize(bool isAudiblePlays = false)
    {
      this._formation = PlaybookState.CurrentFormation.Value;
      if (this._formation == null)
        Debug.LogWarning((object) "Formation was null!");
      else if (this._formation.GetFormationPositions() == null)
      {
        Debug.LogWarning((object) ("Formation " + this._formation.GetName() + " had no positions."));
      }
      else
      {
        this._baseFormation = this._formation.GetFormationPositions().GetBaseFormation();
        this.audiblePlays = new List<PlayData>();
        int num1 = 0;
        int num2 = 0;
        foreach (PlayDataOff play in this._formation.GetPlays())
        {
          if (play != null)
          {
            if (play.GetPlayType() == global::PlayType.Run && num2 < 3)
            {
              this.audiblePlays.Add((PlayData) play);
              ++num2;
            }
            else if (play.GetPlayType().Equals((object) global::PlayType.Pass) && num1 < 3)
            {
              this.audiblePlays.Add((PlayData) play);
              ++num1;
            }
            if (num2 + num1 > 3)
              break;
          }
        }
        this.PlayCount = this.audiblePlays.Count;
      }
    }

    public override void SetupPlayImage(SpriteRenderer playImage, PlayData playData)
    {
      string imageFileName = PlayImageUtil.GetImageFileName(playData);
      playImage.sprite = (Sprite) null;
      if (!((Object) Plays.self.OffSpriteAtlasP1 != (Object) null))
        return;
      Sprite sprite = Plays.self.OffSpriteAtlasP1.GetSprite(imageFileName);
      if (!((Object) sprite != (Object) null))
        return;
      playImage.sprite = sprite;
    }

    public string GetBaseFormationName() => Common.EnumToString(this._baseFormation.ToString());

    public new FormationData GetFormationData(BaseFormation baseFormation, int formationIndex) => this._formation;

    public new int GetFormationsCount(BaseFormation baseFormation) => 1;

    public new BaseFormation GetBaseFormation(int baseIndex) => this._baseFormation;
  }
}
