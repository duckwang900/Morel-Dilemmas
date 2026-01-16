using UnityEngine;

public class ParallaxBehavior : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;
    float farthestBack;

    public float parallaxSpeed;

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        farthestBack = 0f; // 🔧 CHANGE: reset value

        for (int i = 0; i < backCount; i++)
        {
            float depth = Mathf.Abs(backgrounds[i].transform.position.z - cam.position.z);
            if (depth > farthestBack)
                farthestBack = depth;
        }

        if (farthestBack == 0f)
            farthestBack = 1f; // 🔧 CHANGE: prevent divide by zero

        for (int i = 0; i < backCount; i++)
        {
            float depth = Mathf.Abs(backgrounds[i].transform.position.z - cam.position.z);
            backSpeed[i] = 1f - (depth / farthestBack); // 🔧 CHANGE: fixed math + parentheses
        }
    }

    private void LateUpdate()
    {
        // 🔧 CHANGE: invert distance so direction feels natural
        distance = camStartPos.x - cam.position.x;

        // 🔧 CHANGE: make background FOLLOW the camera
        transform.position = new Vector3(
            cam.position.x,
            transform.position.y,
            transform.position.z
        );

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance * speed, 0));
        }
    }
}
