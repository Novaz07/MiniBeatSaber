using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; // Prefab del cubo
    public Vector3 spawnAreaSize = new Vector3(5, 2, 0); // Tamaño del área de spawn
    public Vector3 spawnAreaCenter = new Vector3(0, 1, 10); // Centro del área de spawn
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
            Rigidbody rb = cube.GetComponent<Rigidbody>();

            if (playerTransform != null)
            {
                Vector3 direction = (playerTransform.position - cube.transform.position).normalized;
                rb.velocity = direction * cubeSpeed; // Mueve el cubo hacia el jugador
            }
            else
            {
                rb.velocity = Vector3.back * cubeSpeed; // Fallback en caso de que no haya jugador asignado
            }

            Destroy(cube, 10f); // Destruir después de 10s

            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
