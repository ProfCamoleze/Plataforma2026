using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPersiste : MonoBehaviour
{
    // Instância estática do Singleton
    public static PlayerPersiste Instance { get; private set; }

    public Vector2 iniciarPos;
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
            return;
        }
    }

     private void OnEnable()
    {
        SceneManager.sceneLoaded += AoCarregarCena;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;
    }
      private void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        transform.position = iniciarPos;
    }

}