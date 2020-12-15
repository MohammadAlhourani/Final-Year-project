using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Camera camera;

    private Func<Vector3> GetCameraFollowPos;
    private Func<float> GetCameraZoom;

    public bool EdgeScroll = false;


    public float edgeScrollSpeed;

    private float edgeSize = 10f;

    private float boundary = 25f;

    private Vector3 startPos;

    Color blue = new Color(0, 0, 255);

    public void Setup(Func<Vector3> t_GetCameraFollowPos , Func<float> t_GetCameraZoom)
    {
        this.GetCameraFollowPos = t_GetCameraFollowPos;
        this.GetCameraZoom = t_GetCameraZoom;

        startPos = transform.position;
    }


    public void SetCameraFollowPos(Func<Vector3> t_GetCameraFollowPos)
    {
        this.GetCameraFollowPos = t_GetCameraFollowPos;
    }


    public void setCameraZoom(Func<float> t_GetCameraZoom)
    {
        this.GetCameraZoom = t_GetCameraZoom;
    }


    void Start()
    {
        camera = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EdgeScroll == true)
        {
            edgeScrolling();
        }
        else
        {
            handleMovement();
        }
       
        handleZoom();
    }


    void handleMovement()
    {
        Vector3 FollowPosition = GetCameraFollowPos();

        FollowPosition.z = transform.position.z;
        transform.position = FollowPosition;
    }


    void handleZoom()
    {
        float cameraZoom = GetCameraZoom();

        float cameraZoomDifference = cameraZoom - camera.orthographicSize;

        float cameraZoomSpeed = 1f;

        camera.orthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.deltaTime;


        transform.GetChild(0).transform.GetComponent<Camera>().orthographicSize = camera.orthographicSize;


        if (cameraZoomDifference > 0)
        {
            if (camera.orthographicSize > cameraZoom)
            {
                camera.orthographicSize = cameraZoom;
            }
        }
    }

    void edgeScrolling()
    {
        Vector3 Position = transform.position;


        if (Position.x < startPos.x + boundary)
        {
            if (Input.mousePosition.x > Screen.width - edgeSize)
            {
                //right
                Position.x += edgeScrollSpeed * Time.deltaTime;
            }
        }

        if (Position.x > startPos.x - boundary)
        {
            if (Input.mousePosition.x < edgeSize)
            {
                //left
                Position.x -= edgeScrollSpeed * Time.deltaTime;
            }
        }

        if (Position.y < startPos.y + boundary)
        {
            if (Input.mousePosition.y > Screen.height - edgeSize)
            {
                //up
                Position.y += edgeScrollSpeed * Time.deltaTime;
            }
        }

        if (Position.y > startPos.y - boundary)
        {
            if (Input.mousePosition.y < edgeSize)
            {
                //down
                Position.y -= edgeScrollSpeed * Time.deltaTime;
            }
        }

        transform.position = Position;


        Debug.DrawLine(new Vector3(startPos.x + boundary , startPos.y + boundary , 0), new Vector3(startPos.x + boundary, startPos.y - boundary, 0), blue, 100f);

        Debug.DrawLine(new Vector3(startPos.x  - boundary, startPos.y - boundary, 0), new Vector3(startPos.x + boundary, startPos.y - boundary, 0), blue, 100f);

        Debug.DrawLine(new Vector3(startPos.x + boundary, startPos.y + boundary, 0), new Vector3(startPos.x - boundary, startPos.y + boundary, 0), blue, 100f);

        Debug.DrawLine(new Vector3(startPos.x - boundary, startPos.y - boundary, 0), new Vector3(startPos.x - boundary, startPos.y + boundary, 0), blue, 100f);
    }
}
