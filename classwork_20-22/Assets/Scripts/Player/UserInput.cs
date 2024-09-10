using UnityEngine.EventSystems;
public class UserInput
{
    public event System.Action<UnityEngine.Vector2> OnTouchBegin = default;
    public event System.Action<UnityEngine.Vector2> OnTouchMove = default;
    public event System.Action OnTouchEnd = default;

    private UnityEngine.Vector2 _originPos;

    private bool _isActivated = false;
    public UserInput()
    {

    }

    ~UserInput()
    {

    }
    public void DetectMovementInput()
    {
        if (UnityEngine.Input.touchCount >= 1)
        {
            UnityEngine.Touch currentTouch = UnityEngine.Input.GetTouch(0);

            switch (currentTouch.phase)
            {
                case UnityEngine.TouchPhase.Began:
                    if (EventSystem.current.IsPointerOverGameObject(currentTouch.fingerId))
                    {
                        _isActivated = false;
                        return;
                    }
                    _isActivated = true;

                    _originPos = currentTouch.position;
                    OnTouchBegin?.Invoke(currentTouch.position);
                    break;
                case UnityEngine.TouchPhase.Moved:
                    if (_isActivated)
                        CaseMoved(currentTouch.position);
                    break;
                case UnityEngine.TouchPhase.Ended:
                    if (_isActivated)
                        OnTouchEnd?.Invoke();
                    break;
            }
        }
    }
    private void CaseMoved(UnityEngine.Vector2 currentPos)
    {
        if (IsDistanceFar(currentPos))
        {
            UnityEngine.Vector2 dir = (currentPos- _originPos).normalized;
            //UnityEngine.Debug.Log($"Origin Pos: {_originPos} || capped direction: {_originPos + 100 * dir}");
            OnTouchMove?.Invoke(_originPos + 100 * dir);
        }
        else
        {
            OnTouchMove?.Invoke(currentPos);
        }
    }
    private bool IsDistanceFar(UnityEngine.Vector2 direction)
    {
        float distance = UnityEngine.Vector2.Distance(direction, _originPos);
        //UnityEngine.Debug.Log($"distance between positions {_originPos} and {direction} = {distance}");
        return distance > 100;
    }
}
