// Decompiled with JetBrains decompiler
// Type: DevMessageScrolling
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class DevMessageScrolling : MonoBehaviour
{
  [SerializeField]
  private Transform m_textHandle;
  [SerializeField]
  private Vector3 m_scrollVelocity;
  [SerializeField]
  private float m_scrollDistance;

  private void Update()
  {
    this.m_textHandle.transform.Translate(this.m_scrollVelocity * Time.deltaTime, Space.Self);
    if ((double) this.m_textHandle.localPosition.magnitude <= (double) this.m_scrollDistance)
      return;
    this.m_textHandle.localPosition = Vector3.zero;
  }
}
