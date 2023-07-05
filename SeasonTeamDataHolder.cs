// Decompiled with JetBrains decompiler
// Type: SeasonTeamDataHolder
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class SeasonTeamDataHolder : MonoBehaviour
{
  public static SeasonTeamDataHolder Instance;
  [SerializeField]
  private TeamDataStore[] _teamData;

  private void Awake() => SeasonTeamDataHolder.Instance = this;

  public static TeamDataStore[] GetTeamData() => SeasonTeamDataHolder.Instance._teamData;
}
