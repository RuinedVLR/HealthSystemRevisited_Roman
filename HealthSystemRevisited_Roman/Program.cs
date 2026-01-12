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

            HUD();

            while (isPlaying)
            {
                Input();
                HUD();
            }
        }

        static void GetPlayerName()
        {
            Console.Write("Enter player name: ");
            name = Console.ReadLine();
            Console.Clear();
        }

        static void Input()
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            if(key == ConsoleKey.D)
            {
                int damage = rand.Next(5, 21);
                player.TakeDamage(damage);
                Console.WriteLine();
                Console.WriteLine($"{player.GetName()} took {damage} damage");
                Console.ReadKey(true);
            }
            else if(key == ConsoleKey.H)
            {
                int heal = rand.Next(5, 16);
                player.Health.Heal(heal);
                Console.WriteLine();
                Console.WriteLine($"{player.GetName()} healed {heal} health");
                Console.ReadKey(true);
            }
            else if(key == ConsoleKey.R)
            {
                player.Health.Reset();
                player.Shield.Reset();
                Console.WriteLine();
                Console.WriteLine($"{player.GetName()}'s health and shield have been reset");
                Console.ReadKey(true);
            }
        }

        static void HUD()
        {
            if(player.Health.CurrentHealth <= 0)
            {
                isPlaying = false;
                Defeat();
                return;
            }

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("-------------------------");
            Console.WriteLine($"          {player.GetName()}          ");
            Console.WriteLine($"Health: {player.GetHealth()}   Shield: {player.GetShield()}   Status: {player.GetStatusString()}");
            Console.WriteLine();
            Console.WriteLine("Press D key to Take Damage");
            Console.WriteLine("Press H key to Heal");
            Console.WriteLine("Press R key to Reset Health");
        }

        static void Defeat()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("-------------------------");
            Console.WriteLine($"       {player.GetName()} has fallen!       ");
            Console.WriteLine("       Game Over!       ");
            Console.ReadKey(true);
        }
    }
}
