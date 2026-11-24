using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    public float vitesse = 5f;
    private Vector2 _inputMouvement;

    public void OnMove(InputValue valeur)
    {
        _inputMouvement = valeur.Get<Vector2>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(_inputMouvement.x, 0f, _inputMouvement.y);
        transform.Translate(direction * vitesse * Time.deltaTime, Space.World);
    }
}