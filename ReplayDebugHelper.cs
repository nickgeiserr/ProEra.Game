// Decompiled with JetBrains decompiler
// Type: ReplayDebugHelper
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using ProEra.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ReplayDebugHelper : MonoBehaviour
{
  public static ReplayDebugHelper Instance;
  [SerializeField]
  private GameObject _throwPathPrefab;
  [SerializeField]
  private GameObject _throwLandingPrefab;
  private Dictionary<Color, List<GameObject>> _debugItemList;
  private Dictionary<Color, GameObject> _debugLandingDict;
  private List<GameObject> _debugReceiverRange;

  private void Start()
  {
    AxisReceiver.OnDrawRange += new Action<Transform>(this.DrawRange);
    AutoAim.OnShowPath += new Action<Vector3[], Vector3, Color>(this.DrawPath);
    this._debugItemList = new Dictionary<Color, List<GameObject>>();
    this._debugLandingDict = new Dictionary<Color, GameObject>();
    this._debugReceiverRange = new List<GameObject>();
  }

  private void Update()
  {
  }

  public void DrawPath(Vector3[] points, Vector3 targetPos, Color c)
  {
    if (!(bool) Globals.ReplayMode)
      return;
    if (!this._debugItemList.ContainsKey(c))
    {
      this._debugItemList.Add(c, new List<GameObject>());
      this._debugLandingDict.Add(c, UnityEngine.Object.Instantiate<GameObject>(this._throwLandingPrefab));
    }
    List<GameObject> debugItem = this._debugItemList[c];
    for (int index = 0; index < points.Length; ++index)
    {
      if (debugItem.Count <= index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._throwPathPrefab);
        debugItem.Add(gameObject);
      }
      GameObject gameObject1 = debugItem[index];
      gameObject1.transform.position = points[index];
      gameObject1.GetComponent<MeshRenderer>().material.color = c;
    }
    this._debugLandingDict[c].transform.position = targetPos;
    this._debugLandingDict[c].GetComponent<MeshRenderer>().material.color = c;
  }

  public void DrawRange(Transform t)
  {
    if (!(bool) Globals.ReplayMode)
      return;
    for (int index1 = 0; index1 < 14; ++index1)
    {
      Vector3 inDirection = Vector3.Lerp(t.right, t.forward, 0.55f);
      int index2 = 2 * index1;
      if (this._debugReceiverRange.Count <= index2)
        this._debugReceiverRange.Add(UnityEngine.Object.Instantiate<GameObject>(this._throwPathPrefab));
      GameObject gameObject1 = this._debugReceiverRange[index2];
      gameObject1.transform.position = t.position + inDirection * (float) index1;
      gameObject1.GetComponent<MeshRenderer>().material.color = Color.green;
      int index3 = 2 * index1 + 1;
      if (this._debugReceiverRange.Count <= index3)
        this._debugReceiverRange.Add(UnityEngine.Object.Instantiate<GameObject>(this._throwPathPrefab));
      GameObject gameObject2 = this._debugReceiverRange[index3];
      gameObject2.transform.position = t.position + Vector3.Reflect(inDirection, t.right) * (float) index1;
      gameObject2.GetComponent<MeshRenderer>().material.color = Color.green;
    }
  }
}
