using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public CinemachineBrain cinemachineBrain;
    public GameObject camSwapTo;
    public GameObject camRevertTo;
    public GameObject camDefault;
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //if trigger with player print active camera
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject currentCam = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject;
            if (currentCam.name == camRevertTo.name ) {
                camSwapTo.SetActive(true);
            }
            else
            {
                camRevertTo.SetActive(true);
            }
                currentCam.SetActive(false);
        }
    }
}
