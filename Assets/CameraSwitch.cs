using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Objects for 3 cameras (3rd person, topdown, front)
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;

    // Update is called once per frame
    void Update()
    {
        // If number "1" is pressed, Cam1 is only active
        if(Input.GetButtonDown("1Key"))
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
            cam3.SetActive(false);
        }

        // If number "2" is pressed, Cam2 is only active
        if(Input.GetButtonDown("2Key"))
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            cam3.SetActive(false);
        }

        // If number "3" is pressed, Cam3 is only active
        if(Input.GetButtonDown("3Key"))
        {
            cam1.SetActive(false);
            cam2.SetActive(false);
            cam3.SetActive(true);
        }
    }
}
