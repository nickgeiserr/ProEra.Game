// Decompiled with JetBrains decompiler
// Type: PBC.RemoveScript_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  [ExecuteInEditMode]
  public class RemoveScript_PBC : MonoBehaviour
  {
    private void Awake()
    {
      foreach (Component component1 in Object.FindObjectsOfType<Transform>())
      {
        DragRigidbody_PBC component2;
        if ((bool) (Object) (component2 = component1.GetComponent<DragRigidbody_PBC>()))
        {
          Debug.Log((object) ("Found DragRigidbody script on " + component2.name + ", removed it.\n"));
          Object.DestroyImmediate((Object) component2);
        }
      }
    }

    private void Start() => Object.DestroyImmediate((Object) this);
  }
}
