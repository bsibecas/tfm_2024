using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float sprintSpeed;
    public float staminaRegenRate;
    public float maxStamina;
    public float staminaCostPerSecond;
    public float slipDuration = 0.4f;

    private float originalSpeed;
    private float originalSprintSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isSprinting;
    private float currentStamina;
    private float chargeRate = 10;

    private Coroutine recharge;
    public Image staminaBar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
        UpdateStaminaBar();
        originalSpeed = moveSpeed;
        originalSprintSpeed = sprintSpeed;
    }

    private void Update()
    {
        ProcessInput();
        Move();
        UpdateStamina();
    }

    private void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        isSprinting = Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(moveX) > 0 || Mathf.Abs(moveY) > 0) && currentStamina > 0;
    }

    private void Move()
    {
        float speed = isSprinting ? sprintSpeed : moveSpeed;
        rb.velocity = moveDirection * speed;
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);
        while (currentStamina < maxStamina)
        {
            currentStamina += chargeRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            yield return new WaitForSeconds(.05f);
            UpdateStaminaBar();
            yield return null;
        }
    }

    private void UpdateStamina()
    {
        if (isSprinting)
        {
            currentStamina -= staminaCostPerSecond * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else
        {
            if (recharge == null)
            {
                recharge = StartCoroutine(RechargeStamina());
            }
            else if (currentStamina >= maxStamina)
            {
                StopCoroutine(recharge);
                recharge = null;
            }
        }

        UpdateStaminaBar();
    }


    private void UpdateStaminaBar()
    {
        if (currentStamina < 0) currentStamina = 0;
        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Puddle"))
        {
            StartCoroutine(SlipOff());
        }
    }

    private IEnumerator SlipOff()
    {
        if (isSprinting == true)
        {
            sprintSpeed = originalSprintSpeed * 2;
        } else
        {
            moveSpeed = originalSpeed * 3;
        }
        yield return new WaitForSeconds(slipDuration);
        moveSpeed = originalSpeed;
        sprintSpeed = originalSprintSpeed;
    }


}
