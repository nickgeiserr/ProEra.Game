// Decompiled with JetBrains decompiler
// Type: UDB.Transition
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Reflection;
using UnityEngine;

namespace UDB
{
  public class Transition : CachedMonoBehaviour
  {
    public TransitionState transitionState;
    public Shader shader;
    public float delay = 1f;
    public AnimationCurve curve;
    public bool curveNormalized;
    public CameraType cameraType;
    public Camera cameraTarget;
    public ToType target;
    public Texture targetTexture;
    private Coroutine playRoutine;
    protected float currentTime;
    private bool isRenderActive;
    protected bool hasFirstUpdate;
    protected bool isLingering;
    protected bool isPreparing;
    private TransitionRender _transitionRender;
    private Material _material;
    private Material _waitingMaterial;
    private Vector2 renderTextureScreenSize;
    private RenderTexture _renderTexture;
    private ITransitionHelper _transitionHelper;
    public static Texture previousTexture;

    public float currentTimeNormalized => this.currentTime / this.delay;

    public float currentCurveValue => this.curve.Evaluate(this.curveNormalized ? this.currentTimeNormalized : this.currentTime);

    public bool isWaiting { get; private set; }

    public bool isFinished { get; private set; }

    public bool isPrepared { get; private set; }

    public bool isPlaying => this.playRoutine != null;

    private TransitionRender transitionRender
    {
      get
      {
        if (!(bool) (Object) this._transitionRender)
          this.GetTransitionRender();
        return this._transitionRender;
      }
      set => this._transitionRender = value;
    }

    public Material material
    {
      get
      {
        if ((Object) this._material == (Object) null)
          this._material = new Material(this.shader);
        return this._material;
      }
      private set => this._material = value;
    }

    public Material waitingMaterial
    {
      get
      {
        if ((Object) this._waitingMaterial == (Object) null)
          this._waitingMaterial = new Material(this.shader);
        return this._waitingMaterial;
      }
      private set => this._waitingMaterial = value;
    }

    private RenderTexture renderTexture
    {
      get
      {
        Vector2 vector2 = new Vector2((float) Screen.width, (float) Screen.height);
        if ((Object) this._renderTexture == (Object) null || this.renderTextureScreenSize != vector2)
        {
          if ((bool) (Object) this._renderTexture)
            Object.DestroyImmediate((Object) this._renderTexture);
          this._renderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
          this.renderTextureScreenSize = vector2;
        }
        return this._renderTexture;
      }
      set => this._renderTexture = value;
    }

    protected ITransitionHelper transitionHelper
    {
      get
      {
        if (this._transitionHelper == null)
          this._transitionHelper = this.GetComponent<ITransitionHelper>();
        return this._transitionHelper;
      }
    }

    private void Awake() => this.transitionState = TransitionState.NotActive;

    private void OnDestroy()
    {
      if (!(bool) (Object) this.material)
        return;
      Object.DestroyImmediate((Object) this.material);
    }

    private bool AddedAsRenderer()
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      if (!(bool) (Object) this.transitionRender)
        return false;
      this.transitionRender.AddRender(this);
      return true;
    }

