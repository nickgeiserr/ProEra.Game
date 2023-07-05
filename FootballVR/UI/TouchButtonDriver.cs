// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.TouchButtonDriver
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace FootballVR.UI
{
  [ExecuteAlways]
  public class TouchButtonDriver : MonoBehaviour
  {
    [SerializeField]
    [Range(0.0f, 1f)]
    private float _normalizedPosition;
    [SerializeField]
    private TouchButton _touchButton;

    private void OnValidate()
    {
      if ((Object) this._touchButton == (Object) null)
        this._touchButton = this.GetComponent<TouchButton>();
      this._touchButton.Initialize();
      this._touchButton.SetNormalizedPosition(this._normalizedPosition);
    }

    private void Update()
    {
      this._touchButton.Initialize();
      this._touchButton.SetNormalizedPosition(this._normalizedPosition);
    }
  }
}
