using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D body;
    CapsuleCollider2D collider;

    public float speed = 4.5f;

    public float jumpForce = 5f;
    public ForceMode2D jumpType = ForceMode2D.Impulse;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int jumpCount = 0;

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        // anim.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        bool grounded = false;
        Vector3 max = collider.bounds.max;
        Vector3 min = collider.bounds.min;

        Vector2 bottomRight = new Vector2(max.x, min.y - 0.1f);
        Vector2 bottomLeft = new Vector2(min.x, min.y - 0.1f);

        Debug.DrawLine(bottomLeft, bottomRight, Color.red);

        Collider2D hit = Physics2D.OverlapArea(bottomLeft, bottomRight);

        if (hit != null)
        {
            grounded = true;
            jumpCount = 0;
        }


        if ((grounded || jumpCount < 2) && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, jumpType);
            jumpCount++;
            // EventBus.Publish(EventBus.EventType.PlayerJump);
        }
    }

    // public void Death()
    // {
    //     anim.SetBool("dead", true);
    //     StartCoroutine(DeathTimer2());
    // }

    // IEnumerator DeathTimer()
    // {
    //     yield return new WaitForSeconds(2f);
    //     Destroy(gameObject);
    // }

    // IEnumerator DeathTimer2()
    // {
    //     speed = 0;
    //     AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

    //     //wait until death starts
    //     while (!info.IsName("Death"))
    //     {
    //         yield return new WaitForEndOfFrame();
    //         info = anim.GetCurrentAnimatorStateInfo(0);

    //     }

    //     //wait for death to finish
    //     while (info.normalizedTime < 1) {
    //         Debug.Log("Animation at " + info.normalizedTime);
    //         yield return new WaitForEndOfFrame();
    //         info = anim.GetCurrentAnimatorStateInfo(0);
    //     }

    //     EventBus.Publish(EventBus.EventType.PlayerDie);
    //     Destroy(gameObject);
    // }
}
