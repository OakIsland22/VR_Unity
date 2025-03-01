using UnityEngine;

public class BeybladeAttach : MonoBehaviour
{
    public Transform acoplePosicion; // Punto donde se acopla el Beyblade
    public GameObject referenciaPrefab; // Prefab de la referencia visual
    private GameObject referenciaInstanciada;
    private GameObject beyblade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Beyblade"))
        {
            if (referenciaInstanciada == null && referenciaPrefab != null)
            {
                referenciaInstanciada = Instantiate(referenciaPrefab, acoplePosicion.position, acoplePosicion.rotation);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Beyblade"))
        {
            if (referenciaInstanciada != null)
            {
                Destroy(referenciaInstanciada);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Beyblade"))
        {
            beyblade = other.gameObject;
        }
    }

    public void AcoplarBeyblade()
    {
        if (beyblade != null && acoplePosicion != null)
        {
            beyblade.transform.position = acoplePosicion.position;
            beyblade.transform.rotation = acoplePosicion.rotation;
            Rigidbody rb = beyblade.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true; // Fijar el Beyblade al launcher
            }

            if (referenciaInstanciada != null)
            {
                Destroy(referenciaInstanciada);
            }
        }
    }

    public void LanzarBeyblade(float fuerza)
    {
        if (beyblade != null)
        {
            BeybladeSpin beybladeSpin = beyblade.GetComponent<BeybladeSpin>();
            if (beybladeSpin != null)
            {
                beybladeSpin.Lanzar(fuerza);
            }
        }
    }
}