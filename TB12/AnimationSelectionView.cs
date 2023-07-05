// Decompiled with JetBrains decompiler
// Type: TB12.AnimationSelectionView
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballVR;
using FootballVR.UI;
using FootballWorld;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TB12
{
  public class AnimationSelectionView : MonoBehaviour, ICircularLayoutDataSource
  {
    [SerializeField]
    private CircularTextItem _prefab;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private TouchButton _okButton;
    [Space(10f)]
    [SerializeField]
    private TouchButton _nextPlayerButton;
    [SerializeField]
    private TouchButton _prevPlayerButton;
    [SerializeField]
    private TouchButton _nextTeamButton;
    [SerializeField]
    private TouchButton _prevTeamButton;
    [Space(10f)]
    [SerializeField]
    private TMP_Text _faceType;
    [SerializeField]
    private TMP_Text _bodyType;
    [SerializeField]
    private TMP_Text _headType;
    [Space(10f)]
    [SerializeField]
    private CircularLayout _animationSelection;
    [SerializeField]
    private AnimationDebugJewelCaseStore _animationDebugJewelCaseStore;
    [SerializeField]
    private CharacterCustomizationStore characterStore;
    [Space(10f)]
    [SerializeField]
    private AvatarGraphics avatarGraphics;
    [Space(10f)]
    [SerializeField]
    private TMP_Text labelTeamName;
    [SerializeField]
    private TMP_Text labelPlayerName;
    [SerializeField]
    private TMP_InputField inputField_avatarID;
    public CharacterParameters currentCharacter = new CharacterParameters();
    public TeamData _currentTeamData;
    private int _currentTeamID;
    private int _currentPlayerID;
    private bool _isHome = true;
    private const int MAX_TEAMS = 30;
    private const int MAX_PLAYERS_IN_TEAM = 52;
    private bool _isDirty;

    public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._prefab;

    public int itemCount => this._animationDebugJewelCaseStore.animatorStates.Count;

    protected void Awake()
    {
      this._okButton.onClick += new System.Action(this.HandleOk);
      this._nextPlayerButton.onClick += new System.Action(this.HandleNextPlayer);
      this._prevPlayerButton.onClick += new System.Action(this.HandlePrevPlayer);
      this._nextTeamButton.onClick += new System.Action(this.HandleNextTeam);
      this._prevTeamButton.onClick += new System.Action(this.HandlePrevTeam);
      this.currentCharacter = this.characterStore.GetRandomPreset();
      this.SetupAvatar(this.currentCharacter);
      this.RefreshTeamData();
    }

    private void OnDestroy()
    {
      this._okButton.onClick -= new System.Action(this.HandleOk);
      this._nextPlayerButton.onClick -= new System.Action(this.HandleNextPlayer);
      this._prevPlayerButton.onClick -= new System.Action(this.HandlePrevPlayer);
      this._nextTeamButton.onClick -= new System.Action(this.HandleNextTeam);
      this._prevTeamButton.onClick -= new System.Action(this.HandlePrevTeam);
    }

    public void SetupAvatar(CharacterParameters value) => this.avatarGraphics.SetupAvatar(value, this.characterStore);

    public void SetupAvatar(string avatarID)
    {
      this.currentCharacter = this.avatarGraphics.ConfigAvatar(avatarID);
      this.inputField_avatarID.text = this.currentCharacter.ID;
    }

    public void SetupItem(int itemIndex, CircularLayoutItem item)
    {
      string animatorState = this._animationDebugJewelCaseStore.animatorStates[itemIndex];
      ((CircularTextItem) item).localizationText = animatorState;
    }

    private void HandleNextPlayer()
    {
      ++this._currentPlayerID;
      if (this._currentPlayerID < 0)
        this._currentPlayerID = 52;
      if (this._currentPlayerID > 52)
        this._currentPlayerID = 0;
      this.RefreshTeamPlayerData();
    }

    private void HandlePrevPlayer()
    {
      this._currentPlayerID += -1;
      if (this._currentPlayerID < 0)
        this._currentPlayerID = 52;
      if (this._currentPlayerID > 52)
        this._currentPlayerID = 0;
      this.RefreshTeamPlayerData();
    }

    private void HandleNextTeam()
    {
      ++this._currentTeamID;
      if (this._currentTeamID < 0)
        this._currentTeamID = 30;
      if (this._currentTeamID > 30)
        this._currentTeamID = 0;
      this.RefreshTeamData();
    }

    private void HandlePrevTeam()
    {
      this._currentTeamID += -1;
      if (this._currentTeamID < 0)
        this._currentTeamID = 30;
      if (this._currentTeamID > 30)
        this._currentTeamID = 0;
      this.RefreshTeamData();
    }

    private void HandleOk()
    {
      CircularTextItem currentItem = (CircularTextItem) this._animationSelection.CurrentItem;
      Debug.Log((object) currentItem.localizationText);
      this._animator.Play(currentItem.localizationText);
    }

    private void RefreshTeamData()
    {
      this._currentTeamData = TeamData.NewTeamData(this._currentTeamID);
      this.labelTeamName.text = this._currentTeamData.GetName();
      this.avatarGraphics.SetBasemap(UniformCapture.GetUniformDiffuseByID(TeamDataCache.ToTeamUniformId(this._currentTeamID), this._isHome ? ETeamUniformFlags.Home : ETeamUniformFlags.Away));
      this.RefreshTeamPlayerData();
    }

    private void RefreshTeamPlayerData()
    {
      UniformCapture.ClearAllCachedTextures();
      List<int> numbers = new List<int>();
      List<string> names = new List<string>();
      for (int playerIndex = 0; playerIndex <= 52; ++playerIndex)
      {
        PlayerData player = this._currentTeamData.GetPlayer(playerIndex);
        numbers.Add(player.Number);
        names.Add(player.LastName);
      }
      this.avatarGraphics.SetTextsMap(UniformCapture.GetTextsTexture((int) TeamDataCache.ToTeamUniformId(this._currentTeamID), numbers, names, this._isHome ? 2 : 1), this._currentPlayerID);
      PlayerData player1 = this._currentTeamData.GetPlayer(this._currentPlayerID);
      this.labelPlayerName.text = player1.FullName + " " + player1.Number.ToString();
      this.SetupAvatar(player1.AvatarID);
      this.UpdateLabels();
    }

    private void UpdateLabels()
    {
      this._bodyType.SetText(this.currentCharacter.GetBody(this.characterStore).GetName());
      this._headType.SetText(this.currentCharacter.headRef.GetName());
    }
  }
}
