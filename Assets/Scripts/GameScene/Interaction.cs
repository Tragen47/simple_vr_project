using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class Interaction : CanvasFactory
{
    List<Transform> ChildObjects;
    public UnityEvent<Transform, bool> OnSwitchedEvent;
    public SwitchPanel[] SwitchPanels;
    public CanvasFactory CanvasFactory;
    GameObject Canvas;
    List<Transform> Panels = new List<Transform>();

    static Transform LookedAtObject = null; // A reference to the object the player is looking at

    public SwitchPanel SwitchPanel
    {
        get => default;
        set
        {
        }
    }

    void Start()
    {
        Canvas = CanvasFactory.GetOrCreateCanvas(SwitchPanels);
        ChildObjects = transform.GetAllChildren();

        foreach (Transform panel in Canvas.transform)
            Panels.Add(panel);
        Canvas.SetActive(false); // Make canvas invisible unless a player didn't approach to any interactable object
    }

    // Determines whether a player is looking at the object
    bool GetLookedAtObject()
    {
        var cameraCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));
        if (Physics.Raycast(cameraCenter, Camera.main.transform.forward, out RaycastHit hit, 3))
            return transform == hit.transform || ChildObjects.Any(@object => @object == hit.transform);
        return false;
    }

    void Update()
    {
        if (GetLookedAtObject())
        {
            if (MainMenu.IsVR)
            {
                // Place world canvases at objects' positions and make them rotate towards the player
                Canvas.transform.position = transform.GetComponent<Renderer>().bounds.center;
                Canvas.transform.LookAt(Camera.main.transform);
                Canvas.transform.forward = -Canvas.transform.forward;
            }

            // Make canvas with panels visible
            Canvas.SetActive(true);

            // Pass the reference to the current looked object
            LookedAtObject = transform;
        }
        else if (LookedAtObject == transform)
        {
            // Make canvas with panels invisible
            Canvas.SetActive(false);

            // Clear the reference to the current looked object
            LookedAtObject = null;
        }

        // If the looked at object is the current one
        if (Canvas.activeInHierarchy && LookedAtObject == transform)
            HandleInput();
    }

    void HandleInput()
    {
        for (int i = 0; i < Panels.Count; i++)
        {
            bool isOn = ChildObjects[i].GetComponent<Animator>().GetBool(SwitchPanel.AnimatorBoolName);
            Text PanelText = Panels[i].transform.Find("Text").GetComponent<Text>();

            // Make the panel which set key is pressed yellow during the button pressing
            if (Input.GetKeyDown(SwitchPanels[i].SwitchCode))
                Panels[i].GetComponent<Image>().color = Color.yellow;

            // Switch the state of the object and trigger animation
            if (Input.GetKeyUp(SwitchPanels[i].SwitchCode))
            {
                isOn = !isOn;
                ChildObjects[i].GetComponent<Animator>().SetBool(SwitchPanel.AnimatorBoolName, isOn);
                OnSwitchedEvent?.Invoke(transform, isOn);
                Panels[i].GetComponent<Image>().color = Color.black;
            }
            // Switch the panel text or keep it the same if the button was not pressed
            PanelText.text = (isOn) ? SwitchPanels[i].SwitchOff : SwitchPanels[i].SwitchOn;
            PanelText.text += $" ({SwitchPanels[i].SwitchCode})";
        }
    }
}