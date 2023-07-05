// Decompiled with JetBrains decompiler
// Type: UnluckFlagGUI
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using UnityEngine;

public class UnluckFlagGUI : MonoBehaviour
{
  public GameObject[] prefabs;
  public Material[] bgrs;
  public Light[] lights;
  public GameObject nextButton;
  public GameObject prevButton;
  public GameObject bgrButton;
  public GameObject lightButton;
  public GameObject texturePreview;
  private GameObject activeObj;
  private int counter;
  private int bCounter;
  private int lCounter;
  public TextMesh txt;
  public TextMesh debug;

  public void Start()
  {
    this.Swap();
    if ((Object) this.txt == (Object) null)
      this.txt = this.transform.Find("txt").GetComponent<TextMesh>();
    if ((Object) this.nextButton == (Object) null)
      this.nextButton = this.transform.Find("nextButton").GetComponent<GameObject>();
    if ((Object) this.prevButton == (Object) null)
      this.prevButton = this.transform.Find("prevButton").GetComponent<GameObject>();
    if ((Object) this.bgrButton == (Object) null)
      this.bgrButton = this.transform.Find("bgrButton").GetComponent<GameObject>();
    if ((Object) this.lightButton == (Object) null)
      this.lightButton = this.transform.Find("lightButton").gameObject;
    if ((Object) this.texturePreview == (Object) null)
      this.texturePreview = this.transform.Find("texturePreview").GetComponent<GameObject>();
    if (!((Object) this.debug == (Object) null))
      return;
    this.debug = this.transform.Find("debug").GetComponent<TextMesh>();
  }

  public void Update()
  {
    if (Input.GetMouseButtonUp(0))
      this.ButtonUp();
    if (Input.GetKeyUp("right"))
      this.Next();
    if (Input.GetKeyUp("left"))
      this.Prev();
    if (!Input.GetKeyUp("space"))
      return;
    this.nextButton.SetActive(!this.nextButton.activeInHierarchy);
    this.prevButton.SetActive(this.nextButton.activeInHierarchy);
    this.bgrButton.SetActive(this.nextButton.activeInHierarchy);
    this.texturePreview.SetActive(this.nextButton.activeInHierarchy);
    this.txt.gameObject.SetActive(this.nextButton.activeInHierarchy);
    this.lightButton.gameObject.SetActive(this.nextButton.activeInHierarchy);
  }

  public void ButtonUp()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    UnityEngine.RaycastHit raycastHit = new UnityEngine.RaycastHit();
    ref UnityEngine.RaycastHit local = ref raycastHit;
    if (!Physics.Raycast(ray, out local))
      return;
    if ((Object) raycastHit.transform.gameObject == (Object) this.nextButton)
      this.Next();
    else if ((Object) raycastHit.transform.gameObject == (Object) this.prevButton)
      this.Prev();
    else if ((Object) raycastHit.transform.gameObject == (Object) this.bgrButton)
    {
      this.NextBgr();
    }
    else
    {
      if (!((Object) raycastHit.transform.gameObject == (Object) this.lightButton))
        return;
      this.LightChange();
    }
  }

  public void LightChange()
  {
    if (this.lights.Length == 0)
      return;
    this.lights[this.lCounter].enabled = false;
    ++this.lCounter;
    if (this.lCounter >= this.lights.Length)
      this.lCounter = 0;
    this.lights[this.lCounter].enabled = true;
  }

  public void NextBgr()
  {
    if (this.bgrs.Length == 0)
      return;
    ++this.bCounter;
    if (this.bCounter >= this.bgrs.Length)
      this.bCounter = 0;
    RenderSettings.skybox = this.bgrs[this.bCounter];
  }

  public void Next()
  {
    ++this.counter;
    if (this.counter > this.prefabs.Length - 1)
      this.counter = 0;
    this.Swap();
  }

  public void Prev()
  {
    --this.counter;
    if (this.counter < 0)
      this.counter = this.prefabs.Length - 1;
    this.Swap();
  }

  public void Swap()
  {
    if (this.prefabs.Length == 0)
      return;
    Object.Destroy((Object) this.activeObj);
    this.activeObj = Object.Instantiate<GameObject>(this.prefabs[this.counter]);
    if ((Object) this.txt != (Object) null)
    {
      this.txt.text = this.activeObj.name;
      this.txt.text = this.txt.text.Replace("(Clone)", "");
      this.txt.text = this.txt.text + " " + this.activeObj.GetComponent<UnluckAnimatedMesh>().meshContainerFBX.name;
      this.txt.text = this.txt.text.Replace("_", " ");
      this.txt.text = this.txt.text.Replace("Flag ", "");
    }
    if (!((Object) this.texturePreview != (Object) null))
      return;
    this.texturePreview.GetComponent<Renderer>().sharedMaterial = this.activeObj.GetComponent<Renderer>().sharedMaterial;
  }
}
