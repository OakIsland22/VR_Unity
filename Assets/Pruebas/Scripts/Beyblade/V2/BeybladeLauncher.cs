using UnityEngine;

public class BeybladeLauncher : MonoBehaviour
{
    public Transform referencePoint; // Punto donde la referencia visual
    public Transform attachPoint; // Punto donde acopla el Beyblade
    public float referenceRadius = 1.5f; // zona de referencia (azul)
    public float attachRadius = 0.5f; // zona de acoplamiento (rojo)
    public GameObject referenceIndicator;

    private GameObject currentBeyblade;
    private Rigidbody beybladeRb;

    void Update()
    {
        DetectNearbyBeyblade();
        UpdateAttachment();
    }

    private void DetectNearbyBeyblade()
    {
        Collider[] colliders = Physics.OverlapSphere(referencePoint.position, referenceRadius);
        GameObject detectedBeyblade = null;

        foreach (Collider col in colliders)
        {
            if (col.GetComponent<Rigidbody>() != null)
            {
                detectedBeyblade = col.gameObject;
                break;
            }
        }

        if (detectedBeyblade != null)
        {
            referenceIndicator.SetActive(true);
            currentBeyblade = detectedBeyblade;
            beybladeRb = currentBeyblade.GetComponent<Rigidbody>();
        }
        else
        {
            referenceIndicator.SetActive(false);
            currentBeyblade = null;
            beybladeRb = null;
        }
    }

    private void UpdateAttachment()
    {
        if (currentBeyblade == null || beybladeRb == null) return;

        float distance = Vector3.Distance(currentBeyblade.transform.position, attachPoint.position);

        if (distance <= attachRadius)
        {
            currentBeyblade.transform.position = attachPoint.position;
            currentBeyblade.transform.rotation = attachPoint.rotation;

            beybladeRb.useGravity = false;
            beybladeRb.linearVelocity = Vector3.zero;
            beybladeRb.angularVelocity = Vector3.zero;
        }
        else
        {
            beybladeRb.useGravity = true;
        }
    }

    void OnDrawGizmos()
    {
        if (referencePoint != null)
        {
            Gizmos.color = new Color(0, 0, 1, 0.3f);
            Gizmos.DrawWireSphere(referencePoint.position, referenceRadius);
        }

        if (attachPoint != null)
        {
            Gizmos.color = new Color(1, 0, 0, 0.6f);
            Gizmos.DrawWireSphere(attachPoint.position, attachRadius);
        }
    }
}
