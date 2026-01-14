using System;

class HealthSystem
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Console.WriteLine("Damage cannot be negative.");
            return;
        }

        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
    }

    public void Reset()
    {
        CurrentHealth = MaxHealth;
    }

    public void Heal(int hp)
    {
        if (hp < 0)
        {
            Console.WriteLine("Heal amount cannot be negative.");
            return;
        }

        CurrentHealth += hp;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public HealthSystem(int maxHealth)
    {
        if (maxHealth <= 0)
        {
            Console.WriteLine("Max health must be greater than zero.");
        }
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }
}