using UnityEngine;

public class BeybladeSpin : MonoBehaviour
{
    private Rigidbody rb;
    private bool isSpinning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No se encontró Rigidbody en " + gameObject.name);
        }
    }

    public void Lanzar(float fuerza)
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * fuerza, ForceMode.Impulse);
            rb.AddTorque(Vector3.up * fuerza * 5, ForceMode.Impulse);
            isSpinning = true;
        }
    }

    void Update()
    {
        if (isSpinning && rb != null)
        {
            rb.angularVelocity = Vector3.up * rb.angularVelocity.magnitude;
        }
    }
}