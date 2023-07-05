// Decompiled with JetBrains decompiler
// Type: RouteGraphic
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.UI;

public class RouteGraphic : MonoBehaviour
{
  [SerializeField]
  private RectTransform playerCircleTrans;
  [SerializeField]
  private Image playerCircleImage;
  [SerializeField]
  private RectTransform[] segmentTrans;
  [SerializeField]
  private Image[] segmentImages;
  [SerializeField]
  private Image[] arrowEndCaps;
  [SerializeField]
  private Image[] blockEndCaps;
  private static int maxLineSegments = 3;
  private static int lineWidth = 2;
  private static int blitzLineLength = 35;
  private static int blitzHorizontalAdj = 0;
  private static Color defaultColor = Color.white;
  private static Color runRouteColor = new Color(0.945098042f, 0.05882353f, 0.05882353f);
  private static Color passRouteColor = new Color(0.945098042f, 0.768627465f, 0.05882353f);
  private static Color primaryPassRouteColor = new Color(0.945098042f, 0.05882353f, 0.05882353f);
  private static Color blitzRouteColor = new Color(0.9843137f, 0.05882353f, 0.05882353f);
  private static Color deepZoneColor = new Color(0.05882353f, 0.768627465f, 0.945098042f);
  private static Color midZoneColor = new Color(0.945098042f, 0.768627465f, 0.05882353f);
  private static Color flatZoneColor = new Color(0.05882353f, 0.945098042f, 0.768627465f);

  public void DrawRoute(RouteGraphicData data)
  {
    int num = data.lineLength_1 != 0 ? (data.lineLength_2 != 0 ? (data.lineLength_3 != 0 ? 3 : 2) : 1) : 0;
    this.playerCircleImage.enabled = true;
    for (int index = 0; index < num; ++index)
      this.segmentImages[index].enabled = true;
    for (int index = num; index < RouteGraphic.maxLineSegments; ++index)
      this.segmentImages[index].enabled = false;
    this.HideAllEndCaps();
    if (num > 0)
    {
      if (data.lineEndType == LineEndType.Arrow)
        this.arrowEndCaps[num - 1].enabled = true;
      else if (data.lineEndType == LineEndType.Block)
        this.blockEndCaps[num - 1].enabled = true;
    }
    if (num > 2)
    {
      this.segmentTrans[2].sizeDelta = new Vector2((float) RouteGraphic.lineWidth, (float) data.lineLength_3);
      this.segmentTrans[2].localEulerAngles = new Vector3(0.0f, 0.0f, (float) data.lineAngle_3);
    }
    if (num > 1)
    {
      this.segmentTrans[1].sizeDelta = new Vector2((float) RouteGraphic.lineWidth, (float) data.lineLength_2);
      this.segmentTrans[1].localEulerAngles = new Vector3(0.0f, 0.0f, (float) data.lineAngle_2);
    }
    if (num > 0)
    {
      this.segmentTrans[0].sizeDelta = new Vector2((float) RouteGraphic.lineWidth, (float) data.lineLength_1);
      this.segmentTrans[0].localEulerAngles = new Vector3(0.0f, 0.0f, (float) data.lineAngle_1);
    }
    if ((double) this.playerCircleTrans.localPosition.x <= 0.0)
      return;
    this.Flip();
  }

