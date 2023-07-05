// Decompiled with JetBrains decompiler
// Type: PBC.MoveBaseClass_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

namespace PBC
{
  public abstract class MoveBaseClass_PBC : MonoBehaviour
  {
    [HideInInspector]
    public Vector3 velocity;
    public Vector3 gravity = Physics.gravity;
    [HideInInspector]
    public Vector3 gravity_Hat = Physics.gravity.normalized;
    [HideInInspector]
    public Vector3 relativeVelocity;
    [HideInInspector]
    public Vector3 relativeVelocityT;
    [HideInInspector]
    public Vector3 relativeVelocityT_Hat;
    [HideInInspector]
    public float actualRelativeVelocityMagnitude;
    public bool alwaysGrounded;
    public bool useVerticalRootMotion;
    public bool cGBalance = true;
    public bool tiltAsGravity;
    public bool tiltAsNormal;
    public bool posYByTransform;
    public bool perpendicularGravity;
    [HideInInspector]
    public Vector3 effectiveUpLerped2 = -Physics.gravity.normalized;
    [HideInInspector]
    public float tiltAccTMagnitude;
    [HideInInspector]
    public Quaternion deltaMouseRotation = Quaternion.identity;
    [HideInInspector]
    public Vector3 onPlatformDeltaPos;
    [HideInInspector]
    public Quaternion platformDeltaRotation = Quaternion.identity;
    [HideInInspector]
    public float platformDeltaAngle;
    [HideInInspector]
    public Vector3 platformAxis;
    [HideInInspector]
    public float runScale = 1f;
    [HideInInspector]
    public float strafeScale = 1f;
    [HideInInspector]
    public Vector3 acc;
    [HideInInspector]
    public Vector3 desiredRelativeVelocityT;
    [HideInInspector]
    public Vector3 forwardT_Hat;
    [HideInInspector]
    public Vector3 rightT_Hat;

    public virtual void DoMoveClassUpdate()
    {
    }

    public virtual void MoveCharacter()
    {
    }
  }
}
