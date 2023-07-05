// Decompiled with JetBrains decompiler
// Type: UDB.Note
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace UDB
{
  [AddComponentMenu("UDB/Note/Add Note")]
  public class Note : MonoBehaviour
  {
    public NoteColor color;
    public string title = "Note title";
    public string note = "Your note here!";
  }
}
