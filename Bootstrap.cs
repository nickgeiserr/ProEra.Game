// Decompiled with JetBrains decompiler
// Type: Bootstrap
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using ProEra.Game;
using System.Collections;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
  [SerializeField]
  private MonoBehaviour[] syncObjects;
  [SerializeField]
  private GameObject startupPrefab;
  [SerializeField]
  private GameObject gameManagers;

  private IEnumerator Start()
  {
    Debug.Log((object) "Bootstrap Loaded!");
    yield return (object) new WaitForSeconds(0.2f);
    foreach (IBootSync syncObject in this.syncObjects)
      syncObject?.Init();
    yield return (object) new WaitForSeconds(0.2f);
    Object.Instantiate<GameObject>(this.startupPrefab);
    yield return (object) null;
    Object.Instantiate<GameObject>(this.gameManagers);
  }

  private void OnDestroy() => AddressablesData.instance.Dispose();
}
