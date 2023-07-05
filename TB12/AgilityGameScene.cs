// Decompiled with JetBrains decompiler
// Type: TB12.AgilityGameScene
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using UnityEngine;

namespace TB12
{
  public class AgilityGameScene : MonoBehaviour
  {
    private UniformStore _uniformStore;
    [SerializeField]
    private GameplayStore _gameplayStore;
    [SerializeField]
    private QbStore _qbStore;
    [SerializeField]
    private OpponentQB _qbPrefab;
    [SerializeField]
    private Transform[] _qbPositions;
    private ManagedList<OpponentQB> _qbs;

    public bool Done
    {
      get
      {
        if (this._qbs == null)
          return true;
        foreach (OpponentQB qb in this._qbs)
        {
          if (!qb.Done)
            return false;
        }
        return true;
      }
    }

    private void Awake()
    {
      this._uniformStore = SaveManager.GetUniformStore();
      this._qbs = new ManagedList<OpponentQB>((IObjectPool<OpponentQB>) new MonoBehaviorObjectPool<OpponentQB>(this._qbPrefab, this.transform));
    }

    public void CleanupScene()
    {
      foreach (OpponentQB qb in this._qbs)
        qb.CleanupScene();
    }
  }
}
