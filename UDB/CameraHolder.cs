// Decompiled with JetBrains decompiler
// Type: UDB.CameraHolder
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Sirenix.OdinInspector;
using UnityEngine;

namespace UDB
{
  public class CameraHolder : SerializedMonoBehaviour
  {
    public bool enableCameraOnActive;
    private GameObject[] children;
    public AudioListener _audioListener;
    public GameObject _cameraObject;
    public Camera _cameraComponent;

    public AudioListener audioListener
    {
      get
      {
        if ((Object) this._audioListener == (Object) null && (Object) this.cameraObject != (Object) null)
          this._audioListener = this.cameraObject.GetComponent<AudioListener>();
        return this._audioListener;
      }
    }

    public GameObject cameraObject
    {
      get
      {
        if ((Object) this._cameraObject == (Object) null)
          this.SetCameraObjectAndComponent();
        return this._cameraObject;
      }
    }

    public Camera cameraComponent
    {
      get
      {
        if ((Object) this._cameraComponent == (Object) null)
          this.SetCameraObjectAndComponent();
        return this._cameraComponent;
      }
    }

    private void Awake()
    {
      if (this.enableCameraOnActive)
        return;
      if ((Object) this.audioListener != (Object) null)
        this.audioListener.enabled = false;
      if (!((Object) this.cameraComponent != (Object) null))
        return;
      this.cameraComponent.targetDisplay = 1;
      this.cameraComponent.gameObject.tag = "Untagged";
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
    }

    private void OnEnable() => NotificationCenter<string>.AddListener("activateScene", new Callback<string>(this.SceneActivatedCallback));

    private void OnDisable() => NotificationCenter<string>.RemoveListener("activateScene", new Callback<string>(this.SceneActivatedCallback));

    private void Update()
    {
    }

    private void SceneActivatedCallback(string activatedSceneName)
    {
      if (activatedSceneName.Equals(this.gameObject.scene.name))
        this.AcivateScene();
      else
        this.DeacivateScene();
    }

    private void SetCameraObjectAndComponent()
    {
      this.children = this.gameObject.GetChildren();
      for (int index = 0; index < this.children.Length; ++index)
      {
        Camera component = this.children[index].GetComponent<Camera>();
        if ((Object) component != (Object) null)
        {
          this._cameraComponent = component;
          this._cameraObject = this.children[index];
          break;
        }
      }
    }

    private void AcivateScene()
    {
      DebugManager.StateForKey("Camera Holder Methods");
      if ((Object) this.audioListener != (Object) null)
        this.audioListener.enabled = true;
      if (!((Object) this.cameraComponent != (Object) null))
        return;
      this.cameraComponent.targetDisplay = 0;
      this.cameraComponent.gameObject.tag = "MainCamera";
    }

    private void DeacivateScene()
    {
      DebugManager.StateForKey("Camera Holder Methods");
      if ((Object) this.audioListener != (Object) null)
        this.audioListener.enabled = false;
      if (!((Object) this.cameraComponent != (Object) null))
        return;
      this.cameraComponent.targetDisplay = 1;
      this.cameraComponent.gameObject.tag = "Untagged";
    }
  }
}
