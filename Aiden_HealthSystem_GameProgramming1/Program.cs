using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Aiden_HealthSystem_GameProgramming1
{
    internal class Program
    {
        static int health = 100; //0 to 100
        static string healthStatus; //0 to 100
        static int shield = 100;
        static int lives = 3; //0 to 99
        static void ShowHUD()
        {
            if(health == 100)
            {
                healthStatus = "Perfect Health";
            }
            else if (health <= 99 && health >= 76)
            {
                healthStatus = "Healthy";
            }
            else if(health <= 75 && health >= 51)
            {
                healthStatus = "Hurt";
            }
            else if (health <= 50 && health >= 11)
            {
                healthStatus = "badly Hurt";
            }
            else if (health <= 10 && health >= 0)
            {
                healthStatus = "Imminent Danger";
            }

            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("{0,0}{1,26}{2,19}{3,10}", $"Health = {health}/100 |", $"Status = {healthStatus} |", $"Shield = {shield}/100 |", $"Lives = {lives}");
            Console.WriteLine("--------------------------------------------------------------------------");
        }

        static void TakeDamage(int damage)
        {
            int carriedDamage = 0;
            if(shield >= 1)
            {
                if (shield - damage < 0)
                {
                    carriedDamage = (shield - damage) * -1;
                }

                shield -= damage;
                health -= carriedDamage;
                carriedDamage = 0;

            }
            else
            {
                health -= damage;
                if(health - damage <=0)
                {
                    health = 0;
                }
            }

            if (shield < 0)
            {
                shield = 0;
            }

            if(health <=0 && shield <= 0 && lives > 0)
            {
                Revive();
            }
        }

        static void Heal(int hp)
        {
            health += hp;
            if(health >= 100)
            {
                health = 100;
            }
        }

        static void RegenerateShield(int hp)
        {
            shield += hp;
            if (shield >= 100)
            {
                shield = 100;
            }
        }

        static void Revive()
        {
            shield = 100;
            health = 100;
            lives --;
        }

        static void Main(string[] args)
        {
            ShowHUD();
            Console.ReadKey(false);
            Console.Clear();
            TakeDamage(99);
            ShowHUD();
            Console.ReadKey(false);
            Console.Clear();
            TakeDamage(99);
            ShowHUD();
            Console.ReadKey(false);
            Console.Clear();
            Heal(50);
            RegenerateShield(50);
            ShowHUD();
            Console.ReadKey(false);
            Console.Clear();
        }
    }
}
