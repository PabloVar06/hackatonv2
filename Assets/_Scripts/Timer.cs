using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float tiempo = 10f;
    public bool activo = false;

    [SerializeField] TextMeshProUGUI textoTimer;

    private void Update()
    {
        if (!activo) return;

        if (tiempo > 0)
        {
            tiempo -= Time.deltaTime;
            if (textoTimer != null)
                textoTimer.text = Mathf.Ceil(tiempo).ToString();
        }
        else
        {
            tiempo = 0;
            activo = false;
        }
    }

    public void Iniciar() => activo = true;
    public void Detener() => activo = false;
    public void Reiniciar(float nuevoTiempo)
    {
        tiempo = nuevoTiempo;
        activo = false;
    }
}