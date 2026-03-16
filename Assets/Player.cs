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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        controles = new InputPlayer();

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

    // Debug do GroundCheck
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, raioGroundCheck);
    }
}

