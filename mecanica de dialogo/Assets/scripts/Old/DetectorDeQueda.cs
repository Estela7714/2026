using UnityEngine;

public class DetectorDeQueda : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Tenta pegar o componente da bolinha que caiu
        BolinhaController bolinha = other.GetComponentInParent<BolinhaController>();

        // Se não achar no objeto pai, busca no próprio objeto atingido
        if (bolinha == null)
        {
            bolinha = other.GetComponent<BolinhaController>();
        }

        if (bolinha != null)
        {
            // Desativa o collider da bolinha para evitar que ela acione o detector mais de uma vez
            Collider col = other.GetComponent<Collider>();
            if (col != null) col.enabled = false;

            // Registra a queda no GameManager
            GameManager.Instance.RegistrarQuedaJogador(bolinha.idJogador);
        }
    }
}