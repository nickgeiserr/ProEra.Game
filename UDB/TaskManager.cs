// Decompiled with JetBrains decompiler
// Type: UDB.TaskManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

namespace UDB
{
  public class TaskManager : SingletonBehaviour<TaskManager, MonoBehaviour>
  {
    public static void StartTask(IEnumerator coroutine) => SingletonBehaviour<TaskManager, MonoBehaviour>.instance.StartCoroutine(coroutine);
  }
}
