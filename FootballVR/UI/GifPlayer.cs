// Decompiled with JetBrains decompiler
// Type: FootballVR.UI.GifPlayer
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

namespace FootballVR.UI
{
  public class GifPlayer : MonoBehaviour
  {
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private float _framesPerSecond = 10f;

    private void Update() => this._image.sprite = this._sprites[(int) ((double) Time.time * (double) this._framesPerSecond) % this._sprites.Length];
  }
}
