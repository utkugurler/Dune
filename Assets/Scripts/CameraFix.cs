using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    Transform targetTransform;
    Vector3 tempVec3 = new Vector3();
    public float changeX; 
    public float changeY; 

	private void Start()
	{
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);

        tempVec3.x = targetTransform.position.x + changeX;
        tempVec3.y = targetTransform.position.y + changeY;
        tempVec3.z = this.transform.position.z;
        this.transform.position = tempVec3;
    }
}
