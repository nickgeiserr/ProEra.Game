// Decompiled with JetBrains decompiler
// Type: TB12.UI.GameplayUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

namespace TB12.UI
{
  public class GameplayUI : PersistentSingleton<GameplayUI>
  {
    [SerializeField]
    private GameObject _tooltipBg;
    [SerializeField]
    private HUDPointer _hudPointer;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TextMeshProUGUI _topText;
    [SerializeField]
    private TextMeshProUGUI _centerText;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private GameObject _mutedIcon;
    private Coroutine _arrowUpdateRoutine;
    private static readonly RoutineHandle _hideGameplayTextRoutine = new RoutineHandle();
    private const string LocMultiplayerPregameCountdownGo = "Multiplayer_PregameCountdown_Go";

    public LocalizeStringEvent centerTextLocalizeStringEvent { get; private set; }

    protected override void Awake()
    {
      base.Awake();
      this._canvas.worldCamera = PlayerCamera.Camera;
      PersistentSingleton<GameplayUI>.Instance.centerTextLocalizeStringEvent = PersistentSingleton<GameplayUI>.Instance._centerText.GetComponent<LocalizeStringEvent>();
      this.HideAll();
    }

    public static void PointTo(
      Transform tx,
      string text,
      bool showBg = false,
      bool breakWhenLookAt = true,
      bool showWhenLookAway = true)
    {
      if ((Object) tx == (Object) null)
        return;
      GameplayUI instance = PersistentSingleton<GameplayUI>.Instance;
      instance._text.text = text;
      instance._tooltipBg.SetActive(showBg);
      instance._hudPointer.PointTo(tx, breakWhenLookAt, showWhenLookAway);
    }

    public static void HidePointer() => PersistentSingleton<GameplayUI>.Instance._hudPointer.Hide(true);

    public static void ShowText(string msg, float duration = 1.5f)
    {
      if (PersistentSingleton<GameplayUI>._applicationIsQuitting)
        return;
      if ((Object) PersistentSingleton<GameplayUI>.Instance.centerTextLocalizeStringEvent != (Object) null)
        PersistentSingleton<GameplayUI>.Instance.centerTextLocalizeStringEvent.StringReference.TableReference = new TableReference();
      TextMeshProUGUI centerText = PersistentSingleton<GameplayUI>.Instance._centerText;
      centerText.text = msg;
      GameObject gameObject = centerText.gameObject;
      gameObject.SetActive(true);
      GameplayUI._hideGameplayTextRoutine.Run(GameplayUI.HideObjectDelayed(gameObject, duration));
    }

    public static void ShowText_Go_Localized()
    {
      if (PersistentSingleton<GameplayUI>._applicationIsQuitting)
        return;
      PersistentSingleton<GameplayUI>.Instance.centerTextLocalizeStringEvent.StringReference.TableEntryReference = (TableEntryReference) "Multiplayer_PregameCountdown_Go";
      GameObject gameObject = PersistentSingleton<GameplayUI>.Instance._centerText.gameObject;
      gameObject.SetActive(true);
      GameplayUI._hideGameplayTextRoutine.Run(GameplayUI.HideObjectDelayed(gameObject, 1.5f));
    }

    private static IEnumerator HideObjectDelayed(GameObject obj, float delay = 1.3f)
    {
      yield return (object) new WaitForSeconds(delay);
      obj.SetActive(false);
    }

    public static void ShowPassStats(int successes, int attempts)
    {
      TextMeshProUGUI topText = PersistentSingleton<GameplayUI>.Instance._topText;
      topText.gameObject.SetActive(true);
      topText.text = string.Format("{0} for {1}", (object) successes, (object) attempts);
    }

    public static void SetMicMute(bool muted) => PersistentSingleton<GameplayUI>.Instance._mutedIcon.SetActive(muted);

    public static void Hide() => PersistentSingleton<GameplayUI>.Instance.HideAll();

    private void HideAll()
    {
      this._topText.gameObject.SetActive(false);
      this._centerText.gameObject.SetActive(false);
      this._tooltipBg.SetActive(false);
      this._hudPointer.Hide();
    }
  }
}
