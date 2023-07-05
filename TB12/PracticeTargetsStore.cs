// Decompiled with JetBrains decompiler
// Type: TB12.PracticeTargetsStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Stores/Practice Targets Store", fileName = "PracticeTargetsStore")]
  public class PracticeTargetsStore : ScriptableObject
  {
    private const string _defaultGroupId = "default";
    [SerializeField]
    private TargetsGroup[] _targetsGroups;

    public TargetsController GetTargetsGroup(string id)
    {
      foreach (TargetsGroup targetsGroup in this._targetsGroups)
      {
        if (targetsGroup.name == id)
          return targetsGroup.Prefab;
      }
      return !(id != "default") ? (TargetsController) null : this.GetTargetsGroup("default");
    }
  }
}
