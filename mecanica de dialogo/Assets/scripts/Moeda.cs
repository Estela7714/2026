using UnityEngine;

public class Moeda : MonoBehaviour
{
    public float velocidadeRotacao = 100f;
    private bool jaColetada = false; // 👈 Trava para evitar disparo múltiplo

    void Update()
    {
        transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Se já foi coletada neste milissegundo, ignora qualquer outra colisão!
        if (jaColetada) return;

        BolinhaController bolinha = other.GetComponent<BolinhaController>();

        if (bolinha != null)
        {
            jaColetada = true; // 👈 Ativa a trava imediatamente no primeiro contato!
            
            bolinha.moedasColetadas++;
            Debug.Log($"Moeda coletada por J{bolinha.idJogador}. Total: {bolinha.moedasColetadas}");

            Destroy(gameObject);
        }
    }
}