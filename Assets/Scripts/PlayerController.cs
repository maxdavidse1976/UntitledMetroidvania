using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidBody;
    [SerializeField] float _moveSpeed = 8;
    [SerializeField] float _jumpForce = 20;

    void Start()
    {
        
    }

    void Update()
    {
        _rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, _rigidBody.velocity.y);
    }
}
