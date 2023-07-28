using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] BoxCollider2D _boundsBox;

    PlayerController _player;
    float _halfHeight, _halfWidth;

    void Start()
    {
        _player = FindFirstObjectByType<PlayerController>();
        // Half the height of the camera can be determined by the ortographicSize, to get to the halfWidth we have to look at the aspect ratio.
        _halfHeight = Camera.main.orthographicSize;
        _halfWidth = _halfHeight * Camera.main.aspect;
    }

    void Update()
    {
        if ( _player != null )
        {
            transform.position = new Vector3(
                Mathf.Clamp(_player.transform.position.x, _boundsBox.bounds.min.x + _halfWidth, _boundsBox.bounds.max.x - _halfWidth), 
                Mathf.Clamp(_player.transform.position.y, _boundsBox.bounds.min.y + _halfHeight, _boundsBox.bounds.max.y - _halfHeight), 
                transform.position.z);
        }
    }
}
