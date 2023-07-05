// Decompiled with JetBrains decompiler
// Type: ProEra.Game.DevAchievementsPage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using ProEra.Game.Achievements;
using ProEra.Game.Sources.GameStates.LockerRoom.MainMenu;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProEra.Game
{
  public class DevAchievementsPage : TabletPage
  {
    private const int MAX_ELEMENTS_PER_PAGE = 5;
    [SerializeField]
    private GameObject achvUIElementObject;
    [SerializeField]
    private Transform verticalArea;
    [SerializeField]
    private TouchUI2DButton _careerButton;
    [SerializeField]
    private TouchUI2DButton _seasonButton;
    [SerializeField]
    private TouchUI2DButton _gameButton;
    private List<string> _careerAchievements = new List<string>();
    private List<string> _seasonAchievements = new List<string>();
    private List<string> _gameAchievements = new List<string>();
    private List<string> _currentCollection;
    private int _currentCollectionSize;
    private readonly List<AchievementUIElement> _uiElements = new List<AchievementUIElement>();
    [SerializeField]
    private TouchUI2DButton _previousPageButton;
    [SerializeField]
    private TouchUI2DButton _nextPageButton;
    [SerializeField]
    private TextMeshProUGUI _pageText;
    private static readonly string _pageTextFormat = "{0} / {1}";
    private SaveAchievements _savedData;
    private int _currentIndex;

    private async void Awake()
    {
      DevAchievementsPage achievementsPage = this;
      achievementsPage._pageType = TabletPage.Pages.DevAchievements;
      achievementsPage._savedData = PersistentSingleton<SaveManager>.Instance.GetSaveAchievements();
      await achievementsPage._savedData.Load();
      foreach (string key in Achievement.Names.GetAll())
      {
        Achievement achievement = achievementsPage._savedData.Achievements[key];
        if (achievement.Type == Achievement.Types.Career)
          achievementsPage._careerAchievements.Add(key);
        else if (achievement.Type == Achievement.Types.Season)
          achievementsPage._seasonAchievements.Add(key);
        else
          achievementsPage._gameAchievements.Add(key);
      }
      for (int index = 0; index < 5; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(achievementsPage.achvUIElementObject, Vector3.zero, Quaternion.identity, achievementsPage.verticalArea);
        gameObject.GetComponent<RectTransform>().localEulerAngles = Vector3.zero;
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.GetComponent<RectTransform>().position.x, gameObject.GetComponent<RectTransform>().position.y, 0.0f);
        AchievementUIElement component = gameObject.GetComponent<AchievementUIElement>();
        achievementsPage._uiElements.Add(component);
        component.Setup(achievementsPage._careerAchievements[index], achievementsPage.verticalArea);
      }
      achievementsPage._currentCollection = achievementsPage._careerAchievements;
      achievementsPage._currentCollectionSize = achievementsPage._currentCollection.Count;
      achievementsPage.UpdatePageText();
    }

    private void Start()
    {
      if ((UnityEngine.Object) this._nextPageButton != (UnityEngine.Object) null)
        this._nextPageButton.onClick += new System.Action(this.MoveNextPage);
      if ((UnityEngine.Object) this._previousPageButton != (UnityEngine.Object) null)
        this._previousPageButton.onClick += new System.Action(this.MovePreviousPage);
      if ((UnityEngine.Object) this._careerButton != (UnityEngine.Object) null)
        this._careerButton.onClick += (System.Action) (() => this.SetCurrentCollection(Achievement.Types.Career));
      if ((UnityEngine.Object) this._seasonButton != (UnityEngine.Object) null)
        this._seasonButton.onClick += (System.Action) (() => this.SetCurrentCollection(Achievement.Types.Season));
      if (!((UnityEngine.Object) this._gameButton != (UnityEngine.Object) null))
        return;
      this._gameButton.onClick += (System.Action) (() => this.SetCurrentCollection(Achievement.Types.Game));
    }

    private void MoveNextPage()
    {
      this._currentIndex += 5;
      if (this._currentIndex >= this._currentCollection.Count)
        this._currentIndex = 0;
      this.PopulateElements();
    }

    private void MovePreviousPage()
    {
      this._currentIndex -= 5;
      if (this._currentIndex <= 0)
        this._currentIndex = this._currentCollectionSize - this._currentCollectionSize % 5;
      this.PopulateElements();
    }

    private void PopulateElements()
    {
      int count = this._uiElements.Count;
      for (int index1 = 0; index1 < count; ++index1)
      {
        AchievementUIElement uiElement = this._uiElements[index1];
        int index2 = this._currentIndex + index1;
        if (index2 < this._currentCollectionSize)
        {
          uiElement.Setup(this._currentCollection[index2], this.verticalArea);
          uiElement.gameObject.SetActive(true);
        }
        else
          uiElement.gameObject.SetActive(false);
      }
      this.UpdatePageText();
    }

    private void SetCurrentCollection(string achievementType)
    {
      this._currentCollection = !(achievementType == Achievement.Types.Career) ? (!(achievementType == Achievement.Types.Season) ? this._gameAchievements : this._seasonAchievements) : this._careerAchievements;
      this._currentIndex = 0;
      this._currentCollectionSize = this._currentCollection.Count;
      this.PopulateElements();
    }

    private void UpdatePageText()
    {
      if (!((UnityEngine.Object) this._pageText != (UnityEngine.Object) null))
        return;
      int num1 = this._currentIndex / 5 + 1;
      int num2 = Mathf.CeilToInt((float) this._currentCollectionSize / 5f);
      this._pageText.text = string.Format(DevAchievementsPage._pageTextFormat, (object) num1, (object) num2);
    }
  }
}
