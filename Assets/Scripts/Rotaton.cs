using Unity.VisualScripting;
using UnityEngine;

public class Rotaton : MonoBehaviour
{
    public float rotationSpeed = 0.3f;
    // Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*rotationSpeed*Time.deltaTime);
        // rb.SetRotation(rotationSpeed*Time.deltaTime);
    }
}