using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public int pointsCount;
    public float MaxRadius;
    public float speed;
    public float startWidth;
    public float force;

    private LineRenderer lineRenderer;
    //public ParticleSystem Explosion;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = pointsCount + 1;
    }

    public IEnumerator BlastWave()
    {
        float currentRadius = 0f;
        
        //Explosion.Play();

        while(currentRadius < MaxRadius)
        {
            currentRadius += Time.deltaTime * speed; // speed of circle spread
            Draw(currentRadius);
            Damage(currentRadius);
            yield return null;
        }
    }

    private void Damage(float currentRadius)
    {
        Collider[] hittingObjects = Physics.OverlapSphere(transform.position, currentRadius); // detects objects with rigidbodies and adds them too an array

        for(int i = 0; i < hittingObjects.Length; i++)
        {
            Rigidbody rb = hittingObjects[i].GetComponent<Rigidbody>();

            if (!rb)
                continue;

            Vector3 direction = (hittingObjects[i].transform.position - transform.position).normalized;
            rb.AddForce(direction * force, ForceMode.Impulse); // pushes the objects away from the point of origin
        }
    }

    private void Draw(float currentRadius) // draws the circle itself
    {
        float angleBetweenPoints = 360f / pointsCount; // 360 as its a circle

        for(int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f); // complex math stuff
            Vector3 position = direction * currentRadius;

            lineRenderer.SetPosition(i, position); // increases in size from origin
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / MaxRadius); // makes it thinner as it goes on
    }
}
