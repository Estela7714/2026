using UnityEngine;

public class AudioManager : MonoBehaviour
{
  // #region Singleton 
   public static AudioManager Instance;

   private void Awake()
   {
       if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   //endregion
} 

