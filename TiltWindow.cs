// Decompiled with JetBrains decompiler
// Type: TiltWindow
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class TiltWindow : MonoBehaviour
{
  public Vector2 range = new Vector2(5f, 3f);
  private Transform mTrans;
  private Quaternion mStart;
  private Vector2 mRot = Vector2.zero;

  private void Start()
  {
    this.mTrans = this.transform;
    this.mStart = this.mTrans.localRotation;
  }

  private void Update()
  {
    Vector3 mousePosition = Input.mousePosition;
    float num1 = (float) Screen.width * 0.5f;
    float num2 = (float) Screen.height * 0.5f;
    this.mRot = Vector2.Lerp(this.mRot, new Vector2(Mathf.Clamp((mousePosition.x - num1) / num1, -1f, 1f), Mathf.Clamp((mousePosition.y - num2) / num2, -1f, 1f)), Time.deltaTime * 5f);
    this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.range.y, this.mRot.x * this.range.x, 0.0f);
  }
}
