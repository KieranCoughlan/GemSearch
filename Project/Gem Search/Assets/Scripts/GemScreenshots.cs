using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScreenshots : MonoBehaviour
{
  public GameObject[] Gems;
  

  private int currentIndex = -1;
  private GameObject lastSpawned = null;
  private Camera mainCamera;

  

  // Start is called before the first frame update
  void Start()
  {
    mainCamera = Camera.main;
    Invoke("DoNextGem", 2.0f);
  }

  void DoNextGem()
  {
    currentIndex++;
    if (lastSpawned != null)
      Object.Destroy(lastSpawned);

    lastSpawned = GameObject.Instantiate(Gems[currentIndex], transform);

    Invoke("TakeScreenshot", 1);
    
    if (currentIndex < Gems.Length - 1)
      Invoke("DoNextGem", 3.0f);
  }

  private void TakeScreenshot()
  {
    string name = Gems[currentIndex].name;
  
    RenderTexture renderTexture = new RenderTexture(256, 256, 24);
    mainCamera.targetTexture = renderTexture;
    Texture2D screenshot = new Texture2D(256, 256, TextureFormat.RGB24, false);
    mainCamera.Render();
    RenderTexture.active = renderTexture;
    screenshot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
    screenshot.Apply();
    mainCamera.targetTexture = null;
    RenderTexture.active = null;
    Destroy(renderTexture);

    byte[] bytes = screenshot.EncodeToPNG();
    System.IO.File.WriteAllBytes(string.Format("assets/sprites/{0}.png", name), bytes);
  }
}
