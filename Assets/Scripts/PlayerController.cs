using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidBody;
    [SerializeField] Transform _groundPoint;
    [SerializeField] LayerMask _whatIsGround;
    [SerializeField] Animator _animator;

    [SerializeField] float _groundCheckRadius = .2f;
    [SerializeField] float _moveSpeed = 8;
    [SerializeField] float _jumpForce = 20;

    bool _isOnGround;
    
    void Update()
    {
        _rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, _rigidBody.velocity.y);

        if (_rigidBody.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_rigidBody.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }

        _isOnGround = Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _whatIsGround);
        
        if (Input.GetButtonDown("Jump") && _isOnGround)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
        }

        _animator.SetBool("isOnGround", _isOnGround);
        _animator.SetFloat("speed", Mathf.Abs(_rigidBody.velocity.x));
    }
}
