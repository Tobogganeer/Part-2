using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : MonoBehaviour
{
    public float speed = 2f;
    public float maxHealth = 100f;
    public Transform graphics;
    public UIBar healthBar; // I don't really like adding the UI stuff here but oh well, keep it simple

    Rigidbody2D rb;
    Animator animator;
    Camera cam;

    Vector2 destination;
    float health;
    bool moving;
    bool facingRight;
    bool dead => health <= 0;
    bool clickingOnSelf;
    bool clickingOnCanvas => EventSystem.current.IsPointerOverGameObject();

    const float DestinationDistanceThreshold = 0.05f; // 5cm is close enough
    static readonly string HealthPlayerPrefsKey = "KnightHealth";
    static readonly string PosXPlayerPrefsKey = "KnightPosX";
    static readonly string PosYPlayerPrefsKey = "KnightPosY";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = graphics.GetComponent<Animator>();
        cam = Camera.main; // I know it's cached internally now...

        //health = maxHealth;
        // Load health from prefs
        health = PlayerPrefs.GetFloat(HealthPlayerPrefsKey, maxHealth);
        // Load the position from prefs
        destination = new Vector3(
            PlayerPrefs.GetFloat(PosXPlayerPrefsKey, 0),
            PlayerPrefs.GetFloat(PosYPlayerPrefsKey, 0));
        transform.position = destination;
        
        // Set the health, avoiding lerping/smoothing
        UpdateHealthBar(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !clickingOnSelf && !clickingOnCanvas && !dead)
        {
            destination = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 offset = destination - (Vector2)transform.position;
            facingRight = offset.x > 0;
            // Mirror seems to not work, probably for humanoid rigs
            animator.SetBool(nameof(facingRight), facingRight);
            graphics.localScale = new Vector3(facingRight ? -1f : 1f, 1f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !dead && !clickingOnCanvas)
            animator.SetTrigger("attack");

        animator.SetBool(nameof(moving), moving);
        animator.SetBool(nameof(dead), dead);

        if (dead)
            // Stay still when we get back up
            destination = transform.position;
    }

    private void FixedUpdate()
    {
        // Move towards our destination, but don't go past it
        Vector2 newPosition = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        // Check if we still have a bit to move
        moving = (newPosition - destination).sqrMagnitude > DestinationDistanceThreshold * DestinationDistanceThreshold;

        if (moving && !dead)
            rb.MovePosition(newPosition);
    }

    private void OnMouseDown()
    {
        clickingOnSelf = true;
        Damage(10f);
    }

    private void OnMouseUp()
    {
        clickingOnSelf = false;
    }

    private void OnDestroy()
    {
        // Save the health and position when we are destroyed (called on scene unload)
        PlayerPrefs.SetFloat(HealthPlayerPrefsKey, health);
        PlayerPrefs.SetFloat(PosXPlayerPrefsKey, transform.position.x);
        PlayerPrefs.SetFloat(PosYPlayerPrefsKey, transform.position.y);
        PlayerPrefs.Save();
    }

    public void Damage(float amount)
    {
        health = Mathf.Clamp(health - amount, 0f, maxHealth);
        if (amount > 0f && !dead)
            animator.SetTrigger("damage");

        UpdateHealthBar();
    }

    public void Heal(float amount) => Damage(-amount);

    void UpdateHealthBar(bool lerp = true)
    {
        if (healthBar != null)
            healthBar.SetFill(health, 0f, maxHealth, !lerp);
    }
}
