// Decompiled with JetBrains decompiler
// Type: PlayDataDef
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

public class PlayDataDef : PlayData
{
  public PlayDataDef(
    FormationPositionsDef f,
    string playN,
    PlayConcept concept,
    PlayAssignment p1Route,
    PlayAssignment p2Route,
    PlayAssignment p3Route,
    PlayAssignment p4Route,
    PlayAssignment p5Route,
    PlayAssignment p6Route,
    PlayAssignment p7Route,
    PlayAssignment p8Route,
    PlayAssignment p9Route,
    PlayAssignment p10Route,
    PlayAssignment p11Route)
    : base((FormationPositions) f, playN, concept)
  {
    this.routes = new PlayAssignment[11]
    {
      p1Route,
      p2Route,
      p3Route,
      p4Route,
      p5Route,
      p6Route,
      p7Route,
      p8Route,
      p9Route,
      p10Route,
      p11Route
    };
  }

  public FormationPositionsDef GetFormation() => (FormationPositionsDef) base.GetFormation();
}
