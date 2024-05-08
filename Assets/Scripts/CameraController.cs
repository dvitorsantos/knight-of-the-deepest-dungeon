using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Dino;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Dino.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Dino.transform.position + offset;
    }
}
