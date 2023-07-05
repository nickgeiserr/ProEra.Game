// Decompiled with JetBrains decompiler
// Type: DebugStadiumSwitcher
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugStadiumSwitcher : MonoBehaviour
{
  [SerializeField]
  private TouchButton _nextStadiumButton;
  [SerializeField]
  private TouchButton _prevStadiumButton;
  [SerializeField]
  private TMP_Text _stadiumName;
  [Space(10f)]
  [SerializeField]
  private List<GameObject> _stadiumPrefabs;
  private GameObject _currentStadium;
  private int _currentIndex;

  private void Start()
  {
    if (this._stadiumPrefabs.Count == 0)
      Debug.LogError((object) "Stadium Prefabs list is empty");
    else
      this.RefreshStadium();
    this._nextStadiumButton.onClick += new System.Action(this.HandleNextStadiumClick);
    this._prevStadiumButton.onClick += new System.Action(this.HandlePrevStadiumClick);
  }

  private void OnDestroy()
  {
    this._nextStadiumButton.onClick -= new System.Action(this.HandleNextStadiumClick);
    this._prevStadiumButton.onClick -= new System.Action(this.HandlePrevStadiumClick);
  }

  private void HandleNextStadiumClick()
  {
    ++this._currentIndex;
    if (this._currentIndex >= this._stadiumPrefabs.Count)
      this._currentIndex = 0;
    this.RefreshStadium();
  }

  private void HandlePrevStadiumClick()
  {
    --this._currentIndex;
    if (this._currentIndex < 0)
      this._currentIndex = this._stadiumPrefabs.Count - 1;
    this.RefreshStadium();
  }

  private void RefreshStadium()
  {
    UnityEngine.Object.Destroy((UnityEngine.Object) this._currentStadium);
    this._currentStadium = UnityEngine.Object.Instantiate<GameObject>(this._stadiumPrefabs[this._currentIndex]);
    this._currentStadium.transform.position = Vector3.zero;
    this._stadiumName.text = this._stadiumPrefabs[this._currentIndex].name;
  }
}
