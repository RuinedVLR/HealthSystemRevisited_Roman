using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSystemRevisited_Roman
{
    internal class Program
    {
        static string _name;
        static Random _rand = new Random();
        static bool _isPlaying = true;
        static Player _player = new Player(_name, 100, 50);

        static void Main(string[] args)
        {
            while(_name == null || _name == "")
            {
                GetPlayerName();
            }

            HUD();

            while (_isPlaying)
            {
                Input();
                HUD();
            }
        }

        static void GetPlayerName()
        {
            Console.Write("Enter player name: ");
            _name = Console.ReadLine();
            _player.Name = _name;
            Console.Clear();
        }

        static void Input()
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            if(key == ConsoleKey.D)
            {
                int damage = _rand.Next(1, 21);
                _player.TakeDamage(damage);
                Console.WriteLine();
                Console.WriteLine($"{_player.GetName()} took {damage} damage");
                Console.ReadKey(true);
            }
            else if(key == ConsoleKey.H)
            {
                int heal = _rand.Next(1, 21);
                _player.Health.Heal(heal);
                Console.WriteLine();
                Console.WriteLine($"{_player.GetName()} healed {heal} health");
                Console.ReadKey(true);
            }
            else if(key == ConsoleKey.R)
            {
                _player.Health.Reset();
                _player.Shield.Reset();
                Console.WriteLine();
                Console.WriteLine($"{_player.GetName()}'s health and shield have been reset");
                Console.ReadKey(true);
            }
        }

        static void HUD()
        {
            if(_player.Health.CurrentHealth <= 0)
            {
                _isPlaying = false;
                Defeat();
                return;
            }

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("--------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"          {_player.GetName()}          ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Health: {_player.GetHealth()}   ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"Shield: {_player.GetShield()}   ");
            Console.ForegroundColor = _player.GetStatusColor();
            Console.Write($"Status: { _player.GetStatusString()}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine("Press D key to Take Damage");
            Console.WriteLine("Press H key to Heal");
            Console.WriteLine("Press R key to Reset Health");
        }

        static void Defeat()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"       {_player.GetName()} has fallen!       ");
            Console.WriteLine("          Game Over!       ");
            Console.ReadKey(true);
        }
    }
}
