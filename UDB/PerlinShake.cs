// Decompiled with JetBrains decompiler
// Type: UDB.PerlinShake
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections;
using UnityEngine;

namespace UDB
{
  public class PerlinShake : CachedMonoBehaviour
  {
    public static PerlinShake instance;
    public float duration = 0.5f;
    public float speed = 1f;
    public float magnitude = 0.1f;
    public float commonDuration = 1f;
    public float commonSpeed = 50f;
    public float commonMagnitude = 0.07f;
    public bool test;
    public PerlinShake.State currentState;
    private Vector3 originalCamPos;

    private void Awake()
    {
      PerlinShake.instance = this;
      this.currentState = PerlinShake.State.normal;
      this.originalCamPos = this.transform.localPosition;
    }

    private void Update()
    {
      if (this.test)
      {
        this.test = false;
        this.PlayShake(this.duration, this.speed, this.magnitude);
      }
      int currentState = (int) this.currentState;
    }

    public void PlayShake(float duration, float speed, float magnitude)
    {
      this.duration = duration;
      this.speed = speed;
      this.magnitude = magnitude;
      this.transform.localPosition = this.originalCamPos;
      this.currentState = PerlinShake.State.shakning;
      this.StopAllCoroutines();
      this.StartCoroutine("Shake");
    }

    public void CommonShake()
    {
      this.duration = this.commonDuration;
      this.speed = this.commonSpeed;
      this.magnitude = this.commonMagnitude;
      this.currentState = PerlinShake.State.shakning;
      this.StopAllCoroutines();
      this.StartCoroutine(this.Shake());
    }

    private IEnumerator Shake()
    {
      PerlinShake perlinShake = this;
      float elapsed = 0.0f;
      float randomStart = Random.Range(-1000f, 1000f);
      while ((double) elapsed < (double) perlinShake.duration)
      {
        elapsed += Time.deltaTime;
        float num1 = elapsed / perlinShake.duration;
        float num2 = 1f - Mathf.Clamp((float) (2.0 * (double) num1 - 1.0), 0.0f, 1f);
        float num3 = randomStart + perlinShake.speed * num1;
        float num4 = (float) ((double) Mathf.PerlinNoise(num3, 0.0f) * 2.0 - 1.0);
        float num5 = (float) ((double) Mathf.PerlinNoise(0.0f, num3) * 2.0 - 1.0);
        float x = num4 * (perlinShake.magnitude * num2) + perlinShake.originalCamPos.x;
        float y = num5 * (perlinShake.magnitude * num2) + perlinShake.originalCamPos.y;
        perlinShake.transform.localPosition = new Vector3(x, y, perlinShake.originalCamPos.z);
        yield return (object) null;
      }
      perlinShake.currentState = PerlinShake.State.normal;
      perlinShake.transform.localPosition = perlinShake.originalCamPos;
    }

    public enum State
    {
      normal,
      shakning,
    }
  }
}
