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
    [SerializeField] float _dashSpeed = 25f;
    [SerializeField] float _dashTime = .2f;

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] SpriteRenderer _afterImage;
    [SerializeField] float _afterImageLifetime;
    [SerializeField] float _timeBetweenAfterImages;
    [SerializeField] Color _afterImageColor;

    [SerializeField] float _waitAfterDashing;

    bool _isOnGround;
    bool _canDoubleJump;
    float _dashCounter;
    float _afterImageCounter;
    float _dashRechargeCounter;
    
    void Update()
    {
        if (_dashRechargeCounter > 0)
        {
            _dashRechargeCounter -= Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Fire2"))
            {
                _dashCounter = _dashTime;
                ShowAfterImage();
            }
        }

        if (_dashCounter > 0)
        {
            _dashCounter = _dashCounter - Time.deltaTime;
            _rigidBody.velocity = new Vector2(_dashSpeed * transform.localScale.x, _rigidBody.velocity.y);

            _afterImageCounter -= Time.deltaTime;
            if (_afterImageCounter <= 0)
            {
                ShowAfterImage();
            }
            _dashRechargeCounter = _waitAfterDashing;
        }
        else
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

    public void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(_afterImage, transform.position, transform.rotation);
        image.sprite = _spriteRenderer.sprite;
        image.transform.localScale = transform.localScale;
        image.color = _afterImageColor;

        Destroy(image.gameObject, _afterImageLifetime);

        _afterImageCounter = _timeBetweenAfterImages;
    }
}
