// Decompiled with JetBrains decompiler
// Type: FPS_Camera
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class FPS_Camera : MonoBehaviour
{
  public Camera FPSCam;
  public float HSpeed;
  public float VSpeed;
  public bool invert = true;
  public float Speed;
  private float h;
  private float v;
  private float i = -1f;

  private void Start()
  {
    if (!(bool) (Object) this.FPSCam)
      this.FPSCam = Camera.main;
    if ((double) this.HSpeed == 0.0)
      this.HSpeed = 15f;
    if ((double) this.VSpeed == 0.0)
      this.VSpeed = 15f;
    if ((double) this.Speed == 0.0)
      this.Speed = 5f;
    this.FPSCam.transform.Rotate(0.0f, 0.0f, 0.0f);
  }

  private void Update()
  {
    this.i = !this.invert ? 1f : -1f;
    this.transform.Rotate(0.0f, this.HSpeed * Input.GetAxis("Mouse X") * Time.deltaTime, 0.0f);
    this.FPSCam.transform.Rotate(this.i * this.VSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime, 0.0f, 0.0f);
    if (Input.GetMouseButton(0))
      this.transform.Translate(0.0f, this.Speed * Time.deltaTime, 0.0f);
    if (Input.GetMouseButton(1))
      this.transform.Translate(0.0f, -this.Speed * Time.deltaTime, 0.0f);
    if (Input.GetMouseButton(2))
      this.transform.position = new Vector3(this.transform.position.x, 6f, this.transform.position.z);
    if ((double) Input.GetAxis("Mouse ScrollWheel") > 0.0)
      --this.FPSCam.fieldOfView;
    if ((double) Input.GetAxis("Mouse ScrollWheel") < 0.0)
      ++this.FPSCam.fieldOfView;
    if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
      this.transform.Translate(-this.Speed * Time.deltaTime, 0.0f, this.Speed * Time.deltaTime);
    else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
      this.transform.Translate(this.Speed * Time.deltaTime, 0.0f, this.Speed * Time.deltaTime);
    else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
      this.transform.Translate(-this.Speed * Time.deltaTime, 0.0f, -this.Speed * Time.deltaTime);
    else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
      this.transform.Translate(this.Speed * Time.deltaTime, 0.0f, -this.Speed * Time.deltaTime);
    else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
      this.transform.Translate(0.0f, 0.0f, this.Speed * Time.deltaTime);
    else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
      this.transform.Translate(0.0f, 0.0f, -this.Speed * Time.deltaTime);
    else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    {
      this.transform.Translate(-this.Speed * Time.deltaTime, 0.0f, 0.0f);
    }
    else
    {
      if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow))
        return;
      this.transform.Translate(this.Speed * Time.deltaTime, 0.0f, 0.0f);
    }
  }
}
