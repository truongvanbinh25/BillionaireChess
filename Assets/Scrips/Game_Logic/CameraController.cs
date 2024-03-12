using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float touchSpeed = 0.1f;
    [SerializeField] private float zoomSpeed = 1f;
    private float currentZoom = 10f;
    [SerializeField] private float minY = -10f;
    [SerializeField] private float maxY = 10f;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxZ = 10f;
    [SerializeField] private float scrollSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isSelling)
            return;


        //PC
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * scrollSpeed;
        Camera.main.orthographicSize = Math.Clamp(Camera.main.orthographicSize, minY, maxY);


        //Mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //Lấy vị trí lần chạm đầu tiên
            if (Input.touchCount == 2)
            {
                // Lấy ra hai lần chạm
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                // Tính khoảng cách giữa hai lần chạm
                float touchDelta = (touch0.position - touch1.position).magnitude; //khoảng cách của 2 ngón tay trong frame hiện tại
                float previousTouchDelta = (touch0.position - touch0.deltaPosition - (touch1.position - touch1.deltaPosition)).magnitude; // khoảng cách của 2 ngón tay trong framr ttruocws đó

                // Tính sự thay đổi khoảng cách và điều chỉnh độ dài tiêu điểm của Camera
                float zoomAmount = (previousTouchDelta - touchDelta) * zoomSpeed;
                currentZoom = Mathf.Clamp(currentZoom + zoomAmount, minY, maxY);

                // Cập nhật độ dài tiêu điểm của Camera
                Camera.main.fieldOfView = currentZoom;
            }
            else if (touch.phase == TouchPhase.Moved) // Khi ngón tay di chuyển trên màn hình
            {
                Vector2 touchDeltaPosition = touch.deltaPosition; //touch.deltaPosition: lấy vị trí khoảng cách từ lần chạm trước đến lần chạm hiện tại
                Vector3 cameraMovement = new Vector3(-touchDeltaPosition.x, -touchDeltaPosition.y, 0) * touchSpeed;
                
                //Di chuyển camera
                Camera.main.transform.Translate(cameraMovement);

                //Giới hạn vị trí camera
                Vector3 currentPosition = Camera.main.transform.position;
                currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
                currentPosition.z = Mathf.Clamp(currentPosition.z, minZ, maxZ);
                Camera.main.transform.position = currentPosition;
            }
        }
    }
}
