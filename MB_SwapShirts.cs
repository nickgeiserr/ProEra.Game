// Decompiled with JetBrains decompiler
// Type: MB_SwapShirts
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

public class MB_SwapShirts : MonoBehaviour
{
  public MB3_MeshBaker meshBaker;
  public Renderer[] clothingAndBodyPartsBareTorso;
  public Renderer[] clothingAndBodyPartsBareTorsoDamagedArm;
  public Renderer[] clothingAndBodyPartsHoodie;

  private void Start()
  {
    GameObject[] gos = new GameObject[this.clothingAndBodyPartsBareTorso.Length];
    for (int index = 0; index < this.clothingAndBodyPartsBareTorso.Length; ++index)
      gos[index] = this.clothingAndBodyPartsBareTorso[index].gameObject;
    this.meshBaker.ClearMesh();
    this.meshBaker.AddDeleteGameObjects(gos, (GameObject[]) null);
    this.meshBaker.Apply();
  }

  private void OnGUI()
  {
    if (GUILayout.Button("Wear Hoodie"))
      this.ChangeOutfit(this.clothingAndBodyPartsHoodie);
    if (GUILayout.Button("Bare Torso"))
      this.ChangeOutfit(this.clothingAndBodyPartsBareTorso);
    if (!GUILayout.Button("Damaged Arm"))
      return;
    this.ChangeOutfit(this.clothingAndBodyPartsBareTorsoDamagedArm);
  }

  private void ChangeOutfit(Renderer[] outfit)
  {
    List<GameObject> gameObjectList1 = new List<GameObject>();
    foreach (GameObject gameObject in this.meshBaker.meshCombiner.GetObjectsInCombined())
    {
      Renderer component = gameObject.GetComponent<Renderer>();
      bool flag = false;
      for (int index = 0; index < outfit.Length; ++index)
      {
        if ((Object) component == (Object) outfit[index])
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        gameObjectList1.Add(component.gameObject);
        Debug.Log((object) ("Removing " + ((object) component.gameObject)?.ToString()));
      }
    }
    List<GameObject> gameObjectList2 = new List<GameObject>();
    for (int index = 0; index < outfit.Length; ++index)
    {
      if (!this.meshBaker.meshCombiner.GetObjectsInCombined().Contains(outfit[index].gameObject))
      {
        gameObjectList2.Add(outfit[index].gameObject);
        Debug.Log((object) ("Adding " + ((object) outfit[index].gameObject)?.ToString()));
      }
    }
    this.meshBaker.AddDeleteGameObjects(gameObjectList2.ToArray(), gameObjectList1.ToArray());
    this.meshBaker.Apply();
  }
}
