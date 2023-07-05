// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Achievements.TrophyDisplay
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System;
using System.Collections.Generic;
using TB12;
using UnityEngine;

namespace ProEra.Game.Achievements
{
  public class TrophyDisplay : MonoBehaviour
  {
    private static readonly int HighlightId = Shader.PropertyToID("_Highlight");
    [SerializeField]
    private GameObject trophySpawnAnimationPrefab;
    [SerializeField]
    private string achievementName;
    [SerializeField]
    private int tier;
    [SerializeField]
    private GameObject[] trophies;
    [SerializeField]
    private float pulseRate = 2f;
    [SerializeField]
    private TrophyDisplay.TrophyType trophyType;
    [SerializeField]
    private int teamIndex = -1;
    private MeshRenderer[] trophyMeshRenderers;
    private GameObject activeTrophy;
    private MeshRenderer activeTrophyMeshRenderer;
    private Material[] activeTrophyMaterials;
    private bool initialized;
    private AcknowledgeableAward _award;

    private AchievementData achievementData => SaveManager.GetAchievementData();

    public string Name => this.achievementName;

    public bool IsHighlighted { get; private set; }

    public static float AnimationDuration => 1f;

    private void Awake()
    {
    }

    private void Start()
    {
      if (this.trophyType != TrophyDisplay.TrophyType.TeamRing)
      {
        if (this.trophyMeshRenderers == null || this.trophyMeshRenderers.Length == 0)
        {
          this.trophyMeshRenderers = new MeshRenderer[this.trophies.Length];
          for (int index = 0; index < this.trophies.Length; ++index)
          {
            this.trophies[index].gameObject.SetActive(false);
            this.trophyMeshRenderers[index] = this.trophies[index].GetComponentInChildren<MeshRenderer>();
          }
        }
      }
      else
        this.trophies[0].gameObject.SetActive(false);
      if (this.trophyType != TrophyDisplay.TrophyType.SuperBowlLombardiTrophy)
        return;
      this.InitLombardiTrophy();
    }

    private void FixedUpdate() => this.UpdateHighlightPulse();

    public void PlaySpawnAnimation()
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.trophySpawnAnimationPrefab, this.transform);
      if (this.trophyType == TrophyDisplay.TrophyType.LombardiTrophy)
        AppSounds.PlaySfx(ESfxTypes.kSuperBowlTrophyUnlock);
      else
        AppSounds.Play3DSfx(ESfxTypes.kTrophyUnlock, this.transform);
      this.DisableFx();
      double animationDuration = (double) TrophyDisplay.AnimationDuration;
      UnityEngine.Object.Destroy((UnityEngine.Object) gameObject, (float) animationDuration);
    }

    public void InitializeTrophies()
    {
      switch (this.trophyType)
      {
        case TrophyDisplay.TrophyType.TeamRing:
          this.InitTeamRing();
          break;
        case TrophyDisplay.TrophyType.TeamFootball:
          this.InitTeamFootball();
          break;
        case TrophyDisplay.TrophyType.LombardiTrophy:
        case TrophyDisplay.TrophyType.SuperBowlLombardiTrophy:
          this.InitLombardiTrophy();
          break;
        default:
          this.InitTieredTrophy();
          break;
      }
      this.initialized = true;
    }

    private void InitLombardiTrophy()
    {
      if (this.trophyType != TrophyDisplay.TrophyType.SuperBowlLombardiTrophy && !this.achievementData.UserHasWonSuperBowl())
        return;
      this.ShowTrophy(false);
    }

    private void InitTieredTrophy()
    {
      int num = -1;
      try
      {
        num = this.achievementData.Achievements[this.achievementName].CurrentTier;
      }
      catch (KeyNotFoundException ex)
      {
        Debug.LogError((object) (ex.ToString() + ": Missing key was " + this.achievementName));
      }
      if (num < 0)
        return;
      this.UpdateTrophyHighlight(num);
      Achievement achievement = this.achievementData.Achievements[this.achievementName];
      this.ShowTrophy(!achievement.Acknowledged && achievement.ShouldPlaySfx, num);
    }

    private void InitTeamRing()
    {
      this._award = this.achievementData.GetAwardForSuperBowlTeam(this.teamIndex);
      if ((this._award == null ? 0 : (this._award.HasBeenAwarded ? 1 : 0)) == 0)
        return;
      this.UpdateAwardHighlight(this._award);
      this.IsHighlighted = !this._award.Acknowledged && this._award.ShouldPlaySfx;
      this.trophies[0].SetActive(true);
    }

    private void InitTeamFootball()
    {
      this._award = this.achievementData.GetAwardForTeamDefeated(this.teamIndex);
      if (this._award == null || !this._award.HasBeenAwarded)
        return;
      this.UpdateAwardHighlight(this._award);
      this.ShowTrophy(!this._award.Acknowledged && this._award.ShouldPlaySfx);
    }

    private void ShowTrophy(bool isHighlighted, int trophyTier = 0)
    {
      this.activeTrophy = this.trophies[trophyTier];
      this.activeTrophy.gameObject.SetActive(true);
      this.activeTrophyMeshRenderer = this.trophyMeshRenderers[trophyTier];
      if ((UnityEngine.Object) this.activeTrophyMeshRenderer != (UnityEngine.Object) null)
        this.activeTrophyMaterials = this.activeTrophyMeshRenderer.materials;
      this.IsHighlighted = isHighlighted;
    }

    private void UpdateTrophyHighlight(int tier = 0) => this.trophies[tier].GetComponent<TouchDrag3D>().OnGrabObject += (Action<ITouchInput>) (input =>
    {
      this.achievementData.Achievements[this.achievementName].Acknowledged = true;
      this.achievementData.Save();
      this.IsHighlighted = false;
    });

    private void UpdateAwardHighlight(AcknowledgeableAward award) => this.trophies[this.tier].GetComponent<TouchDrag3D>().OnGrabObject += (Action<ITouchInput>) (input =>
    {
      award.Acknowledged = true;
      this.achievementData.Save();
      this.IsHighlighted = false;
    });

    private void UpdateHighlightPulse()
    {
      if (!this.initialized || this.activeTrophyMaterials == null)
        return;
      float num = 0.0f;
      if (this.IsHighlighted)
        num = Mathf.PingPong(Time.time * this.pulseRate, 1f);
      foreach (Material activeTrophyMaterial in this.activeTrophyMaterials)
        activeTrophyMaterial.SetFloat(TrophyDisplay.HighlightId, num);
    }

    private void DisableFx()
    {
      if (!((UnityEngine.Object) this.achievementData != (UnityEngine.Object) null))
        return;
      switch (this.trophyType)
      {
        case TrophyDisplay.TrophyType.TieredTrophy:
          this.achievementData.Achievements[this.achievementName]?.DisableFx();
          break;
        case TrophyDisplay.TrophyType.TeamRing:
          this.achievementData.GetAwardForSuperBowlTeam(this.teamIndex)?.DisableFx();
          break;
        case TrophyDisplay.TrophyType.TeamFootball:
          this.achievementData.GetAwardForTeamDefeated(this.teamIndex)?.DisableFx();
          break;
      }
    }

    public enum TrophyType
    {
      TieredTrophy,
      TeamRing,
      TeamFootball,
      LombardiTrophy,
      SuperBowlLombardiTrophy,
    }
  }
}
