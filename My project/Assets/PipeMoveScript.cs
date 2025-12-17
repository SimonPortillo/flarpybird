using System.Diagnostics;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float deadZone = -4f;
    private LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (!logic.isGameStarted) return;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
            UnityEngine.Debug.Log("Pipe Deleted");
        }
        transform.position += Vector3.left * logic.pipeMoveSpeed * Time.deltaTime;
        
    }
}
