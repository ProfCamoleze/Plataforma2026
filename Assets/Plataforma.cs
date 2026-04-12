using System.Collections;
using UnityEngine;

public class Plataforma : MonoBehaviour



{
    [Header("Pontos do movimento")]
    public Transform pontoA;
    public Transform pontoB;

    [Header("Configuração")]
    public float velocidade = 2f;
    public float tempoDeEspera = 1f;

    private Rigidbody2D rb;
    private Vector2 alvoAtual;
    private bool esperando = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (pontoA != null && pontoB != null)
        {
            transform.position = pontoA.position;
            alvoAtual = pontoB.position;
        }
    }

    private void FixedUpdate()
    {
        if (pontoA == null || pontoB == null || esperando)
            return;

        Vector2 novaPosicao = Vector2.MoveTowards(rb.position, alvoAtual, velocidade * Time.fixedDeltaTime);
        rb.MovePosition(novaPosicao);

        if (Vector2.Distance(rb.position, alvoAtual) < 0.05f)
        {
            StartCoroutine(TrocarDestino());
        }
    }

IEnumerator TrocarDestino()
    {
        esperando = true;

        yield return new WaitForSeconds(tempoDeEspera);

        if (Vector2.Distance(alvoAtual, (Vector2)pontoA.position) < 0.05f)
        {
            alvoAtual = pontoB.position;
        }
        else
        {
            alvoAtual = pontoA.position;
        }

        esperando = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}