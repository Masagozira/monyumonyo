using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleSystem Clash_2;
    public float effectInterval = 1f; // エフェクトの再生間隔（秒）
    public float effectDuration = 3f; // エフェクトの再生継続時間（秒）

    private bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("woll")  && !isColliding)
        {
            isColliding = true;
            StartCoroutine(PlayParticleEffect());
        }

         if (collider.CompareTag("Destroyobj")  && !isColliding)
        {
            isColliding = true;
            StartCoroutine(PlayParticleEffect());
        }
    }

    private IEnumerator PlayParticleEffect()
    {
        float elapsedTime = 0f;

        while (isColliding && elapsedTime < effectDuration)
        {
            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(Clash_2);

            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = transform.position;

            // パーティクルを発生させる。
            newParticle.Play();

            // エフェクト再生間隔ごとに待機
            yield return new WaitForSeconds(effectInterval);

            // 経過時間を更新
            elapsedTime += effectInterval;
        }

        // 一定時間後にエフェクトの生成を停止
        isColliding = false;
    }
}
