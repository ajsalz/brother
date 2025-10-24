using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Boat : MonoBehaviour
{
    public Rigidbody2D rgbd;
    private InputManager input;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 150f;
    [SerializeField] private float drag = 2f;

    [Header("Fire Settings")]
    public bool frenchFry = false;
    public float fireDuration = 3f;
    public GameObject FireEffect; // attach a fire particle prefab to this in Inspector

    private bool canPickUp = true;

    public InventoryManager inventory;

    private void Awake()
    {
        if (rgbd == null)
            rgbd = GetComponent<Rigidbody2D>();

        rgbd.linearDamping = drag; 
        rgbd.angularDamping = 1f;

        //input = new KeyboardInput();
        input = gameObject.AddComponent<PaddleInput>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = input.ProcessInput().normalized;
        float magnitude = Mathf.Clamp01(input.Magnitude());

        BoatRotation(direction.x, magnitude);
        BoatMovement(direction.y, magnitude);
    }

    private void BoatRotation(float horizontalInput, float magnitude)
    {
        // Rotate the boat based on horizontal input
        float rotationAmount = -horizontalInput * rotationSpeed * Time.fixedDeltaTime * magnitude;
        rgbd.MoveRotation(rgbd.rotation + rotationAmount);
    }

    private void BoatMovement(float verticalInput, float magnitude)
    {
        Vector2 forward = rgbd.transform.right;
        Vector2 force = forward * verticalInput * magnitude * moveSpeed;
        rgbd.AddForce(force);

        if(rgbd.linearVelocity.magnitude > moveSpeed)
        {
            rgbd.linearVelocity = rgbd.linearVelocity.normalized * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("fire"))
        {
            if (!frenchFry)
                StartCoroutine(CatchFire());
            Debug.Log("she's burning");
        }

        if (other.CompareTag("food"))
        {
            if (frenchFry)
            {
                Debug.Log("burning up up up");
                Destroy(other.gameObject);
            }
            else if (canPickUp)
            {
                Debug.Log("ay you can get stuff hehehe");
                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator CatchFire()
    {
        frenchFry = true;
        canPickUp = false;
        Debug.Log("Fire Called");

        if (Crafting.Instance != null)
        {
            Crafting.Instance.ClearCollectedAndInventory();
        }

        if (FireEffect != null)
        {
            ParticleSystem ps = FireEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Clear();
                ps.Play();
            }
            Debug.Log("fire on boat");
        }

        yield return new WaitForSeconds(fireDuration);

        frenchFry = false;
        canPickUp = true;

        if (FireEffect != null)
        {
            ParticleSystem ps = FireEffect.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            Debug.Log("fire begone");
        }
    }




}
