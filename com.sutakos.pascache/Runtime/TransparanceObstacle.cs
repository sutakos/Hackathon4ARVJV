using UnityEngine;
using System.Collections.Generic;

public class TransparanceObstacle : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> listeDeCible = new List<MonoBehaviour>();
    [SerializeField] private float DegreTransparance = 0.3f;

    private void Update()
    {
        for (int i = 0; i < listeDeCible.Count; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, listeDeCible[i].transform.position - transform.position, out hit, 1f))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    Color color = hit.collider.GetComponent<Renderer>().material.color;
                    color.a = DegreTransparance; // Set alpha to the specified degree
                    hit.collider.GetComponent<Renderer>().material.color = color;
                }
            }
        }
    }
}