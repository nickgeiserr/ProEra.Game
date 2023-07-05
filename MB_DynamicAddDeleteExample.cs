// Decompiled with JetBrains decompiler
// Type: MB_DynamicAddDeleteExample
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_DynamicAddDeleteExample : MonoBehaviour
{
  public GameObject prefab;
  private List<GameObject> objsInCombined = new List<GameObject>();
  private MB3_MeshBaker mbd;
  private GameObject[] objs;

  private float GaussianValue()
  {
    float num1;
    float f;
    do
    {
      num1 = (float) (2.0 * (double) Random.Range(0.0f, 1f) - 1.0);
      float num2 = (float) (2.0 * (double) Random.Range(0.0f, 1f) - 1.0);
      f = (float) ((double) num1 * (double) num1 + (double) num2 * (double) num2);
    }
    while ((double) f >= 1.0);
    float num3 = Mathf.Sqrt(-2f * Mathf.Log(f) / f);
    return num1 * num3;
  }

  private void Start()
  {
    this.mbd = this.GetComponentInChildren<MB3_MeshBaker>();
    int num1 = 25;
    GameObject[] gos = new GameObject[num1 * num1];
    for (int index1 = 0; index1 < num1; ++index1)
    {
      for (int index2 = 0; index2 < num1; ++index2)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.prefab);
        gos[index1 * num1 + index2] = gameObject.GetComponentInChildren<MeshRenderer>().gameObject;
        float num2 = Random.Range(-4f, 4f);
        float num3 = Random.Range(-4f, 4f);
        gameObject.transform.position = new Vector3(9f * (float) index1 + num2, 0.0f, 9f * (float) index2 + num3);
        float y = (float) Random.Range(0, 360);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, y, 0.0f);
        Vector3 vector3 = Vector3.one + Vector3.one * this.GaussianValue() * 0.15f;
        gameObject.transform.localScale = vector3;
        if ((index1 * num1 + index2) % 3 == 0)
          this.objsInCombined.Add(gos[index1 * num1 + index2]);
      }
    }
    this.mbd.AddDeleteGameObjects(gos, (GameObject[]) null);
    this.mbd.Apply();
    this.objs = this.objsInCombined.ToArray();
    this.StartCoroutine(this.largeNumber());
  }

  private IEnumerator largeNumber()
  {
    while (true)
    {
      yield return (object) new WaitForSeconds(1.5f);
      this.mbd.AddDeleteGameObjects((GameObject[]) null, this.objs);
      this.mbd.Apply();
      yield return (object) new WaitForSeconds(1.5f);
      this.mbd.AddDeleteGameObjects(this.objs, (GameObject[]) null);
      this.mbd.Apply();
    }
  }

  private void OnGUI() => GUILayout.Label("Dynamically instantiates game objects. \nRepeatedly adds and removes some of them\n from the combined mesh.");
}
