using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class timeTeller : MonoBehaviour
{
    public GameObject timeTextObject;
    private string url = "http://worldtimeapi.org/api/timezone/";
    public string location = "Europe/Paris";
    public bool is12hour = false;
    CultureInfo provider = CultureInfo.InvariantCulture;

    public class TimeObject {
        public string datetime;
        public string utc_datetime;
    }

    void Start()
    {
        InvokeRepeating("fetchText", 0f, 10f);
    }

    void fetchText() {
        StartCoroutine(GetText());
    }
    
    public IEnumerator GetText() {
        using (UnityWebRequest request = UnityWebRequest.Get(url + location))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError) {
                Debug.LogError(request.error);
            } else {
                Debug.Log("Successfully downloaded time info");
                var text = request.downloadHandler.text;
                TimeObject time = JsonUtility.FromJson<TimeObject>(text);
                if(is12hour) {
                    var hours = int.Parse(time.datetime.Substring(11, 2));
                    var minutes = int.Parse(time.datetime.Substring(14, 2));
                    var isAMorPM = " AM";
                    if(hours > 12) {
                        hours -= 12;
                        isAMorPM = " PM";
                    } else if(hours == 0) {
                        hours += 12;
                        isAMorPM = " AM";
                    } else {
                        isAMorPM = " AM";
                    }
                    timeTextObject.GetComponent<TextMeshPro>().text = hours.ToString() + ":" + minutes.ToString() + isAMorPM;
                } else {
                    timeTextObject.GetComponent<TextMeshPro>().text = time.datetime.Substring(11, 5);
                }
            }
        }
    }
}