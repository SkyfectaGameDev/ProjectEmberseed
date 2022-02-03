using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] public int ember;
    [SerializeField] private int state;

    public Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capCollider;
    
    public Color emberTint;
    private Material material;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        ember = 0;
        state = 0;
        emberTint = new Color(1, 0.57f, 0.33f, 0f);
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        // ----- Setting Animator Parameters -----
        anim.SetBool("Run", (body.velocity.x > 1 || body.velocity.x < -1) && isGrounded());
        anim.SetBool("Grounded", isGrounded());
        anim.SetFloat("Vspeed", body.velocity.y);
        anim.SetFloat("Hspeed", body.velocity.x);
        anim.SetInteger("State", state);

        EmberMechanics();

        if (state == 0)
        {
            ControlsNormal();
            AnimationsNormal();
        }
    }

    private void FixedUpdate()
    {
        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        
        // ----- Moving Left & Right -----
        body.velocity = new Vector2(HorizontalInput * xSpeed, body.velocity.y);
    }

    //------------------------------------------------------------ Controls - Normal ------------------------------------------------------------
    private void ControlsNormal()
    {
        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");



        // ----- Jumping - Initialisation -----
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
            Jump();

        // ----- Prevents Sliding Down Hills -----
        if (HorizontalInput == 0f && isGrounded())
        {
            body.constraints = RigidbodyConstraints2D.FreezePositionX;
            body.freezeRotation = true;
        }
        else
        {
            body.constraints = RigidbodyConstraints2D.None;
            body.freezeRotation = true;
        }
    }

    // ----- Bounce Object Mechanics -----
    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Bounce") && (body.velocity.y <= 0f))
        {
            Bounce();
        }
    }

    public void Bounce()
    {
        body.velocity = new Vector2(body.velocity.x, ySpeed * 0.8f);
    }

    //----------------------------------------------------------- Animations - Normal State -----------------------------------------------------------
    private void AnimationsNormal()
    {
        float HorizontalInput = Input.GetAxisRaw("Horizontal");

        // ----- Flip Image According to Player Direction -----
        if (HorizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (HorizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    //----------------------------------------------------------- Other Functions -----------------------------------------------------------

    // ----- Ground Collision Check -----
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capCollider.bounds.center, capCollider.bounds.size - new Vector3(0f, -0.1f, 0f), 0f, Vector2.down, 0.1f, collisionLayer);
        return raycastHit.collider != null && anim.GetCurrentAnimatorStateInfo(0).IsTag("NonGrounded") != true;
    }

    // ----- Jumping - Code -----
    private void Jump()
    {
        float VerticalInput = Input.GetAxisRaw("Vertical");

        // ----- As Long As Down Is Not Held, Player Will Jump -----
        if (VerticalInput > -0.01f)
        {
            body.velocity = new Vector2(body.velocity.x, ySpeed);
        }
        else
        {
        }
    }

    public void EmberMechanics()
    {
        if (emberTint.a > 0)
        {
            emberTint.a -= 0.01f;
            material.SetColor("_Tint", emberTint);
        }

        if (ember > 30)
            ember = 30;
        if (ember < 0)
            ember = 0;

        if (Input.GetKeyDown(KeyCode.E))
        {
            ember += 1;
            emberTint.a += 0.15f;
        }
    }
}