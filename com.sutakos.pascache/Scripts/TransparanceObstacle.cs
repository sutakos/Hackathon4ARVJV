using UnityEngine;

public class TransparanceObstacle : MonoBehaviour
{
    //listeDeCible List<MonoBehaviour>;


    private void init()
    {
        
    }
    private void Update()
    {
        //for i in listeDeCible()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, i.transform.position-transform.position, out hit, 1f))
            {
                if (hit.collider.CompareTag())
                {
                    Color color = hit.collider.GetComponent<Renderer>().material.color;
                    color.a = 0.3f; // Set alpha to 30%
                    hit.collider.GetComponent<Renderer>().material.color = color;
                }
            }
        }
    }
}