using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rigidBody;
    [SerializeField] Transform _groundPoint;
    [SerializeField] LayerMask _whatIsGround;
    [SerializeField] Animator _animator;
    [SerializeField] BulletController _shotToFire;
    [SerializeField] Transform _shotPoint;

    [SerializeField] float _groundCheckRadius = .2f;
    [SerializeField] float _moveSpeed = 8;
    [SerializeField] float _jumpForce = 20;

    bool _isOnGround;
    bool _canDoubleJump;
    
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
        
        if (Input.GetButtonDown("Jump") && (_isOnGround || _canDoubleJump))
        {
            if (_isOnGround)
            {
                _canDoubleJump = true;
            }
            else
            {
                _canDoubleJump = false;
                _animator.SetTrigger("doubleJump");
            }
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(_shotToFire, _shotPoint.position, _shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0f);
            _animator.SetTrigger("shotFired");
        }

        _animator.SetBool("isOnGround", _isOnGround);
        _animator.SetFloat("speed", Mathf.Abs(_rigidBody.velocity.x));
    }
}
