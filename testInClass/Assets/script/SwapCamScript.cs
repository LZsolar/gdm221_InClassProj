using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCamScript : MonoBehaviour
{
    public GameObject camera1, camera2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
    }
}
