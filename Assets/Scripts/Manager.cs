using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI inputWidth;
    public TextMeshProUGUI inputHeight;
    public TextMeshProUGUI inputDistanceBetweenWheels;

    public GameObject textManager;
    public GameObject photoManager;
    
    public void Calculate ()
    {
        if (Regex.Replace(inputWidth.text, "[^.0-9]", "") != "" 
        && Regex.Replace(inputHeight.text, "[^.0-9]", "") != "" 
        && Regex.Replace(inputDistanceBetweenWheels.text, "[^.0-9]", "") != "")
        {
            textManager.GetComponent<TextManager>().start = true;
        }
    }
}
