// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.DepthChartFormatter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;

namespace MessagePack.Formatters
{
  public sealed class DepthChartFormatter : IMessagePackFormatter<DepthChart>, IMessagePackFormatter
  {
    public void Serialize(
      ref MessagePackWriter writer,
      DepthChart value,
      MessagePackSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNil();
      }
      else
      {
        IFormatterResolver resolver = options.Resolver;
        writer.WriteArrayHeader(74);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.goallinePlayers_Heavy, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.goallinePlayers_TwinsOver, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.iFormPlayers_Normal, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.iFormPlayers_Tight, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.iFormPlayers_SlotFlex, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.iFormPlayers_TwinTE, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.iFormPlayers_Twins, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.iFormPlayers_YTrips, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.strongIPlayers_Close, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.strongIPlayers_Normal, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.strongIPlayers_Tight, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.strongIPlayers_TwinTE, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.strongIPlayers_Twins, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.strongIPlayers_TwinsFlex, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.weakIPlayers_CloseTwins, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.weakIPlayers_TwinsFlex, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.weakIPlayers_Normal, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.weakIPlayers_TwinTE, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.weakIPlayers_Twins, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.splitBackPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.singleBackPlayers_Big, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.singleBackPlayers_BigTwins, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.singleBackPlayers_Bunch, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.singleBackPlayers_Slot, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.singleBackPlayers_Spread, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.singleBackPlayers_TreyOpen, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.singleBackPlayers_Trio, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.singleBackPlayers_Trio4WR, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.emptyPlayers_TreyOpen, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.emptyPlayers_FlexTrips, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.pistolPlayers_Ace, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.pistolPlayers_Bunch, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.pistolPlayers_Slot, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.pistolPlayers_SpreadFlex, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.pistolPlayers_TreyOpen, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.pistolPlayers_Trio, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.pistolPlayers_YTrips, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_Normal, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_NormalDimeDropping, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_NormalYFlex, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_QuadsTrio, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_SlotOffset, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_SplitOffset, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_Spread, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_Tight, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_Trey, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_Trips, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_Spread5WR, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_Bunch5WR, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.hailMaryPlayers_Normal, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.qbKneelPlayers_Normal, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.fieldGoalPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.puntPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.fgBlockPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.puntRetPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.kickoffPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.kickRetPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.kickRetOnsidePlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.threeFourPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.fourThreePlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.nickelPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.dimePlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.fourFourPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.fiveThreePlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.sixTwoPlayers, options);
        writer.Write(value.NumberOfDLUsed);
        writer.Write(value.NumberOfLBUsed);
        resolver.GetFormatterWithVerify<List<int>>().Serialize(ref writer, value.playerList, options);
        resolver.GetFormatterWithVerify<List<int>>().Serialize(ref writer, value.playerOVR, options);
        resolver.GetFormatterWithVerify<RosterData>().Serialize(ref writer, value.MainRoster, options);
        resolver.GetFormatterWithVerify<RosterData>().Serialize(ref writer, value.PracticeSquad, options);
        resolver.GetFormatterWithVerify<Dictionary<string, int>>().Serialize(ref writer, value.DefaultPlayers, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.shotgunPlayers_TightWideBack, options);
        resolver.GetFormatterWithVerify<int[]>().Serialize(ref writer, value.nickelPlayers_TwoFour, options);
      }
    }

    public DepthChart Deserialize(
      ref MessagePackReader reader,
      MessagePackSerializerOptions options)
    {
      if (reader.TryReadNil())
        return (DepthChart) null;
      options.Security.DepthStep(ref reader);
      IFormatterResolver resolver = options.Resolver;
      int num = reader.ReadArrayHeader();
      DepthChart depthChart = new DepthChart();
      for (int index = 0; index < num; ++index)
      {
        switch (index)
        {
          case 0:
            depthChart.goallinePlayers_Heavy = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 1:
            depthChart.goallinePlayers_TwinsOver = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 2:
            depthChart.iFormPlayers_Normal = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 3:
            depthChart.iFormPlayers_Tight = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 4:
            depthChart.iFormPlayers_SlotFlex = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 5:
            depthChart.iFormPlayers_TwinTE = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 6:
            depthChart.iFormPlayers_Twins = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 7:
            depthChart.iFormPlayers_YTrips = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 8:
            depthChart.strongIPlayers_Close = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 9:
            depthChart.strongIPlayers_Normal = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 10:
            depthChart.strongIPlayers_Tight = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 11:
            depthChart.strongIPlayers_TwinTE = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 12:
            depthChart.strongIPlayers_Twins = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 13:
            depthChart.strongIPlayers_TwinsFlex = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 14:
            depthChart.weakIPlayers_CloseTwins = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 15:
            depthChart.weakIPlayers_TwinsFlex = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 16:
            depthChart.weakIPlayers_Normal = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 17:
            depthChart.weakIPlayers_TwinTE = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 18:
            depthChart.weakIPlayers_Twins = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 19:
            depthChart.splitBackPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 20:
            depthChart.singleBackPlayers_Big = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 21:
            depthChart.singleBackPlayers_BigTwins = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 22:
            depthChart.singleBackPlayers_Bunch = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 23:
            depthChart.singleBackPlayers_Slot = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 24:
            depthChart.singleBackPlayers_Spread = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 25:
            depthChart.singleBackPlayers_TreyOpen = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 26:
            depthChart.singleBackPlayers_Trio = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 27:
            depthChart.singleBackPlayers_Trio4WR = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 28:
            depthChart.emptyPlayers_TreyOpen = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 29:
            depthChart.emptyPlayers_FlexTrips = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 30:
            depthChart.pistolPlayers_Ace = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 31:
            depthChart.pistolPlayers_Bunch = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 32:
            depthChart.pistolPlayers_Slot = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 33:
            depthChart.pistolPlayers_SpreadFlex = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 34:
            depthChart.pistolPlayers_TreyOpen = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 35:
            depthChart.pistolPlayers_Trio = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 36:
            depthChart.pistolPlayers_YTrips = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 37:
            depthChart.shotgunPlayers_Normal = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 38:
            depthChart.shotgunPlayers_NormalDimeDropping = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 39:
            depthChart.shotgunPlayers_NormalYFlex = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 40:
            depthChart.shotgunPlayers_QuadsTrio = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 41:
            depthChart.shotgunPlayers_SlotOffset = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 42:
            depthChart.shotgunPlayers_SplitOffset = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 43:
            depthChart.shotgunPlayers_Spread = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 44:
            depthChart.shotgunPlayers_Tight = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 45:
            depthChart.shotgunPlayers_Trey = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 46:
            depthChart.shotgunPlayers_Trips = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 47:
            depthChart.shotgunPlayers_Spread5WR = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 48:
            depthChart.shotgunPlayers_Bunch5WR = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 49:
            depthChart.hailMaryPlayers_Normal = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 50:
            depthChart.qbKneelPlayers_Normal = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 51:
            depthChart.fieldGoalPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 52:
            depthChart.puntPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 53:
            depthChart.fgBlockPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 54:
            depthChart.puntRetPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 55:
            depthChart.kickoffPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 56:
            depthChart.kickRetPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 57:
            depthChart.kickRetOnsidePlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 58:
            depthChart.threeFourPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 59:
            depthChart.fourThreePlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 60:
            depthChart.nickelPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 61:
            depthChart.dimePlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 62:
            depthChart.fourFourPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 63:
            depthChart.fiveThreePlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 64:
            depthChart.sixTwoPlayers = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 65:
            reader.ReadInt32();
            break;
          case 66:
            reader.ReadInt32();
            break;
          case 67:
            depthChart.playerList = resolver.GetFormatterWithVerify<List<int>>().Deserialize(ref reader, options);
            break;
          case 68:
            depthChart.playerOVR = resolver.GetFormatterWithVerify<List<int>>().Deserialize(ref reader, options);
            break;
          case 69:
            depthChart.MainRoster = resolver.GetFormatterWithVerify<RosterData>().Deserialize(ref reader, options);
            break;
          case 70:
            depthChart.PracticeSquad = resolver.GetFormatterWithVerify<RosterData>().Deserialize(ref reader, options);
            break;
          case 71:
            depthChart.DefaultPlayers = resolver.GetFormatterWithVerify<Dictionary<string, int>>().Deserialize(ref reader, options);
            break;
          case 72:
            depthChart.shotgunPlayers_TightWideBack = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          case 73:
            depthChart.nickelPlayers_TwoFour = resolver.GetFormatterWithVerify<int[]>().Deserialize(ref reader, options);
            break;
          default:
            reader.Skip();
            break;
        }
      }
      --reader.Depth;
      return depthChart;
    }
  }
}
