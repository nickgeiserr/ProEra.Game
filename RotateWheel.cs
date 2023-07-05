// Decompiled with JetBrains decompiler
// Type: RotateWheel
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System;
using UnityEngine;

public class RotateWheel : MonoBehaviour, ICircularLayoutDataSource
{
  [SerializeField]
  private CircularLayout circularLayout;
  [SerializeField]
  private GameObject playerModel;
  [SerializeField]
  private CircularTextItem _prefab;
  [SerializeField]
  private int degrees;

  public CircularLayoutItem ItemPrefab => (CircularLayoutItem) this._prefab;

  public int itemCount => this.degrees;

  private void Start() => this.circularLayout.OnCurrentIndexChanged += new Action<int>(this.CircularLayout_OnCurrentIndexChanged);

  private void OnDestroy() => this.circularLayout.OnCurrentIndexChanged -= new Action<int>(this.CircularLayout_OnCurrentIndexChanged);

  private void CircularLayout_OnCurrentIndexChanged(int obj)
  {
    float num = (float) -this.degrees * this.circularLayout.GetCurrentSpeed() * Time.deltaTime;
    Debug.Log((object) num);
    this.playerModel.transform.Rotate(Vector3.up, num);
  }

  public void SetupItem(int itemIndex, CircularLayoutItem item) => ((CircularTextItem) item).localizationText = "";
}
