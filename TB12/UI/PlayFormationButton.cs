// Decompiled with JetBrains decompiler
// Type: TB12.UI.PlayFormationButton
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using TMPro;
using UnityEngine;

namespace TB12.UI
{
  public class PlayFormationButton : CircularLayoutItem
  {
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private RectTransform _textGroup;
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private CanvasGroup _textCanvasGroup;
    [SerializeField]
    private PlayFormationPlayerIcon _playerIconPrefab;
    [SerializeField]
    private Color _idleTextColor;
    [SerializeField]
    private Color _selectedTextColor;
    [SerializeField]
    private Transform[] _iconOptimizationHolders;
    [SerializeField]
    private PlayFormationPlayerIcon[] _playbookFormationIcons;
    [Header("Player Icons")]
    public float xMin = -66f;
    public float xMax = 66f;
    public float yMin = -30f;
    public float yMax = 75f;
    private float[] xLoc;
    private float[] zLoc;
    private int[] playersInForm;

    public override RectTransform TextGroup => this._textGroup;

    public string Text
    {
      set => this._text.text = value;
    }

    public override float alpha
    {
      set
      {
        this._canvasGroup.alpha = value;
        this._textCanvasGroup.alpha = value;
      }
    }

    private void Awake()
    {
      if (this._playbookFormationIcons != null)
        return;
      this._playbookFormationIcons = new PlayFormationPlayerIcon[11];
      for (int index = 0; index < 11; ++index)
      {
        this._playbookFormationIcons[index] = Object.Instantiate<PlayFormationPlayerIcon>(this._playerIconPrefab, this.transform);
        this._playbookFormationIcons[index].OptimizeDrawCalls(this._iconOptimizationHolders);
      }
    }

    protected override void OnStateChanged(bool state) => this._text.color = state ? this._selectedTextColor : this._idleTextColor;

    public override void SetVisible(bool state)
    {
      this._text.enabled = state;
      foreach (PlayFormationPlayerIcon playbookFormationIcon in this._playbookFormationIcons)
        playbookFormationIcon.SetVisible(state);
    }

    public void SetPlayersInForm(FormationData data)
    {
      FormationPositions formationPositions = data.GetFormationPositions();
      FormationType formationType = data.GetFormationType();
      this.xLoc = formationPositions.GetXLocations();
      this.zLoc = formationPositions.GetZLocations();
      TeamData team = TeamState.GetTeam(Player.One);
      this.playersInForm = team.TeamDepthChart.GetPlayersInFormation(formationPositions);
      int num1 = -180;
      int num2 = 180;
      int num3 = -130;
      int num4 = -250;
      float num5 = -180f;
      float num6 = -50f;
      float num7 = 1.5f;
      float num8 = 1f;
      switch (formationType)
      {
        case FormationType.Offense:
          num6 = -90f;
          num8 = 1.5f;
          break;
        case FormationType.Defense:
          num6 = -210f;
          break;
        case FormationType.OffSpecial:
          num6 = -80f;
          break;
        case FormationType.DefSpecial:
          num6 = -205f;
          num8 = 1.3f;
          break;
        default:
          Debug.Log((object) "Selecting a formation that isn't in the list!");
          break;
      }
      float[] numArray1 = new float[this.xLoc.Length];
      float[] numArray2 = new float[this.zLoc.Length];
      for (int index = 0; index < this._playbookFormationIcons.Length; ++index)
      {
        float num9 = (float) (((double) this.xLoc[index] * (double) num7 - -16.0) / 32.0);
        numArray1[index] = num5 + num9 * (float) (num2 - num1);
        float num10 = this.zLoc[index] / -8f * num8;
        numArray2[index] = num6 + num10 * (float) (num4 - num3);
      }
      float num11 = numArray1[0];
      float num12 = numArray1[0];
      float num13 = numArray2[0];
      float num14 = numArray2[0];
      for (int index = 1; index < numArray1.Length; ++index)
      {
        num11 = Mathf.Min(num11, numArray1[index]);
        num12 = Mathf.Max(num12, numArray1[index]);
        num13 = Mathf.Min(num13, numArray2[index]);
        num14 = Mathf.Max(num14, numArray2[index]);
      }
      for (int index = 0; index < this._playbookFormationIcons.Length; ++index)
      {
        PlayFormationPlayerIcon playbookFormationIcon = this._playbookFormationIcons[index];
        PlayerData player = team.GetPlayer(this.playersInForm[index]);
        playbookFormationIcon.SetPosition(this.MapPositions(numArray1[index], numArray2[index], num11, num12, num13, num14));
        playbookFormationIcon.SetPlayerNumber(player.Number);
        playbookFormationIcon.SetIconColor(FatigueManager.GetFatigueColor((float) player.Fatigue));
      }
    }

    private Vector3 MapPositions(
      float x,
      float y,
      float xInputMin,
      float xInputMax,
      float yInputMin,
      float yInputMax)
    {
      x = MathUtils.MapRange(x, Mathf.Min(-180f, xInputMin), Mathf.Max(180f, xInputMax), this.xMin, this.xMax);
      y = MathUtils.MapRange(y, Mathf.Min(-250f, yInputMin), Mathf.Max(-90f, yInputMax), this.yMin, this.yMax);
      return new Vector3(x, y);
    }

    private void OnDestroy()
    {
      if (!((Object) this._textGroup != (Object) null) || !((Object) this._textGroup.gameObject != (Object) null))
        return;
      Object.Destroy((Object) this._textGroup.gameObject);
    }
  }
}
