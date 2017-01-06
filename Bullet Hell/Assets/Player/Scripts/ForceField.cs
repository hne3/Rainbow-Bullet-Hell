using System.Collections;
using UnityEngine;

public class ForceField : MonoBehaviour {

    [SerializeField]
    private Material material;

    public int NumCentersAllowed = 30;
    public float ForceFieldGrowthRate = 1.0f;
    public float TimeBetweenHitRadiiChange = 0.01f;
    public float DefaultRadius = 1.0f;

    public Vector3 DefaultPos = new Vector3(1000, 1000, 1000);
    public Vector3 FieldScale = new Vector3(1, 1, 1);

    // Variables for centers and radii
    private Vector4[] centers;
    private float[] radii;

    private WaitForEndOfFrame frame;
    private WaitForSeconds hitChange;
    private int currentCenter = 0;

    private void Start()
    {
        // Initialize centers and radii array
        DefaultPos = new Vector4(1000, 1000, 1000, 0);

        centers = new Vector4[NumCentersAllowed];
        radii = new float[NumCentersAllowed];

        for(int i = 0; i < NumCentersAllowed; i++)
        {
            centers[i] = DefaultPos;
            radii[i] = DefaultRadius;
        }

        material.SetVectorArray("_Centers", centers);
        material.SetFloatArray("_Radii", radii);

        hitChange = new WaitForSeconds(TimeBetweenHitRadiiChange);
        frame = new WaitForEndOfFrame();
        StartCoroutine(AnimateField());
    }

    private IEnumerator AnimateField()
    {
        // Grow force field animation
        while (transform.localScale.x < FieldScale.x ||
            transform.localScale.y < FieldScale.y ||
            transform.localScale.z < FieldScale.z)
        {
            Vector3 newScale = transform.localScale;
            float multiplier = Time.deltaTime * ForceFieldGrowthRate;
            if (transform.localScale.x < FieldScale.x)
            {
                newScale.x += FieldScale.x * multiplier;
            }
            if (transform.localScale.y < FieldScale.y)
            {
                newScale.y += FieldScale.y * multiplier;
            }
            if (transform.localScale.z < FieldScale.z)
            {
                newScale.z += FieldScale.z * multiplier;
            }
            transform.localScale = newScale;
            yield return frame;
        }
    }
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Bullet")
        {
            // Recycle oldest point if we need to
            if(currentCenter >= NumCentersAllowed)
            {
                currentCenter = 0;
                Reset(currentCenter);
            }
            // Set current contact point, fade out, and incremenmt center point
            centers[currentCenter] = c.contacts[0].point;
            material.SetVectorArray("_Centers", centers);
            StartCoroutine(FadeOut(DefaultRadius, c.gameObject, currentCenter));
            currentCenter++;
        }
    }

    private IEnumerator FadeOut(float r, GameObject bullet, int index)
    {
        bullet.SetActive(false);
        while (r > 0.01)
        {
            // Fade out
            r = r * 0.9f;
            radii[index] = r;
            material.SetFloatArray("_Radii", radii);
            yield return hitChange;
        }
        Reset(index);
    }

    private void Reset(int index)
    {
        radii[index] = DefaultRadius;
        centers[index] = DefaultPos;
        material.SetFloatArray("_Radii", radii);
        material.SetVectorArray("_Centers", centers);
    }
}
