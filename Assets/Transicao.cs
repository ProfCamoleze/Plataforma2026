using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicao : MonoBehaviour
{

    public string sceneToLoad;
    public Vector2 posicaoCena;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisão detectada com o jogador!");    
            PlayerPersiste.Instance.iniciarPos = posicaoCena; 
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

