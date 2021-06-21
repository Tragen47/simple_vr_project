using UnityEngine;
using System.Collections.Generic;

public class CanvasFactory : MonoBehaviour
{
    public GameObject CanvasLayout;
    public GameObject Panel;
    Dictionary<int, GameObject> CreatedCanvases = new Dictionary<int, GameObject>();

    public GameObject GetOrCreateCanvas(SwitchPanel[] switchPanels)
    {
        // Return canvas with the particular number of panels if the one is created
        if (CreatedCanvases.TryGetValue(switchPanels.Length, out GameObject canvas))
            return canvas;

        // Else create a new one
        canvas = Instantiate(CanvasLayout);

        // Instantiate panels and set the canvas as their parent
        for (int i = 0; i < switchPanels.Length; i++)
            Instantiate(Panel).transform.SetParent(canvas.transform);
        if (MainMenu.IsVR)
        {
            // Set the canvas render mode to world space
            canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("VR_UI_dummyCamera").GetComponent<Camera>();
            canvas.transform.localScale = new Vector3(0.003f, 0.003f, 0.003f);
        }
        CreatedCanvases.Add(switchPanels.Length, canvas);

        return canvas;
    }
}
