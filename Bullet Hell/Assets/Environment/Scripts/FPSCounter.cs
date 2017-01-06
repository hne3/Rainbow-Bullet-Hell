using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

    public Text text;
    
	private void Start ()
    {
	    if(!text)
        {
            text = GetComponent<Text>();
        }
        InvokeRepeating("FPSUpdate", 0, 0.5f);
	}
	
	private void FPSUpdate ()
    {
        text.text = "FPS: " + (1 / Time.deltaTime).ToString("0.00");
	}
}
