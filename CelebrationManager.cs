// Decompiled with JetBrains decompiler
// Type: CelebrationManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class CelebrationManager
{
  private AnimatorCommunicator _anim;
  private int _celebrationValue;
  private CelebrationCategory _celebrationCategory;
  private string _celebrationEvent;
  private static Dictionary<CelebrationCategory, int> _categoryAnimationCount = new Dictionary<CelebrationCategory, int>()
  {
    {
      CelebrationCategory.None,
      0
    },
    {
      CelebrationCategory.UpsetWithCall,
      5
    },
    {
      CelebrationCategory.QBCelebration,
      1
    },
    {
      CelebrationCategory.FirstDownCelebration,
      8
    },
    {
      CelebrationCategory.DefensiveCelebration,
      20
    },
    {
      CelebrationCategory.MissedFG,
      1
    },
    {
      CelebrationCategory.Frustrated,
      24
    },
    {
      CelebrationCategory.Touchdown,
      29
    },
    {
      CelebrationCategory.Generic,
      13
    },
    {
      CelebrationCategory.GiveUpTouchdown,
      14
    },
    {
      CelebrationCategory.IncompletePass,
      12
    },
    {
      CelebrationCategory.Neutral,
      10
    },
    {
      CelebrationCategory.SuperbowlCelebration,
      6
    }
  };

  public CelebrationManager(AnimatorCommunicator playerAnimator) => this._anim = playerAnimator;

  public void ResetCelebration()
  {
    this.SetCelebration(CelebrationCategory.None);
    this._anim.celebrationType = this._celebrationValue;
  }

  private void SetCelebration(CelebrationCategory category)
  {
    this._celebrationValue = (int) category;
    this._anim.celebrationType = this._celebrationValue;
    this._anim.mirrorCelebration = (double) Random.value > 0.5;
    this._anim.randomCelebrationIndex = Random.Range(0, CelebrationManager._categoryAnimationCount[category]);
    this._anim.randomCelebrationOffset = Random.Range(0.0f, 0.1f);
    this._celebrationCategory = category;
  }

  public void SetFrustratedInPlaceCelebration()
  {
    int[] numArray = new int[4]{ 12, 13, 16, 17 };
    this._celebrationValue = 6;
    this._anim.celebrationType = this._celebrationValue;
    this._anim.mirrorCelebration = (double) Random.value > 0.5;
    this._anim.randomCelebrationIndex = numArray[Random.Range(0, numArray.Length - 1)];
    this._anim.randomCelebrationOffset = Random.Range(0.0f, 0.1f);
    this._celebrationCategory = CelebrationCategory.Frustrated;
  }

  public CelebrationCategory GetCelebrationCategory() => this._celebrationCategory;

  public void SetCelebrationFromCategory(CelebrationCategory category)
  {
    switch (category)
    {
      case CelebrationCategory.QBCelebration:
        if ((double) Random.value < 0.25)
        {
          this.SetCelebration(category);
          break;
        }
        this.SetCelebration(CelebrationCategory.Generic);
        break;
      case CelebrationCategory.FirstDownCelebration:
        if ((double) Random.value < 0.40000000596046448)
        {
          this.SetCelebration(category);
          break;
        }
        this.SetCelebration(CelebrationCategory.Generic);
        break;
      case CelebrationCategory.DefensiveCelebration:
        if ((double) Random.value < 0.5)
        {
          this.SetCelebration(category);
          break;
        }
        this.SetCelebration(CelebrationCategory.Generic);
        break;
      case CelebrationCategory.MissedFG:
        if ((double) Random.value < 0.5)
        {
          this.SetCelebration(category);
          break;
        }
        this.SetCelebration(CelebrationCategory.Frustrated);
        break;
      default:
        this.SetCelebration(category);
        break;
    }
  }
}
