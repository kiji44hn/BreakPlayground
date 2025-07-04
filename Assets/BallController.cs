using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector2 launchForce = new Vector2(0, 300f); // 上方向の力
    private Rigidbody2D rb;
    private Vector3 startPos;
    public ParticleSystem myParticleSystem; // エフェクトをアタッチする
    public GameObject breakEffect; // ブロック破壊エフェクトをアタッチする

    private bool effectTriggered = false; // トリガー制御用フラグ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position; // 初期位置を記憶
    }

    void Update()
    {
        // スペースキーでボールを動かす
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.zero; // 速度をリセット
            rb.AddForce(launchForce); // 力を加えてボールを動かす
        }

        // ボールの画面内制限
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -8f, 8f); // X方向の範囲制限
        pos.y = Mathf.Clamp(pos.y, -2.5f, 5f); // Y方向の範囲制限
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var main = myParticleSystem.main;
        main.startColor = new Color(0.9433962f, 0.9027687f, 0.6719473f); // 好きな色（R, G, B）

        if (collision.gameObject.CompareTag("Block"))
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity); // パーティクル生成
            Destroy(collision.gameObject, 0.5f); // 0.5秒後に破壊
            rb.velocity = Vector2.zero; // 速度をリセット
            transform.position = startPos; // 初期位置に戻す
        }
        else if (!effectTriggered && collision.gameObject.CompareTag("Wall"))
        {
            effectTriggered = true; // トリガーを一度だけ実行
            myParticleSystem.Play(); // パーティクル再生

            StartCoroutine(ResetEffectTrigger()); // トリガーのリセット処理
            TriggerEffectWithDuration(10f); // 10秒間再生して停止
        }
    }

    IEnumerator ResetEffectTrigger()
    {
        yield return new WaitForSeconds(2f); // 2秒待機後にリセット
        effectTriggered = false;
    }

    void TriggerEffectWithDuration(float duration)
    {
        myParticleSystem.Play();
        StartCoroutine(StopEffectAfterDuration(duration));
    }

    IEnumerator StopEffectAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        myParticleSystem.Stop();
        myParticleSystem.Clear();
    }
}
