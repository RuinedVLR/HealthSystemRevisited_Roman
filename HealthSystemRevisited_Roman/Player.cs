using System;

class Player
{
    public string Name { get; set; }
    public HealthSystem Health { get; private set; }
    public HealthSystem Shield { get; private set; }
    public ConsoleColor StatusColor { get; private set; }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Console.WriteLine("Damage cannot be negative.");
            return;
        }

        int shieldDamage = Math.Min(Shield.CurrentHealth, damage);
        Shield.TakeDamage(shieldDamage);
        int remainingDamage = damage - shieldDamage;
        Health.TakeDamage(remainingDamage);
    }

    public string GetStatusString()
    {
        if (Health.CurrentHealth == 100)
        {
            StatusColor = ConsoleColor.DarkGreen;
            return ($"{Name} is in perfect health!");
        }
        else if (Health.CurrentHealth >= 80)
        {
            StatusColor = ConsoleColor.Green;
            return ($"{Name} has minor injuries.");
        }
        else if (Health.CurrentHealth >= 50)
        {
            StatusColor = ConsoleColor.Yellow;
            return ($"{Name} is wounded.");
        }
        else if (Health.CurrentHealth >= 30)
        {
            StatusColor = ConsoleColor.DarkYellow;
            return ($"{Name} is seriously injured.");
        }
        else if (Health.CurrentHealth > 0)
        {
            StatusColor = ConsoleColor.Red;
            return ($"{Name} is critically injured!");
        }
        else
        {
            StatusColor = ConsoleColor.DarkRed;
            return ($"{Name} has fallen!");
        }
    }

    public int GetHealth()
    {
        return Health.CurrentHealth;
    }

    public int GetShield()
    {
        return Shield.CurrentHealth;
    }

    public string GetName()
    {
        return Name;
    }

    public ConsoleColor GetStatusColor()
    {
        GetStatusString();
        return StatusColor;
    }

    public Player(string name, int health, int shield)
    {
        Name = name;
        Health = new HealthSystem(health);
        Shield = new HealthSystem(shield);
    }
}