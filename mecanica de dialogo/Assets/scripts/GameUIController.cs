using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    public Slider sliderCooldownJ1;
    public Slider sliderCooldownJ2;
    public TMPro.TextMeshProUGUI textoPlacar;

    [Header("Referências das Bolinhas (Para ler as Moedas)")]
    public BolinhaController bolinhaJ1;
    public BolinhaController bolinhaJ2;

    void OnEnable()
    {
        BolinhaController.OnEmpurraoUsado += IniciarBarraCooldown;
    }

    void OnDisable()
    {
        BolinhaController.OnEmpurraoUsado -= IniciarBarraCooldown;
    }

    void Start()
    {
        if (sliderCooldownJ1 != null) sliderCooldownJ1.value = 1f;
        if (sliderCooldownJ2 != null) sliderCooldownJ2.value = 1f;
    }

    void Update()
    {
        // Atualiza o texto em tempo real com as moedas coletadas de cada um
        if (bolinhaJ1 != null && bolinhaJ2 != null && textoPlacar != null)
        {
            textoPlacar.text = $"Moedas J1: {bolinhaJ1.moedasColetadas}     Moedas J2: {bolinhaJ2.moedasColetadas}";
        }
    }

    void IniciarBarraCooldown(int idJogador, float tempo)
    {
        if (idJogador == 1 && sliderCooldownJ1 != null) 
            StartCoroutine(RotinaAnimaCooldown(sliderCooldownJ1, tempo));
        else if (idJogador == 2 && sliderCooldownJ2 != null) 
            StartCoroutine(RotinaAnimaCooldown(sliderCooldownJ2, tempo));
    }

    IEnumerator RotinaAnimaCooldown(Slider slider, float tempo)
    {
        slider.value = 0f;
        float decorrido = 0f;
        while (decorrido < tempo)
        {
            decorrido += Time.deltaTime;
            slider.value = decorrido / tempo;
            yield return null;
        }
        slider.value = 1f;
    }
}