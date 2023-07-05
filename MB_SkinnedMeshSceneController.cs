// Decompiled with JetBrains decompiler
// Type: MB_SkinnedMeshSceneController
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class MB_SkinnedMeshSceneController : MonoBehaviour
{
  public GameObject swordPrefab;
  public GameObject hatPrefab;
  public GameObject glassesPrefab;
  public GameObject workerPrefab;
  public GameObject targetCharacter;
  public MB3_MeshBaker skinnedMeshBaker;
  private GameObject swordInstance;
  private GameObject glassesInstance;
  private GameObject hatInstance;

  private void Start()
  {
    GameObject gameObject = Object.Instantiate<GameObject>(this.workerPrefab);
    gameObject.transform.position = new Vector3(1.31f, 0.985f, -0.25f);
    Animation component = gameObject.GetComponent<Animation>();
    component.wrapMode = WrapMode.Loop;
    component.cullingType = AnimationCullingType.AlwaysAnimate;
    component.Play("run");
    this.skinnedMeshBaker.AddDeleteGameObjects(new GameObject[1]
    {
      gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject
    }, (GameObject[]) null);
    this.skinnedMeshBaker.Apply();
  }

  private void OnGUI()
  {
    if (GUILayout.Button("Add/Remove Sword"))
    {
      if ((Object) this.swordInstance == (Object) null)
      {
        Transform transform = this.SearchHierarchyForBone(this.targetCharacter.transform, "RightHandAttachPoint");
        this.swordInstance = Object.Instantiate<GameObject>(this.swordPrefab);
        this.swordInstance.transform.parent = transform;
        this.swordInstance.transform.localPosition = Vector3.zero;
        this.swordInstance.transform.localRotation = Quaternion.identity;
        this.swordInstance.transform.localScale = Vector3.one;
        this.skinnedMeshBaker.AddDeleteGameObjects(new GameObject[1]
        {
          this.swordInstance.GetComponentInChildren<MeshRenderer>().gameObject
        }, (GameObject[]) null);
        this.skinnedMeshBaker.Apply();
      }
      else if (this.skinnedMeshBaker.CombinedMeshContains(this.swordInstance.GetComponentInChildren<MeshRenderer>().gameObject))
      {
        this.skinnedMeshBaker.AddDeleteGameObjects((GameObject[]) null, new GameObject[1]
        {
          this.swordInstance.GetComponentInChildren<MeshRenderer>().gameObject
        });
        this.skinnedMeshBaker.Apply();
        Object.Destroy((Object) this.swordInstance);
        this.swordInstance = (GameObject) null;
      }
    }
    if (GUILayout.Button("Add/Remove Hat"))
    {
      if ((Object) this.hatInstance == (Object) null)
      {
        Transform transform = this.SearchHierarchyForBone(this.targetCharacter.transform, "HeadAttachPoint");
        this.hatInstance = Object.Instantiate<GameObject>(this.hatPrefab);
        this.hatInstance.transform.parent = transform;
        this.hatInstance.transform.localPosition = Vector3.zero;
        this.hatInstance.transform.localRotation = Quaternion.identity;
        this.hatInstance.transform.localScale = Vector3.one;
        this.skinnedMeshBaker.AddDeleteGameObjects(new GameObject[1]
        {
          this.hatInstance.GetComponentInChildren<MeshRenderer>().gameObject
        }, (GameObject[]) null);
        this.skinnedMeshBaker.Apply();
      }
      else if (this.skinnedMeshBaker.CombinedMeshContains(this.hatInstance.GetComponentInChildren<MeshRenderer>().gameObject))
      {
        this.skinnedMeshBaker.AddDeleteGameObjects((GameObject[]) null, new GameObject[1]
        {
          this.hatInstance.GetComponentInChildren<MeshRenderer>().gameObject
        });
        this.skinnedMeshBaker.Apply();
        Object.Destroy((Object) this.hatInstance);
        this.hatInstance = (GameObject) null;
      }
    }
    if (!GUILayout.Button("Add/Remove Glasses"))
      return;
    if ((Object) this.glassesInstance == (Object) null)
    {
      Transform transform = this.SearchHierarchyForBone(this.targetCharacter.transform, "NoseAttachPoint");
      this.glassesInstance = Object.Instantiate<GameObject>(this.glassesPrefab);
      this.glassesInstance.transform.parent = transform;
      this.glassesInstance.transform.localPosition = Vector3.zero;
      this.glassesInstance.transform.localRotation = Quaternion.identity;
      this.glassesInstance.transform.localScale = Vector3.one;
      this.skinnedMeshBaker.AddDeleteGameObjects(new GameObject[1]
      {
        this.glassesInstance.GetComponentInChildren<MeshRenderer>().gameObject
      }, (GameObject[]) null);
      this.skinnedMeshBaker.Apply();
    }
    else
    {
      if (!this.skinnedMeshBaker.CombinedMeshContains(this.glassesInstance.GetComponentInChildren<MeshRenderer>().gameObject))
        return;
      this.skinnedMeshBaker.AddDeleteGameObjects((GameObject[]) null, new GameObject[1]
      {
        this.glassesInstance.GetComponentInChildren<MeshRenderer>().gameObject
      });
      this.skinnedMeshBaker.Apply();
      Object.Destroy((Object) this.glassesInstance);
      this.glassesInstance = (GameObject) null;
    }
  }

  public Transform SearchHierarchyForBone(Transform current, string name)
  {
    if (current.name.Equals(name))
      return current;
    for (int index = 0; index < current.childCount; ++index)
    {
      Transform transform = this.SearchHierarchyForBone(current.GetChild(index), name);
      if ((Object) transform != (Object) null)
        return transform;
    }
    return (Transform) null;
  }
}
