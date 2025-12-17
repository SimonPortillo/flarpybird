using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipePrefab;
    public LogicScript logic;
    private float timer = 0;
    public float heightOffset = 0.8f;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spawnPipe();
    }

    void Update()
    {
        if (!logic.isGameStarted) return;
        if (logic.isGameOver) return;

        var interval = logic.CurrentSpawnInterval;

        if (timer < interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            spawnPipe();
        }
    }
    private void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(pipePrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
