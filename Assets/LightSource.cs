using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public GameObject cube;
    public Light LightSource1;
    public Light LightSource2;
    public Light LightSource3;
    public Light LightSource4;
    public Light mainLight;
    public bool IsUpsideDown = false;
    // Start is called before the first frame update
    void Start()
    {
        mainLight.enabled = true;
        LightSource1.enabled = false;
        LightSource2.enabled = false;
        LightSource3.enabled = false;
        LightSource4.enabled = false;
        InvokeRepeating("ChangeLightSource", 0f, 5f);
    }

    void DoDelayAction(float delayTime) 
    {
        StartCoroutine(DelayAction(delayTime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeLightSource() {
        var zRotation = cube.transform.localEulerAngles.z;
        Debug.Log(zRotation);
        if (zRotation > 150) {
            IsUpsideDown = !IsUpsideDown;
            Debug.Log("Turning Upside Down : New Light Source");
            mainLight.enabled = !IsUpsideDown;
            LightSource1.enabled = IsUpsideDown;
            LightSource2.enabled = IsUpsideDown;
            LightSource3.enabled = IsUpsideDown;
            LightSource4.enabled = IsUpsideDown;
        }
    }
}
