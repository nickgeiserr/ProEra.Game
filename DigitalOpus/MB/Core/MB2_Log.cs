// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB2_Log
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public class MB2_Log
  {
    public static void Log(MB2_LogLevel l, string msg, MB2_LogLevel currentThreshold)
    {
      if (l > currentThreshold)
        return;
      if (l == MB2_LogLevel.error)
        Debug.LogError((object) msg);
      if (l == MB2_LogLevel.warn)
        Debug.LogWarning((object) string.Format("frm={0} WARN {1}", (object) Time.frameCount, (object) msg));
      if (l == MB2_LogLevel.info)
        Debug.Log((object) string.Format("frm={0} INFO {1}", (object) Time.frameCount, (object) msg));
      if (l == MB2_LogLevel.debug)
        Debug.Log((object) string.Format("frm={0} DEBUG {1}", (object) Time.frameCount, (object) msg));
      if (l != MB2_LogLevel.trace)
        return;
      Debug.Log((object) string.Format("frm={0} TRACE {1}", (object) Time.frameCount, (object) msg));
    }

    public static string Error(string msg, params object[] args)
    {
      string message = string.Format("f={0} ERROR {1}", (object) Time.frameCount, (object) string.Format(msg, args));
      Debug.LogError((object) message);
      return message;
    }

    public static string Warn(string msg, params object[] args)
    {
      string message = string.Format("f={0} WARN {1}", (object) Time.frameCount, (object) string.Format(msg, args));
      Debug.LogWarning((object) message);
      return message;
    }

    public static string Info(string msg, params object[] args)
    {
      string message = string.Format("f={0} INFO {1}", (object) Time.frameCount, (object) string.Format(msg, args));
      Debug.Log((object) message);
      return message;
    }

    public static string LogDebug(string msg, params object[] args)
    {
      string message = string.Format("f={0} DEBUG {1}", (object) Time.frameCount, (object) string.Format(msg, args));
      Debug.Log((object) message);
      return message;
    }

    public static string Trace(string msg, params object[] args)
    {
      string message = string.Format("f={0} TRACE {1}", (object) Time.frameCount, (object) string.Format(msg, args));
      Debug.Log((object) message);
      return message;
    }
  }
}
