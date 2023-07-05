// Decompiled with JetBrains decompiler
// Type: PBC.CameraViewControl_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  [RequireComponent(typeof (Camera))]
  [RequireComponent(typeof (AudioListener))]
  public class CameraViewControl_PBC : MonoBehaviour
  {
    private Camera camera3rd;
    private AudioListener audioListener;
    public GameObject alternativeCamera;
    private bool thirdView = true;
    private bool lockCursorn;

    private void Awake()
    {
      this.camera3rd = this.GetComponent<Camera>();
      this.audioListener = this.GetComponent<AudioListener>();
      this.camera3rd.enabled = false;
      this.audioListener.enabled = false;
      if ((bool) (Object) this.alternativeCamera && !this.alternativeCamera.tag.Equals("MainCamera"))
        this.alternativeCamera.tag = "MainCamera";
      for (int index = 0; index < 10 && (bool) (Object) Camera.main; ++index)
        Camera.main.gameObject.SetActive(false);
    }

    private void Start()
    {
      if (!(bool) (Object) Camera.main || !this.alternativeCamera.gameObject.activeInHierarchy)
        this.thirdView = true;
      if (this.thirdView)
      {
        if ((bool) (Object) this.alternativeCamera)
          this.alternativeCamera.SetActive(false);
        this.camera3rd.enabled = true;
        this.audioListener.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
      }
      else
      {
        if ((bool) (Object) this.alternativeCamera)
          this.alternativeCamera.SetActive(true);
        this.camera3rd.enabled = false;
        this.audioListener.enabled = false;
      }
    }

    private void OnGUI()
    {
      if (!this.camera3rd.enabled)
        return;
      GUI.Box(new Rect(1f, 1f, 129f, 209f), "Move = WASD\nDrag character =\n=Middle mouse\nLaunch ball = B\nSuspend ragoll = H\nSprint = Shift\nWalk = Ctrl\nRoundkick = R\nJump = space\nSwitch Camera = C\nWall gravity = G\nSlomo = N\nClimb = T,Y\n\n.");
    }

    private void Update()
    {
      if (!Input.GetKeyDown(KeyCode.C) || !(bool) (Object) this.alternativeCamera)
        return;
      if (!this.thirdView)
      {
        this.alternativeCamera.SetActive(false);
        this.camera3rd.enabled = true;
        this.audioListener.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        this.lockCursorn = false;
        this.thirdView = true;
      }
      else
      {
        this.alternativeCamera.SetActive(true);
        this.camera3rd.enabled = false;
        this.audioListener.enabled = false;
        if (this.lockCursorn)
          Cursor.lockState = CursorLockMode.Locked;
        this.thirdView = false;
      }
    }
  }
}
