// Decompiled with JetBrains decompiler
// Type: MiniCampSelectionUIController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using TB12;
using UnityEngine;
using UnityEngine.UI;

public class MiniCampSelectionUIController : MonoBehaviour
{
  [SerializeField]
  private Image _logoImage;
  [SerializeField]
  private Image _star1Image;
  [SerializeField]
  private Image _star2Image;
  [SerializeField]
  private Image _star3Image;
  [SerializeField]
  private Image _lockImage;
  [SerializeField]
  private TouchUI2DButton _playButton;
  [SerializeField]
  private EGameMode _gameModeToPlay;
  private bool _buttonEnabled = true;
  [SerializeField]
  private bool _isLocked;

  public bool ButtonEnabled
  {
    get => this._buttonEnabled;
    set
    {
      this._buttonEnabled = value;
      this._playButton.gameObject.SetActive(!this._isLocked && this._buttonEnabled);
    }
  }

  public bool IsLocked
  {
    get => this._isLocked;
    set
    {
      this._isLocked = value;
      this._lockImage.enabled = this._isLocked;
      this._playButton.gameObject.SetActive(!this._isLocked && this._buttonEnabled);
    }
  }

  private void Awake() => this._playButton.onClick += new System.Action(this.HandlePlayButton);

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) this._playButton != (UnityEngine.Object) null))
      return;
    this._playButton.onClick -= new System.Action(this.HandlePlayButton);
  }

  private void HandlePlayButton()
  {
    if (this.IsLocked)
      return;
    PersistentSingleton<GamePlayerController>.Instance.CachePositionInLockerRoom();
    GameplayManager.LoadLevelActivation(this._gameModeToPlay, ETimeOfDay.Clear);
  }

  public void Inject(
    Sprite logo,
    int stars,
    bool isLocked,
    EGameMode modeToHookIntoPlayButton,
    string teamName)
  {
    this.SetSelectionState(true);
    this._logoImage.sprite = logo;
    this._star1Image.enabled = stars >= 1;
    this._star2Image.enabled = stars >= 2;
    this._star3Image.enabled = stars >= 3;
    this.IsLocked = isLocked;
    this._gameModeToPlay = modeToHookIntoPlayButton;
  }

  public void SetSelectionState(bool state)
  {
    this._logoImage.enabled = state;
    this._star1Image.enabled = state;
    this._star2Image.enabled = state;
    this._star3Image.enabled = state;
    this._lockImage.enabled = state;
    this._playButton.gameObject.SetActive(state);
  }
}
