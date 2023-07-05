// Decompiled with JetBrains decompiler
// Type: SplashScript
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

public class SplashScript : MonoBehaviour
{
  private void Awake()
  {
    LoadingScreenManager.self.HideWindow();
    this.StartCoroutine(this.ProceedToTitleScreen());
  }

  private IEnumerator ProceedToTitleScreen()
  {
    yield return (object) new WaitForSeconds(4f);
    LoadingScreenManager.self.LoadScene("Title Screen", "");
    LoadingScreenManager.self.HideWindowImmediately();
  }
}
