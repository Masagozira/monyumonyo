// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EffectReferences : MonoBehaviour
// {
//     // PlayerEffectスクリプトへの参照
//     [SerializeField] public PlayerEffect playerEffect;

//     private void Update()
//     {
//         transform.rotation = Quaternion.Euler(0, 0, 0); // カメラがZ軸を基準に回転しないようにする

//         // キー入力に応じてエフェクトを再生
//         if (Input.GetKeyDown(KeyCode.W))
//         {
//             TriggerGoodSmellEffect();
//         }
//         if (Input.GetKeyDown(KeyCode.S))
//         {
//             TriggerBadSmellEffect();
//         }
//         if (Input.GetKeyDown(KeyCode.U))
//         {
//             TriggerFailedSmellChangeEffect();
//         }

//     }

//     // 例: 何かしらのトリガーで呼ばれるメソッド
//     public void TriggerGoodSmellEffect()
//     {
//         // プレイヤーのいい匂いエフェクトを再生
//         playerEffect.FloEffect();
//     }

//     public void TriggerBadSmellEffect()
//     {
//         // プレイヤーのまずい匂いエフェクトを再生
//         playerEffect.OdrEffect();
//     }

//     public void TriggerFailedSmellChangeEffect()
//     {
//         // プレイヤーの匂い変更失敗エフェクトを再生
//         playerEffect.FailEffect();
//     }
// }
