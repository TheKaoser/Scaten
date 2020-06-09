using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public Text radius;
    public Text angle;
    public GameObject countdown;

    public TextMeshProUGUI inputTimeLap;

    int width;
    int height;
    int distanceBetweenWheels;
    float timeLap;
    
    public float timeBetweenPhotographs;
    float time = 6;
    bool startCountdown;
    public bool start;

    public GameObject photoManager;
    public Canvas results;
    public Canvas inputs;

    int auxAngle;

    void Update()
    {
        if (start)
        {
            results.enabled = true;
            inputs.enabled = false;
            photoManager.GetComponent<PhotoManager>().process = 1;

            width = int.Parse(Regex.Replace(GameObject.Find("Manager").GetComponent<Manager>().inputWidth.text, "[^.0-9]", ""));
            height = int.Parse(Regex.Replace(GameObject.Find("Manager").GetComponent<Manager>().inputHeight.text, "[^.0-9]", ""));

            if (width > height)
                radius.text = 3.75f * width / 4.5f * 3 / 2 + width + "";
            else
                radius.text = 3.75f * height / 4.5f * 3 / 2 + width + "";

            distanceBetweenWheels = int.Parse(Regex.Replace(GameObject.Find("Manager").GetComponent<Manager>().inputDistanceBetweenWheels.text, "[^.0-9]", ""));

            angle.text = (int)((int)(distanceBetweenWheels * 360 / (2 * Mathf.PI * float.Parse(radius.text)) / 5) * 5) + "";

            start = false;
        }

        if (startCountdown)
        {
            photoManager.GetComponent<PhotoManager>().process = 3;
            time -= Time.deltaTime;
            countdown.GetComponentInChildren<Text>().text = (int)time + "";
            if ( time <= 0 )
            {
                startCountdown = false;
                timeLap = float.Parse(Regex.Replace(inputTimeLap.text, "[^.0-9]", ""));
                timeBetweenPhotographs = timeLap / 50f;
                photoManager.GetComponent<PhotoManager>().process = 4;
                results.enabled = false;
            }
        }
    }

    public void Countdown ()
    {
        if (Regex.Replace(inputTimeLap.text, "[^.0-9]", "") != "")
        {
            countdown.GetComponentInChildren<Image>().enabled = true;
            countdown.GetComponentInChildren<Text>().enabled = true;

            startCountdown = true;
        }

    }
}
