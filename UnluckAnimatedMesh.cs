// Decompiled with JetBrains decompiler
// Type: UnluckAnimatedMesh
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class UnluckAnimatedMesh : MonoBehaviour
{
  public MeshFilter[] meshCache;
  [HideInInspector]
  public Transform meshCached;
  public Transform meshContainerFBX;
  public float playSpeed = 1f;
  public float playSpeedRandom;
  public bool randomSpeedLoop;
  private float currentSpeed;
  [HideInInspector]
  public float currentFrame;
  [HideInInspector]
  public int meshCacheCount;
  [HideInInspector]
  public MeshFilter meshFilter;
  [HideInInspector]
  public Renderer rendererComponent;
  public float updateInterval = 0.05f;
  public bool randomRotateX;
  public bool randomRotateY;
  public bool randomRotateZ;
  public bool randomStartFrame = true;
  public bool randomRotateLoop;
  public bool loop = true;
  public bool pingPong;
  public bool playOnAwake = true;
  public Vector2 randomStartDelay = new Vector2(0.0f, 0.0f);
  private float startDelay;
  private float startDelayCounter;
  public static float updateSeed;
  private bool pingPongToggle;
  public Transform transformCache;
  public float delta;

  public void Start()
  {
    this.transformCache = this.transform;
    this.CheckIfMeshHasChanged();
    this.startDelay = Random.Range(this.randomStartDelay.x, this.randomStartDelay.y);
    UnluckAnimatedMesh.updateSeed += 0.0005f;
    if (this.playOnAwake)
      this.Invoke("Play", this.updateInterval + UnluckAnimatedMesh.updateSeed);
    if ((double) UnluckAnimatedMesh.updateSeed >= (double) this.updateInterval)
      UnluckAnimatedMesh.updateSeed = 0.0f;
    if (!((Object) this.rendererComponent == (Object) null))
      return;
    this.GetRequiredComponents();
  }

  public void Play()
  {
    this.CancelInvoke();
    this.currentFrame = !this.randomStartFrame ? 0.0f : (float) this.meshCacheCount * Random.value;
    this.meshFilter.sharedMesh = this.meshCache[(int) this.currentFrame].sharedMesh;
    this.enabled = true;
    this.RandomizePlaySpeed();
    this.RandomRotate();
  }

  public void RandomRotate()
  {
    if (this.randomRotateX)
    {
      Quaternion localRotation = this.transformCache.localRotation;
      Vector3 eulerAngles = localRotation.eulerAngles with
      {
        x = (float) Random.Range(0, 360)
      };
      localRotation.eulerAngles = eulerAngles;
      this.transformCache.localRotation = localRotation;
    }
    if (this.randomRotateY)
    {
      Quaternion localRotation = this.transformCache.localRotation;
      Vector3 eulerAngles = localRotation.eulerAngles with
      {
        y = (float) Random.Range(0, 360)
      };
      localRotation.eulerAngles = eulerAngles;
      this.transformCache.localRotation = localRotation;
    }
    if (!this.randomRotateZ)
      return;
    Quaternion localRotation1 = this.transformCache.localRotation;
    Vector3 eulerAngles1 = localRotation1.eulerAngles with
    {
      z = (float) Random.Range(0, 360)
    };
    localRotation1.eulerAngles = eulerAngles1;
    this.transformCache.localRotation = localRotation1;
  }

  public void GetRequiredComponents() => this.rendererComponent = this.GetComponent<Renderer>();

  public void RandomizePlaySpeed()
  {
    if ((double) this.playSpeedRandom > 0.0)
      this.currentSpeed = Random.Range(this.playSpeed - this.playSpeedRandom, this.playSpeed + this.playSpeedRandom);
    else
      this.currentSpeed = this.playSpeed;
  }

  public void FillCacheArray()
  {
    this.GetRequiredComponents();
    if ((Object) this.transformCache == (Object) null)
      this.transformCache = this.transform;
    this.meshFilter = this.transformCache.GetComponent<MeshFilter>();
    this.meshCacheCount = this.meshContainerFBX.childCount;
    this.meshCached = this.meshContainerFBX;
    this.meshCache = new MeshFilter[this.meshCacheCount];
    for (int index = 0; index < this.meshCacheCount; ++index)
      this.meshCache[index] = this.meshContainerFBX.GetChild(index).GetComponent<MeshFilter>();
    this.currentFrame = (float) this.meshCacheCount * Random.value;
    this.meshFilter.sharedMesh = this.meshCache[(int) this.currentFrame].sharedMesh;
  }

  public void CheckIfMeshHasChanged()
  {
    if (!((Object) this.meshCached != (Object) this.meshContainerFBX) || !((Object) this.meshContainerFBX != (Object) null))
      return;
    this.FillCacheArray();
  }

  public void Update()
  {
    this.delta = Time.deltaTime;
    this.startDelayCounter += this.delta;
    if ((double) this.startDelayCounter > (double) this.startDelay)
    {
      this.rendererComponent.enabled = true;
      this.Animate();
    }
    if (this.enabled)
      return;
    this.rendererComponent.enabled = false;
  }

  public bool PingPongFrame()
  {
    if (this.pingPongToggle)
      this.currentFrame += this.currentSpeed * this.delta;
    else
      this.currentFrame -= this.currentSpeed * this.delta;
    if ((double) this.currentFrame <= 0.0)
    {
      this.currentFrame = 0.0f;
      this.pingPongToggle = true;
      return true;
    }
    if ((double) this.currentFrame < (double) this.meshCacheCount)
      return false;
    this.pingPongToggle = false;
    this.currentFrame = (float) (this.meshCacheCount - 1);
    return true;
  }

  public bool NextFrame()
  {
    this.currentFrame += this.currentSpeed * this.delta;
    if ((double) this.currentFrame > (double) (this.meshCacheCount + 1))
    {
      this.currentFrame = 0.0f;
      if (!this.loop)
        this.enabled = false;
      return true;
    }
    if ((double) this.currentFrame < (double) this.meshCacheCount)
      return false;
    this.currentFrame = (float) this.meshCacheCount - this.currentFrame;
    if (!this.loop)
      this.enabled = false;
    return true;
  }

  public void RandomizePropertiesAfterLoop()
  {
    if (this.randomSpeedLoop)
      this.RandomizePlaySpeed();
    if (!this.randomRotateLoop)
      return;
    this.RandomRotate();
  }

  public void Animate()
  {
    if (!this.rendererComponent.isVisible)
      return;
    if (this.pingPong && this.PingPongFrame())
      this.RandomizePropertiesAfterLoop();
    else if (!this.pingPong && this.NextFrame())
      this.RandomizePropertiesAfterLoop();
    this.meshFilter.sharedMesh = this.meshCache[(int) this.currentFrame].sharedMesh;
  }
}
