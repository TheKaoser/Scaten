using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class PhotoManager : MonoBehaviour 
{
    WebCamTexture webCamTexture;
    private SpriteRenderer sr;
    int numberPhotos = 50;
    int numberPhoto = 0;
    public int process = 0;
    public GameObject image;
    public GameObject alignSquare;
    public GameObject background;
    public GameObject textAlign;

    public GameObject textManager;
    Texture2D photo;

    void Start ()
    {
        sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        transform.position = new Vector3(1.5f, 1.5f, 0.0f);

        webCamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();

        photo = new Texture2D(webCamTexture.width, webCamTexture.height);
    }

    void Update() 
    {
        if (process == 1)
        {
            StartCoroutine(PositionTarget());
            process = 2;
        }
        if (process == 4)
        {
            image.GetComponent<Image>().enabled = true;
            StartCoroutine(TakePhotos());
            process = 0;
        }
    }

    IEnumerator PositionTarget()
    {
        while (process == 1 || process == 2)
        {
            yield return new WaitForEndOfFrame(); 

            photo.SetPixels(webCamTexture.GetPixels());
            photo.Apply();
            // photo = rotateTexture(photo, true);
            // Show image
            alignSquare.GetComponent<Image>().sprite = Sprite.Create(photo, new Rect(0.0f, 0.0f, photo.width, photo.height), new Vector2(0.5f, 0.5f), 100.0f);

            yield return new WaitForSeconds(0.1f);
        }

        alignSquare.GetComponent<Image>().enabled = false;
        background.GetComponent<Image>().enabled = false;
        textAlign.GetComponent<Text>().enabled = false;
    }

    IEnumerator TakePhotos()
    {
        for (numberPhoto = 0; numberPhoto < numberPhotos; numberPhoto++)
        {
            yield return new WaitForEndOfFrame(); 

            photo.SetPixels(webCamTexture.GetPixels());
            photo.Apply();
            // photo = rotateTexture(photo, true);
            image.GetComponent<Image>().sprite = Sprite.Create(photo, new Rect(0.0f, 0.0f, photo.width, photo.height), new Vector2(0.5f, 0.5f), 100.0f);

            //Encode to a PNG
            byte[] bytes = photo.EncodeToPNG();
            //Write out the PNG
            File.WriteAllBytes(Application.persistentDataPath + "/photo" + Random.Range(1,100) + numberPhoto.ToString() + ".png", bytes);
            yield return new WaitForSeconds(textManager.GetComponent<TextManager>().timeBetweenPhotographs);
        }

        image.transform.parent.GetChild(1).GetComponentInChildren<Image>().enabled = true;
        image.transform.parent.GetComponentInChildren<Text>().enabled = true;
        image.transform.parent.GetChild(1).GetChild(2).GetComponent<Text>().enabled = true;
    }

     Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
     {
         Color32[] original = originalTexture.GetPixels32();
         Color32[] rotated = new Color32[original.Length];
         int w = originalTexture.width;
         int h = originalTexture.height;
 
         int iRotated, iOriginal;
 
         for (int j = 0; j < h; ++j)
         {
             for (int i = 0; i < w; ++i)
             {
                 iRotated = (i + 1) * h - j - 1;
                 iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                 rotated[iRotated] = original[iOriginal];
             }
         }
 
         Texture2D rotatedTexture = new Texture2D(h, w);
         rotatedTexture.SetPixels32(rotated);
         rotatedTexture.Apply();
         return rotatedTexture;
     }
}