using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidBody;
    [SerializeField] Transform _groundPoint;
    [SerializeField] float _moveSpeed = 8;
    [SerializeField] float _jumpForce = 20;
    [SerializeField] LayerMask _whatIsGround;
    [SerializeField] float _groundCheckRadius = .2f;
    [SerializeField] Animator _animator;

    bool _isOnGround;
    

    void Start()
    {
        
    }

    void Update()
    {
        _rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, _rigidBody.velocity.y);

        _isOnGround = Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _whatIsGround);
        
        if (Input.GetButtonDown("Jump") && _isOnGround)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
        }

        _animator.SetBool("isOnGround", _isOnGround);
        _animator.SetFloat("speed", Mathf.Abs(_rigidBody.velocity.x));
    }
}
