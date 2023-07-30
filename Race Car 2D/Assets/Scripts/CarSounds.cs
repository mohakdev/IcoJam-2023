using RadiantTools.AudioSystem;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    CarController carController;
    Rigidbody2D rbCar;
    [SerializeField] AudioSource engineSFX;
    void Start()
    {
        rbCar = GetComponent<Rigidbody2D>();
        carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        engineSFX.volume = Mathf.Clamp(rbCar.velocity.magnitude / 25,0f,1f);
        engineSFX.pitch = Mathf.Lerp(0, 1.5f, engineSFX.volume);
    }

}
