using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector2 launchForce = new Vector2(0, 300f);
    private Rigidbody2D rb;
    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(launchForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            collision.gameObject.SetActive(false); // ブロックを消す
            rb.velocity = Vector2.zero;
            transform.position = startPos; // ボールを初期位置へ
        }
    }
}
