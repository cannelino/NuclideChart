using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendAligner : MonoBehaviour
{
    public Transform Head;
    public float DistanceOffset;
    public float HeightOffset;
    public bool isFloor;
    public bool isTraining = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFloor)
        {
            Vector3 lookdirection = new Vector3(Head.forward.x, 0, Head.forward.z).normalized;
            transform.position = Head.position + HeightOffset * Vector3.up + lookdirection * DistanceOffset;
            transform.forward = lookdirection;
        }
        else if (isTraining)
        {
            transform.position = new Vector3((Head.position + Vector3.forward * DistanceOffset).x, (Head.position + HeightOffset * Vector3.up).y, (Head.position + Vector3.forward * DistanceOffset).z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (Head.position + Vector3.forward * DistanceOffset).z);
        }
    }
}
