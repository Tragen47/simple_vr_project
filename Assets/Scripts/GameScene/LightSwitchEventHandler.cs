using UnityEngine;

public class LightSwitchEventHandler : MonoBehaviour
{
    // Turn on/off lights when switched
    public void SwitchLight(Transform @this, bool isOn)
        => GameObject.Find("Chandeliers").transform.Find(@this.name).GetComponent<Light>().enabled = isOn;
}