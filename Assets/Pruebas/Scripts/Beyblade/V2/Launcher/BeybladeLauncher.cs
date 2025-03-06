using System.Collections;
using UnityEngine;

public class BeybladeLauncher : MonoBehaviour
{
    public Transform referencePoint; // Punto donde se mostrará la referencia visual
    public Transform attachPoint; // Punto exacto donde se acopla el Beyblade
    public float referenceRadius = 1.5f; // Radio de la zona de referencia (azul)
    public float attachRadius = 0.5f; // Radio de la zona de acoplamiento (rojo)
    public GameObject referenceIndicator; // Objeto visual de referencia

    private GameObject currentBeyblade;
    private Rigidbody beybladeRb;
    private BeybladeSpin beybladeSpin; // Referencia al script del Beyblade

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
            if (col.GetComponent<Rigidbody>() != null && col.gameObject != gameObject) // No detectar el Launcher
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
            beybladeSpin = currentBeyblade.GetComponent<BeybladeSpin>(); // Obtener el script BeybladeSpin
        }
        else
        {
            referenceIndicator.SetActive(false);
            currentBeyblade = null;
            beybladeRb = null;
            beybladeSpin = null;
        }
    }

    private void UpdateAttachment()
    {
        if (currentBeyblade == null || beybladeRb == null || beybladeSpin == null) return;

        float distance = Vector3.Distance(currentBeyblade.transform.position, attachPoint.position);

        if (distance <= attachRadius)
        {
            currentBeyblade.transform.position = attachPoint.position;
            currentBeyblade.transform.rotation = attachPoint.rotation;

            // Desactiva la gravedad cuando está en la zona roja
            beybladeRb.useGravity = false;
            beybladeRb.linearVelocity = Vector3.zero;
            beybladeRb.angularVelocity = Vector3.zero;
        }
        else
        {
            // Si el Beyblade sale, reactiva la gravedad
            beybladeRb.useGravity = true;
        }

        // 🔥 Si la velocidad del Beyblade es mayor a 1, desactivar el launcher
        if (beybladeSpin.currentSpinSpeed > 1f)
        {
            DisableLauncherTemporarily();
        }
    }

    public void DisableLauncherTemporarily()
    {
        StartCoroutine(DisableLauncherCoroutine());
    }

    private IEnumerator DisableLauncherCoroutine()
    {
        enabled = false; // Desactiva el script del launcher
        yield return new WaitForSeconds(1f); // Espera antes de reactivar
        enabled = true; // Reactiva el launcher
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