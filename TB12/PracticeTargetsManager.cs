// Decompiled with JetBrains decompiler
// Type: TB12.PracticeTargetsManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  public class PracticeTargetsManager : MonoBehaviour
  {
    [SerializeField]
    private PracticeTargetsStore _targetsStore;
    private readonly Dictionary<string, TargetsController> _targetGroups = new Dictionary<string, TargetsController>();
    private readonly RoutineHandle _routineHandle = new RoutineHandle();
    private TargetsController _targetsController;
    private string _targetGroupId;

    public void LoadTargets(string id)
    {
      if (id == this._targetGroupId)
        this._targetsController.SetState(true);
      else
        this._routineHandle.Run(this.SwitchTargetsRoutine(id));
    }

    public void HideTargets()
    {
      if (!((Object) this._targetsController != (Object) null))
        return;
      this._targetsController.SetState(false);
    }

    private IEnumerator SwitchTargetsRoutine(string groupId)
    {
      PracticeTargetsManager practiceTargetsManager = this;
      if ((Object) practiceTargetsManager._targetsController != (Object) null)
      {
        practiceTargetsManager._targetsController.SetState(false);
        yield return (object) new WaitForSeconds(0.5f);
      }
      if (!practiceTargetsManager._targetGroups.TryGetValue(groupId, out practiceTargetsManager._targetsController))
      {
        TargetsController targetsGroup = practiceTargetsManager._targetsStore.GetTargetsGroup(groupId);
        if ((Object) targetsGroup == (Object) null)
        {
          Debug.LogError((object) ("Failed to find target group " + groupId));
          yield break;
        }
        else
        {
          practiceTargetsManager._targetsController = Object.Instantiate<TargetsController>(targetsGroup, practiceTargetsManager.transform);
          practiceTargetsManager._targetsController.transform.ResetTransform();
          practiceTargetsManager._targetGroups.Add(groupId, practiceTargetsManager._targetsController);
        }
      }
      practiceTargetsManager._targetGroupId = groupId;
      practiceTargetsManager._targetsController.SetState(true);
    }
  }
}
