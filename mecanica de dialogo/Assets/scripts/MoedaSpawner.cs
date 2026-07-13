using UnityEngine;
using System.Collections;

public class MoedaSpawner : MonoBehaviour
{
    public GameObject prefabMoeda;
    public Vector3 limiteMinimo; // Ex: X:-10, Z:-10
    public Vector3 limiteMaximo; // Ex: X:10, Z:10
    public float intervaloSpawn = 4f;

    void Start()
    {
        StartCoroutine(SpawnMoedasRotina());
    }

    IEnumerator SpawnMoedasRotina()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloSpawn);

            float randomX = Random.Range(limiteMinimo.x, limiteMaximo.x);
            float randomZ = Random.Range(limiteMinimo.z, limiteMaximo.z);
            Vector3 posicaoSpawn = new Vector3(randomX, 1.5f, randomZ);

            Instantiate(prefabMoeda, posicaoSpawn, Quaternion.identity);
            Debug.Log($"Moeda gerada na posição: {posicaoSpawn}"); // 👈 Teste
        }
    }
}