// Decompiled with JetBrains decompiler
// Type: SavedMatchState
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

public struct SavedMatchState
{
  private float _yardLine;
  private float _firstDownLine;
  private float _hash;
  private int _down;
  private int _teamOnOffense;
  private int _teamOnDefense;
  private int _quarterLength;
  private int _playerScore;
  private int _compScore;
  private bool _runClock;
  private bool _runningPAT;

  public SavedMatchState(
    float yardLine,
    float firstDownLine,
    float hash,
    int down,
    int teamOnOffense,
    int teamOnDefense,
    int quarterLength,
    int playerScore,
    int compScore,
    bool runClock,
    bool runningPAT)
  {
    this._yardLine = yardLine;
    this._firstDownLine = firstDownLine;
    this._hash = hash;
    this._down = down;
    this._teamOnOffense = teamOnOffense;
    this._teamOnDefense = teamOnDefense;
    this._quarterLength = quarterLength;
    this._playerScore = playerScore;
    this._compScore = compScore;
    this._runClock = runClock;
    this._runningPAT = runningPAT;
  }

  public float YardLine
  {
    get => this._yardLine;
    set => this._yardLine = value;
  }

  public float FirstDownLine
  {
    get => this._firstDownLine;
    set => this._firstDownLine = value;
  }

  public float Hash
  {
    get => this._hash;
    set => this._hash = value;
  }

  public int Down
  {
    get => this._down;
    set => this._down = value;
  }

  public int TeamOnOffense
  {
    get => this._teamOnOffense;
    set => this._teamOnOffense = value;
  }

  public int TeamOnDefense
  {
    get => this._teamOnDefense;
    set => this._teamOnDefense = value;
  }

  public int QuarterLength
  {
    get => this._quarterLength;
    set => this._quarterLength = value;
  }

  public int PlayerScore
  {
    get => this._playerScore;
    set => this._playerScore = value;
  }

  public int CompScore
  {
    get => this._compScore;
    set => this._compScore = value;
  }

  public bool RunClock
  {
    get => this._runClock;
    set => this._runClock = value;
  }

  public bool RunningPAT
  {
    get => this._runningPAT;
    set => this._runningPAT = value;
  }
}
