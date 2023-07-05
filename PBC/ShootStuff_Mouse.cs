// Decompiled with JetBrains decompiler
// Type: PBC.ShootStuff_Mouse
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public class ShootStuff_Mouse : MonoBehaviour
  {
    private Camera thisCamera;
    private AudioSource audioSource;
    public Texture crosshairTexture;
    public AudioClip gunSound;
    [Range(0.0f, 1f)]
    [SerializeField]
    private float soundVolume = 0.2f;
    [Range(20f, 400f)]
    [SerializeField]
    private float bulletImpulse = 100f;
    private bool cursorInGUI;
    private bool fire;
    private Rect guiBox = new Rect(5f, (float) (Screen.height - 50), 160f, 45f);
    private Rect guiBox2 = new Rect(10f, (float) (Screen.height - 25), 150f, 15f);
    private RaycastHit raycastHit;

    private void Awake()
    {
      if (!(bool) (Object) (this.thisCamera = this.GetComponent<Camera>()))
        Debug.LogWarning((object) ("ShootStuff script wants to be on a camera.\n" + this.name + " is not a camera.\n"));
      else if (!(bool) (Object) this.crosshairTexture)
        Debug.LogWarning((object) ("You need to assign crosshairTexture in the ShootStuff script on " + this.name + "\n"));
      if ((bool) (Object) (this.audioSource = this.GetComponent<AudioSource>()))
      {
        this.audioSource.clip = this.gunSound;
        this.audioSource.volume = this.soundVolume;
      }
      else
      {
        if (!(bool) (Object) this.gunSound)
          return;
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.audioSource.clip = this.gunSound;
        this.audioSource.volume = this.soundVolume;
      }
    }

    private void Update()
    {
      if (!this.thisCamera.enabled)
        return;
      this.cursorInGUI = this.guiBox.Contains(Input.mousePosition.x * Vector2.right + ((float) Screen.height - Input.mousePosition.y) * Vector2.up);
      if (Input.GetMouseButtonDown(0) && !this.cursorInGUI)
      {
        this.fire = true;
        if ((bool) (Object) this.audioSource)
          this.audioSource.Play();
      }
      if (Input.GetMouseButton(1) && !this.cursorInGUI)
        this.thisCamera.fieldOfView = 30f;
      else
        this.thisCamera.fieldOfView = 60f;
    }

    private void FixedUpdate()
    {
      if (!this.fire)
        return;
      this.fire = false;
      Ray ray = this.thisCamera.ScreenPointToRay((Vector3) new Vector2(Input.mousePosition.x, Input.mousePosition.y));
      if (!Physics.Raycast(ray, out this.raycastHit, 100f))
        return;
      this.raycastHit.transform.SendMessage("ReceiveBulletHit", (object) new BulletInfo_PBC(this.bulletImpulse * ray.direction, this.raycastHit, Vector3.zero), SendMessageOptions.DontRequireReceiver);
      if (!(bool) (Object) this.raycastHit.rigidbody || this.raycastHit.rigidbody.isKinematic)
        return;
      this.raycastHit.rigidbody.AddForceAtPosition(this.bulletImpulse * ray.direction, this.raycastHit.point, ForceMode.Impulse);
    }

    private void OnGUI()
    {
      if (!this.thisCamera.enabled)
        return;
      GUI.DrawTexture(new Rect(Input.mousePosition.x - 20f, (float) ((double) Screen.height - (double) Input.mousePosition.y - 20.0), 40f, 40f), this.crosshairTexture, ScaleMode.ScaleToFit, true);
      this.bulletImpulse = GUI.HorizontalSlider(this.guiBox2, this.bulletImpulse, 20f, 400f);
      if (this.cursorInGUI && Input.GetMouseButton(0))
        GUI.Box(this.guiBox, this.bulletImpulse.ToString());
      else
        GUI.Box(this.guiBox, "Bullet impulse");
    }
  }
}
