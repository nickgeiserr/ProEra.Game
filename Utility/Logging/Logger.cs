// Decompiled with JetBrains decompiler
// Type: Utility.Logging.Logger
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utility.Logging
{
  public class Logger
  {
    private string _path;
    private string _filename;
    private StringBuilder _contents;
    private Dictionary<LogType, string> _prefix;

    public Logger(string path, string filenamePrefix, string filetype, bool addTimestamp)
    {
      this._contents = new StringBuilder();
      this._prefix = new Dictionary<LogType, string>()
      {
        {
          LogType.ERROR,
          "ERROR: "
        },
        {
          LogType.WARNING,
          "WARNING: "
        },
        {
          LogType.INFO,
          "INFO: "
        }
      };
      this._path = path;
      if (!Directory.Exists(this._path))
        Directory.CreateDirectory(this._path);
      this._contents.Append(filenamePrefix);
      if (addTimestamp)
        this._contents.Append(DateTime.Now.ToString("_yyyyMMddHHmmss"));
      this._contents.Append("." + filetype);
      this._filename = this._contents.ToString();
    }

    public void Log(LogType type, string message)
    {
      this._contents.Length = 0;
      this._contents.Append(this._prefix[type]).Append(message).Append(Environment.NewLine);
      File.AppendAllText(this._path + this._filename, this._contents.ToString());
    }
  }
}
