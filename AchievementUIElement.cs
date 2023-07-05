// Decompiled with JetBrains decompiler
// Type: AchievementUIElement
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using ProEra.Game.Achievements;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class AchievementUIElement : MonoBehaviour
{
  private bool isInitialized;
  [SerializeField]
  private TMP_Text Title;
  [SerializeField]
  private TMP_Text Tier;
  [SerializeField]
  private TMP_Text Progress;
  [SerializeField]
  private TouchUI2DButton tierMinus;
  [SerializeField]
  private TouchUI2DButton tierPluss;
  [SerializeField]
  private TouchUI2DButton tierOffset;
  [FormerlySerializedAs("points")]
  [SerializeField]
  private TouchUI2DButton pointsAdd;
  [SerializeField]
  private TMP_InputField pointsReadout;
  [SerializeField]
  private TouchUI2DButton _almostThereButton;
  private Achievement _achievement;

  public bool Setup(string achvName, Transform parent)
  {
    SaveAchievements saveData = PersistentSingleton<SaveManager>.Instance.GetSaveAchievements();
    if (saveData == null || !saveData.Achievements.ContainsKey(achvName))
      return false;
    this._achievement = saveData.Achievements[achvName];
    this.gameObject.transform.SetParent(parent);
    this.UpdateLabels();
    this.tierMinus.onClick += (System.Action) (() =>
    {
      Achievement.Progress.TryDecreaseTier(achvName);
      this.UpdateLabels();
    });
    this.tierPluss.onClick += (System.Action) (() =>
    {
      Achievement.Progress.TryIncreaseTier(achvName);
      this.UpdateLabels();
    });
    this.tierOffset.onClick += (System.Action) (() =>
    {
      Achievement.Progress.TryIncreaseTierOffsetOne(achvName);
      this.UpdateLabels();
    });
    this.pointsAdd.onClick += (System.Action) (() =>
    {
      int result;
      int.TryParse(this.pointsReadout.text, out result);
      Achievement.Progress.TryAddProgress(achvName, result);
      this.UpdateLabels();
    });
    this._almostThereButton.onClick += (System.Action) (() =>
    {
      Debug.Log((object) "#DG: almost there clicked!");
      this._achievement.AlmostThere();
      saveData.Save().SafeFireAndForget();
      this.UpdateLabels();
    });
    if (!this.isInitialized)
      this.isInitialized = true;
    return true;
  }

  private void OnEnable()
  {
    if (!this.isInitialized)
      return;
    this.UpdateLabels();
  }

  private void UpdateLabels()
  {
    this.Title.text = this._achievement.Name + ":" + this._achievement.Description;
    this.Tier.text = "Tier: " + (this._achievement.CurrentTier == -1 ? 0 : this._achievement.CurrentTier).ToString() + " of " + this._achievement.Tiers.Count.ToString();
    this.Progress.text = "Progress: " + this._achievement.CurrentValue.ToString() + " of " + this._achievement.Tiers.Levels[0].ToString();
  }
}
