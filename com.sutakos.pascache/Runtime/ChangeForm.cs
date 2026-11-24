using System.Collections.Generic;
using UnityEngine;

public class changeForm : MonoBehaviour
{
    /*
        script attacher a un objet qui va changer de taille pour un temps defini et de taille defini.
    */
    [SerializeField] private Vector3 sizeToChangeTo = new Vector3(2f, 2f, 2f); // taille a changer
    [SerializeField] private float timeToChangeSize = 1f; // temps pour changer de taille
    private Vector3 originalSize; // taille originale de l'objet

    private void Start()
    {
        originalSize = transform.localScale; // recuperer la taille originale de l'objet
    }
    public void ChangeSize()
    {
        // changer la taille de l'objet
        float x = transform.localScale.x * sizeToChangeTo[0];
        float y = transform.localScale.y * sizeToChangeTo[1];
        float z = transform.localScale.z * sizeToChangeTo[2];
        transform.localScale = new Vector3(x, y, z);

        Invoke("ResetSize", timeToChangeSize); // appeler la fonction ResetSize apres le temps defini
    }
    private void ResetSize()
    {
        transform.localScale = originalSize; // restaurer la taille originale de l'objet
    }
}