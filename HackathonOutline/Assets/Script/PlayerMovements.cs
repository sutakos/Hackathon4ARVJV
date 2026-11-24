using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    public float vitesse = 5f;
    private Vector2 _inputMouvement;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue valeur)
    {
        _inputMouvement = valeur.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
        
        Vector3 direction = new Vector3(_inputMouvement.x, 0f, _inputMouvement.y);
        _rb.MovePosition(_rb.position + direction * vitesse * Time.fixedDeltaTime);
    }
}