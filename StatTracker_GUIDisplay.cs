// Decompiled with JetBrains decompiler
// Type: StatTracker_GUIDisplay
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class StatTracker_GUIDisplay : MonoBehaviour
{
  [SerializeField]
  private StatTracker StatTrackerObject;
  [SerializeField]
  private Transform StatTickerParentTransform;
  [SerializeField]
  private GameObject StatTrackerGUIPrefab;

  private void OnEnable() => this.Initialize();

  private void Initialize()
  {
    this.ClearDisplay();
    Transform p = (bool) (Object) this.StatTickerParentTransform ? this.StatTickerParentTransform : this.transform;
    foreach (TrackedStat stat in this.StatTrackerObject.GetStats())
    {
      Transform transform = this.BuildStatTicker(stat).transform;
      transform.SetParent(p);
      transform.localPosition = Vector3.zero;
      transform.localRotation = Quaternion.identity;
      transform.localScale = Vector3.one;
    }
  }

  public void ClearDisplay(bool safety = true)
  {
    if (!((Object) this.StatTickerParentTransform != (Object) null) || (Object) this.StatTickerParentTransform == (Object) this.transform & safety)
      return;
    int childCount = this.StatTickerParentTransform.childCount;
    for (int index = 0; index < childCount; ++index)
      Object.DestroyImmediate((Object) this.StatTickerParentTransform.GetChild(0).gameObject);
  }

  public GameObject BuildStatTicker(TrackedStat stat)
  {
    GameObject gameObject = (GameObject) null;
    if ((Object) this.StatTrackerGUIPrefab != (Object) null)
    {
      gameObject = Object.Instantiate<GameObject>(this.StatTrackerGUIPrefab);
      Transform transform = gameObject.transform;
      TMP_Text component = transform.GetChild(1).GetComponent<TMP_Text>();
      TMP_Text statValue = transform.GetChild(2).GetComponent<TMP_Text>();
      string statDisplayName = stat.StatDisplayName;
      component.text = statDisplayName;
      statValue.text = (string) stat.StatValue;
      stat.OnValueChangedUnityEvent.AddListener((UnityAction) (() => statValue.text = (string) stat.StatValue));
    }
    return gameObject;
  }
}
