using UnityEngine;

public class SwerveInputSystem : MonoBehaviour
{
    float _lastFrameFingerPosX;
    float _moveFactorX;
    public float MoveFactorX => _moveFactorX;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            _lastFrameFingerPosX = Input.mousePosition.x;
        else if (Input.GetMouseButton(0))
        {
            var PosX = Input.mousePosition.x;
            _moveFactorX = PosX - _lastFrameFingerPosX;
            _lastFrameFingerPosX = PosX;

        }
        else if (Input.GetMouseButtonUp(0))
            _moveFactorX = 0f;
    }
}
