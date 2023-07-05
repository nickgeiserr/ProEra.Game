// Decompiled with JetBrains decompiler
// Type: BOnly.Main_BOnly
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using PBC;
using System.Collections;
using UnityEngine;

namespace BOnly
{
  public class Main_BOnly : MonoBehaviour
  {
    private MoveScript_BOnly moveScript;
    private UserCustomIK_BOnly userCustomIK;
    private AnimFollow_PBC animFollow;
    private RagdollControl_PBC ragdollControl;
    private SeeAnimatedMaster_PBC seeAnimatedMaster;
    private Renderer ragdollRenderer;
    [SerializeField]
    private bool destroyWhenDead;
    private bool userNeedsToFixStuff;
    [HideInInspector]
    public float realCharacterScale = 1f;
    private Transform thisRoot;

    private void Awake()
    {
      this.userNeedsToFixStuff = !this.WeHaveAllTheStuff();
      this.thisRoot = this.transform.root;
      this.userCustomIK = this.GetComponent<UserCustomIK_BOnly>();
      this.seeAnimatedMaster = this.GetComponent<SeeAnimatedMaster_PBC>();
      int num = (bool) (Object) this.seeAnimatedMaster ? 1 : 0;
    }

    private void Update()
    {
      if (this.userNeedsToFixStuff)
        return;
      int num = this.ragdollControl.stayDown2 ? 1 : 0;
    }

    private void FixedUpdate()
    {
      if (this.userNeedsToFixStuff)
        return;
      if (!this.ragdollControl.stayDown2)
      {
        this.StartCoroutine(this.DoExecuteScripts());
      }
      else
      {
        if (this.destroyWhenDead && !this.ragdollRenderer.isVisible)
          Object.Destroy((Object) this.thisRoot);
        this.ragdollControl.stayDown2 = this.ragdollControl.stayDown;
      }
    }

    private IEnumerator DoExecuteScripts()
    {
      if (!this.animFollow.useLagHack1)
      {
        yield return (object) new WaitForFixedUpdate();
        this.animFollow.BeforeMove();
      }
      else
        this.animFollow.ResetMaster();
      this.moveScript.MoveCharacter();
      if ((bool) (Object) this.seeAnimatedMaster)
        this.seeAnimatedMaster.DrawMaster(Color.yellow);
      if ((bool) (Object) this.userCustomIK)
        this.userCustomIK.DoCustomIK();
      this.ragdollControl.DoRagdollControl(Vector3.down);
      this.animFollow.AfterIK(Physics.gravity);
      if (this.animFollow.useLagHack1)
      {
        yield return (object) new WaitForFixedUpdate();
        this.animFollow.BeforeMove();
      }
    }

    public void MoveParent()
    {
      Vector3 translation = this.transform.position - this.transform.parent.position;
      this.transform.parent.Translate(translation, Space.World);
      this.transform.Translate(-translation, Space.World);
      this.ragdollControl.transform.Translate(-translation, Space.World);
    }

    private bool WeHaveAllTheStuff()
    {
      if (!(bool) (Object) (this.moveScript = this.GetComponent<MoveScript_BOnly>()))
      {
        Debug.LogWarning((object) ("No script MoveScript on " + this.name + "\n"));
        return false;
      }
      if (!(bool) (Object) (this.animFollow = this.GetComponent<AnimFollow_PBC>()))
      {
        Debug.LogWarning((object) ("No script AnimFollow on " + this.name + "\n"));
        return false;
      }
      if (!(bool) (Object) (this.ragdollControl = this.transform.root.GetComponentInChildren<RagdollControl_PBC>()))
      {
        Debug.LogWarning((object) ("No script RagdollControl in children of " + this.transform.root.name + "\n"));
        return false;
      }
      if ((bool) (Object) this.ragdollControl.master && (Object) this.ragdollControl.master != (Object) this.transform)
      {
        Debug.LogWarning((object) ("master of RagdollControl is not set to " + ((object) this.transform)?.ToString() + "\n"));
        return false;
      }
      if ((bool) (Object) (this.ragdollRenderer = this.transform.GetComponentInChildren<Renderer>()))
        return true;
      Debug.LogWarning((object) ("No renderer on " + this.name + "\n"));
      return false;
    }
  }
}
