using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    public Slider sliderCooldownJ1;
    public Slider sliderCooldownJ2;
    public TMPro.TextMeshProUGUI textoPlacar;

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
        sliderCooldownJ1.value = 1f;
        sliderCooldownJ2.value = 1f;

        // Atualiza interface com placar vindo do GameManager
        textoPlacar.text = $"J1: {GameManager.Instance.vitoriasJ1}  |  J2: {GameManager.Instance.vitoriasJ2}";
    }

    void IniciarBarraCooldown(int idJogador, float tempo)
    {
        if (idJogador == 1) StartCoroutine(RotinaAnimaCooldown(sliderCooldownJ1, tempo));
        else StartCoroutine(RotinaAnimaCooldown(sliderCooldownJ2, tempo));
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