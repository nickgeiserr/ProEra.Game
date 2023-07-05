// Decompiled with JetBrains decompiler
// Type: TB12.BallPlacementManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UDB;
using UnityEngine;

namespace TB12
{
  public class BallPlacementManager : MonoBehaviour
  {
    private int m_ballYardLine;
    private float m_ballHashPosition;
    private const float DISTANCE_FROM_CENTER_TO_HASH = 2.8194f;
    private const float DISTANCE_FROM_HASH_TO_SIDELINE = 21.5646f;
    [SerializeField]
    private int _incrementAmount = 1;
    [SerializeField]
    private GameObject _placementGuidePrefab;
    private PlacementGuideManager m_placementGuideManager;
    private GameObject m_placementGuideObject;

    public int BallYardLine
    {
      get => this.m_ballYardLine;
      private set
      {
        this.m_ballYardLine = Mathf.RoundToInt((float) value);
        this.m_placementGuideManager.SetYardLineGuideToYard(this.m_ballYardLine);
      }
    }

    public float BallHashPosition
    {
      get => this.m_ballHashPosition;
      private set
      {
        this.m_ballHashPosition = value;
        this.m_placementGuideManager.SetHashGuide(this.m_ballHashPosition);
      }
    }

    private void OnDisable() => this.ClearPlacementGuide();

    public void Initialize()
    {
      try
      {
        if (this.m_placementGuideObject != null)
          this.ClearPlacementGuide();
        this.m_placementGuideObject = Object.Instantiate<GameObject>(this._placementGuidePrefab);
        if (this.m_placementGuideObject.TryGetComponent<PlacementGuideManager>(out this.m_placementGuideManager))
          Debug.Log((object) "Connected PlacementGuideManager to BallPlacementManager");
        else
          Debug.LogWarning((object) ("Could not find a PlacementGuideManager on gameobject " + this.m_placementGuideObject.name));
      }
      catch (UnityException ex)
      {
        Debug.LogWarning((object) "No placement guide has been assigned for ball placement.");
      }
      if (this.BallYardLine == 0)
      {
        this.BallYardLine = 20;
        this.BallHashPosition = 0.0f;
      }
      else
        this.SetBallLocation();
    }

    private void ClearPlacementGuide()
    {
      Object.Destroy((Object) this.m_placementGuideObject);
      this.m_placementGuideObject = (GameObject) null;
      this.m_placementGuideManager = (PlacementGuideManager) null;
    }

    public void PlaceBallAtCenterOfHash() => this.BallHashPosition = 0.0f;

    public void PlaceBallAtLeftHash() => this.BallHashPosition = -2.8194f;

    public void PlaceBallAtRightHash() => this.BallHashPosition = 2.8194f;

    public void IncrementBallPosition() => this.BallYardLine += this._incrementAmount;

    public void PlaceBallAtCenterOfField() => this.BallYardLine = 50;

    public void DecrementBallPosition() => this.BallYardLine -= this._incrementAmount;

    public void SetIncrementAmount(int increment) => this._incrementAmount = increment;

    public void SetBallLocation()
    {
      float locationByYardline = Field.GetFieldLocationByYardline(this.BallYardLine, true);
      Vector3 zero = Vector3.zero with
      {
        x = this.BallHashPosition,
        z = locationByYardline
      };
      MatchManager.instance.SetBallOn(locationByYardline);
      MatchManager.instance.SetBallHashPosition(zero.x);
      SingletonBehaviour<BallManager, MonoBehaviour>.instance.SetPosition(zero);
      SingletonBehaviour<FieldManager, MonoBehaviour>.instance.SetLineOfScrimmageLine();
    }
  }
}
