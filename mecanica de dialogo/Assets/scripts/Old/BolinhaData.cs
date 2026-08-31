using UnityEngine;

[CreateAssetMenu(fileName = "NovaBolinha", menuName = "Sumo/Bolinha Data")]
public class BolinhaData : ScriptableObject
{
    public string nomeDaBolinha;
    public float velocidadeInicial = 10f;
    public float forcaEmpurraoBase = 15f;
    public float tamanhoEscala = 1f;
    public float massaRigidbody = 1f;
    public Sprite fotoMenu;
    public Material corJogador1;
    public Material corJogador2;
}