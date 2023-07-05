// Decompiled with JetBrains decompiler
// Type: StaticStuff_PBC
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public static class StaticStuff_PBC
{
  public static void PID(
    float P,
    float I,
    float D,
    out float signal,
    float error,
    ref float lastError,
    ref float integrator,
    float deltaTime,
    float reciDeltaTime)
  {
    if ((double) I == 0.0)
      integrator = 0.0f;
    else
      integrator += I * P * error * deltaTime;
    float num = (float) ((double) D * (double) P * ((double) error - (double) lastError)) * reciDeltaTime;
    lastError = error;
    signal = P * error + integrator + num;
  }

  public static void PID(
    float P,
    float I,
    float D,
    out Vector3 signal,
    Vector3 error,
    ref Vector3 lastError,
    ref Vector3 integrator,
    float deltaTime,
    float reciDeltaTime)
  {
    if ((double) I == 0.0)
      integrator = Vector3.zero;
    else
      integrator += I * P * error * deltaTime;
    Vector3 vector3 = D * P * (error - lastError) * reciDeltaTime;
    lastError = error;
    signal = P * error + integrator + vector3;
  }

  public static void PID2(
    float P,
    float I,
    float D,
    out Vector3 signal,
    Vector3 error,
    ref Vector3 lastError,
    ref Vector3 integrator,
    float deltaTime,
    float reciDeltaTime)
  {
    StaticStuff_PBC.PID(P, I, D, out signal.x, error.x, ref lastError.x, ref integrator.x, deltaTime, reciDeltaTime);
    StaticStuff_PBC.PID(P, I, D, out signal.y, error.y, ref lastError.y, ref integrator.y, deltaTime, reciDeltaTime);
    StaticStuff_PBC.PID(P, I, D, out signal.z, error.z, ref lastError.z, ref integrator.z, deltaTime, reciDeltaTime);
  }

  public static void PDControl(
    float P,
    float D,
    out float signal,
    float error,
    ref float lastError,
    float reciDeltaTime)
  {
    signal = P * (error + D * (error - lastError) * reciDeltaTime);
    lastError = error;
  }

  public static void PDControl(
    float P,
    float D,
    out Vector3 signal,
    Vector3 error,
    ref Vector3 lastError,
    float reciDeltaTime)
  {
    signal = P * (error + D * (error - lastError) * reciDeltaTime);
    lastError = error;
  }

  public static Vector3 Project(Vector3 vector, Vector3 ontoPlaneNormal_Hat, Vector3 along_Hat)
  {
    float num1 = Vector3.Dot(vector, ontoPlaneNormal_Hat);
    float num2 = Vector3.Dot(ontoPlaneNormal_Hat, along_Hat);
    if ((double) num2 == 0.0)
      return Vector3.zero;
    float num3 = num1 / num2;
    return vector - num3 * along_Hat;
  }

  public static Vector3 Divide(Vector3 nominator, Vector3 denominator)
  {
    Vector3 vector3;
    vector3.x = nominator.x / denominator.x;
    vector3.y = nominator.y / denominator.y;
    vector3.z = nominator.z / denominator.z;
    return vector3;
  }

  public static bool SafeDivide(out Vector3 result, Vector3 nominator, Vector3 denominator)
  {
    result = Vector3.zero;
    bool flag = true;
    if ((double) denominator.x != 0.0)
      result.x = nominator.x / denominator.x;
    else
      flag = false;
    if ((double) denominator.y != 0.0)
      result.y = nominator.y / denominator.y;
    else
      flag = false;
    if ((double) denominator.z != 0.0)
      result.z = nominator.z / denominator.z;
    else
      flag = false;
    return flag;
  }

  public static Vector3 SafeDivide(Vector3 nominator, Vector3 denominator)
  {
    Vector3 zero = Vector3.zero;
    if ((double) denominator.x != 0.0)
      zero.x = nominator.x / denominator.x;
    if ((double) denominator.y != 0.0)
      zero.y = nominator.y / denominator.y;
    if ((double) denominator.z != 0.0)
      zero.z = nominator.z / denominator.z;
    return zero;
  }

  public static Vector3 Clamp(Vector3 vector, Vector3 min, Vector3 max)
  {
    vector.x = Mathf.Clamp(vector.x, min.x, max.x);
    vector.y = Mathf.Clamp(vector.y, min.y, max.y);
    vector.z = Mathf.Clamp(vector.z, min.z, max.z);
    return vector;
  }

  public static Vector3 Clamp(Vector3 vector, float min, float max)
  {
    vector.x = Mathf.Clamp(vector.x, min, max);
    vector.y = Mathf.Clamp(vector.y, min, max);
    vector.z = Mathf.Clamp(vector.z, min, max);
    return vector;
  }

  public static Vector3 VAbs(Vector3 vector)
  {
    vector.x = Mathf.Abs(vector.x);
    vector.y = Mathf.Abs(vector.y);
    vector.z = Mathf.Abs(vector.z);
    return vector;
  }

  public static float FixEulerAngle(float angle) => (double) angle > 180.0 ? angle - 360f : angle;

  public static Vector3 FixEulerAngle(Vector3 angle)
  {
    if ((double) angle.x > 180.0)
      angle.x -= 360f;
    if ((double) angle.y > 180.0)
      angle.y -= 360f;
    if ((double) angle.z > 180.0)
      angle.z -= 360f;
    return angle;
  }

  public static float FixEulerRadian(float angle) => (double) angle > 3.1415927410125732 ? angle - 6.28318548f : angle;

  public static float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector) => Vector3.Angle(fromVector, toVector) * Mathf.Sign(Vector3.Dot(Vector3.Cross(fromVector, toVector), upVector));

  public static void DrawCircle(
    Vector3 center,
    float radius,
    Vector3 normal,
    Color color,
    float persistTime)
  {
    int num = 24;
    Vector3 vector3_1 = Vector3.Cross(normal, Vector3.forward).normalized * radius;
    for (int index = 0; index <= num; ++index)
    {
      Vector3 vector3_2 = Quaternion.AngleAxis(360f / (float) num, normal) * vector3_1;
      Debug.DrawLine(center + vector3_1, center + vector3_2, color, persistTime);
      vector3_1 = vector3_2;
    }
  }

  public static Vector3 FindThird(Vector3 vector, float length, Vector3 direction)
  {
    double magnitude = (double) vector.magnitude;
    Vector3 normalized = direction.normalized;
    float num1 = (float) magnitude * Vector3.Dot(vector.normalized, normalized);
    float num2 = (float) (magnitude * magnitude - (double) length * (double) length);
    float f = num1 * num1 - num2;
    if ((double) f >= 0.0)
      return (-num1 + Mathf.Sqrt(f)) * normalized;
    Debug.Log((object) ("p " + num1.ToString()));
    Debug.Log((object) ("q " + num2.ToString()));
    Debug.Log((object) ("b " + f.ToString()));
    return Vector3.zero;
  }

  public static Quaternion RoundRotation(Quaternion from, Quaternion to)
  {
    Quaternion quaternion1 = Quaternion.Inverse(from) * to;
    Vector3 euler = new Vector3(Mathf.Round(quaternion1.eulerAngles.x / 90f) * 90f, Mathf.Round(quaternion1.eulerAngles.y / 90f) * 90f, Mathf.Round(quaternion1.eulerAngles.z / 90f) * 90f);
    Quaternion quaternion2 = Quaternion.Euler(euler);
    if ((double) Vector3.Dot(quaternion1 * Vector3.forward, quaternion2 * Vector3.forward) > 0.7070000171661377 && (double) Vector3.Dot(quaternion1 * Vector3.up, quaternion2 * Vector3.up) > 0.7070000171661377 && (double) Vector3.Dot(quaternion1 * Vector3.right, quaternion2 * Vector3.right) > 0.7070000171661377)
      return quaternion2;
    for (int index1 = 0; index1 < 4; ++index1)
    {
      euler.x += (float) index1 * 90f;
      for (int index2 = 0; index2 < 4; ++index2)
      {
        euler.y += (float) index2 * 90f;
        for (int index3 = 0; index3 < 4; ++index3)
        {
          euler.z += (float) index3 * 90f;
          Quaternion quaternion3 = Quaternion.Euler(euler);
          if ((double) Vector3.Dot(quaternion1 * Vector3.forward, quaternion3 * Vector3.forward) > 0.7070000171661377 && (double) Vector3.Dot(quaternion1 * Vector3.up, quaternion3 * Vector3.up) > 0.7070000171661377 && (double) Vector3.Dot(quaternion1 * Vector3.right, quaternion3 * Vector3.right) > 0.7070000171661377)
            return quaternion3;
        }
      }
    }
    Debug.LogWarning((object) "Setup error");
    return Quaternion.identity;
  }

  public static void RagdollInertia(
    Transform masterTransform,
    Vector3 ragdollCenterOfMass,
    Rigidbody[] ragdollRigidbodies,
    out Vector3 ragdollInertiaTensor,
    out Vector3 ragdollAngularMomentum)
  {
    Vector3 zero1 = Vector3.zero;
    Vector3 zero2 = Vector3.zero;
    Vector3 zero3 = Vector3.zero;
    Vector3 zero4 = Vector3.zero;
    Quaternion quaternion1 = Quaternion.Inverse(masterTransform.rotation);
    for (int index = 0; index < ragdollRigidbodies.Length; ++index)
    {
      Vector3 rhs = ragdollRigidbodies[index].worldCenterOfMass - ragdollCenterOfMass;
      Vector3 vector3_1 = quaternion1 * rhs;
      Vector3 vector3_2;
      vector3_2.x = (float) ((double) vector3_1.y * (double) vector3_1.y + (double) vector3_1.z * (double) vector3_1.z);
      vector3_2.y = (float) ((double) vector3_1.x * (double) vector3_1.x + (double) vector3_1.z * (double) vector3_1.z);
      vector3_2.z = (float) ((double) vector3_1.x * (double) vector3_1.x + (double) vector3_1.y * (double) vector3_1.y);
      vector3_2 *= ragdollRigidbodies[index].mass;
      zero1 += vector3_2;
      Quaternion quaternion2 = Quaternion.Inverse(ragdollRigidbodies[index].transform.rotation);
      Quaternion rotation = quaternion2 * masterTransform.rotation;
      zero2 += Quaternion.Inverse(rotation) * Vector3.Scale(rotation * Vector3.one, ragdollRigidbodies[index].inertiaTensor);
      zero3 -= Vector3.Cross(ragdollRigidbodies[index].velocity, rhs) * ragdollRigidbodies[index].mass;
      zero4 += ragdollRigidbodies[index].transform.rotation * Vector3.Scale(quaternion2 * ragdollRigidbodies[index].angularVelocity, ragdollRigidbodies[index].inertiaTensor);
    }
    ragdollInertiaTensor = zero1 + zero2;
    ragdollAngularMomentum = zero3 + zero4;
  }
}
