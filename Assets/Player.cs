using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    Rigidbody2D rig;
    float horizontal;
    Vector2 mover;
    InputPlayer controles;
    [Header("Pulo")]
    public float forcaPulo = 6f;
    public int maximoPulos = 2;
    // Controle de pulo duplo
    public int pulosRestantes;

    [Header("Chão (Ground Check)")]
    public Transform groundCheck;
    public float raioGroundCheck = 0.2f;
    public LayerMask layerGround;
    // Estado de chão
    private bool estaNoChao;



    Animator anim;
    // Start is called once before the first execution o
    // f Update after the MonoBehaviour is created
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        controles = new InputPlayer();
        anim = GetComponent<Animator>();

    }
    private void OnEnable()
    {
        controles.Enable();
    }
    void OnDisable()
    {
        controles.Disable();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mover= controles.Player.Move.ReadValue<Vector2>();
        // Checa se está no chão
        estaNoChao = Physics2D.OverlapCircle(
            groundCheck.position,
            raioGroundCheck,
            layerGround
        );

        // Aqui lemos o botão diretamente
        if (controles.Player.Jump.WasPressedThisFrame())
        {
            Pular();
        }
        // Resetar pulos se tocar no chão
        if (estaNoChao)
        {
            pulosRestantes = maximoPulos;
        }

        // Virar personagem com rotação
        if (mover.x > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (mover.x < -0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        AtualizarAnimacoes();
    }

    private void FixedUpdate()
    {
        rig.linearVelocityX = mover.x * velocidade;
    }

    // Evento do pulo
    private void Pular()
    { 
        pulosRestantes--;
        if (pulosRestantes > 0)
        {
            rig.linearVelocityY = 0f;
            rig.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
           
        }
    }
    // ✅ NOVO: Método separado para organizar todas as animações
    private void AtualizarAnimacoes()
    {

        // Animação de movimento no chão
        anim.SetFloat("andar", Mathf.Abs(rig.linearVelocity.x));

        if (estaNoChao)
        {
            // No chão = não está pulando nem caindo
            anim.SetBool("Pulando", false);
            anim.SetBool("caindo", false);
        }
        else
        {
            // Subindo
            if (rig.linearVelocity.y > 0.1f)
            {
                anim.SetBool("Pulando", true);
                anim.SetBool("caindo", false);
            }
            // Descendo
            else if (rig.linearVelocity.y < -0.1f)
            {
                anim.SetBool("Pulando", false);
                anim.SetBool("caindo", true);
            }
        }
    }

        // Debug do GroundCheck
        void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, raioGroundCheck);
    }
}

