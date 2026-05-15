using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Poste : MonoBehaviour
{
    public Light2D luzdoPoste;
  public  bool interruptor = true;
   public bool estouDentro = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InterrupirLuz();

    }
    public void InterrupirLuz()
    {
        Debug.Log("interagiu no poste");
        interruptor = !interruptor;
        if (estouDentro)
            luzdoPoste.enabled = interruptor;
    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("entrou");
                estouDentro=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("saiu");
            estouDentro =false;
        }
    }
}
