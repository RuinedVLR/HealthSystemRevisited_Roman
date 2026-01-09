using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSystemRevisited_Roman
{
    class HealthSystem
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }

        public void TakeDamage(int damage)
        {
            if(damage < 0)
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
            if(hp < 0)
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
            if(maxHealth <= 0)
            {
                Console.WriteLine("Max health must be greater than zero.");
            }
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }
    }

    class Player
    {
        public string Name { get; private set; }
        public HealthSystem Health { get; private set; }
        public HealthSystem Shield { get; private set; }

        public void TakeDamage(int damage)
        {
            if(damage < 0)
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
                return($"{Name} is in perfect health!");
            }
            else if (Health.CurrentHealth >= 70)
            {
                return ($"{Name} has minor injuries.");
            }
            else if (Health.CurrentHealth >= 40)
            {
                return ($"{Name} is seriously injured.");
            }
            else if (Health.CurrentHealth > 0)
            {
                return ($"{Name} is critically injured!");
            }
            else
            {
                return ($"{Name} has fallen in battle.");
            }
        }

        public Player(string name, int health, int shield)
        {
            Name = name;
            Health = new HealthSystem(health);
            Shield = new HealthSystem(shield);
        }
    }

    internal class Program
    {
        static string name;
        static Random rand = new Random();
        static bool isPlaying = true;
        static Player player = new Player(name, 100, 50);

        static void Main(string[] args)
        {
            while(name == null || name == "")
            {
                GetPlayerName();
            }

            while (isPlaying)
            {
                HUD();
                if()
            }
        }

        static void GetPlayerName()
        {
            Console.Write("Enter player name: ");
            name = Console.ReadLine();
            Console.Clear();
        }

        static void HUD()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("-------------------------");
            Console.WriteLine($"          {player.Name}          ");
            Console.WriteLine($"Health: {player.Health}   Shield: {player.Shield}   Status: {player.GetStatusString()}");
            Console.WriteLine();
            Console.WriteLine("Press D key to Take Damage");
            Console.WriteLine("Press H key to Heal");
            Console.WriteLine("Press R key to Reset Health");

            Console.ReadKey(true);
        }
    }
}
