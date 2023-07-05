// Decompiled with JetBrains decompiler
// Type: BlockZoneScript
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class BlockZoneScript : MonoBehaviour
{
  public Transform[] blockPos;
  public bool[] blockPosFilled;

  private void Start()
  {
    this.blockPosFilled = new bool[7];
    for (int index = 0; index < this.blockPos.Length; ++index)
      this.blockPos[index].position *= Field.ONE_YARD;
  }

  public int FindBlockPosition(Vector3 playerPos)
  {
    float num1 = 50f;
    int blockPosition = 0;
    if ((bool) ProEra.Game.MatchState.Turnover && (double) Vector3.Distance(playerPos, this.blockPos[0].position) > 15.0)
      return 0;
    for (int index = 0; index < this.blockPosFilled.Length; ++index)
    {
      if (!this.blockPosFilled[index])
      {
        float num2 = (float) index * 0.5f;
        float num3 = Vector3.Distance(playerPos, this.blockPos[index].position) + num2;
        if ((double) num3 < (double) num1)
        {
          num1 = num3;
          blockPosition = index;
        }
      }
    }
    this.blockPosFilled[blockPosition] = true;
    return blockPosition;
  }
}
