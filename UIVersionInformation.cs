// Decompiled with JetBrains decompiler
// Type: UIVersionInformation
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;

public class UIVersionInformation : MonoBehaviour
{
  private void Start()
  {
    TMP_Text component = this.GetComponent<TMP_Text>();
    if (Debug.isDebugBuild)
    {
      if (!string.IsNullOrEmpty(VersionInformation.Build))
        component.text = "Build: " + VersionInformation.Build + " " + VersionInformation.Major + "." + VersionInformation.Minor + "." + VersionInformation.Patch + " " + VersionInformation.SemVer + "\nBranch: " + VersionInformation.Branch + " Commit: " + VersionInformation.Changeset + " " + VersionInformation.Platform + "  " + VersionInformation.BuildDate + "\nClean Build: " + VersionInformation.CleanBuild;
      else
        component.text = "Local Build: " + Application.version + " Platform: " + Application.platform.ToString();
    }
    else if (!string.IsNullOrEmpty(VersionInformation.Build))
      component.text = "version " + VersionInformation.Major + "." + VersionInformation.Minor + "." + VersionInformation.Patch;
    else
      component.text = "version " + Application.version;
  }
}
