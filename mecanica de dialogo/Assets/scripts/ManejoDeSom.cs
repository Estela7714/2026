using UnityEngine;
using System.Collections.Generic;
public class ManejoDeSom : MonoBehaviour
{
   
   private ManejoDeSom systemSource;
   private List<AudioSource> activeSources;
   
  public static ManejoDeSom Instance;
  private void Awake()
   {if (Instance == null)
       {
          Instance = this;
          DontDestroyOnLoad(gameObject);
          systemSource = GetComponent<ManejoDeSom>();
          activeSources = new List<AudioSource>();
       }
       else
       {
          Destroy(gameObject);
       }
   }

   #region AudioComtrole

   public void PlaySound(AudioClip clip)
   {
      systemSource.Stop();
      systemSource.clip = clip;
      systemSource.Play();
   }

   public void pauseSound()
   {
      systemSource.Pause();
   }

   public void StopSound()
   {
      systemSource.Stop();
   }

   public void PlaySound(AudioClip clip)
   {
      systemSource.PlayOneShot
   }
   

   #endregion
}
