using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController _player;

    void Start()
    {
        _player = FindFirstObjectByType<PlayerController>();
    }

    void Update()
    {
        if ( _player != null )
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
        }
    }
}
