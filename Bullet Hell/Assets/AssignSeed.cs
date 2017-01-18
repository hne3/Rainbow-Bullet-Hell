using UnityEngine;

public class AssignSeed : MonoBehaviour {

    private void Awake()
    {
        ParticleSystem p = GetComponent<ParticleSystem>();
        p.randomSeed = (uint)Random.Range(0, int.MaxValue);
        p.Simulate(0, true, true);
        p.Play();
    }
}
