using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] LapDetector mainDetector;
    CarController carController;
    public int lapCompleted = 0;
    int targetSideDetector = 1;
    bool firstLap = true;
    void Start()
    {
        carController = GetComponent<CarController>();
    }
    void Update()
    {
        float accInput = 0.7f;
        if(SetSteeringInput() > 0.3)
        {
            accInput = 0.1f;
        }
        carController.SetInputVector(accInput, SetSteeringInput());
    }

    public float SetSteeringInput()
    {
        Vector2 sideDetectDir = (GetSideDetector().transform.position - transform.position).normalized;
        float angle = Vector2.SignedAngle(sideDetectDir, transform.right.normalized);
        return angle / 90;
    }

    public GameObject GetSideDetector()
    {
        return mainDetector.transform.Find("Side" + targetSideDetector).gameObject;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform == mainDetector.transform) { EnemyLapCompleted(); }
        if (collision.transform.parent != mainDetector.transform) { return; }
        if (collision.transform.name != "Side" + targetSideDetector) { return; }
        if(targetSideDetector == 16)
        {
            targetSideDetector = 1;
            return;
        }
        targetSideDetector++;
    }

    void EnemyLapCompleted()
    {
        if (firstLap) { firstLap = false; return; }
        lapCompleted++;
        if(lapCompleted == 3) 
        {
            GameObject.FindGameObjectWithTag("RaceManager").GetComponent<RaceManager>().RaceFinished(false);
        }
    }
}
