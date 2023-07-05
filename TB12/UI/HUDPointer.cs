// Decompiled with JetBrains decompiler
// Type: TB12.UI.HUDPointer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TB12.UI
{
  public class HUDPointer : MonoBehaviour
  {
    [SerializeField]
    private float _lerpCoef = 0.5f;
    [SerializeField]
    private float _radius = 500f;
    [SerializeField]
    private float _minDist = 75f;
    private Camera _cam;
    private Rect _screenRect;
    private Transform _camTx;
    private RectTransform _rectTransform;
    private bool _breakWhenLookAt;
    private bool _showWhenLookAway;
    private Vector2 _rectCenter;
    private Vector3 _prevPos;
    private float _prevAngle;
    private Transform _target;
    private bool playerIsInLockerRoom;
    private bool targetIsSet;
    private bool targetWasSet;
    private bool isShown;
    private const float ARROW_ANGLE_FACE_RIGHT = 0.0f;
    private const float ARROW_ANGLE_FACE_LEFT = 180f;
    private HashSet<HandoffType> _handoffSideSet = new HashSet<HandoffType>()
    {
      HandoffType.ShotgunLeftSideHole2,
      HandoffType.ShotgunLeftSideHole1,
      HandoffType.ShotgunLeftSideReadPassOption,
      HandoffType.ShotgunLeftSideReadOption,
      HandoffType.ShotgunLeftSideDraw,
      HandoffType.SinglebackHole2,
      HandoffType.SinglebackHole4,
      HandoffType.SinglebackHole6,
      HandoffType.ShotgunLeftSideHole2,
      HandoffType.ShotgunLeftSideHole1,
      HandoffType.PistolHole2,
      HandoffType.PistolHole4,
      HandoffType.PistolHole6
    };

    private void Start()
    {
      this._rectTransform = (RectTransform) this.transform;
      SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
    }

    private void OnEnable()
    {
      if ((Object) this._cam == (Object) null)
      {
        this._cam = PlayerCamera.Camera;
        this._camTx = this._cam.transform;
        this._screenRect = this._cam.pixelRect;
        this._rectCenter = this._screenRect.center;
      }
      this._prevPos = (Vector3) this._screenRect.center;
    }

    private void Update()
    {
      this.targetWasSet = this.targetIsSet;
      if ((Object) this._target == (Object) null)
      {
        this.targetIsSet = false;
      }
      else
      {
        if ((Object) this._cam == (Object) null)
          return;
        bool flag1 = false;
        this.targetIsSet = true;
        if (MatchManager.Exists())
          flag1 = this._handoffSideSet.Contains(MatchManager.instance.playManager.savedOffPlay.GetHandoffType());
        if (this.targetIsSet && !this.targetWasSet)
          this.Show();
        Vector3 position1 = this._target.position with
        {
          y = Field.ONE_FOOT * 5f
        };
        Vector3 screenPoint = this._cam.WorldToScreenPoint(position1);
        Vector3 position2 = PersistentSingleton<GamePlayerController>.Instance.PlayerRefs.headAnchor.position with
        {
          y = Field.ONE_FOOT * 5f
        };
        Vector3 from = new Vector3(position2.x, Field.ONE_FOOT * 5f, Field.OFFENSIVE_GOAL_LINE) - position2;
        double f = (double) Vector3.SignedAngle(from, position1 - position2, Vector3.up);
        bool flag2 = f > 0.0;
        bool flag3 = (double) Mathf.Abs((float) f) > 130.0;
        bool flag4 = (double) Vector3.SignedAngle(from, PersistentSingleton<GamePlayerController>.Instance.PlayerRefs.headAnchor.forward, Vector3.up) > 0.0;
        if ((double) screenPoint.z < 0.0)
        {
          screenPoint.y *= -1f;
          if (!flag1)
            screenPoint.x *= -1f;
          else
            screenPoint.x = this._screenRect.width * 2f - screenPoint.x;
        }
        Vector2 vector2_1 = (Vector2) screenPoint - this._rectCenter;
        Vector3 b;
        if (this._screenRect.Contains(screenPoint))
        {
          if (this._breakWhenLookAt && (double) Vector3.Dot(this._camTx.forward, (position1 - this._camTx.position).normalized) > 0.93999999761581421)
            this.Hide();
          if (this._showWhenLookAway && (double) Vector3.Dot(this._camTx.forward, (position1 - this._camTx.position).normalized) <= 0.93999999761581421)
            this.Show();
          float num = Mathf.Clamp(vector2_1.magnitude - this._minDist, -this._radius, this._radius);
          b = (Vector3) (this._rectCenter + vector2_1.normalized * num);
        }
        else
          b = (Vector3) (this._rectCenter + vector2_1.normalized * this._radius);
        Vector2 vector2_2 = (Vector2) (screenPoint - b);
        this._prevAngle = Mathf.LerpAngle(this._prevAngle, Mathf.Atan2(vector2_2.y, vector2_2.x) * 57.29578f, this._lerpCoef);
        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, this._prevAngle);
        this._prevPos = (Vector3) Vector2.Lerp((Vector2) this._prevPos, (Vector2) b, this._lerpCoef);
        this._rectTransform.anchoredPosition = (Vector2) this._prevPos;
        if (flag3)
        {
          if (!this.isShown)
            this.Show();
          if (flag1 && !flag4)
            quaternion = Quaternion.Euler(0.0f, 0.0f, 0.0f);
          if (!flag1 & flag4)
            quaternion = Quaternion.Euler(0.0f, 0.0f, 180f);
        }
        else if (!flag1)
        {
          if (flag2 && !flag4)
            quaternion = Quaternion.Euler(0.0f, 0.0f, 0.0f);
          if (!flag2 & flag4)
            quaternion = Quaternion.Euler(0.0f, 0.0f, 180f);
        }
        else
        {
          if (!flag2 & flag4)
            quaternion = Quaternion.Euler(0.0f, 0.0f, 180f);
          if (flag2 && !flag4)
            quaternion = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        this.transform.localRotation = quaternion;
      }
    }

    public void PointTo(Transform target, bool breakWhenLookAt = false, bool showWhenLookAway = false)
    {
      this._target = target;
      this._breakWhenLookAt = breakWhenLookAt;
      this._showWhenLookAway = showWhenLookAway;
      this.gameObject.SetActive(true);
      this.isShown = true;
    }

    public void Hide(bool clearFlags = false)
    {
      if (clearFlags)
      {
        this._showWhenLookAway = false;
        this._breakWhenLookAt = false;
      }
      if (!this._showWhenLookAway)
      {
        this._target = (Transform) null;
        this.gameObject.SetActive(false);
      }
      else
      {
        for (int index = 0; index < this.transform.childCount; ++index)
          this.transform.GetChild(index).gameObject.SetActive(false);
      }
      this.isShown = false;
    }

    public void Show()
    {
      if (!this._showWhenLookAway)
        return;
      for (int index = 0; index < this.transform.childCount; ++index)
        this.transform.GetChild(index).gameObject.SetActive(true);
      this.isShown = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      if (scene.name.Contains("LockerRoomUI"))
      {
        this.playerIsInLockerRoom = true;
        this.Hide();
      }
      else
        this.playerIsInLockerRoom = false;
    }
  }
}
