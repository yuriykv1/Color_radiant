using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, Health
{
    [SerializeField] private int _startHealth = 100;
    [SerializeField] private TMP_Text _healthText;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = _startHealth;
        UpdateHealthText();
    }

    void Update()
    {
        // Можно добавить логику смерти или другое
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0)
            _currentHealth = 0;

        UpdateHealthText();

        // Тут можно вызвать метод смерти, если хп 0
        if (_currentHealth == 0)
        {
            Die();
        }
    }

    private void UpdateHealthText()
    {
        _healthText.text = _currentHealth.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(8);
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        // Тут можно добавить логику смерти: проигрыш, анимация и т.д.
    }
}
