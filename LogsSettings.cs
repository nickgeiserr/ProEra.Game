// Decompiled with JetBrains decompiler
// Type: LogsSettings
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

[Serializable]
public class LogsSettings
{
  private const string FILENAME = "logs.json";
  private bool isSaving;
  public bool isActive = true;
  public bool FirstCriticalErrorOnScreen;

  public void Save()
  {
    if (this.isSaving)
      return;
    this.isSaving = true;
  }

  public static LogsSettings Load() => (LogsSettings) null;
}
