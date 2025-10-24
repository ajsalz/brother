using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float lifetime = 5f;       
    public float fadeTime = 1f;       

    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps != null)
            ps.Play();

        StartCoroutine(LifeCycle());
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifetime - fadeTime);

        if (ps != null)
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        yield return new WaitForSeconds(fadeTime);

        Destroy(gameObject);
    }
}
