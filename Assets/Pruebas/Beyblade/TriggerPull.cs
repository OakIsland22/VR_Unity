using UnityEngine;

public class TriggerPull : MonoBehaviour
{
    public Transform startPosition; // Posición inicial del gatillo
    public Transform endPosition; // Posición máxima de jalón
    public BeybladeAttach launcherScript; // Referencia al launcher

    private Vector3 initialGrabPosition;
    private bool isBeingPulled = false;

    void Start()
    {
        if (startPosition != null)
        {
            transform.position = startPosition.position;
        }
        else
        {
            Debug.LogError("StartPosition no asignado en " + gameObject.name);
        }
    }

    public void OnGrabStart()
    {
        isBeingPulled = true;
        initialGrabPosition = transform.position;
    }

    public void OnGrabEnd()
    {
        if (isBeingPulled)
        {
            isBeingPulled = false;

            if (initialGrabPosition != null && endPosition != null)
            {
                float pullDistance = Vector3.Distance(initialGrabPosition, transform.position);
                float pullSpeed = pullDistance * 10;

                if (launcherScript != null)
                {
                    launcherScript.AcoplarBeyblade();
                    launcherScript.LanzarBeyblade(pullSpeed); // Lanzarlo con la fuerza calculada
                }
                else
                {
                    Debug.LogError("LauncherScript no asignado en " + gameObject.name);
                }
            }

            ResetTrigger();
        }
    }

    void ResetTrigger()
    {
        if (startPosition != null)
        {
            transform.position = startPosition.position;
        }
    }
}
