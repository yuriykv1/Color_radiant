using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, Health
{
    [SerializeField] private int _startHealth;
    [SerializeField] private TMP_Text _healthText;
    private int _currentHealth;
    void Start()
    {
        _currentHealth = _startHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthText.text = _currentHealth.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(8);
        }
    }
}
