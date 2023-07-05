// Decompiled with JetBrains decompiler
// Type: TB12.GameplaySequences
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using System.Collections;
using TB12.UI;
using UnityEngine;

namespace TB12
{
  public static class GameplaySequences
  {
    public static IEnumerator LocatePlayer(FootballVR.Avatar avatar, Transform camTx)
    {
      avatar.Outline = EOutlineMode.kIdle;
      bool spotted = false;
      while (!spotted)
      {
        yield return (object) null;
        yield return (object) null;
        if ((double) Vector3.Dot(camTx.forward, (avatar.SpotTarget - camTx.position).normalized) >= 0.93999999761581421)
        {
          avatar.Outline = EOutlineMode.kHighlight;
          spotted = true;
          AppSounds.PlaySfx(ESfxTypes.kButtonPress);
        }
      }
      GameplayUI.Hide();
      yield return (object) new WaitForSeconds(0.7f);
      avatar.Outline = EOutlineMode.kDisabld;
    }
  }
}
