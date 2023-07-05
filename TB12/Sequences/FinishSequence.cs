// Decompiled with JetBrains decompiler
// Type: TB12.Sequences.FinishSequence
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using Framework.UI;
using System.Collections;
using TB12.UI;
using Vars;

namespace TB12.Sequences
{
  public static class FinishSequence
  {
    private static bool running;

    public static void Stop()
    {
      if (!FinishSequence.running)
        return;
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Activation)
      {
        UIDispatch.FrontScreen.HideView(EScreens.kHighscore, false);
        UIDispatch.FrontScreen.HideView(EScreens.kLeaderboardEntry, false);
        UIDispatch.FrontScreen.HideView(EScreens.kActivatorSummary, false);
        UIDispatch.FrontScreen.HideView(EScreens.kThankYouScreen, false);
      }
      else
        UIDispatch.FrontScreen.HideView(EScreens.kCompletedSuccessfully);
      FinishSequence.running = false;
    }

    public static IEnumerator Routine(bool win = true)
    {
      FinishSequence.running = true;
      if ((EAppMode) (Variable<EAppMode>) AppState.AppMode == EAppMode.Activation)
      {
        if (AppState.GameMode != EGameMode.kTrainingCamp)
        {
          yield return (object) UIDispatch.FrontScreen.DisplayDialog(EScreens.kHighscore, new DialogRequest(2f));
          yield return (object) UIDispatch.FrontScreen.DisplayDialog(EScreens.kLeaderboardEntry);
          yield return (object) UIDispatch.FrontScreen.DisplayDialog(EScreens.kActivatorSummary);
          yield return (object) UIDispatch.FrontScreen.DisplayDialog(EScreens.kThankYouScreen, new DialogRequest(2.5f));
          FinishSequence.running = false;
          AppEvents.LoadMainMenu.Trigger();
        }
        else
          UIDispatch.FrontScreen.DisplayView(win ? EScreens.kCompletedSuccessfully : EScreens.kPlayFailed);
      }
      else
        UIDispatch.FrontScreen.DisplayView(win ? EScreens.kCompletedSuccessfully : EScreens.kPlayFailed);
    }
  }
}
