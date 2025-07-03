using System.Diagnostics;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector2 launchForce = new Vector2(0, 300f); // 上方向の力
    private Rigidbody2D rb;
    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        // スペースキー入力でボールを動かす
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.zero; // 速度をリセット
            rb.AddForce(launchForce); // 上方向に力を加える
        }

        // ボールの位置を画面内に制限
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -8f, 8f); // X方向の制限
        pos.y = Mathf.Clamp(pos.y, -0.03f, 5f); // Y方向の制限
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            collision.gameObject.SetActive(false); // ブロックを消す
            rb.velocity = Vector2.zero; // 速度をリセット
            transform.position = startPos; // ボールを初期位置へ戻す
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero; // 速度をリセット
        }
    }
}
