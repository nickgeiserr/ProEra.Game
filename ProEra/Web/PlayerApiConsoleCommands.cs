// Decompiled with JetBrains decompiler
// Type: ProEra.Web.PlayerApiConsoleCommands
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using IngameDebugConsole;
using UnityEngine;

namespace ProEra.Web
{
  public class PlayerApiConsoleCommands : MonoBehaviour
  {
    [ConsoleMethod("PlayerCreate", "Creates a player based on device identifiers and logs them in", new string[] {})]
    public static void CreatePlayer()
    {
      PlayerApiConsoleCommands.ValidateInstance();
      PersistentSingleton<PlayerApi>.Instance.CreateUser();
    }

    [ConsoleMethod("PlayerLogin", "Logs a player in with the given credentials", new string[] {})]
    public static void LoginPlayer(string username, string password)
    {
      PlayerApiConsoleCommands.ValidateInstance();
      PersistentSingleton<PlayerApi>.Instance.Login(username, password);
    }

    [ConsoleMethod("PlayerRefresh", "Refreshes a players auth token", new string[] {})]
    public static void LoginPlayer(string refreshToken)
    {
      PlayerApiConsoleCommands.ValidateInstance();
      PersistentSingleton<PlayerApi>.Instance.Login(refreshToken);
    }

    [ConsoleMethod("PlayerUpdateUsername", "Updates player's username in the database", new string[] {})]
    public static void UpdateUsername(string username)
    {
      PlayerApiConsoleCommands.ValidateInstance();
      PersistentSingleton<PlayerApi>.Instance.UpdateUsername(username);
    }

    private static void ValidateInstance()
    {
    }
  }
}