    private void GetTransitionRender()
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      Camera targetCamera = this.GetTargetCamera();
      if (!(bool) (Object) targetCamera)
        return;
      if (DebugManager.StateForKey("Transition Messages"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name + "\ncam : " + targetCamera.gameObject.scene.name));
      this._transitionRender = targetCamera.GetComponent<TransitionRender>();
      if ((bool) (Object) this._transitionRender)
        return;
      this._transitionRender = targetCamera.gameObject.AddComponent<TransitionRender>();
    }

    protected Texture CameraSnapshot(Camera cam)
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      if (!(bool) (Object) cam.targetTexture)
      {
        cam.targetTexture = this.renderTexture;
        if (!cam.tag.IsEqual("MainCamera"))
          cam.Render();
        cam.targetTexture = (RenderTexture) null;
      }
      return (Texture) this.renderTexture;
    }

    private Texture CameraSnapshot(Camera[] cams)
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      for (int index = 0; index < cams.Length; ++index)
      {
        if (!(bool) (Object) cams[index].targetTexture)
        {
          cams[index].targetTexture = this.renderTexture;
          cams[index].Render();
          cams[index].targetTexture = (RenderTexture) null;
        }
      }
      return (Texture) this.renderTexture;
    }

    private void PrepareHelper()
    {
      if (this.transitionHelper == null)
        return;
      this.transitionHelper.Prepare();
    }

    private bool Linger() => this.transitionHelper != null && this.transitionHelper.Linger();

    private void RenderToCamera(Texture source, RenderTexture destination)
    {
      if (this.isLingering)
        this.OnLingerRender(source, destination);
      else if (this.isWaiting || this.isPreparing)
        this.OnWaitRender(source, destination);
      else if (this.transitionHelper.NeedsFirstUpdate() && !this.hasFirstUpdate)
      {
        this.OnFirstUpdate();
        this.OnWaitRender(source, destination);
      }
      else
        Graphics.Blit(source, destination, this.material);
    }

    protected void SetMaterialTexture(
      MaterialType materialType,
      SourceType source,
      Texture sourceTexture)
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      switch (source)
      {
        case SourceType.CameraSnapShot:
          switch (this.cameraType)
          {
            case CameraType.Main:
              if (materialType == MaterialType.Source)
              {
                this.material.SetTexture("_SourceTex", this.CameraSnapshot(Camera.main));
                return;
              }
              this.waitingMaterial.SetTexture("_SourceTex", this.CameraSnapshot(Camera.main));
              return;
            case CameraType.All:
              if (materialType == MaterialType.Source)
              {
                this.material.SetTexture("_SourceTex", this.CameraSnapshot(CameraExtentions.GetAllCameraDepthSorted()));
                return;
              }
              this.waitingMaterial.SetTexture("_SourceTex", this.CameraSnapshot(CameraExtentions.GetAllCameraDepthSorted()));
              return;
            case CameraType.Target:
              if (materialType == MaterialType.Source)
              {
                this.material.SetTexture("_SourceTex", this.CameraSnapshot((bool) (Object) this.cameraTarget ? this.cameraTarget : Camera.main));
                return;
              }
              this.waitingMaterial.SetTexture("_SourceTex", this.CameraSnapshot((bool) (Object) this.cameraTarget ? this.cameraTarget : Camera.main));
              return;
            default:
              return;
          }
        case SourceType.Texture:
          if (materialType == MaterialType.Source)
          {
            this.material.SetTexture("_SourceTex", sourceTexture);
            break;
          }
          this.waitingMaterial.SetTexture("_SourceTex", sourceTexture);
          break;
      }
    }

    protected Camera GetTargetCamera()
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      switch (this.cameraType)
      {
        case CameraType.Main:
          return Camera.main;
        case CameraType.All:
          Camera[] allCameras = Camera.allCameras;
          float num = float.MinValue;
          int index1 = -1;
          for (int index2 = 0; index2 < allCameras.Length; ++index2)
          {
            if ((double) allCameras[index2].depth > (double) num)
            {
              num = allCameras[index2].depth;
              index1 = index2;
            }
          }
          return index1 != -1 ? allCameras[index1] : Camera.main;
        case CameraType.Target:
          return !(bool) (Object) this.cameraTarget ? Camera.main : this.cameraTarget;
        default:
          return (Camera) null;
      }
    }

    protected void SetTranistionState(TransitionState newState)
    {
      if (newState == this.transitionState)
        return;
      if (DebugManager.StateForKey("Transition Methods"))
      {
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
        Debug.Log((object) ("OLD STATE: " + this.transitionState.ToString() + " NEW STATE: " + newState.ToString()));
      }
      this.transitionState = newState;
    }

    protected void FinishedPreparing()
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      this.isPrepared = true;
      this.isPreparing = false;
    }

    public void ActivateRender(bool active)
    {
      this.isRenderActive = active;
      if (this.isRenderActive)
      {
        this.AddedAsRenderer();
      }
      else
      {
        if (!(bool) (Object) this.transitionRender)
          return;
        this.transitionRender.RemoveRender(this);
        this.transitionRender = (TransitionRender) null;
      }
    }

    public void Wait()
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      this.isWaiting = true;
    }

    public void Prepare()
    {
      if (this.isPrepared)
        return;
      this.isPreparing = true;
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      this.PrepareHelper();
      this.currentTime = 0.0f;
      this.OnPrepare();
    }

    public void Play()
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      if (this.playRoutine != null)
        this.StopCoroutine(this.playRoutine);
      this.isWaiting = false;
      this.playRoutine = this.StartCoroutine(this.DoPlay());
    }

    public void End()
    {
      if (DebugManager.StateForKey("Transition Methods"))
        Debug.Log((object) (((object) this).GetType().Name + " at " + MethodBase.GetCurrentMethod().Name + " in " + this.gameObject.scene.name));
      this.currentTime = this.delay;
      this.isPrepared = false;
      this.ActivateRender(false);
      if (this.playRoutine != null)
      {
        this.StopCoroutine(this.playRoutine);
        this.playRoutine = (Coroutine) null;
      }
      this.OnFinish();
      if (!(bool) (Object) this.renderTexture)
        return;
      Object.DestroyImmediate((Object) this.renderTexture);
      this.renderTexture = (RenderTexture) null;
    }

    public IEnumerator DoPlay()
    {
      Transition transition = this;
      transition.Prepare();
      if (transition.isPreparing)
        yield return (object) null;
      if (DebugManager.StateForKey("Transition Co-Routines"))
        Debug.Log((object) (((object) transition).GetType().Name + " at DoPlay CO-ROUTINE BEGIN  in " + transition.gameObject.scene.name));
      transition.ActivateRender(true);
      if (!(bool) (Object) transition.transitionRender)
      {
        transition.End();
      }
      else
      {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        while ((double) transition.currentTime < (double) transition.delay)
        {
          yield return (object) wait;
          transition.currentTime = Mathf.Min(transition.currentTime + Time.smoothDeltaTime, transition.delay);
          transition.OnUpdate();
        }
        transition.playRoutine = (Coroutine) null;
        transition.isLingering = transition.Linger();
        transition.isFinished = false;
        if (DebugManager.StateForKey("Transition Messages"))
          Debug.Log((object) (((object) transition).GetType().Name + " at DoPlay CO-ROUTINE FINISHED  in " + transition.gameObject.scene.name));
      }
    }

    protected virtual void OnFirstUpdate()
    {
      this.Prepare();
      this.hasFirstUpdate = true;
    }

    protected virtual void OnPrepare()
    {
    }

    protected virtual void OnUpdate()
    {
    }

    protected virtual void OnFinish()
    {
    }

    public virtual void OnRenderImage(Texture source, RenderTexture destination)
    {
      if ((bool) (Object) this.material)
      {
        switch (this.target)
        {
          case ToType.Camera:
            this.RenderToCamera(source, destination);
            break;
          case ToType.Texture:
            Graphics.Blit(this.targetTexture, destination, this.material);
            break;
        }
      }
      else
        Graphics.Blit(source, destination);
    }

    protected virtual void OnLingerRender(Texture source, RenderTexture destination)
    {
    }

    protected virtual void OnWaitRender(Texture source, RenderTexture destination)
    {
    }
  }
}
