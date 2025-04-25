using UnityEngine;

public class TreeRespawner : MonoBehaviour
{
    public GameObject treePrefab; // Префаб дерева (должен иметь HeatItem)
    public Transform[] spawnPoints; // Три точки спавна (задать в инспекторе)

    private float timer = 0f;
    private bool spawningStarted = false;
    private float spawnInterval = 40f;
    private float nextSpawnTime;

    private void Update()
    {
        timer += Time.deltaTime;

        if (!spawningStarted && timer >= 90f)
        {
            spawningStarted = true;
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (spawningStarted && Time.time >= nextSpawnTime)
        {
            SpawnTree();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnTree()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        // Проверяем, нет ли дерева уже на этой позиции
        Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, 0.5f);
        foreach (var col in colliders)
        {
            if (col.GetComponent<HeatItem>())
            {
                return; // Не спавним, если уже есть дерево
            }
        }

        Instantiate(treePrefab, spawnPoint.position, spawnPoint.rotation);
        TextManager.Instance?.ShowMessage("Появилось новое дерево.");
    }
}
