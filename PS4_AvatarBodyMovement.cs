// Decompiled with JetBrains decompiler
// Type: PS4_AvatarBodyMovement
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class PS4_AvatarBodyMovement : MonoBehaviour
{
  [SerializeField]
  private Transform HeadPosition;
  [SerializeField]
  private Transform TorsoTarget;
  [SerializeField]
  private Vector3 HeadOffset;
  [SerializeField]
  private GameObject[] DeformationBonesToDelete;
  private Transform HeadTarget;
  private bool bInit;

  private void Awake() => this.enabled = false;

  private void LateUpdate()
  {
  }
}
