// Decompiled with JetBrains decompiler
// Type: LockerRoomItemText
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class LockerRoomItemText : MonoBehaviour
{
  [SerializeField]
  private string _label;
  [SerializeField]
  private TextMeshPro _text;
  [SerializeField]
  private Transform _bg;
  [SerializeField]
  private bool rotateAroundRight = true;
  private Transform _playerCamera;
  private const float SCALE_PER_CHAR = 0.111111112f;
  private const int PADDING = 2;

  private void Start() => this._playerCamera = Camera.main.transform;

  private void Update()
  {
    if (this.rotateAroundRight)
    {
      this.transform.right = this.transform.position - this._playerCamera.position;
    }
    else
    {
      this.transform.forward = this.transform.position - this._playerCamera.position;
      this.transform.Rotate(Vector3.up, -90f);
    }
  }
}
