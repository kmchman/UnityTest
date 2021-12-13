using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    [SerializeField] private RectTransform zoomTargetRt;
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.


    private readonly float _ZOOM_IN_MAX = 2f;
    private readonly float _ZOOM_OUT_MAX = 1f;
    private readonly float _ZOOM_SPEED = 1.5f;

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            var currentScale = zoomTargetRt.localScale.x;
            var zoomAmount = deltaMagnitudeDiff * currentScale * _ZOOM_SPEED; // zoomAmount == deltaScale
            /* clamp & zoom */
            var zoomedScale = currentScale + zoomAmount;
            if (zoomedScale < _ZOOM_OUT_MAX)
            {
                zoomedScale = _ZOOM_OUT_MAX;
            }
            if (_ZOOM_IN_MAX < zoomedScale)
            {
                zoomedScale = _ZOOM_IN_MAX;
            }
            zoomTargetRt.localScale = zoomedScale * Vector3.one;
        }
    }
}