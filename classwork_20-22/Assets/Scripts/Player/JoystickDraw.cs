public class JoystickDraw
{
    UnityEngine.GameObject _innerCircle;
    UnityEngine.GameObject _outerCircle;

    private Joystick _joystick;
    public JoystickDraw(Joystick joystick, UnityEngine.GameObject inner, UnityEngine.GameObject outer)
    {
        _joystick = joystick;
        _innerCircle = inner;
        _outerCircle = outer;

        HideJoystick();
    }

    ~JoystickDraw()
    {

    }
    public void Draw(UnityEngine.Vector2 currentPos)
    {
        float distance = UnityEngine.Vector2.Distance(currentPos, _outerCircle.transform.position);

        if (distance < 100)
            _innerCircle.transform.position = currentPos;
        else
        {
            UnityEngine.Vector2 dir = (currentPos - _joystick.OriginPos).normalized;
            _innerCircle.transform.position = _joystick.OriginPos + 100 * dir;
        }
    }
    public void ShowJoystick(UnityEngine.Vector2 touchPos)
    {
        _innerCircle.transform.position = touchPos;
        _outerCircle.transform.position = touchPos;

        _innerCircle.SetActive(true);
        _outerCircle.SetActive(true);
    }
    public void HideJoystick()
    {
        _innerCircle.SetActive(false);
        _outerCircle.SetActive(false);
    }
}
