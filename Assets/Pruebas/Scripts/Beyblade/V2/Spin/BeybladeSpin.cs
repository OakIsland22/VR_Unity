using UnityEngine;

public class BeybladeSpin : MonoBehaviour
{
    private Rigidbody beybladeRb; // Rigidbody del Beyblade
    private LeverForceCalculator leverForce; // Referencia al script de la palanca
    public float spinMultiplier = 50f; // Multiplicador para ajustar la velocidad de giro
    public float currentSpinSpeed = 0f; // Velocidad actual de giro
    private float decelerationRate = 150; // Velocidad de desaceleración
    private float stopThreshold = 0.5f; // Se detiene antes
    private float maxSpinSpeed = 1500; // 🔥 Límite máximo de velocidad


    public BeybladeLauncher launcher;
    void Start()
    {
        beybladeRb = GetComponent<Rigidbody>();
        leverForce = FindFirstObjectByType<LeverForceCalculator>(); // Encuentra la palanca en la escena

        if (beybladeRb == null)
        {
            Debug.LogError("No se encontró el Rigidbody en el Beyblade.");
        }
    }

    void Update()
    {
        SpinBeyblade();
    }

    private void SpinBeyblade()
    {
        if (leverForce != null)
        {
            float newSpinSpeed = leverForce.pullForce * spinMultiplier;

            if (newSpinSpeed > 0)
            {
                currentSpinSpeed = Mathf.Min(newSpinSpeed, maxSpinSpeed); // 🔥 Limita la velocidad a 1000
            }
            else
            {
                // 🔥 Se detiene más rápido ahora
                currentSpinSpeed = Mathf.MoveTowards(currentSpinSpeed, 0, decelerationRate * Time.deltaTime);
            }

            // Aplicar rotación solo si la velocidad es mayor al umbral
            if (Mathf.Abs(currentSpinSpeed) > stopThreshold)
            {
                transform.Rotate(Vector3.forward * currentSpinSpeed * Time.deltaTime);
            }
        }

        beybladeRb.useGravity = true;
    }
}