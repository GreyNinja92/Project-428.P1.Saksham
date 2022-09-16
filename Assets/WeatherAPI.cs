using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;

public class WeatherAPI : MonoBehaviour
{
    public float lat;
    public float lon;
    public GameObject weatherText;
    public bool InMetricUnits;

    void Start()
    {
       InvokeRepeating("fetchText", 0f, 600f);
    }

   void fetchText()
    {
        StartCoroutine(GetRequest());
    }

    public IEnumerator GetRequest()
    {
        string weatherApiUrl = "https://api.openweathermap.org/data/2.5/weather?lat=" + lat.ToString() + "&lon=" + lon.ToString() + "&APPID=5e45d856ae56685e52492811ced2fe8c";
        if(InMetricUnits) {
            weatherApiUrl += "&units=metric";
        } else {
            weatherApiUrl += "&units=imperial";
        }
        
        using (UnityWebRequest request = UnityWebRequest.Get(weatherApiUrl))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);         
            }
            else
            {
                Debug.Log("Successfully downloaded weather info");
                var text = request.downloadHandler.text;
                var weatherObj = JSON.Parse(text);
                if(InMetricUnits) {
                    weatherText.GetComponent<TextMeshPro>().text = weatherObj["main"]["temp"].ToString() + " °C";
                } else {
                    weatherText.GetComponent<TextMeshPro>().text = weatherObj["main"]["temp"].ToString() + " °F";
                }
            }
        }
    }
}