// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.CoachingStaffCsvConverter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [ExecuteInEditMode]
  public class CoachingStaffCsvConverter : MonoBehaviour, ICsvConverter
  {
    [SerializeField]
    private CoachingStaffCsvConverter.CoachingStaffTransferObject[] _objectsToConvert;

    public void ConvertObjects()
    {
      Debug.Log((object) "Converting objects...");
      foreach (CoachingStaffCsvConverter.CoachingStaffTransferObject staffTransferObject in this._objectsToConvert)
      {
        staffTransferObject.coachingStaffDataObject.SetDataFromCsv(staffTransferObject.coachingStaffCsvFile.text);
        Debug.Log((object) ("Finished converting " + staffTransferObject.coachingStaffDataObject.name));
      }
    }

    [Serializable]
    public class CoachingStaffTransferObject
    {
      [SerializeField]
      public CoachingStaffDataObject coachingStaffDataObject;
      [SerializeField]
      public TextAsset coachingStaffCsvFile;
    }
  }
}
