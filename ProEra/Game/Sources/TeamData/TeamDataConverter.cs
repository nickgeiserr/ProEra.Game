// Decompiled with JetBrains decompiler
// Type: ProEra.Game.Sources.TeamData.TeamDataConverter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace ProEra.Game.Sources.TeamData
{
  [ExecuteInEditMode]
  public class TeamDataConverter : MonoBehaviour, ICsvConverter
  {
    [SerializeField]
    private TeamDataTransferObject[] objectsToConvert;

    public void ConvertObjects()
    {
      Debug.Log((object) "Converting objects...");
      foreach (TeamDataTransferObject dataTransferObject in this.objectsToConvert)
      {
        dataTransferObject.teamDataObject.SetDataFromConfigFile(dataTransferObject.teamFile.text);
        Debug.Log((object) ("Finished converting " + dataTransferObject.teamDataObject.name));
      }
    }
  }
}
