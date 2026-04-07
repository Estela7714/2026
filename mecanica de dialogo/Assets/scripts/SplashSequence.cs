using UnityEngine;
using System.Collections;

public class SplashSequence : MonoBehaviour
{
    IEnumerator Start()
    {
        GameManager.Instance.ChangeState(GameState.ExibindoSplash);
        yield return new WaitForSeconds(2f); // Espera 2 segundos
        
        GameManager.Instance.ChangeState(GameState.MenuPrincipal);
        GameManager.Instance.RequestSceneLoad("MenuPrincipal");
    }
}