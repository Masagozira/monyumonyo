using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject prefabToSpawn; // プレハブとして指定するオブジェクト
    public Vector2 spawnPosition; // 生成位置の座標
    public float objectLifetime = 5f; // オブジェクトの寿命
    public float spawnInterval = 4f; // 生成間隔

    void Start()
    {
        // 一定間隔でGenerateObjectメソッドを呼び出す
        InvokeRepeating("GenerateObject", 0f, spawnInterval);
    }

    void GenerateObject()
    {
        // 指定された座標にオブジェクトを生成
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // オブジェクトの寿命が終わったら削除
        Destroy(spawnedObject, objectLifetime);
    }
}