  public void DrawZoneCoverage(RectTransform zoneTrans)
  {
    this.playerCircleImage.enabled = true;
    for (int index = 1; index < RouteGraphic.maxLineSegments; ++index)
      this.segmentImages[index].enabled = false;
    this.HideAllEndCaps();
    float y = Vector3.Distance(zoneTrans.position, this.playerCircleTrans.position) * 10f;
    this.segmentImages[0].enabled = true;
    this.segmentTrans[0].sizeDelta = new Vector2((float) RouteGraphic.lineWidth, y);
    Vector2 vector2_1 = new Vector2(this.playerCircleTrans.position.x, this.playerCircleTrans.position.y);
    Vector2 vector2_2 = new Vector2(zoneTrans.position.x, zoneTrans.position.y);
    this.segmentTrans[0].localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Atan2(vector2_2.y - vector2_1.y, vector2_2.x - vector2_1.x) * 57.2957764f - 90f);
  }

  public void DrawBlitzRoute(Vector3 blitzPosition, int blitzOffset)
  {
    this.playerCircleImage.enabled = true;
    for (int index = 1; index < RouteGraphic.maxLineSegments; ++index)
      this.segmentImages[index].enabled = false;
    this.HideAllEndCaps();
    this.arrowEndCaps[0].enabled = true;
    this.segmentImages[0].enabled = true;
    this.segmentTrans[0].sizeDelta = new Vector2((float) RouteGraphic.lineWidth, (float) RouteGraphic.blitzLineLength);
    Vector2 vector2_1 = new Vector2(this.playerCircleTrans.position.x, this.playerCircleTrans.position.y);
    Vector2 vector2_2 = new Vector2(blitzPosition.x + (float) (blitzOffset * RouteGraphic.blitzHorizontalAdj), blitzPosition.y);
    this.segmentTrans[0].localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Atan2(vector2_2.y - vector2_1.y, vector2_2.x - vector2_1.x) * 57.2957764f - 90f);
  }

  public void SetPosition(Vector3 position)
  {
    this.playerCircleTrans.localPosition = position;
    this.segmentTrans[0].localPosition = position;
  }

  public void HideAll()
  {
    this.HideAllEndCaps();
    this.HideAllSegments();
    this.playerCircleImage.enabled = false;
  }

  private void HideAllEndCaps()
  {
    for (int index = 0; index < RouteGraphic.maxLineSegments; ++index)
    {
      this.arrowEndCaps[index].enabled = false;
      this.blockEndCaps[index].enabled = false;
    }
  }

  private void HideAllSegments()
  {
    for (int index = 0; index < RouteGraphic.maxLineSegments; ++index)
      this.segmentImages[index].enabled = false;
  }

  public void Flip()
  {
    this.playerCircleTrans.localScale = new Vector3(-1f, 1f, 1f);
    this.segmentTrans[0].localScale = new Vector3(-1f, 1f, 1f);
    this.segmentTrans[0].localEulerAngles = new Vector3(0.0f, 0.0f, 360f - this.segmentTrans[0].localEulerAngles.z);
  }

  public void UnFlip()
  {
    this.playerCircleTrans.localScale = Vector3.one;
    this.segmentTrans[0].localScale = Vector3.one;
    this.segmentTrans[0].localEulerAngles = new Vector3(0.0f, 0.0f, 360f - this.segmentTrans[0].localEulerAngles.z);
  }

  public RectTransform GetParentCircle() => this.playerCircleTrans;

  public void ColorZone(ZoneType z)
  {
    switch (z)
    {
      case ZoneType.Deep1of2:
      case ZoneType.Deep2of2:
      case ZoneType.Deep1of3:
      case ZoneType.Deep2of3:
      case ZoneType.Deep3of3:
      case ZoneType.Deep1of4:
      case ZoneType.Deep2of4:
      case ZoneType.Deep3of4:
      case ZoneType.Deep4of4:
        this.ColorAll(RouteGraphic.deepZoneColor);
        break;
      case ZoneType.Mid1of2:
      case ZoneType.Mid2of2:
      case ZoneType.Mid1of3:
      case ZoneType.Mid2of3:
      case ZoneType.Mid3of3:
      case ZoneType.Mid1of4:
      case ZoneType.Mid2of4:
      case ZoneType.Mid3of4:
      case ZoneType.Mid4of4:
        this.ColorAll(RouteGraphic.midZoneColor);
        break;
      case ZoneType.FlatLeft:
      case ZoneType.FlatRight:
        this.ColorAll(RouteGraphic.flatZoneColor);
        break;
    }
  }

  public void ColorDefault() => this.ColorAll(RouteGraphic.defaultColor);

  public void ColorRunRoute() => this.ColorAll(RouteGraphic.runRouteColor);

  public void ColorPassRoute() => this.ColorAll(RouteGraphic.passRouteColor);

  public void ColorPrimaryReceiver() => this.ColorAll(RouteGraphic.primaryPassRouteColor);

  public void ColorBlitzRoute() => this.ColorAll(RouteGraphic.blitzRouteColor);

  public void ColorDeepZone() => this.ColorAll(RouteGraphic.deepZoneColor, false);

  public void ColorMidZone() => this.ColorAll(RouteGraphic.midZoneColor, false);

  public void ColorFlatZone() => this.ColorAll(RouteGraphic.flatZoneColor, false);

  private void ColorAll(Color c, bool colorPlayerCircle = true)
  {
    for (int index = 0; index < this.segmentImages.Length; ++index)
    {
      this.segmentImages[index].color = c;
      this.arrowEndCaps[index].color = c;
      this.blockEndCaps[index].color = c;
    }
    if (colorPlayerCircle)
      this.playerCircleImage.color = c;
    else
      this.playerCircleImage.color = RouteGraphic.defaultColor;
  }
}
