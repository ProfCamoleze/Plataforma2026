using UnityEngine;

public class PlayerTop : MonoBehaviour
{

    public float speed = 5f;
    Rigidbody2D rig;
    Vector2 movement;
    InputPlayer player;
    [Header("Animação")]
    public Animator anim;
    public GameObject luz;
    Poste poste;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        player = new InputPlayer();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        player.Enable();
    }
    private void OnDisable()
    {
        player.Disable();
    }

    void FixedUpdate()
    {
        rig.linearVelocity = movement.normalized * speed;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = player.Player.Move.ReadValue<Vector2>();
        // Envia os valores atuais para o Blend Tree de movimento
        anim.SetFloat("MoverX", movement.x);
        anim.SetFloat("MoverY", movement.y);

        // Speed serve para saber se o personagem está andando ou parado
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if(player.Player.Interact.WasPressedThisFrame())
        {
            Debug.Log("interagiu");
            luz.GetComponent<Poste>().InterrupirLuz();
        }
        
    }
}
