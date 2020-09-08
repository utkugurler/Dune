using UnityEngine;

public class CameraFix : MonoBehaviour
{
    public float changeX;
    public float changeY;

    Transform targetTransform;
    Vector3 tempVec3 = new Vector3();
    
	private void Start()
	{
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);

        // Belli değerler girerek kameranın x ve y siyle oynayıp kilitliyoruz
        tempVec3.x = targetTransform.position.x + changeX;
        tempVec3.y = targetTransform.position.y + changeY;
        tempVec3.z = this.transform.position.z; // Karakterin rotasyonu değişince kamera değişmemesi için kendi z ekseninde kalıyor
        this.transform.position = tempVec3;
    }
}
