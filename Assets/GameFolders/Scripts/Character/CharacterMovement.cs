using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float forwardSpeed;
    
    private InputManagers _inputManager;
    private float _difference;
    private float _firstTouchX;
    private float _lastTouchX;

    private Rigidbody _rigidBody;
    

    private void Awake()
    {
        _inputManager = new InputManagers();
        _rigidBody = GetComponentInChildren<Rigidbody>();
    }

    public void Movement()
    {
        if (!GameManager.instance.isPlaying) return;
        var moveVector = new Vector3(0, 0, forwardSpeed * Time.deltaTime);
        if (_inputManager.MouseDown)
        {
            _firstTouchX = Input.mousePosition.x;
        }
        else if (_inputManager.MouseHold)
        {
            _lastTouchX = Input.mousePosition.x;
            _difference = _lastTouchX - _firstTouchX;
            moveVector += new Vector3(_difference * Time.deltaTime, 0, 0);
            _firstTouchX = _lastTouchX;           
        }
        //_rigidBody.velocity += moveVector;
        
        transform.position += moveVector;
    }
    public void ClampX()
    {
        if (!GameManager.instance.isPlaying) return;
        if (_inputManager.MouseHold)
        {
            var position = transform.position;
            position = new Vector3(Mathf.Clamp(position.x, -4.3f, 4.3f), position.y, position.z);
            transform.position = position;
        }
    }
}
