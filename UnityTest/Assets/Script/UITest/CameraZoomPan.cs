using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomPan : MonoBehaviour
{

    [SerializeField] private float zoomFactor;
    [SerializeField] private float minCamSize;
    [SerializeField] private float maxCamSize;
    [SerializeField] Camera mainCam;
    [SerializeField] RectTransform bgMap;


    Vector3 touchStart;
    public float touchOutMin = 1;
    public float touchOutMax = 8;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    private void Awake()
    {
        mapMinX = bgMap.position.x - bgMap.sizeDelta.x / 2;
        mapMaxX = bgMap.position.x + bgMap.sizeDelta.x / 2;
        mapMinY = bgMap.position.y - bgMap.sizeDelta.y / 2;
        mapMaxY = bgMap.position.y + bgMap.sizeDelta.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            Zoom(currentMagnitude - prevMagnitude);
        }
        if (Input.GetMouseButton(0))
        { 
            Vector3 direction = touchStart - mainCam.ScreenToWorldPoint(Input.mousePosition);
            mainCam.transform.position = ClampCamera(mainCam.transform.position + direction);
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Zoom(float increment)
    {
        if (increment != 0)
        {
            float newSize = mainCam.orthographicSize - increment * zoomFactor;
            mainCam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
            mainCam.transform.position = ClampCamera(mainCam.transform.position);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = mainCam.orthographicSize;
        float camWidgh = mainCam.orthographicSize * mainCam.aspect;

        float minX = mapMinX + camWidgh;
        float maxX = mapMaxX - camWidgh;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);

    }
}
