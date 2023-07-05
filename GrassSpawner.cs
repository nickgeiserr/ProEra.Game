// Decompiled with JetBrains decompiler
// Type: GrassSpawner
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
  [SerializeField]
  private GameObject _grassPrefab;
  [SerializeField]
  private float _range = 1f;
  [SerializeField]
  private int _density = 100;
  private List<GameObject> _grassPool;
  private Transform _player;
  private Vector3 _lastPosition;
  private bool _alternate;

  private void Start()
  {
    this._player = PersistentSingleton<GamePlayerController>.Instance.transform;
    this._lastPosition = this._player.position;
    this._grassPool = new List<GameObject>();
    for (int index = 0; index < this._density; ++index)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(this._grassPrefab);
      gameObject.transform.SetParent(this.transform);
      gameObject.transform.position = this._lastPosition + new Vector3(Random.Range(-1f, 1f) * this._range, 0.0f, Random.Range(-1f, 1f) * this._range);
      gameObject.transform.localEulerAngles = new Vector3(0.0f, Random.Range(0.0f, 360f), 0.0f);
      this._grassPool.Add(gameObject);
    }
  }

  private void FixedUpdate()
  {
    if ((double) Vector3.Distance(this._lastPosition, this._player.position) <= 0.10000000149011612)
      return;
    this._lastPosition = this._player.position;
    for (int index = 0; index < this._grassPool.Count; ++index)
    {
      double num = (double) Vector3.Distance(this._lastPosition, this._grassPool[index].transform.position);
      if ((double) Vector3.Distance(this._lastPosition, this._grassPool[index].transform.position) > (double) this._range)
      {
        if (this._alternate)
        {
          Vector3 vector3 = (this._lastPosition - this._grassPool[index].transform.position).normalized * this._range;
          this._grassPool[index].transform.position = this._lastPosition + vector3;
        }
        else
          this._grassPool[index].transform.position = this._lastPosition + new Vector3(Random.Range(-1f, 1f) * this._range, 0.0f, Random.Range(-1f, 1f) * this._range);
        this._alternate = !this._alternate;
      }
    }
  }
}
