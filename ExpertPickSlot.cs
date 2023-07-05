// Decompiled with JetBrains decompiler
// Type: ExpertPickSlot
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpertPickSlot : MonoBehaviour
{
  [SerializeField]
  private Image portrait_Img;
  [SerializeField]
  private TextMeshProUGUI name_Txt;
  [SerializeField]
  private TextMeshProUGUI analysis_Txt;
  [SerializeField]
  private Image teamBackground_Img;
  [SerializeField]
  private Image teamLogo_Img;
  [SerializeField]
  private TextMeshProUGUI score_Txt;
  private TeamData winningTeam;
  private TeamData losingTeam;

  public void FillPick(GameResults gameResults, string analystName, int skinType, int portraitID)
  {
    this.portrait_Img.sprite = PortraitManager.self.LoadPlayerPortrait(skinType, portraitID);
    this.name_Txt.text = analystName;
    this.winningTeam = gameResults.WinningTeam == PersistentData.GetHomeTeamIndex() ? PersistentData.GetHomeTeamData() : PersistentData.GetAwayTeamData();
    this.losingTeam = gameResults.WinningTeam == PersistentData.GetHomeTeamIndex() ? PersistentData.GetAwayTeamData() : PersistentData.GetHomeTeamData();
    this.teamBackground_Img.color = this.winningTeam.GetPrimaryColor();
    this.teamLogo_Img.sprite = this.winningTeam.GetSmallLogo();
    int score1;
    int score2;
    if (gameResults.AwayGameSummary.TeamGameStats.Score > gameResults.HomeGameSummary.TeamGameStats.Score)
    {
      score1 = gameResults.AwayGameSummary.TeamGameStats.Score;
      score2 = gameResults.HomeGameSummary.TeamGameStats.Score;
    }
    else
    {
      score1 = gameResults.HomeGameSummary.TeamGameStats.Score;
      score2 = gameResults.AwayGameSummary.TeamGameStats.Score;
    }
    this.score_Txt.text = score1.ToString() + " - " + score2.ToString();
    this.SetPredictionText(gameResults, score1, score2);
  }

  private void SetPredictionText(GameResults gameResults, int winnerScore, int loserScore)
  {
    string str1 = "";
    int num1 = Random.Range(0, 100);
    int num2 = Random.Range(0, 100);
    string str2;
    if (num1 < 20)
    {
      if (loserScore <= 14)
      {
        string camelCase = this.FormatPlayerNameToCamelCase(gameResults.DefensivePlayerOfTheGame.playerFullName);
        if (num2 < 20)
          str2 = str1 + "I'm expecting a big defensive performance out of " + camelCase + " today.";
        else if (num2 < 40)
          str2 = str1 + camelCase + " is going to shut down " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + "'s offensive today.";
        else if (num2 < 60)
          str2 = str1 + "Look for " + camelCase + " to step it up on the defensive side of the ball today for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ".";
        else if (num2 < 80)
          str2 = str1 + camelCase + " has a monster game on defense today to secure the win for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ".";
        else
          str2 = str1 + camelCase + " leads the defensive effort in a win for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " today.";
      }
      else if (winnerScore >= 28)
      {
        string camelCase = this.FormatPlayerNameToCamelCase(gameResults.OffensivePlayerOfTheGame.playerFullName);
        if (num2 < 20)
          str2 = str1 + "Expect a star offensive performance out of " + camelCase + " today.";
        else if (num2 < 40)
          str2 = str1 + camelCase + " is going to light up the scoreboard today.";
        else if (num2 < 60)
          str2 = str1 + "Keep your eyes on " + camelCase + " today. He's going to make a couple trips into the endzone.";
        else if (num2 < 80)
          str2 = str1 + camelCase + " is going to have a field day with " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + "'s defense today.";
        else
          str2 = str1 + camelCase + " leads the offense in a win for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " today.";
      }
      else
      {
        string camelCase = this.FormatPlayerNameToCamelCase(this.winningTeam.MainRoster.GetPlayer(this.winningTeam.MainRoster.FindBestPlayer()).FullName);
        if (num2 < 33)
          str2 = str1 + camelCase + " has another solid performance today, leading " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " to victory.";
        else if (num2 < 66)
          str2 = str1 + "I just don't see how " + camelCase + " can be contained. He helps secure the win today for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity());
        else
          str2 = str1 + camelCase + " is the best player on his team. He shows us why again today.";
      }
    }
    else if (num1 < 40)
    {
      if (loserScore <= 14)
      {
        if (num2 < 20)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + "'s defense is just too much for " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " today.";
        else if (num2 < 40)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + "'s defense is going to shut evertying down. They get the W.";
        else if (num2 < 60)
          str2 = str1 + "I'm predicting a big day for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + "'s defense. I just don't see " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " moving the ball.";
        else if (num2 < 80)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + "'s defenders swarm to the ball and make a long day for " + this.losingTeam.TeamDepthChart.GetStartingQB().FirstInitalAndLastName;
        else
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + "'s defense will overpower " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + "'s offense. In the end, it's a W for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ".";
      }
      else if (winnerScore >= 30)
      {
        if (num2 < 20)
          str2 = str1 + "Expect " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " to put on an offensive clinic today.";
        else if (num2 < 40)
          str2 = str1 + "I forsee " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " lighting up the scoreboard in this matchup.";
        else if (num2 < 60)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " is going to have a field day with " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + "'s defense.";
        else if (num2 < 80)
          str2 = str1 + "I'm going with a big offensive performance for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " in today's contest.";
        else
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + "'s offense is going to be too much for " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " to handle today.";
      }
      else if (num2 < 33)
        str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " is just a better team all-around. They'll get the victory today.";
      else if (num2 < 66)
        str2 = str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " just doesn't have the talent to match up with " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ". They will fall short.";
      else
        str2 = str1 + "At the end of the day, I just don't see " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " pulling this one out. I'm going with " + this.FormatTeamToCamelCase(this.winningTeam.GetCity());
    }
    else if (num1 < 60 && PersistentData.gameType == GameType.SeasonMode)
    {
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      if (this.winningTeam.CurrentSeasonStats.streak > 0)
        num4 = this.winningTeam.CurrentSeasonStats.streak;
      else if (this.winningTeam.CurrentSeasonStats.streak < 0)
        num3 = Mathf.Abs(this.winningTeam.CurrentSeasonStats.streak);
      if (this.losingTeam.CurrentSeasonStats.streak > 0)
        num5 = this.losingTeam.CurrentSeasonStats.streak;
      else if (this.losingTeam.CurrentSeasonStats.streak < 0)
        num6 = Mathf.Abs(this.losingTeam.CurrentSeasonStats.streak);
      if (num4 > 0)
      {
        if (num2 < 25)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " continues their winning streak today with an impressive win.";
        else if (num2 < 50)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " had an impressive win last week. I see them repeating that again today.";
        else if (num2 < 75)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " adds another tally in the win column today to keep their streak going.";
        else
          str2 = str1 + "I don't see " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " breaking " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + "'s win streak. I'm going with " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ".";
      }
      else if (num3 > 0)
        str2 = num2 >= 25 ? (num2 >= 50 ? (num2 >= 75 ? str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " is better than they played last week. They will turn it around today." : str1 + "I think " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " breaks their losing streak with a statement win today.") : str1 + "I see " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " turning a corner today and starting a winning streak.") : str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " breaks their losing streak today and gets the W.";
      else if (num6 > 0)
        str2 = num2 >= 25 ? (num2 >= 50 ? (num2 >= 75 ? str1 + "More bad new for " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + ". They will continue their losing streak today." : str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " will scramble much like they did last week. I don't see them getting the win today.") : str1 + "Coming off of a loss last week, " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " will struggle again today. They'll take another loss.") : str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " has been struggling. I'd like to say they'll turn it around today, but I don't see that happening.";
      else if (num5 > 0)
      {
        if (num2 < 25)
          str2 = str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " is riding high off their win last week, but things will come crashing down today.";
        else if (num2 < 50)
          str2 = str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " will get a dose of reality today when they face a tough " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " team.";
        else if (num2 < 75)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " will break " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + "'s winning record today with a statement win.";
        else
          str2 = str1 + "After a solid victory last week, " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " falls in a tough loss today.";
      }
      else
        str2 = num2 >= 25 ? (num2 >= 50 ? (num2 >= 75 ? str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " gets a bad start to the season today. They'll take an L in their first game." : str1 + "I see " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " starting off this season out with a mark in the loss column.") : str1 + "I predict " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " begins the season with a big statement win.") : str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " starts the season off on the right foot today.";
    }
    else if (num1 < 80)
    {
      int num7 = this.winningTeam.GetTeamRating_OFF() + this.winningTeam.GetTeamRating_DEF();
      int num8 = this.losingTeam.GetTeamRating_OFF() + this.losingTeam.GetTeamRating_OFF();
      if (num7 > num8 + 5)
      {
        if (num2 < 25)
          str2 = str1 + "I'm going with the obvious choice here. " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " gets the win today.";
        else if (num2 < 50)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " is simply a much better team. They'll be the victor today.";
        else if (num2 < 75)
          str2 = str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " just doesn't matchup with " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ". They'll play catchup all day.";
        else
          str2 = str1 + "Don't expect an upset today. " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " beats a team they should beat.";
      }
      else if (num8 > num7 + 5)
      {
        if (num2 < 20)
          str2 = str1 + "I'm going with the upset today. I just feel like " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " is going to do something special.";
        else if (num2 < 40)
          str2 = str1 + "Upset time! Look for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " to shock the crowd today.";
        else if (num2 < 60)
          str2 = str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " really drops the ball today and take a loss against a team they should beat.";
        else if (num2 < 80)
          str2 = str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " underestimates " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " and loses a game that they should have won.";
        else
          str2 = str1 + "The underdog gets the win today. This will be a big upset victory for " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ".";
      }
      else
        str2 = num2 >= 25 ? (num2 >= 50 ? (num2 >= 75 ? str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " gets the win in this close matchup." : str1 + "On paper, these teams are very closly matched, but my gut tells me to go with " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ".") : str1 + "These teams are pretty closely matched, but I'm going to go with " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ".") : str1 + "These teams are pretty evenly matched, but I think " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " has the edge.";
    }
    else
    {
      int num9 = winnerScore - loserScore;
      if (num9 >= 14)
      {
        if (num2 < 20)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " makes it look like they're playing in a different league today.";
        else if (num2 < 40)
          str2 = str1 + "It's going to be a rough day for " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " fans. They're going to take a big loss.";
        else if (num2 < 60)
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " will put on a clinic today in this blowout win.";
        else if (num2 < 80)
          str2 = str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " will look silly out there today. They'll feel this loss for a while.";
        else
          str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " wins in decisive style. " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " simply won't be able to keep up.";
      }
      else if (num9 <= 7)
        str2 = num2 >= 20 ? (num2 >= 40 ? (num2 >= 60 ? (num2 >= 80 ? str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " will put up a good fight today, but they will fall short in the end." : str1 + "I struggled to pick a winner in this one, but I think " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " squeaks by in a nail-biter.") : str1 + "This will be a close game, but I think " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " pulls it out in the end.") : str1 + "After a back-and-forth match, " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " just isn't able to pull this one out.") : str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " squeeks out a win in this close one.";
      else if (num2 < 25)
        str2 = str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " will keep it close for a while, but " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " makes some good second-half adjustments to pull away.";
      else if (num2 < 50)
        str2 = str1 + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " will fight hard, but they'll simply run out of ways to contain " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + ".";
      else if (num2 < 75)
        str2 = str1 + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " tires " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " out and gets the win in the end.";
      else
        str2 = str1 + "I see this one going back-and-forth a bit, but I just don't think that " + this.FormatTeamToCamelCase(this.losingTeam.GetCity()) + " can hang with " + this.FormatTeamToCamelCase(this.winningTeam.GetCity()) + " for an entire game.";
    }
    this.analysis_Txt.text = str2;
  }

  private string FormatTeamToCamelCase(string team)
  {
    int length = team.LastIndexOf(" ");
    return length != -1 ? team.Substring(0, 1).ToUpper() + team.Substring(1, length).ToLower() + team.Substring(length + 1, 1).ToUpper() + team.Substring(length + 2).ToLower() : team.Substring(0, 1).ToUpper() + team.Substring(1).ToLower();
  }

  private string FormatPlayerNameToCamelCase(string name)
  {
    int length = name.LastIndexOf(" ");
    return name.Substring(0, 1).ToUpper() + name.Substring(1, length).ToLower() + name.Substring(length + 1, 1).ToUpper() + name.Substring(length + 2).ToLower();
  }
}
