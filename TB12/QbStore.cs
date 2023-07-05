// Decompiled with JetBrains decompiler
// Type: TB12.QbStore
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using UnityEngine;

namespace TB12
{
  [CreateAssetMenu(menuName = "TB12/Stores/QbStore", fileName = "QbStore")]
  [AppStore]
  public class QbStore : ScriptableObject
  {
    [SerializeField]
    private List<QbData> _qbs;

    public QbData GetData(string qbName)
    {
      for (int index = 0; index < this._qbs.Count; ++index)
      {
        QbData qb = this._qbs[index];
        if (qb.name == qbName)
          return qb;
      }
      Debug.LogError((object) ("Couldn't find qb by name " + qbName));
      return (QbData) null;
    }
  }
}
