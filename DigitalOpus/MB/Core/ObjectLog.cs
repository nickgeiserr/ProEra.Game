// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.ObjectLog
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Text;

namespace DigitalOpus.MB.Core
{
  public class ObjectLog
  {
    private int pos;
    private string[] logMessages;

    private void _CacheLogMessage(string msg)
    {
      if (this.logMessages.Length == 0)
        return;
      this.logMessages[this.pos] = msg;
      ++this.pos;
      if (this.pos < this.logMessages.Length)
        return;
      this.pos = 0;
    }

    public ObjectLog(short bufferSize) => this.logMessages = new string[(int) bufferSize];

    public void Log(MB2_LogLevel l, string msg, MB2_LogLevel currentThreshold)
    {
      MB2_Log.Log(l, msg, currentThreshold);
      this._CacheLogMessage(msg);
    }

    public void Error(string msg, params object[] args) => this._CacheLogMessage(MB2_Log.Error(msg, args));

    public void Warn(string msg, params object[] args) => this._CacheLogMessage(MB2_Log.Warn(msg, args));

    public void Info(string msg, params object[] args) => this._CacheLogMessage(MB2_Log.Info(msg, args));

    public void LogDebug(string msg, params object[] args) => this._CacheLogMessage(MB2_Log.LogDebug(msg, args));

    public void Trace(string msg, params object[] args) => this._CacheLogMessage(MB2_Log.Trace(msg, args));

    public string Dump()
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num = 0;
      if (this.logMessages[this.logMessages.Length - 1] != null)
        num = this.pos;
      for (int index1 = 0; index1 < this.logMessages.Length; ++index1)
      {
        int index2 = (num + index1) % this.logMessages.Length;
        if (this.logMessages[index2] != null)
          stringBuilder.AppendLine(this.logMessages[index2]);
        else
          break;
      }
      return stringBuilder.ToString();
    }
  }
}
