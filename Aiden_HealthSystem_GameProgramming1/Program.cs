using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
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

        static int xp = 0;
        static int level = 1;
        static int xpForLevel = 100;

        #region Requirements
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

            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,0}{1,26}{2,19}{3,12}{4,13}{5,10}", $"Health = {health}/100 |", $"Status = {healthStatus} |", $"Shield = {shield}/100 |", $"Lives = {lives} |", $"Xp = {xp}/{xpForLevel} |", $"Level = {level}");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
        }
        static void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Console.WriteLine("ERROR: Damage had a negative value; Damage MUST have a positive value.");
                Console.ReadKey();

                return;
            }

            int carriedDamage = 0;
            if(shield >= 1)
            {
                if (shield - damage < 0)
                {
                    carriedDamage = (shield - damage) * -1;
                }

                shield -= damage;

                if (health - carriedDamage <= 0)
                {
                    health = 0;
                }
                else
                {
                    health -= carriedDamage;
                }

                    carriedDamage = 0;

            }
            else
            {
                health -= damage;

                if (health - damage <= 0)
                {
                    health = 0;
                }
            }

            if (shield < 0)
            {
                shield = 0;
            }

        }

        static void Heal(int hp)
        {
            if (hp < 0)
            {
                Console.WriteLine("ERROR: hp had a negative value; hp MUST have a positive value.");
                Console.ReadKey();

                return;
            }

            health += hp;
            if(health >= 100)
            {
                health = 100;
            }
        }

        static void RegenerateShield(int hp)
        {
            if (hp < 0)
            {
                Console.WriteLine("ERROR: hp had a negative value; hp MUST have a positive value.");
                Console.ReadKey();

                return;
            }

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
        #endregion

        #region Extra Mile

        static void IncreaseXP(int exp)
        {
            xp += exp;

            if(xp >= xpForLevel)
            {
                xp -= xpForLevel;
                level++;
            }

            xpForLevel = 100 * level;

        }

        #endregion

        #region Unit Test
        static void UnitTestHealthSystem()
        {
            Debug.WriteLine("Unit testing Health System started...");

            // TakeDamage()

            // TakeDamage() - only shield
            shield = 100;
            health = 100;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield and health
            shield = 10;
            health = 100;
            lives = 3;
            TakeDamage(50);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 60);
            Debug.Assert(lives == 3);

            // TakeDamage() - only health
            shield = 0;
            health = 50;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 40);
            Debug.Assert(lives == 3);

            // TakeDamage() - health and lives
            shield = 0;
            health = 10;
            lives = 3;
            TakeDamage(25);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield, health, and lives
            shield = 5;
            health = 100;
            lives = 3;
            TakeDamage(110);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            TakeDamage(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Heal()

            // Heal() - normal
            shield = 0;
            health = 90;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 95);
            Debug.Assert(lives == 3);

            // Heal() - already max health
            shield = 90;
            health = 100;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // Heal() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            Heal(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // RegenerateShield()

            // RegenerateShield() - normal
            shield = 50;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 60);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - already max shield
            shield = 100;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            RegenerateShield(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Revive()

            // Revive()
            shield = 0;
            health = 0;
            lives = 2;
            Revive();
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 1);

            Debug.WriteLine("Unit testing Health System completed.");
            Console.Clear();
        }

        static void UnitTestXPSystem()
        {
            Debug.WriteLine("Unit testing XP / Level Up System started...");

            // IncreaseXP()

            // IncreaseXP() - no level up; remain at level 1
            xp = 0;
            level = 1;
            IncreaseXP(10);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 1);

            // IncreaseXP() - level up to level 2 (costs 100 xp)
            xp = 0;
            level = 1;
            IncreaseXP(105);
            Debug.Assert(xp == 5);
            Debug.Assert(level == 2);

            // IncreaseXP() - level up to level 3 (costs 200 xp)
            xp = 0;
            level = 2;
            IncreaseXP(210);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 3);

            // IncreaseXP() - level up to level 4 (costs 300 xp)
            xp = 0;
            level = 3;
            IncreaseXP(315);
            Debug.Assert(xp == 15);
            Debug.Assert(level == 4);

            // IncreaseXP() - level up to level 5 (costs 400 xp)
            xp = 0;
            level = 4;
            IncreaseXP(499);
            Debug.Assert(xp == 99);
            Debug.Assert(level == 5);

            Debug.WriteLine("Unit testing XP / Level Up System completed.");
            Console.Clear();
        }

        #endregion


        static void Main(string[] args)
        {
            UnitTestHealthSystem();
            UnitTestXPSystem();
            Console.ReadKey(false);

        }
    }
}
