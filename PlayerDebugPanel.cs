// Decompiled with JetBrains decompiler
// Type: PlayerDebugPanel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TB12;
using TMPro;
using UnityEngine;

public class PlayerDebugPanel : MonoBehaviour
{
  [SerializeField]
  private TMP_Text _idText;
  [SerializeField]
  private TMP_Text _nameText;
  [SerializeField]
  private TMP_Text _healthText;
  [SerializeField]
  private TMP_Text _scoreText;
  [SerializeField]
  private TMP_Text _ballsThrownText;
  [SerializeField]
  private TMP_Text _isBossText;
  [SerializeField]
  private TMP_Text _onTeamOneText;

  public string ID
  {
    get => this._idText.text;
    set => this._idText.text = value;
  }

  public string Name
  {
    get => this._nameText.text;
    set => this._nameText.text = value;
  }

  public string Health
  {
    get => this._healthText.text;
    set => this._healthText.text = "Health: " + value;
  }

  public string Score
  {
    get => this._scoreText.text;
    set => this._scoreText.text = "Score: " + value;
  }

  public string BallsThrown
  {
    get => this._ballsThrownText.text;
    set => this._ballsThrownText.text = "Balls Thrown: " + value;
  }

  public string IsBoss
  {
    get => this._isBossText.text;
    set => this._isBossText.text = value;
  }

  public string OnTeamOne
  {
    get => this._onTeamOneText.text;
    set => this._onTeamOneText.text = value;
  }

  public void UpdateWithData(PlayerStatsData dataToSet)
  {
    Debug.Log((object) string.Format("Updating debug data for Player {0}", (object) dataToSet.playerId));
    this.ID = dataToSet.playerId.ToString();
    this.Name = "Player " + dataToSet.playerId.ToString();
    this.Health = dataToSet.health.ToString();
    this.Score = dataToSet.score.ToString();
    this.BallsThrown = dataToSet.throwsMade.ToString();
    this.IsBoss = dataToSet.isBoss ? "Boss?: Yes" : "Boss?: No";
    this.OnTeamOne = dataToSet.onTeamOne ? "Team: 1" : "Team: 2";
  }
}
