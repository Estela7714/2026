using UnityEngine;

public class DetectorDeQueda : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BolinhaController bolinha = other.GetComponent<BolinhaController>();
        if (bolinha != null)
        {
            GameManager.Instance.RegistrarQuedaJogador(bolinha.idJogador);
        }
    }
}