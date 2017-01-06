using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour {

    public float lifetime = 10.0f; // Seconds to timeout
    public Color startColor;
    public Color endColor;

    private Gradient g;
    GradientColorKey[] gck;
    GradientAlphaKey[] gak;

    private float start;


    private void Start()
    {
        start = Time.time;

        g = new Gradient();
        gck = new GradientColorKey[2];
        gak = new GradientAlphaKey[2];

        gck[0].color = startColor;
        gck[0].time = 0.0F;
        gck[1].color = endColor;
        gck[1].time = 1.0F;

        gak[0].alpha = 1.0F;
        gak[0].time = 0.0F;
        gak[1].alpha = 0.0F;
        gak[1].time = 1.0F;

        g.SetKeys(gck, gak);
    }
    
    private void Update()
    {
        float increment = (Time.time - start) / lifetime;
        if(increment < 1)
        {
            GetComponent<Light>().color = g.Evaluate(increment);
        }
        else
        {
            start = Time.time; // Loop forever

            // Swap colors for smooth gradient
            Color e = endColor;
            endColor = startColor;
            startColor = e;

            gck[0].color = startColor;
            gck[1].color = endColor;

            g.SetKeys(gck, gak);
        }
    }
}
