// Decompiled with JetBrains decompiler
// Type: TB12.Activator.UI.LeaderboardDistanceHandler
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework;
using System;
using UnityEngine;

namespace TB12.Activator.UI
{
  public class LeaderboardDistanceHandler : MonoBehaviour
  {
    [SerializeField]
    private RectTransform[] _transforms;

    private UISettings _uiSettings => ScriptableSingleton<UISettings>.Instance;

    private void OnEnable()
    {
      this._uiSettings.LeaderboardDistance.OnValueChanged += new Action<float>(this.OnLeaderboardDistanceChanged);
      this.OnLeaderboardDistanceChanged((float) this._uiSettings.LeaderboardDistance);
    }

    private void OnDisable() => this._uiSettings.LeaderboardDistance.OnValueChanged -= new Action<float>(this.OnLeaderboardDistanceChanged);

    private void OnLeaderboardDistanceChanged(float distance)
    {
      Vector3 vector3 = UIAnchoring.PlayerPosition + this.transform.forward * distance;
      foreach (RectTransform transform in this._transforms)
        transform.position = transform.position.SetX(vector3.x);
    }
  }
}
