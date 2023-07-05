// Decompiled with JetBrains decompiler
// Type: UDB.TouchKit
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace UDB
{
  public class TouchKit : SingletonBehaviour<TouchKit, MonoBehaviour>
  {
    [HideInInspector]
    public bool simulateTouches = true;
    [HideInInspector]
    public bool simulateMultitouch = true;
    [HideInInspector]
    public bool drawTouches;
    [HideInInspector]
    public bool drawDebugBoundaryFrames;
    public bool autoScaleRectsAndDistances = true;
    public bool shouldAutoUpdateTouches = true;
    private Vector2 _designTimeResolution = new Vector2(320f, 180f);
    public int maxTouchesToProcess = 2;
    private List<TKAbstractGestureRecognizer> gestureRecognizers = new List<TKAbstractGestureRecognizer>(5);
    private TKTouch[] touchCache;
    private List<TKTouch> liveTouches = new List<TKTouch>(2);
    private bool shouldCheckForLostTouches;
    private const float inchesToCentimeters = 2.54f;
    [Range(0.0f, 1f)]
    public float pinchSensitivity = 1f;

    public Vector2 designTimeResolution
    {
      get => this._designTimeResolution;
      set
      {
        this._designTimeResolution = value;
        this.SetupRuntimeScale();
      }
    }

    public Vector2 runtimeScaleModifier { get; private set; }

    public float runtimeDistanceModifier { get; private set; }

    public Vector2 pixelsToUnityUnitsMultiplier { get; private set; }

    public static float screenPixelsPerCm
    {
      get
      {
        float num = 72f;
        return (double) Screen.dpi != 0.0 ? Screen.dpi / 2.54f : num / 2.54f;
      }
    }

    private new void Awake()
    {
      Camera cam = Camera.main ?? Camera.allCameras[0];
      if (cam.orthographic)
        TouchKit.SetupPixelsToUnityUnitsMultiplierWithCamera(cam);
      else
        this.pixelsToUnityUnitsMultiplier = Vector2.one;
      this.SetupRuntimeScale();
      this.touchCache = new TKTouch[this.maxTouchesToProcess];
      for (int fingerId = 0; fingerId < this.maxTouchesToProcess; ++fingerId)
        this.touchCache[fingerId] = new TKTouch(fingerId);
    }

    private void Update()
    {
      if (!this.shouldAutoUpdateTouches)
        return;
      this.InternalUpdateTouches();
    }

    private void OnApplicationQuit() => Object.Destroy((Object) this.gameObject);

    private void AddTouchesUnityForgotToEndToLiveTouchesList()
    {
      for (int index = 0; index < this.touchCache.Length; ++index)
      {
        if (this.touchCache[index].phase != TouchPhase.Ended)
        {
          Debug.LogWarning((object) ("found touch Unity forgot to end with phase: " + this.touchCache[index].phase.ToString()));
          this.touchCache[index].phase = TouchPhase.Ended;
          this.liveTouches.Add(this.touchCache[index]);
        }
      }
    }

    private void InternalUpdateTouches()
    {
      if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(0))
        this.liveTouches.Add(this.touchCache[0].populateFromMouse());
      if (Input.touchCount > 0)
      {
        this.shouldCheckForLostTouches = true;
        int num = Mathf.Min(Input.touches.Length, this.maxTouchesToProcess);
        for (int index = 0; index < num; ++index)
        {
          Touch touch = Input.touches[index];
          if (touch.fingerId < this.maxTouchesToProcess)
            this.liveTouches.Add(this.touchCache[touch.fingerId].PopulateWithTouch(touch));
        }
      }
      else if (this.shouldCheckForLostTouches)
      {
        this.AddTouchesUnityForgotToEndToLiveTouchesList();
        this.shouldCheckForLostTouches = false;
      }
      if (this.liveTouches.Count <= 0)
        return;
      for (int index = 0; index < this.gestureRecognizers.Count; ++index)
        this.gestureRecognizers[index].RecognizeTouches(this.liveTouches);
      this.liveTouches.Clear();
    }

    protected void SetupRuntimeScale()
    {
      SingletonBehaviour<TouchKit, MonoBehaviour>.instance.runtimeScaleModifier = new Vector2((float) Screen.width / SingletonBehaviour<TouchKit, MonoBehaviour>.instance.designTimeResolution.x, (float) Screen.height / SingletonBehaviour<TouchKit, MonoBehaviour>.instance.designTimeResolution.y);
      SingletonBehaviour<TouchKit, MonoBehaviour>.instance.runtimeDistanceModifier = (float) (((double) SingletonBehaviour<TouchKit, MonoBehaviour>.instance.runtimeScaleModifier.x + (double) SingletonBehaviour<TouchKit, MonoBehaviour>.instance.runtimeScaleModifier.y) / 2.0);
      if (SingletonBehaviour<TouchKit, MonoBehaviour>.instance.autoScaleRectsAndDistances)
        return;
      SingletonBehaviour<TouchKit, MonoBehaviour>.instance.runtimeScaleModifier = Vector2.one;
      SingletonBehaviour<TouchKit, MonoBehaviour>.instance.runtimeDistanceModifier = 1f;
    }

    public static void SetupPixelsToUnityUnitsMultiplierWithCamera(Camera cam)
    {
      if (!cam.orthographic)
      {
        Debug.LogError((object) "Attempting to setup unity pixel-to-units modifier with a non-orthographic camera");
      }
      else
      {
        Vector2 vector2 = new Vector2((float) ((double) cam.aspect * (double) cam.orthographicSize * 2.0), cam.orthographicSize * 2f);
        SingletonBehaviour<TouchKit, MonoBehaviour>.instance.pixelsToUnityUnitsMultiplier = new Vector2(vector2.x / (float) Screen.width, vector2.y / (float) Screen.height);
      }
    }

    public static void UpdateTouches()
    {
      if ((Object) SingletonBehaviour<TouchKit, MonoBehaviour>.instance == (Object) null)
        return;
      SingletonBehaviour<TouchKit, MonoBehaviour>.instance.InternalUpdateTouches();
    }

    public static void AddGestureRecognizer(TKAbstractGestureRecognizer recognizer)
    {
      recognizer.pinchSensitivity = SingletonBehaviour<TouchKit, MonoBehaviour>.instance.pinchSensitivity;
      SingletonBehaviour<TouchKit, MonoBehaviour>.instance.gestureRecognizers.Add(recognizer);
      if (recognizer.zIndex <= 0U)
        return;
      SingletonBehaviour<TouchKit, MonoBehaviour>.instance.gestureRecognizers.Sort();
      SingletonBehaviour<TouchKit, MonoBehaviour>.instance.gestureRecognizers.Reverse();
    }

    public static void RemoveGestureRecognizer(TKAbstractGestureRecognizer recognizer)
    {
      if ((Object) SingletonBehaviour<TouchKit, MonoBehaviour>.instance == (Object) null)
        return;
      if (!SingletonBehaviour<TouchKit, MonoBehaviour>.instance.gestureRecognizers.Contains(recognizer))
      {
        Debug.LogError((object) ("Trying to remove gesture recognizer that has not been added: " + recognizer?.ToString()));
      }
      else
      {
        recognizer.Reset();
        SingletonBehaviour<TouchKit, MonoBehaviour>.instance.gestureRecognizers.Remove(recognizer);
      }
    }

    public static void RemoveAllGestureRecognizers()
    {
      if ((Object) SingletonBehaviour<TouchKit, MonoBehaviour>.instance == (Object) null)
        return;
      SingletonBehaviour<TouchKit, MonoBehaviour>.instance.gestureRecognizers.Clear();
    }
  }
}
