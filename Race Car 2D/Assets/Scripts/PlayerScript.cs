using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    CarController carController;
    void Start()
    {
        carController = GetComponent<CarController>();    
    }
    // Update is called once per frame
    void Update()
    {
        carController.SetInputVector(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            carController.DriftTires(true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            carController.DriftTires(false);
        }
    }
}
