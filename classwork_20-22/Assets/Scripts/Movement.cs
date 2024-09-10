using UnityEngine;


public class Movement : MonoBehaviour
{
    private const float _TIMER = 0.5f;

    [SerializeField] private int _direction;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _joystick, _button;

    private bool _pressed = false;

    private bool _doubleClick = false;
    private bool _startTimer = false;
    private float _clock;

    private System.Collections.Generic.Queue<Vector2> _commands =
        new System.Collections.Generic.Queue<Vector2>();

    private void Start()
    {
        
    }
    private void Update()
    {
        Inputs();
    }
    private void FixedUpdate()
    {
        if (_startTimer)
            _clock += Time.deltaTime;
    }
    private void Inputs()
    {
        
        // Первая секунда после нажатия
        if (Input.touchCount > 0)
        {
            SetVisibility(true);

            Touch touch = Input.GetTouch(0);

            Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);

            if (!_pressed)
            {
                _joystick.transform.position = pos;
                _pressed = true;
            }

            DoubleClickOnPress();

            _button.transform.position = pos;
        }

        // В конце нажатия
        if (Input.touchCount == 0 && _pressed)
        {
            DoubleClickOnRelease();

            SetVisibility(false);

            Vector2 joystickPos = _joystick.transform.position;
            Vector2 buttonPos = _button.transform.position;

            float HorizontalDist = Mathf.Abs(joystickPos.x - buttonPos.x);
            float VerticalDist = Mathf.Abs(joystickPos.y - buttonPos.y);

            if (HorizontalDist > VerticalDist)
            {
                //Debug.Log($"[buttonX: {(int)(buttonPos.x / 2) * 10} : joystickX: {(int)(joystickPos.x / 2) * 10}]");
                //Debug.Log($"[buttonX: {buttonPos.x} : joystickX: {joystickPos.x}]");

                if ((int)(buttonPos.x / 2) * 10 > (int)(joystickPos.x / 2) * 10)
                    Move(Vector3.right);
                if ((int)(buttonPos.x / 2) * 10 < (int)(joystickPos.x / 2) * 10)
                    Move(Vector3.left);
            } else
            {
                if ((int)(buttonPos.y / 2) * 10 > (int)(joystickPos.y / 2) * 10)
                    Move(Vector3.up);
                if ((int)(buttonPos.y / 2) * 10 < (int)(joystickPos.y / 2) * 10)
                    Move(Vector3.down);
            }

            _button.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            _joystick.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            _pressed = false;
        }
    }

    private void DoubleClickOnPress()
    {
        if (!_doubleClick)
            _doubleClick = true;
    }
    private void DoubleClickOnRelease()
    {
        if (_doubleClick && !_startTimer)
        {
            _startTimer = true;
        }
        else if (_doubleClick && _startTimer)
        {
            if (_clock < _TIMER)
            {
                if (_commands.Count != 0)
                    Move(_commands.Dequeue(), true);

                Debug.Log("double click");
                _doubleClick = false;
                _startTimer = false;
                _clock = 0;
            }
            else
            {
                Debug.Log("no double click");
                _doubleClick = false;
                _startTimer = false;
                _clock = 0;
            }
        }
    }
    private void SetVisibility(bool status)
    {
        _button.SetActive(status);
        _joystick.SetActive(status);
    }

    private void Move(Vector2 direction, bool isRepeat = false)
    {
        gameObject.transform.position += (Vector3)direction;
        _commands.Enqueue(direction);
    }
}
