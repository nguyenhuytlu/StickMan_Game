using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    private float speed = 10f;
    private float jumpPower = 35f;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; // thuc hien nhay double tren khong
    private float coyoteCouter; //bao nhieu lau ke tu khi nguoi choi chay khoi box

    [Header("Multiple Jumps")]
    [SerializeField]private int extraJumps; // nhay nhieu lan
    private int jumpCounter;

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
         horizontalInput = Input.GetAxis("Horizontal");

        //dao chieu mat nhan vat
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput < -0.01f) 
            transform.localScale = new Vector3(-1,1,1);


        //cai dat hanh dong
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //jump
        if(Input.GetKeyDown(KeyCode.W))
            Jump();
        
        //dieu chinh do cao nhay
        if(Input.GetKeyDown(KeyCode.W) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y /2);

        //kiem tra nguoi choi co o tren tuong khong
        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if(isGrounded())
            {
                coyoteCouter = coyoteTime; //reset thoi gian nhay khi o tren mat dat
                jumpCounter = extraJumps; // reset so lan nhay kep
            }
            else
                coyoteCouter -= Time.deltaTime;//thoi gian bat dau giam khi khong o tren mat dat
        }
    }

    private void Jump()
    {
        if (coyoteCouter < 0 && !onWall() && jumpCounter <=0) return;
        SoundManager.instance.PlaySound(jumpSound);

        if(onWall())
            wallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                //neu khong o tren mat dat va lon hon khong thi nhay "normal"
                if(coyoteCouter > 0 )
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if(jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }
            //dat lai toan bo ve khong tranh nguoi choi nhay 2 lan khi an 2 lan W
            coyoteCouter = 0;
        }
    }
    private void wallJump()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0) , 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
