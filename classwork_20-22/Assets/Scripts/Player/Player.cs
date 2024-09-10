using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [Header("Joystick Components:")]
    [SerializeField] private GameObject _innerCircle;
    [SerializeField] private GameObject _outerCircle;

    [Header("Movement Components:")]
    [SerializeField] private Rigidbody2D _playerRB;

    [Header("Animation Components:")]
    [SerializeField] private Animator _animator;

    [Header("Button Components:")]
    [SerializeField] private Button _recordButton;
    [SerializeField] private Button _rewindButton;


    private UserInput _inputs;
    private Joystick _joystick;
    private JoystickDraw _joystickDraw;
    private PlayerMovement _movement;
    private PlayerAnimation _animation;
    private RewindMoves _rewindMoves;

    private bool _isRecording = false;
    private bool _isRewinding = false;

    private bool _isPlaying = false;
    private float _time = 0;
    private float _timer = 0;
    private float _gameTime = 0;
    private void Start()
    {
        _inputs = new UserInput();
        _joystick = new Joystick();
        _joystickDraw = new JoystickDraw(_joystick, _innerCircle, _outerCircle);

        _rewindMoves = new RewindMoves();

        _inputs.OnTouchBegin += _joystick.SetOrigin;
        _inputs.OnTouchBegin += _joystick.SetDirection;
        _inputs.OnTouchMove += _joystick.SetDirection;
        _inputs.OnTouchEnd += _joystick.ResetDirection;

        _inputs.OnTouchBegin += _joystickDraw.ShowJoystick;
        _inputs.OnTouchMove += _joystickDraw.Draw;
        _inputs.OnTouchEnd += _joystickDraw.HideJoystick;

        _movement = new PlayerMovement(_playerRB);
        _animation = new PlayerAnimation(_animator);

        ChangeColor(_recordButton, true);
        ChangeColor(_rewindButton, true);
    }
    private void OnDestroy()
    {
        _inputs.OnTouchBegin -= _joystick.SetOrigin;
        _inputs.OnTouchMove -= _joystick.SetDirection;
        _inputs.OnTouchEnd -= _joystick.ResetDirection;

        _inputs.OnTouchBegin -= _rewindMoves.CreateMove;
        _inputs.OnTouchMove -= _rewindMoves.AddDirectionToMove;

        _inputs.OnTouchBegin -= _joystickDraw.ShowJoystick;
        _inputs.OnTouchMove -= _joystickDraw.Draw;
        _inputs.OnTouchEnd -= _joystickDraw.HideJoystick;
    }
    private void Update()
    {
        //Debug.Log("Delta Time: " + Time.deltaTime);

        if (_isRewinding == false)
        {
            _inputs.DetectMovementInput();
            _movement.MoveByDirection((_joystick.Direction - _joystick.OriginPos) / 100.0f);
        }

        _animation.SetAnimByDirection(_playerRB.velocity);
    }
    public void OnRecordPressed()
    {
        if (_isRecording == false && _isRewinding == false)
        {
            ChangeActions(_isRecording);
            ChangeColor(_recordButton, _isRecording);

            _isRecording = true;
        }
        else if (_isRecording == false && _isRewinding == true)
        {
        }
        else
        {
            ChangeActions(_isRecording);
            ChangeColor(_recordButton, _isRecording);

            _isRecording = false;
        }
    }
    public void OnRewindPressed()
    {
        if (_isRewinding == false)
        {
            if (_isRecording == true)
                OnRecordPressed();

            ChangeColor(_rewindButton, _isRewinding);
            _isRewinding = true;

            RewindPlay();
        }
        else
        {
            ChangeColor(_rewindButton, _isRewinding);
            _isRewinding = false;
        }
    }
    private void ChangeColor(Button button, bool isActive = false)
    {
        if (isActive)
        {
            button.GetComponent<Image>().color = Color.white;
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
        else
        {
            button.GetComponent<Image>().color = Color.red;
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }
    private void ChangeActions(bool isRecording = false)
    {
        if (isRecording)
        {
            _inputs.OnTouchBegin -= _rewindMoves.CreateMove;
            _inputs.OnTouchMove -= _rewindMoves.AddDirectionToMove;
        }
        else
        {
            _rewindMoves.Reset();
            _inputs.OnTouchBegin += _rewindMoves.CreateMove;
            _inputs.OnTouchMove += _rewindMoves.AddDirectionToMove;
        }
    }
    private void RewindPlay()
    {
        StartCoroutine(ApplyDirectionForTime());       
    }
    private IEnumerator ApplyDirectionForTime()
    {
        while (_rewindMoves.CanRewindPlay(out float time, out Vector2 direction))
        {
            _gameTime = Time.time;
            Debug.Log("Move Time: " + time);
            
            _time = time;
            _movement.MoveByDirection(direction / 100f);

            yield return new WaitForSeconds(time);
            //yield return new WaitUntil(() => !_isPlaying);
            Debug.Log("Delta Time: " + (Time.time - _gameTime));
        }

        _joystick.ResetDirection();
        _rewindMoves.Reset();
        OnRewindPressed();
    }
}