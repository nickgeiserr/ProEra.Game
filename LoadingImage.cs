// Decompiled with JetBrains decompiler
// Type: LoadingImage
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
  [SerializeField]
  private float _spinRate = 5f;
  [SerializeField]
  private Image _loadingImage;
  private bool spinning;
  private Vector3 rot;
  private float zRot;

  public void StartSpinning() => this.spinning = true;

  public void StopSpinning() => this.spinning = false;

  private void Update()
  {
    if (!this.spinning)
      return;
    this.rot = this._loadingImage.transform.eulerAngles;
    this.zRot = this._loadingImage.transform.eulerAngles.z;
    this.zRot += this._spinRate;
    this.rot.z = this.zRot;
    this._loadingImage.transform.eulerAngles = this.rot;
  }
}
