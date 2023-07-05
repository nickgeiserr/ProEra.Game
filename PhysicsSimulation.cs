// Decompiled with JetBrains decompiler
// Type: PhysicsSimulation
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsSimulation
{
  private static Scene scene;
  private static PhysicsScene scenePhysics;

  public static void SimulateRigidbody(Rigidbody rigidbody, float time)
  {
    GameObject gameObject = PhysicsSimulation.CreateSimulationEnvironment(rigidbody).gameObject;
    for (; (double) time > 0.0; time -= Time.fixedDeltaTime)
      PhysicsSimulation.scenePhysics.Simulate(Time.fixedDeltaTime);
    Object.Destroy((Object) gameObject);
  }

  public static void SimulateRigidbody(Rigidbody rigidbody, Vector3 targetPoint)
  {
    GameObject gameObject = PhysicsSimulation.CreateSimulationEnvironment(rigidbody).gameObject;
    float num1;
    float num2;
    do
    {
      num1 = Vector3.Distance(gameObject.transform.position, targetPoint);
      PhysicsSimulation.scenePhysics.Simulate(Time.fixedDeltaTime);
      num2 = Vector3.Distance(gameObject.transform.position, targetPoint);
    }
    while ((double) num1 > (double) num2);
    Object.Destroy((Object) gameObject);
  }

  public static float GetArrivalTimeSimulateRigidbody(Rigidbody rigidbody, Vector3 targetPoint)
  {
    Rigidbody simulationEnvironment = PhysicsSimulation.CreateSimulationEnvironment(rigidbody);
    float num1 = 0.0f;
    float num2;
    float num3;
    do
    {
      num2 = Vector3.Distance(simulationEnvironment.position, targetPoint);
      PhysicsSimulation.scenePhysics.Simulate(Time.fixedDeltaTime);
      num3 = Vector3.Distance(simulationEnvironment.position, targetPoint);
      num1 += Time.fixedDeltaTime;
    }
    while ((double) num2 > (double) num3);
    float num4 = num1 - Time.fixedDeltaTime + MathUtils.MapRange(num2, 0.0f, num2 + num3, 0.0f, Time.fixedDeltaTime);
    Object.Destroy((Object) simulationEnvironment.gameObject);
    return num4 + Time.time;
  }

  private static void CreateDebugSphere(Vector3 position)
  {
    GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    primitive.transform.position = position;
    primitive.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
  }

  private static Rigidbody CreateSimulationEnvironment(Rigidbody rigidbody)
  {
    Rigidbody dummyRigidbody = PhysicsSimulation.CreateDummyRigidbody(rigidbody.name);
    PhysicsSimulation.TransferProperties(rigidbody, dummyRigidbody);
    PhysicsSimulation.InitiatePhysicsScene();
    SceneManager.MoveGameObjectToScene(dummyRigidbody.gameObject, PhysicsSimulation.scene);
    return dummyRigidbody;
  }

  public static void InitiatePhysicsScene()
  {
    if (!PhysicsSimulation.scene.isLoaded)
      PhysicsSimulation.scene = SceneManager.CreateScene("PhysicsScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
    PhysicsSimulation.scenePhysics = PhysicsSimulation.scene.GetPhysicsScene();
  }

  public static void DestroyPhysicsScene()
  {
    if (!PhysicsSimulation.scene.isLoaded)
      return;
    SceneManager.UnloadSceneAsync(PhysicsSimulation.scene);
  }

  public static void SimulateRigidbody(
    Rigidbody sourceRB,
    Rigidbody dummyRB,
    float timeStep,
    PhysicsSimulation.SimulationEndCondition endCondition,
    out PositionAnimationCurves positionalCurves)
  {
    PhysicsSimulation.TransferProperties(sourceRB, dummyRB);
    SceneManager.MoveGameObjectToScene(dummyRB.gameObject, PhysicsSimulation.scene);
    positionalCurves = new PositionAnimationCurves();
    Transform transform = dummyRB.transform;
    float simulationTime = 0.0f;
    float time = Time.time;
    positionalCurves.AddKey(time + simulationTime, transform.position);
    while (!endCondition(dummyRB, simulationTime))
    {
      PhysicsSimulation.scenePhysics.Simulate(timeStep);
      simulationTime += timeStep;
      positionalCurves.AddKey(time + simulationTime, transform.position);
    }
  }

  public static Rigidbody CreateDummyRigidbody(string name) => new GameObject(name + "(Simulated)").AddComponent<Rigidbody>();

  public static void TransferProperties(Rigidbody source, Rigidbody target)
  {
    Transform transform1 = target.transform;
    Transform transform2 = source.transform;
    transform1.position = transform2.position;
    transform1.rotation = transform2.rotation;
    transform1.localScale = transform2.localScale;
    target.constraints = source.constraints;
    target.drag = source.drag;
    target.interpolation = source.interpolation;
    target.mass = source.mass;
    target.position = source.position;
    target.rotation = source.rotation;
    target.velocity = source.velocity;
    target.angularDrag = source.angularDrag;
    target.angularVelocity = source.angularVelocity;
    target.detectCollisions = source.detectCollisions;
    target.freezeRotation = source.freezeRotation;
    target.inertiaTensor = source.inertiaTensor;
    target.isKinematic = source.isKinematic;
    target.sleepThreshold = source.sleepThreshold;
    target.solverIterations = source.solverIterations;
    target.useGravity = source.useGravity;
    target.centerOfMass = source.centerOfMass;
    target.collisionDetectionMode = source.collisionDetectionMode;
    target.inertiaTensorRotation = source.inertiaTensorRotation;
    target.maxAngularVelocity = source.maxAngularVelocity;
    target.maxDepenetrationVelocity = source.maxDepenetrationVelocity;
    target.solverVelocityIterations = source.solverVelocityIterations;
  }

  public delegate bool SimulationEndCondition(Rigidbody simulatedRigidBody, float simulationTime);
}
