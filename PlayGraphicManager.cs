// Decompiled with JetBrains decompiler
// Type: PlayGraphicManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayGraphicManager : MonoBehaviour
{
  [Header("Play Graphic")]
  [SerializeField]
  private RouteGraphic[] routeGraphics;
  [SerializeField]
  private RectTransform[] zoneTransforms;
  [SerializeField]
  private Image[] zoneImages;
  [SerializeField]
  private RectTransform blitzPoint;
  public GameObject playGraphicSection;
  [SerializeField]
  private RectTransform playGraphicTrans;
  [SerializeField]
  private GameObject playInformationSection;
  [Header("Play Information")]
  [SerializeField]
  private TextMeshProUGUI controllerPlayNameText;
  [SerializeField]
  private TextMeshProUGUI keyboardPlayNameText;
  [SerializeField]
  private TextMeshProUGUI playTypeText;
  [SerializeField]
  private TextMeshProUGUI personnelText;
  [SerializeField]
  private TextMeshProUGUI conceptText;
  [SerializeField]
  private GameObject playType_GO;
  private RouteGraphicData routeData;
  private PlayData playData;
  private PlayDataOff playDataOff;
  private PlayDataDef playDataDef;
  private static int xMin = -135;
  private static int xMax = 135;
  private static int yMin = -130;
  private static int yMax = -250;
  private float[] xLoc;
  private float[] zLoc;

  public void Clear()
  {
    for (int index = 0; index < 11; ++index)
      this.routeGraphics[index].HideAll();
    this.playGraphicSection.SetActive(false);
    this.playInformationSection.SetActive(false);
  }

  public void DrawPlay(FormationData formation, int playIndex)
  {
    this.playData = formation.GetPlay(playIndex);
    this.SetPlayersInFormation(formation.GetFormationType(), this.playData.GetFormation());
    this.HideAllZones();
    bool flag = this.playData is PlayDataOff;
    PlayType playType = PlayType.Run;
    int num = 0;
    if (flag)
    {
      this.playDataOff = (PlayDataOff) this.playData;
      playType = this.playDataOff.GetPlayType();
      num = this.playDataOff.GetPlayTarget();
    }
    this.controllerPlayNameText.text = this.playData.GetPlayName();
    this.keyboardPlayNameText.text = this.playData.GetPlayName();
    this.personnelText.text = formation.GetFormationPositions().GetPersonnel();
    if (flag)
    {
      this.playType_GO.SetActive(true);
      this.playTypeText.text = this.playDataOff.GetPlayType().ToString().ToUpper();
      this.conceptText.text = this.playDataOff.GetPlayConceptString();
    }
    else
    {
      this.playType_GO.SetActive(false);
      this.playTypeText.text = "";
      this.conceptText.text = this.playData.GetPlayConceptString();
    }
    this.playGraphicSection.SetActive(true);
    this.playInformationSection.SetActive(true);
    for (int i = 0; i < 11; ++i)
    {
      this.routeData = this.playData.GetRouteGraphicData(i);
      this.routeGraphics[i].UnFlip();
      int assignmentType = this.playData.GetAssignmentType(i);
      if (flag)
      {
        this.routeGraphics[i].DrawRoute(this.routeData);
        if (assignmentType == 0)
        {
          switch (playType)
          {
            case PlayType.Run:
              this.routeGraphics[i].ColorRunRoute();
              continue;
            case PlayType.Pass:
              if (i == num)
              {
                this.routeGraphics[i].ColorPrimaryReceiver();
                continue;
              }
              this.routeGraphics[i].ColorPassRoute();
              continue;
            default:
              this.routeGraphics[i].ColorDefault();
              continue;
          }
        }
        else
          this.routeGraphics[i].ColorDefault();
      }
      else if (assignmentType == 0)
      {
        this.routeGraphics[i].DrawRoute(this.routeData);
        this.routeGraphics[i].ColorPassRoute();
      }
      else if (assignmentType == 0 || assignmentType == 1)
      {
        this.routeGraphics[i].DrawRoute(this.routeData);
        this.routeGraphics[i].ColorDefault();
      }
      else
      {
        switch (assignmentType)
        {
          case 2:
            this.routeGraphics[i].DrawZoneCoverage(this.zoneTransforms[(int) this.routeData.zoneType]);
            this.routeGraphics[i].ColorZone(this.routeData.zoneType);
            this.zoneImages[(int) this.routeData.zoneType].enabled = true;
            continue;
          case 3:
            if (this.routeData.blitzType == BlitzType.Lineman || this.routeData.blitzType == BlitzType.None)
            {
              this.routeGraphics[i].DrawRoute(this.routeData);
              this.routeGraphics[i].ColorDefault();
              continue;
            }
            this.routeGraphics[i].DrawBlitzRoute(this.blitzPoint.position, (int) this.routeData.blitzType);
            this.routeGraphics[i].ColorBlitzRoute();
            continue;
          case 4:
            this.routeGraphics[i].DrawRoute(this.routeData);
            this.routeGraphics[i].ColorDefault();
            continue;
          default:
            this.routeGraphics[i].DrawRoute(this.routeData);
            this.routeGraphics[i].ColorDefault();
            continue;
        }
      }
    }
  }

  private void SetPlayersInFormation(
    FormationType formationType,
    FormationPositions formationPositions)
  {
    this.xLoc = formationPositions.GetXLocations();
    this.zLoc = formationPositions.GetZLocations();
    float num1 = 0.0f;
    float num2 = -135f;
    float num3 = 0.8f;
    float num4 = 1f;
    int num5 = 0;
    switch (formationType)
    {
      case FormationType.Offense:
        num4 = 0.5f;
        num3 = 0.8f;
        num5 = -4;
        break;
      case FormationType.Defense:
        num1 = -55f;
        num4 = 0.65f;
        break;
      case FormationType.OffSpecial:
        num1 = 25f;
        num4 = 0.5f;
        break;
      case FormationType.DefSpecial:
        num1 = -40f;
        num4 = 0.4f;
        break;
      case FormationType.KickBlock:
        num1 = 30f;
        num4 = 0.2f;
        break;
      case FormationType.Kickoff:
        num1 = 80f;
        num4 = 0.7f;
        num3 = 0.7f;
        break;
      case FormationType.KickReturn:
        num1 = -80f;
        num4 = 0.17f;
        num3 = 0.5f;
        break;
      default:
        Debug.Log((object) "Selecting a formation that isn't in the list!");
        break;
    }
    for (int index = 0; index < this.routeGraphics.Length; ++index)
    {
      float num6 = (float) (((double) this.xLoc[index] * (double) num3 - -16.0) / 32.0);
      float x = num2 + num6 * (float) (PlayGraphicManager.xMax - PlayGraphicManager.xMin);
      float num7 = this.zLoc[index] / -8f * num4;
      float y = num1 + num7 * (float) (PlayGraphicManager.yMax - PlayGraphicManager.yMin);
      if ((double) y > 70.0)
        y = 50f;
      if (index == 5)
        y += (float) num5;
      this.routeGraphics[index].SetPosition(new Vector3(x, y));
    }
  }

  public void FlipPlay() => this.playGraphicTrans.localScale = new Vector3(-1f, 1f, 1f);

  public void UnflipPlay() => this.playGraphicTrans.localScale = Vector3.one;

  private void HideAllZones()
  {
    for (int index = 0; index < this.zoneImages.Length; ++index)
      this.zoneImages[index].enabled = false;
  }
}
