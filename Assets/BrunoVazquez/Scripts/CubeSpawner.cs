using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; // Prefab del cubo
    public Vector3 spawnAreaSize = new Vector3(5, 2, 0); // Tama�o del �rea de spawn
    public Vector3 spawnAreaCenter = new Vector3(0, 1, 10); // Centro del �rea de spawn
    public float spawnRate = 1.5f; // Tiempo entre spawns
    public float cubeSpeed = 5f; // Velocidad de los cubos
    public Transform playerTransform; // Referencia al jugador (XR Rig)

    private bool spawning = true;

    void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    IEnumerator SpawnCubes()
    {
        while (spawning)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
                spawnAreaCenter.z
            );

            GameObject cube = Instantiate(cubePrefab, randomPosition, Quaternion.identity);
            cube.AddComponent<CubeMovement>().SetTarget(playerTransform, cubeSpeed); // Asigna el movimiento

            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
