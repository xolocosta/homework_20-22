using System.Diagnostics;

public class Joystick
{
    public UnityEngine.Vector2 Direction { get => _joystickDirection; }
    public UnityEngine.Vector2 OriginPos { get => _originPos; }

    private UnityEngine.Vector2 _joystickDirection;
    private UnityEngine.Vector2 _originPos;

    public Joystick()
    {

    }

    ~Joystick()
    {

    }
    public void SetOrigin(UnityEngine.Vector2 originPos)
        => _originPos = originPos;
    public void SetDirection(UnityEngine.Vector2 direction)
    {
        _joystickDirection = direction;
        //UnityEngine.Debug.Log($"Origin Pos: {_originPos} || Direction Pos: {direction}");
    }
    public void ResetDirection()
    {
        SetDirection(UnityEngine.Vector2.zero);
        SetOrigin(UnityEngine.Vector2.zero);
    }
}
