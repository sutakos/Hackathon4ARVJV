using UnityEngine;
using System.Collections.Generic;

public class ChangeColorMaterial: MonoBehaviour
{
    /*
     script attacher a l'objet qui va changer de couleur pour un temps defini et de couleur defini.
     active la fonction a chaque saut du joueur.
    */
    [SerializeField] private Color colorToChangeTo = Color.red; // couleur a changer
    [SerializeField] private float timeToChangeColor = 1f; // temps pour changer de couleur
    private Color originalColor; // couleur originale de l'objet

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color; // recuperer la couleur originale de l'objet
    }

    public void ChangeColor()
    {
        GetComponent<Renderer>().material.color = colorToChangeTo; // changer la couleur de l'objet
        Invoke("ResetColor", timeToChangeColor); // appeler la fonction ResetColor apres le temps defini
    }

    public static void ChangeColorOfGround(CharacterController characterController)
    {
        if (characterController == null)
        {
            Debug.LogError("A CharacterController is required to find the ground object.");
            return;
        }

        float rayDistance = characterController.bounds.extents.y
                          + characterController.skinWidth
                          + 0.1f;

        if (!Physics.Raycast(
                characterController.bounds.center,
                Vector3.down,
                out RaycastHit hit,
                rayDistance,
                Physics.DefaultRaycastLayers,
                QueryTriggerInteraction.Ignore))
        {
            Debug.LogError("Could not find the ground object under the player.", characterController);
            return;
        }

        ChangeColorMaterial colorMaterial = hit.collider.GetComponentInParent<ChangeColorMaterial>();
        if (colorMaterial == null)
        {
            Debug.LogError(
                $"Ground object '{hit.collider.name}' has no ChangeColorMaterial component.",
                hit.collider);
            return;
        }

        colorMaterial.ChangeColor();
    }

    private void ResetColor()
    {
        GetComponent<Renderer>().material.color = originalColor; // restaurer la couleur originale de l'objet
    }
}