using System.Runtime.InteropServices;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Transform transitionPoint;
    public Transform player;
    public Transform room1;
    public Transform room2;
    private Transform selectedRoom;
    private Vector3 recordedCameraPos;
    public float timer;
    public AnimationCurve curve;
    public float offsetMult = .5f;
    public Transform zoomedWorkstation;
    public float cameraUnzoomed = 5;
    public float cameraZoomed = .7f;
    private float recordedCameraZoom;
    private float timerMult = 1;

    void Awake()
    {
        FixedUpdate();
    }
    void FixedUpdate()
    {
        Transform previousSelected = selectedRoom;
        if(transitionPoint.position.y < player.position.y)
        {
            selectedRoom = room1;
        }
        else
        {
            selectedRoom = room2;
        }

        if(previousSelected!=selectedRoom) {
            prepareMove();
        }
    }

    public void prepareMove(Transform zoom = null, float timeMult = 1)
    {
        timerMult = timeMult;
        timer = 0;
        recordedCameraPos = transform.position;
        recordedCameraZoom = Camera.main.orthographicSize;
        zoomedWorkstation = zoom;
    }

    void Update()
    {
        if (zoomedWorkstation!=null)
        {
            if(timer<1) {
                timer+= Time.deltaTime/timerMult;
                transform.position = Vector3.Lerp((Vector2)recordedCameraPos, (Vector2)zoomedWorkstation.position, curve.Evaluate(timer))+new Vector3(0, 0, -10);
                Camera.main.orthographicSize = Mathf.Lerp(recordedCameraZoom, cameraZoomed, curve.Evaluate(timer));
            }
            return;
        }
        Vector3 offset = (Vector3)((Vector2)player.transform.position-(Vector2)selectedRoom.position)*offsetMult+new Vector3(0, 0, -10);
        if(timer<1) {
            timer+= Time.deltaTime/timerMult;
            transform.position = Vector3.Lerp(recordedCameraPos, selectedRoom.transform.position+offset, curve.Evaluate(timer));
            Camera.main.orthographicSize = Mathf.Lerp(recordedCameraZoom, cameraUnzoomed, curve.Evaluate(timer));
        }
        else
        {
            transform.position = selectedRoom.transform.position+offset;
        }
    }
}
