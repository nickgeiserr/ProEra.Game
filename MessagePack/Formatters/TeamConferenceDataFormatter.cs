// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TeamConferenceDataFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

namespace MessagePack.Formatters
{
  public sealed class TeamConferenceDataFormatter : 
    IMessagePackFormatter<TeamConferenceData>,
    IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      TeamConferenceData value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(4);
        resolver.GetFormatterWithVerify<Conference>().Serialize(ref writer, value.conference, options);
        resolver.GetFormatterWithVerify<Division>().Serialize(ref writer, value.division, options);
        writer.Write(value.numInDivision);
        resolver.GetFormatterWithVerify<SeasonModeTeamMap>().Serialize(ref writer, value.teamMap, options);
      }
    }

    public TeamConferenceData Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (TeamConferenceData) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      TeamConferenceData teamConferenceData = new TeamConferenceData();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            teamConferenceData.conference = resolver.GetFormatterWithVerify<Conference>().Deserialize(ref reader, options);
            break;
          case 1:
            teamConferenceData.division = resolver.GetFormatterWithVerify<Division>().Deserialize(ref reader, options);
            break;
          case 2:
            teamConferenceData.numInDivision = reader.ReadInt32();
            break;
          case 3:
            teamConferenceData.teamMap = resolver.GetFormatterWithVerify<SeasonModeTeamMap>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return teamConferenceData;
    }
  }
}
