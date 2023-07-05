// Decompiled with JetBrains decompiler
// Type: DigitalOpus.MB.Core.MB2_TexturePacker
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
  public class MB2_TexturePacker
  {
    public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;
    private MB2_TexturePacker.ProbeResult bestRoot;
    public int atlasY;
    public bool doPowerOfTwoTextures = true;

    private static void printTree(MB2_TexturePacker.Node r, string spc)
    {
      Debug.Log((object) (spc + "Nd img=" + (r.img != null).ToString() + " r=" + r.r?.ToString()));
      if (r.child[0] != null)
        MB2_TexturePacker.printTree(r.child[0], spc + "      ");
      if (r.child[1] == null)
        return;
      MB2_TexturePacker.printTree(r.child[1], spc + "      ");
    }

    private static void flattenTree(MB2_TexturePacker.Node r, List<MB2_TexturePacker.Image> putHere)
    {
      if (r.img != null)
      {
        r.img.x = r.r.x;
        r.img.y = r.r.y;
        putHere.Add(r.img);
      }
      if (r.child[0] != null)
        MB2_TexturePacker.flattenTree(r.child[0], putHere);
      if (r.child[1] == null)
        return;
      MB2_TexturePacker.flattenTree(r.child[1], putHere);
    }

    private static void drawGizmosNode(MB2_TexturePacker.Node r)
    {
      Vector3 size1 = new Vector3((float) r.r.w, (float) r.r.h, 0.0f);
      Vector3 center = new Vector3((float) r.r.x + size1.x / 2f, (float) -r.r.y - size1.y / 2f, 0.0f);
      Gizmos.color = Color.yellow;
      Vector3 size2 = size1;
      Gizmos.DrawWireCube(center, size2);
      if (r.img != null)
      {
        Gizmos.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        size1 = new Vector3((float) r.img.w, (float) r.img.h, 0.0f);
        Gizmos.DrawCube(new Vector3((float) r.r.x + size1.x / 2f, (float) -r.r.y - size1.y / 2f, 0.0f), size1);
      }
      if (r.child[0] != null)
      {
        Gizmos.color = Color.red;
        MB2_TexturePacker.drawGizmosNode(r.child[0]);
      }
      if (r.child[1] == null)
        return;
      Gizmos.color = Color.green;
      MB2_TexturePacker.drawGizmosNode(r.child[1]);
    }

    private static Texture2D createFilledTex(Color c, int w, int h)
    {
      Texture2D filledTex = new Texture2D(w, h);
      for (int x = 0; x < w; ++x)
      {
        for (int y = 0; y < h; ++y)
          filledTex.SetPixel(x, y, c);
      }
      filledTex.Apply();
      return filledTex;
    }

    public void DrawGizmos()
    {
      if (this.bestRoot == null)
        return;
      MB2_TexturePacker.drawGizmosNode(this.bestRoot.root);
      Gizmos.color = Color.yellow;
      Vector3 size = new Vector3((float) this.bestRoot.outW, (float) -this.bestRoot.outH, 0.0f);
      Gizmos.DrawWireCube(new Vector3(size.x / 2f, size.y / 2f, 0.0f), size);
    }

    private bool ProbeSingleAtlas(
      MB2_TexturePacker.Image[] imgsToAdd,
      int idealAtlasW,
      int idealAtlasH,
      float imgArea,
      int maxAtlasDim,
      MB2_TexturePacker.ProbeResult pr)
    {
      MB2_TexturePacker.Node r = new MB2_TexturePacker.Node(MB2_TexturePacker.NodeType.maxDim);
      r.r = new MB2_TexturePacker.PixRect(0, 0, idealAtlasW, idealAtlasH);
      for (int index = 0; index < imgsToAdd.Length; ++index)
      {
        if (r.Insert(imgsToAdd[index], false) == null)
          return false;
        if (index == imgsToAdd.Length - 1)
        {
          int x = 0;
          int y = 0;
          this.GetExtent(r, ref x, ref y);
          int outw = x;
          int outh = y;
          bool fits;
          float e;
          float sq;
          if (this.doPowerOfTwoTextures)
          {
            outw = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(x), maxAtlasDim);
            outh = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(y), maxAtlasDim);
            if (outh < outw / 2)
              outh = outw / 2;
            if (outw < outh / 2)
              outw = outh / 2;
            fits = x <= maxAtlasDim && y <= maxAtlasDim;
            float num1 = Mathf.Max(1f, (float) x / (float) maxAtlasDim);
            float num2 = Mathf.Max(1f, (float) y / (float) maxAtlasDim);
            float num3 = (float) outw * num1 * (float) outh * num2;
            e = (float) (1.0 - ((double) num3 - (double) imgArea) / (double) num3);
            sq = 1f;
          }
          else
          {
            e = (float) (1.0 - ((double) (x * y) - (double) imgArea) / (double) (x * y));
            sq = x >= y ? (float) y / (float) x : (float) x / (float) y;
            fits = x <= maxAtlasDim && y <= maxAtlasDim;
          }
          pr.Set(x, y, outw, outh, r, fits, e, sq);
          if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Probe success efficiency w=" + x.ToString() + " h=" + y.ToString() + " e=" + e.ToString() + " sq=" + sq.ToString() + " fits=" + fits.ToString());
          return true;
        }
      }
      Debug.LogError((object) "Should never get here.");
      return false;
    }

    private bool ProbeMultiAtlas(
      MB2_TexturePacker.Image[] imgsToAdd,
      int idealAtlasW,
      int idealAtlasH,
      float imgArea,
      int maxAtlasDim,
      MB2_TexturePacker.ProbeResult pr)
    {
      int num = 0;
      MB2_TexturePacker.Node node1 = new MB2_TexturePacker.Node(MB2_TexturePacker.NodeType.maxDim);
      node1.r = new MB2_TexturePacker.PixRect(0, 0, idealAtlasW, idealAtlasH);
      for (int index = 0; index < imgsToAdd.Length; ++index)
      {
        if (node1.Insert(imgsToAdd[index], false) == null)
        {
          if (imgsToAdd[index].x > idealAtlasW && imgsToAdd[index].y > idealAtlasH)
            return false;
          MB2_TexturePacker.Node node2 = new MB2_TexturePacker.Node(MB2_TexturePacker.NodeType.Container);
          node2.r = new MB2_TexturePacker.PixRect(0, 0, node1.r.w + idealAtlasW, idealAtlasH);
          node2.child[1] = new MB2_TexturePacker.Node(MB2_TexturePacker.NodeType.maxDim)
          {
            r = new MB2_TexturePacker.PixRect(node1.r.w, 0, idealAtlasW, idealAtlasH)
          };
          node2.child[0] = node1;
          node1 = node2;
          node1.Insert(imgsToAdd[index], false);
          ++num;
        }
      }
      pr.numAtlases = num;
      pr.root = node1;
      pr.totalAtlasArea = (float) (num * maxAtlasDim * maxAtlasDim);
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        MB2_Log.LogDebug("Probe success efficiency numAtlases=" + num.ToString() + " totalArea=" + pr.totalAtlasArea.ToString());
      return true;
    }

    private void GetExtent(MB2_TexturePacker.Node r, ref int x, ref int y)
    {
      if (r.img != null)
      {
        if (r.r.x + r.img.w > x)
          x = r.r.x + r.img.w;
        if (r.r.y + r.img.h > y)
          y = r.r.y + r.img.h;
      }
      if (r.child[0] != null)
        this.GetExtent(r.child[0], ref x, ref y);
      if (r.child[1] == null)
        return;
      this.GetExtent(r.child[1], ref x, ref y);
    }

    private int StepWidthHeight(int oldVal, int step, int maxDim)
    {
      if (this.doPowerOfTwoTextures && oldVal < maxDim)
        return oldVal * 2;
      int num = oldVal + step;
      if (num > maxDim && oldVal < maxDim)
        num = maxDim;
      return num;
    }

    public static int RoundToNearestPositivePowerOfTwo(int x)
    {
      int positivePowerOfTwo = (int) Mathf.Pow(2f, (float) Mathf.RoundToInt(Mathf.Log((float) x) / Mathf.Log(2f)));
      switch (positivePowerOfTwo)
      {
        case 0:
        case 1:
          positivePowerOfTwo = 2;
          break;
      }
      return positivePowerOfTwo;
    }

    public static int CeilToNearestPowerOfTwo(int x)
    {
      int nearestPowerOfTwo = (int) Mathf.Pow(2f, Mathf.Ceil(Mathf.Log((float) x) / Mathf.Log(2f)));
      switch (nearestPowerOfTwo)
      {
        case 0:
        case 1:
          nearestPowerOfTwo = 2;
          break;
      }
      return nearestPowerOfTwo;
    }

    public AtlasPackingResult[] GetRects(
      List<Vector2> imgWidthHeights,
      int maxDimension,
      int padding)
    {
      return this.GetRects(imgWidthHeights, maxDimension, padding, false);
    }

    public AtlasPackingResult[] GetRects(
      List<Vector2> imgWidthHeights,
      int maxDimension,
      int padding,
      bool doMultiAtlas)
    {
      if (doMultiAtlas)
        return this._GetRectsMultiAtlas(imgWidthHeights, maxDimension, padding, 2 + padding * 2, 2 + padding * 2, 2 + padding * 2, 2 + padding * 2);
      AtlasPackingResult rectsSingleAtlas = this._GetRectsSingleAtlas(imgWidthHeights, maxDimension, padding, 2 + padding * 2, 2 + padding * 2, 2 + padding * 2, 2 + padding * 2, 0);
      if (rectsSingleAtlas == null)
        return (AtlasPackingResult[]) null;
      return new AtlasPackingResult[1]{ rectsSingleAtlas };
    }

    private AtlasPackingResult _GetRectsSingleAtlas(
      List<Vector2> imgWidthHeights,
      int maxDimension,
      int padding,
      int minImageSizeX,
      int minImageSizeY,
      int masterImageSizeX,
      int masterImageSizeY,
      int recursionDepth)
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        Debug.Log((object) string.Format("_GetRects numImages={0}, maxDimension={1}, padding={2}, minImageSizeX={3}, minImageSizeY={4}, masterImageSizeX={5}, masterImageSizeY={6}, recursionDepth={7}", (object) imgWidthHeights.Count, (object) maxDimension, (object) padding, (object) minImageSizeX, (object) minImageSizeY, (object) masterImageSizeX, (object) masterImageSizeY, (object) recursionDepth));
      if (recursionDepth > 10)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.error)
          Debug.LogError((object) "Maximum recursion depth reached. Couldn't find packing for these textures.");
        return (AtlasPackingResult) null;
      }
      float num1 = 0.0f;
      int num2 = 0;
      int num3 = 0;
      MB2_TexturePacker.Image[] imageArray = new MB2_TexturePacker.Image[imgWidthHeights.Count];
      for (int index = 0; index < imageArray.Length; ++index)
      {
        int x = (int) imgWidthHeights[index].x;
        int y = (int) imgWidthHeights[index].y;
        MB2_TexturePacker.Image image = imageArray[index] = new MB2_TexturePacker.Image(index, x, y, padding, minImageSizeX, minImageSizeY);
        num1 += (float) (image.w * image.h);
        num2 = Mathf.Max(num2, image.w);
        num3 = Mathf.Max(num3, image.h);
      }
      if ((double) num3 / (double) num2 > 2.0)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("Using height Comparer");
        Array.Sort<MB2_TexturePacker.Image>(imageArray, (IComparer<MB2_TexturePacker.Image>) new MB2_TexturePacker.ImageHeightComparer());
      }
      else if ((double) num3 / (double) num2 < 0.5)
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("Using width Comparer");
        Array.Sort<MB2_TexturePacker.Image>(imageArray, (IComparer<MB2_TexturePacker.Image>) new MB2_TexturePacker.ImageWidthComparer());
      }
      else
      {
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("Using area Comparer");
        Array.Sort<MB2_TexturePacker.Image>(imageArray, (IComparer<MB2_TexturePacker.Image>) new MB2_TexturePacker.ImageAreaComparer());
      }
      int x1 = (int) Mathf.Sqrt(num1);
      int x2;
      int x3;
      if (this.doPowerOfTwoTextures)
      {
        x3 = x2 = MB2_TexturePacker.RoundToNearestPositivePowerOfTwo(x1);
        if (num2 > x3)
          x3 = MB2_TexturePacker.CeilToNearestPowerOfTwo(x3);
        if (num3 > x2)
          x2 = MB2_TexturePacker.CeilToNearestPowerOfTwo(x2);
      }
      else
      {
        x3 = x1;
        x2 = x1;
        if (num2 > x1)
        {
          x3 = num2;
          x2 = Mathf.Max(Mathf.CeilToInt(num1 / (float) num2), num3);
        }
        if (num3 > x1)
        {
          x3 = Mathf.Max(Mathf.CeilToInt(num1 / (float) num3), num2);
          x2 = num3;
        }
      }
      if (x3 == 0)
        x3 = 4;
      if (x2 == 0)
        x2 = 4;
      int step1 = (int) ((double) x3 * 0.15000000596046448);
      int step2 = (int) ((double) x2 * 0.15000000596046448);
      if (step1 == 0)
        step1 = 1;
      if (step2 == 0)
        step2 = 1;
      int num4 = 2;
      int num5 = x2;
      while (num4 >= 1 && num5 < x1 * 1000)
      {
        bool flag = false;
        num4 = 0;
        int num6 = x3;
        while (!flag && num6 < x1 * 1000)
        {
          MB2_TexturePacker.ProbeResult pr = new MB2_TexturePacker.ProbeResult();
          if (this.LOG_LEVEL >= MB2_LogLevel.trace)
            Debug.Log((object) ("Probing h=" + num5.ToString() + " w=" + num6.ToString()));
          if (this.ProbeSingleAtlas(imageArray, num6, num5, num1, maxDimension, pr))
          {
            flag = true;
            if (this.bestRoot == null)
              this.bestRoot = pr;
            else if ((double) pr.GetScore(this.doPowerOfTwoTextures) > (double) this.bestRoot.GetScore(this.doPowerOfTwoTextures))
              this.bestRoot = pr;
          }
          else
          {
            ++num4;
            num6 = this.StepWidthHeight(num6, step1, maxDimension);
            if (this.LOG_LEVEL >= MB2_LogLevel.trace)
              MB2_Log.LogDebug("increasing Width h=" + num5.ToString() + " w=" + num6.ToString());
          }
        }
        num5 = this.StepWidthHeight(num5, step2, maxDimension);
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug("increasing Height h=" + num5.ToString() + " w=" + num6.ToString());
      }
      if (this.bestRoot == null)
        return (AtlasPackingResult) null;
      int outW;
      int outH;
      if (this.doPowerOfTwoTextures)
      {
        outW = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(this.bestRoot.w), maxDimension);
        outH = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(this.bestRoot.h), maxDimension);
        if (outH < outW / 2)
          outH = outW / 2;
        if (outW < outH / 2)
          outW = outH / 2;
      }
      else
      {
        outW = Mathf.Min(this.bestRoot.w, maxDimension);
        outH = Mathf.Min(this.bestRoot.h, maxDimension);
      }
      this.bestRoot.outW = outW;
      this.bestRoot.outH = outH;
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        Debug.Log((object) ("Best fit found: atlasW=" + outW.ToString() + " atlasH" + outH.ToString() + " w=" + this.bestRoot.w.ToString() + " h=" + this.bestRoot.h.ToString() + " efficiency=" + this.bestRoot.efficiency.ToString() + " squareness=" + this.bestRoot.squareness.ToString() + " fits in max dimension=" + this.bestRoot.largerOrEqualToMaxDim.ToString()));
      List<MB2_TexturePacker.Image> imageList = new List<MB2_TexturePacker.Image>();
      MB2_TexturePacker.flattenTree(this.bestRoot.root, imageList);
      imageList.Sort((IComparer<MB2_TexturePacker.Image>) new MB2_TexturePacker.ImgIDComparer());
      AtlasPackingResult fitMaxDim = this.ScaleAtlasToFitMaxDim(this.bestRoot, imgWidthHeights, imageList, maxDimension, padding, minImageSizeX, minImageSizeY, masterImageSizeX, masterImageSizeY, outW, outH, recursionDepth);
      if (this.LOG_LEVEL < MB2_LogLevel.debug)
        return fitMaxDim;
      MB2_Log.LogDebug(string.Format("Done GetRects atlasW={0} atlasH={1}", (object) this.bestRoot.w, (object) this.bestRoot.h));
      return fitMaxDim;
    }

    private AtlasPackingResult ScaleAtlasToFitMaxDim(
      MB2_TexturePacker.ProbeResult root,
      List<Vector2> imgWidthHeights,
      List<MB2_TexturePacker.Image> images,
      int maxDimension,
      int padding,
      int minImageSizeX,
      int minImageSizeY,
      int masterImageSizeX,
      int masterImageSizeY,
      int outW,
      int outH,
      int recursionDepth)
    {
      int minImageSizeX1 = minImageSizeX;
      int minImageSizeY1 = minImageSizeY;
      bool flag = false;
      float num1 = (float) padding / (float) outW;
      if (root.w > maxDimension)
      {
        num1 = (float) padding / (float) maxDimension;
        float num2 = (float) maxDimension / (float) root.w;
        if (this.LOG_LEVEL >= MB2_LogLevel.warn)
          Debug.LogWarning((object) ("Packing exceeded atlas width shrinking to " + num2.ToString()));
        for (int index = 0; index < images.Count; ++index)
        {
          MB2_TexturePacker.Image image = images[index];
          if ((double) image.w * (double) num2 < (double) masterImageSizeX)
          {
            if (this.LOG_LEVEL >= MB2_LogLevel.debug)
              Debug.Log((object) "Small images are being scaled to zero. Will need to redo packing with larger minTexSizeX.");
            flag = true;
            minImageSizeX1 = Mathf.CeilToInt((float) minImageSizeX / num2);
          }
          int num3 = (int) ((double) (image.x + image.w) * (double) num2);
          image.x = (int) ((double) num2 * (double) image.x);
          image.w = num3 - image.x;
        }
        outW = maxDimension;
      }
      float num4 = (float) padding / (float) outH;
      if (root.h > maxDimension)
      {
        num4 = (float) padding / (float) maxDimension;
        float num5 = (float) maxDimension / (float) root.h;
        if (this.LOG_LEVEL >= MB2_LogLevel.warn)
          Debug.LogWarning((object) ("Packing exceeded atlas height shrinking to " + num5.ToString()));
        for (int index = 0; index < images.Count; ++index)
        {
          MB2_TexturePacker.Image image = images[index];
          if ((double) image.h * (double) num5 < (double) masterImageSizeY)
          {
            if (this.LOG_LEVEL >= MB2_LogLevel.debug)
              Debug.Log((object) "Small images are being scaled to zero. Will need to redo packing with larger minTexSizeY.");
            flag = true;
            minImageSizeY1 = Mathf.CeilToInt((float) minImageSizeY / num5);
          }
          int num6 = (int) ((double) (image.y + image.h) * (double) num5);
          image.y = (int) ((double) num5 * (double) image.y);
          image.h = num6 - image.y;
        }
        outH = maxDimension;
      }
      if (!flag)
      {
        AtlasPackingResult fitMaxDim = new AtlasPackingResult();
        fitMaxDim.rects = new Rect[images.Count];
        fitMaxDim.srcImgIdxs = new int[images.Count];
        fitMaxDim.atlasX = outW;
        fitMaxDim.atlasY = outH;
        fitMaxDim.usedW = -1;
        fitMaxDim.usedH = -1;
        for (int index = 0; index < images.Count; ++index)
        {
          MB2_TexturePacker.Image image = images[index];
          Rect rect = fitMaxDim.rects[index] = new Rect((float) image.x / (float) outW + num1, (float) image.y / (float) outH + num4, (float) ((double) image.w / (double) outW - (double) num1 * 2.0), (float) ((double) image.h / (double) outH - (double) num4 * 2.0));
          fitMaxDim.srcImgIdxs[index] = image.imgId;
          if (this.LOG_LEVEL >= MB2_LogLevel.debug)
            MB2_Log.LogDebug("Image: " + index.ToString() + " imgID=" + image.imgId.ToString() + " x=" + (rect.x * (float) outW).ToString() + " y=" + (rect.y * (float) outH).ToString() + " w=" + (rect.width * (float) outW).ToString() + " h=" + (rect.height * (float) outH).ToString() + " padding=" + padding.ToString());
        }
        return fitMaxDim;
      }
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        Debug.Log((object) "==================== REDOING PACKING ================");
      root = (MB2_TexturePacker.ProbeResult) null;
      return this._GetRectsSingleAtlas(imgWidthHeights, maxDimension, padding, minImageSizeX1, minImageSizeY1, masterImageSizeX, masterImageSizeY, recursionDepth + 1);
    }

    private AtlasPackingResult[] _GetRectsMultiAtlas(
      List<Vector2> imgWidthHeights,
      int maxDimensionPassed,
      int padding,
      int minImageSizeX,
      int minImageSizeY,
      int masterImageSizeX,
      int masterImageSizeY)
    {
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        Debug.Log((object) string.Format("_GetRects numImages={0}, maxDimension={1}, padding={2}, minImageSizeX={3}, minImageSizeY={4}, masterImageSizeX={5}, masterImageSizeY={6}", (object) imgWidthHeights.Count, (object) maxDimensionPassed, (object) padding, (object) minImageSizeX, (object) minImageSizeY, (object) masterImageSizeX, (object) masterImageSizeY));
      float imgArea = 0.0f;
      int a1 = 0;
      int a2 = 0;
      MB2_TexturePacker.Image[] imageArray = new MB2_TexturePacker.Image[imgWidthHeights.Count];
      int num1 = maxDimensionPassed;
      if (this.doPowerOfTwoTextures)
        num1 = MB2_TexturePacker.RoundToNearestPositivePowerOfTwo(num1);
      for (int index = 0; index < imageArray.Length; ++index)
      {
        int x = (int) imgWidthHeights[index].x;
        int y = (int) imgWidthHeights[index].y;
        int tw = Mathf.Min(x, num1 - padding * 2);
        int th = Mathf.Min(y, num1 - padding * 2);
        MB2_TexturePacker.Image image = imageArray[index] = new MB2_TexturePacker.Image(index, tw, th, padding, minImageSizeX, minImageSizeY);
        imgArea += (float) (image.w * image.h);
        a1 = Mathf.Max(a1, image.w);
        a2 = Mathf.Max(a2, image.h);
      }
      int idealAtlasH;
      int idealAtlasW;
      if (this.doPowerOfTwoTextures)
      {
        idealAtlasH = MB2_TexturePacker.RoundToNearestPositivePowerOfTwo(num1);
        idealAtlasW = MB2_TexturePacker.RoundToNearestPositivePowerOfTwo(num1);
      }
      else
      {
        idealAtlasH = num1;
        idealAtlasW = num1;
      }
      if (idealAtlasW == 0)
        idealAtlasW = 4;
      if (idealAtlasH == 0)
        idealAtlasH = 4;
      MB2_TexturePacker.ProbeResult pr = new MB2_TexturePacker.ProbeResult();
      Array.Sort<MB2_TexturePacker.Image>(imageArray, (IComparer<MB2_TexturePacker.Image>) new MB2_TexturePacker.ImageHeightComparer());
      if (this.ProbeMultiAtlas(imageArray, idealAtlasW, idealAtlasH, imgArea, num1, pr))
        this.bestRoot = pr;
      Array.Sort<MB2_TexturePacker.Image>(imageArray, (IComparer<MB2_TexturePacker.Image>) new MB2_TexturePacker.ImageWidthComparer());
      if (this.ProbeMultiAtlas(imageArray, idealAtlasW, idealAtlasH, imgArea, num1, pr) && (double) pr.totalAtlasArea < (double) this.bestRoot.totalAtlasArea)
        this.bestRoot = pr;
      Array.Sort<MB2_TexturePacker.Image>(imageArray, (IComparer<MB2_TexturePacker.Image>) new MB2_TexturePacker.ImageAreaComparer());
      if (this.ProbeMultiAtlas(imageArray, idealAtlasW, idealAtlasH, imgArea, num1, pr) && (double) pr.totalAtlasArea < (double) this.bestRoot.totalAtlasArea)
        this.bestRoot = pr;
      if (this.bestRoot == null)
        return (AtlasPackingResult[]) null;
      if (this.LOG_LEVEL >= MB2_LogLevel.debug)
        Debug.Log((object) ("Best fit found: w=" + this.bestRoot.w.ToString() + " h=" + this.bestRoot.h.ToString() + " efficiency=" + this.bestRoot.efficiency.ToString() + " squareness=" + this.bestRoot.squareness.ToString() + " fits in max dimension=" + this.bestRoot.largerOrEqualToMaxDim.ToString()));
      List<AtlasPackingResult> atlasPackingResultList = new List<AtlasPackingResult>();
      List<MB2_TexturePacker.Node> nodeList = new List<MB2_TexturePacker.Node>();
      Stack<MB2_TexturePacker.Node> nodeStack = new Stack<MB2_TexturePacker.Node>();
      for (MB2_TexturePacker.Node root = this.bestRoot.root; root != null; root = root.child[0])
        nodeStack.Push(root);
      while (nodeStack.Count > 0)
      {
        MB2_TexturePacker.Node node1 = nodeStack.Pop();
        if (node1.isFullAtlas == MB2_TexturePacker.NodeType.maxDim)
          nodeList.Add(node1);
        if (node1.child[1] != null)
        {
          for (MB2_TexturePacker.Node node2 = node1.child[1]; node2 != null; node2 = node2.child[0])
            nodeStack.Push(node2);
        }
      }
      for (int index1 = 0; index1 < nodeList.Count; ++index1)
      {
        List<MB2_TexturePacker.Image> putHere = new List<MB2_TexturePacker.Image>();
        MB2_TexturePacker.flattenTree(nodeList[index1], putHere);
        Rect[] rectArray = new Rect[putHere.Count];
        int[] numArray = new int[putHere.Count];
        for (int index2 = 0; index2 < putHere.Count; ++index2)
        {
          rectArray[index2] = new Rect((float) (putHere[index2].x - nodeList[index1].r.x), (float) putHere[index2].y, (float) putHere[index2].w, (float) putHere[index2].h);
          numArray[index2] = putHere[index2].imgId;
        }
        AtlasPackingResult rr = new AtlasPackingResult();
        this.GetExtent(nodeList[index1], ref rr.usedW, ref rr.usedH);
        rr.usedW -= nodeList[index1].r.x;
        int w = nodeList[index1].r.w;
        int h = nodeList[index1].r.h;
        int num2;
        int num3;
        if (this.doPowerOfTwoTextures)
        {
          num2 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(rr.usedW), nodeList[index1].r.w);
          num3 = Mathf.Min(MB2_TexturePacker.CeilToNearestPowerOfTwo(rr.usedH), nodeList[index1].r.h);
          if (num3 < num2 / 2)
            num3 = num2 / 2;
          if (num2 < num3 / 2)
            num2 = num3 / 2;
        }
        else
        {
          num2 = rr.usedW;
          num3 = rr.usedH;
        }
        rr.atlasY = num3;
        rr.atlasX = num2;
        rr.rects = rectArray;
        rr.srcImgIdxs = numArray;
        atlasPackingResultList.Add(rr);
        this.normalizeRects(rr, padding);
        if (this.LOG_LEVEL >= MB2_LogLevel.debug)
          MB2_Log.LogDebug(string.Format("Done GetRects "));
      }
      return atlasPackingResultList.ToArray();
    }

    private void normalizeRects(AtlasPackingResult rr, int padding)
    {
      for (int index = 0; index < rr.rects.Length; ++index)
      {
        rr.rects[index].x = (rr.rects[index].x + (float) padding) / (float) rr.atlasX;
        rr.rects[index].y = (rr.rects[index].y + (float) padding) / (float) rr.atlasY;
        rr.rects[index].width = (rr.rects[index].width - (float) (padding * 2)) / (float) rr.atlasX;
        rr.rects[index].height = (rr.rects[index].height - (float) (padding * 2)) / (float) rr.atlasY;
      }
    }

    private enum NodeType
    {
      Container,
      maxDim,
      regular,
    }

    private class PixRect
    {
      public int x;
      public int y;
      public int w;
      public int h;

      public PixRect()
      {
      }

      public PixRect(int xx, int yy, int ww, int hh)
      {
        this.x = xx;
        this.y = yy;
        this.w = ww;
        this.h = hh;
      }

      public override string ToString() => string.Format("x={0},y={1},w={2},h={3}", (object) this.x, (object) this.y, (object) this.w, (object) this.h);
    }

    private class Image
    {
      public int imgId;
      public int w;
      public int h;
      public int x;
      public int y;

      public Image(int id, int tw, int th, int padding, int minImageSizeX, int minImageSizeY)
      {
        this.imgId = id;
        this.w = Mathf.Max(tw + padding * 2, minImageSizeX);
        this.h = Mathf.Max(th + padding * 2, minImageSizeY);
      }

      public Image(MB2_TexturePacker.Image im)
      {
        this.imgId = im.imgId;
        this.w = im.w;
        this.h = im.h;
        this.x = im.x;
        this.y = im.y;
      }
    }

    private class ImgIDComparer : IComparer<MB2_TexturePacker.Image>
    {
      public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
      {
        if (x.imgId > y.imgId)
          return 1;
        return x.imgId == y.imgId ? 0 : -1;
      }
    }

    private class ImageHeightComparer : IComparer<MB2_TexturePacker.Image>
    {
      public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
      {
        if (x.h > y.h)
          return -1;
        return x.h == y.h ? 0 : 1;
      }
    }

    private class ImageWidthComparer : IComparer<MB2_TexturePacker.Image>
    {
      public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
      {
        if (x.w > y.w)
          return -1;
        return x.w == y.w ? 0 : 1;
      }
    }

    private class ImageAreaComparer : IComparer<MB2_TexturePacker.Image>
    {
      public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
      {
        int num1 = x.w * x.h;
        int num2 = y.w * y.h;
        if (num1 > num2)
          return -1;
        return num1 == num2 ? 0 : 1;
      }
    }

    private class ProbeResult
    {
      public int w;
      public int h;
      public int outW;
      public int outH;
      public MB2_TexturePacker.Node root;
      public bool largerOrEqualToMaxDim;
      public float efficiency;
      public float squareness;
      public float totalAtlasArea;
      public int numAtlases;

      public void Set(
        int ww,
        int hh,
        int outw,
        int outh,
        MB2_TexturePacker.Node r,
        bool fits,
        float e,
        float sq)
      {
        this.w = ww;
        this.h = hh;
        this.outW = outw;
        this.outH = outh;
        this.root = r;
        this.largerOrEqualToMaxDim = fits;
        this.efficiency = e;
        this.squareness = sq;
      }

      public float GetScore(bool doPowerOfTwoScore)
      {
        float num = this.largerOrEqualToMaxDim ? 1f : 0.0f;
        return doPowerOfTwoScore ? num * 2f + this.efficiency : this.squareness + 2f * this.efficiency + num;
      }

      public void PrintTree() => MB2_TexturePacker.printTree(this.root, "  ");
    }

    private class Node
    {
      public MB2_TexturePacker.NodeType isFullAtlas;
      public MB2_TexturePacker.Node[] child = new MB2_TexturePacker.Node[2];
      public MB2_TexturePacker.PixRect r;
      public MB2_TexturePacker.Image img;

      public Node(MB2_TexturePacker.NodeType rootType) => this.isFullAtlas = rootType;

      private bool isLeaf() => this.child[0] == null || this.child[1] == null;

      public MB2_TexturePacker.Node Insert(MB2_TexturePacker.Image im, bool handed)
      {
        int index1;
        int index2;
        if (handed)
        {
          index1 = 0;
          index2 = 1;
        }
        else
        {
          index1 = 1;
          index2 = 0;
        }
        if (!this.isLeaf())
          return this.child[index1].Insert(im, handed) ?? this.child[index2].Insert(im, handed);
        if (this.img != null)
          return (MB2_TexturePacker.Node) null;
        if (this.r.w < im.w || this.r.h < im.h)
          return (MB2_TexturePacker.Node) null;
        if (this.r.w == im.w && this.r.h == im.h)
        {
          this.img = im;
          return this;
        }
        this.child[index1] = new MB2_TexturePacker.Node(MB2_TexturePacker.NodeType.regular);
        this.child[index2] = new MB2_TexturePacker.Node(MB2_TexturePacker.NodeType.regular);
        if (this.r.w - im.w > this.r.h - im.h)
        {
          this.child[index1].r = new MB2_TexturePacker.PixRect(this.r.x, this.r.y, im.w, this.r.h);
          this.child[index2].r = new MB2_TexturePacker.PixRect(this.r.x + im.w, this.r.y, this.r.w - im.w, this.r.h);
        }
        else
        {
          this.child[index1].r = new MB2_TexturePacker.PixRect(this.r.x, this.r.y, this.r.w, im.h);
          this.child[index2].r = new MB2_TexturePacker.PixRect(this.r.x, this.r.y + im.h, this.r.w, this.r.h - im.h);
        }
        return this.child[index1].Insert(im, handed);
      }
    }
  }
}
