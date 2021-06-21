using UnityEngine;

public class VRInit : MonoBehaviour
{
    Transform VRcharacter;

    void Awake()
    {
        // Choose between standard and VR first person controller
        Destroy(MainMenu.IsVR ? GameObject.Find("FPSController") : GameObject.Find("VR character"));
        if (MainMenu.IsVR)
        {
            VRcharacter = GameObject.Find("VR character").transform.GetChild(0);
            VRcharacter.gameObject.SetActive(true);
        }
    }

    void Start()
    {
        if (MainMenu.IsVR)
        {
            // Reset cameras to make them work
            var rCamera = VRcharacter.GetChild(0).GetChild(0).Find("RCamera").gameObject;
            var lCamera = VRcharacter.GetChild(0).GetChild(0).Find("LCamera").gameObject;
            rCamera.SetActive(false);
            lCamera.SetActive(false);
            rCamera.SetActive(true);
            lCamera.SetActive(true);
        }
    }
}