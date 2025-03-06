using UnityEngine;

public class LeverForceCalculator : MonoBehaviour
{
    private Vector3 lastVelocity; // Velocidad del frame anterior
    public float pullForce; // Fuerza de jalón calculada
    private Rigidbody leverRb; // Rigidbody de la palanca
    private bool isAttached = false; // Indica si la palanca está acoplada
    private bool canCalculate = false; // Controla si la fuerza puede calcularse

    void Start()
    {
        leverRb = GetComponent<Rigidbody>();
        lastVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (isAttached && canCalculate) // Solo calcular después del retraso
        {
            CalculatePullForce();
        }
    }

    private void CalculatePullForce()
    {
        if (leverRb != null)
        {
            Vector3 acceleration = (leverRb.linearVelocity - lastVelocity) / Time.fixedDeltaTime;
            pullForce = leverRb.mass * acceleration.magnitude;
            lastVelocity = leverRb.linearVelocity;
        }
    }

    // Método para marcar la palanca como acoplada
    public void SetAttached(bool attached)
    {
        if (attached)
        {
            isAttached = true;
            canCalculate = false; // Bloquea el cálculo inicialmente
            Invoke(nameof(EnableCalculation), 1f); // Espera 1 segundo antes de activar el cálculo
            CancelInvoke(nameof(DisableAttachment)); // Si se vuelve a acoplar rápido, cancela la espera
        }
        else
        {
            Invoke(nameof(DisableAttachment), 0.5f); // Espera 0.5s antes de desactivar
        }
    }

    private void EnableCalculation()
    {
        canCalculate = true; // Permite calcular la fuerza después del retraso
    }

    private void DisableAttachment()
    {
        isAttached = false;
        canCalculate = false;
        pullForce = 0; // Resetea la fuerza cuando se desacopla después de 0.5s
    }
}