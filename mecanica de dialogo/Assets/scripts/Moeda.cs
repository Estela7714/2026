using UnityEngine;

public class Moeda : MonoBehaviour
{
    [Header("Configurações da Moeda")]
    public float velocidadeRotacao = 100f; // Velocidade do giro no próprio eixo

    void Update()
    {
        // Faz a moeda girar continuamente para dar um efeito bonito
        transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se quem encostou na moeda tem o script de controle da Bolinha
        BolinhaController bolinha = other.GetComponent<BolinhaController>();

        if (bolinha != null)
        {
            // Aumenta a quantidade de moedas coletadas na bolinha que encostou
            bolinha.moedasColetadas++;

            // Opcional: Se tiver sistema de áudio no seu projeto, você pode tocar o som aqui:
            // ManejoDeSom.Instance.Play2DOneShot(somMoeda);

            // Destrói a moeda da cena
            Destroy(gameObject);
        }
    }
}