// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.ScheduleCsvConverter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [ExecuteInEditMode]
  public class ScheduleCsvConverter : MonoBehaviour
  {
    [SerializeField]
    private ScheduleTransferObject[] _objectsToConvert;

    public void ConvertObjects()
    {
      Debug.Log((object) "Converting objects...");
      foreach (ScheduleTransferObject scheduleTransferObject in this._objectsToConvert)
      {
        Debug.Log((object) ("Converting " + scheduleTransferObject.scheduleCsvFile.name));
        if (!scheduleTransferObject.scheduleDataObject.SetDataFromCsv(scheduleTransferObject.scheduleCsvFile.text))
          Debug.LogError((object) ("Aborted asset conversion: " + scheduleTransferObject.scheduleCsvFile.name));
        else
          Debug.Log((object) ("Finished converting " + scheduleTransferObject.scheduleDataObject.name));
      }
    }
  }
}
