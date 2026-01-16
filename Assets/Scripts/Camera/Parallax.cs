
using UnityEngine;

public class Parallax : MonoBehaviour
{

    Material mat;
    float distance;


    public float speed = 0.2f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
        
    }
}
