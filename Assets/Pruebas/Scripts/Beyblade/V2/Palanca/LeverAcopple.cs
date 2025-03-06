using UnityEngine;

public class LeverAcopple : MonoBehaviour
{
    public Transform referencePoint; // Punto donde se mostrará la referencia visual
    public Transform attachPoint; // Punto exacto donde se acopla la palanca
    public float referenceRadius = 1.5f; // Radio de la zona de referencia (azul)
    public float attachRadius = 0.5f; // Radio de la zona de acoplamiento (rojo)
    public GameObject referenceIndicator; // Objeto visual de referencia

    private string leverTag = "Lever";
    private GameObject currentLever; // La palanca detectada
    private Rigidbody leverRb;

    void Update()
    {
        DetectNearbyLever();
        UpdateAttachment();
    }

    private void DetectNearbyLever()
    {
        Collider[] colliders = Physics.OverlapSphere(referencePoint.position, referenceRadius);
        GameObject detectedLever = null;

        foreach (Collider col in colliders)
        {
            if (col.GetComponent<Rigidbody>() != null && col.gameObject != gameObject && col.CompareTag(leverTag))
            {
                detectedLever = col.gameObject;
                break;
            }
        }

        if (detectedLever != null)
        {
            referenceIndicator.SetActive(true);
            currentLever = detectedLever;
            leverRb = currentLever.GetComponent<Rigidbody>();
        }
        else
        {
            referenceIndicator.SetActive(false);
            currentLever = null;
            leverRb = null;
        }
    }

    private void UpdateAttachment()
    {
        if (currentLever == null || leverRb == null) return;

        float distance = Vector3.Distance(currentLever.transform.position, attachPoint.position);

        if (distance <= attachRadius)
        {
            currentLever.transform.position = attachPoint.position;
            currentLever.transform.rotation = attachPoint.rotation;
            leverRb.useGravity = false;
            leverRb.linearVelocity = Vector3.zero;
            leverRb.angularVelocity = Vector3.zero;

            currentLever.GetComponent<LeverForceCalculator>()?.SetAttached(true);
        }
        else
        {
            leverRb.useGravity = true;

            currentLever.GetComponent<LeverForceCalculator>()?.SetAttached(false);
        }
    }


    void OnDrawGizmos()
    {
        if (referencePoint != null)
        {
            Gizmos.color = new Color(0, 0, 1, 0.3f); // Azul transparente (Zona de referencia)
            Gizmos.DrawWireSphere(referencePoint.position, referenceRadius);
        }

        if (attachPoint != null)
        {
            Gizmos.color = new Color(1, 0, 0, 0.6f); // Rojo más visible (Zona de acoplamiento)
            Gizmos.DrawWireSphere(attachPoint.position, attachRadius);
        }
    }
}