using System;
using UnityEngine;

public class ParallaxScript: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Transform cam;
    Vector3 camStartPos;
    float distance;
    GameObject[] backgrounds;

    Material[] mat;
    float[] backSpeed;
    float farthestBack;

    [Range(0f, 0.5f)]
    public float parallaxSpeed;
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;
        int backgroundCount = transform.childCount;
        mat = new Material[backgroundCount];
        backSpeed = new float[backgroundCount];
        backgrounds = new GameObject[backgroundCount];


        for (int i = 0; i < backgroundCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;

        }



    }



    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }

        }
        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = (1 - (backgrounds[i].transform.position.z - cam.position.z)) / farthestBack;

        }

    }
    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.transform.position.x, transform.position.y, 0);
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}