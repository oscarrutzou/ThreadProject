using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadGame
{
    public static class Ressources
    {
        public readonly static object foodLock = new object();
        public readonly static object moneyLock = new object();
        public readonly static object monsterDropLock = new object(); //For slime

        public static int food {  get; private set; }
        public static int money {  get; private set; }
        public static int monsterDrop {  get; private set; }

        public static void SetStartRessources()
        {
            money = 10;
            food = 10;
            monsterDrop = 10;
        }

        /// <summary>
        /// This is how much food is needed for the action, so it dosent die when spawned.
        /// </summary>
        /// <param name="requiredFood"></param>
        /// <param name="requiredMoney"></param>
        /// <returns></returns>
        public static bool TryUseMoneyCheckFood(int requiredFood, int requiredMoney)
        {
            lock (moneyLock)
            {
                // Check if we have enough money
                if (money - requiredMoney < 0)
                {
                    return false;
                }

                lock (foodLock)
                {
                    // Check if we have enough food
                    if (food - requiredFood < 0)
                    {
                        return false;
                    }

                    // If we have enough money and food so it dosen't die immediately, make the purchase
                    money -= requiredMoney;
                    return true;
                }
            }
        }

        public static bool UseFood(int foodAmount)
        {
            lock (foodLock)
            {
                if (food - foodAmount >= 0) {
                    food -= foodAmount;
                    return true;
                }

                return false;
            }
        }

        public static bool UseMoney(int moneyAmount)
        {
            lock (moneyLock)
            {
                if (money - moneyAmount >= 0)
                {
                    money -= moneyAmount;
                    return true;
                }

                return false;
            }
        }

        public static bool UseMonsterDrops(int dropAmount)
        {
            lock (monsterDropLock)
            {
                if (monsterDrop - dropAmount >= 0)
                {
                    monsterDrop -= dropAmount;
                    return true;
                }

                return false;
            }
        }

        public static void AddFood(int foodAmount)
        {
            lock (foodLock)
            {
                food += foodAmount;
            }
        }
        public static void AddMoney(int moneyAmount)
        {
            lock (moneyLock)
            {
                money += moneyAmount;
            }
        }
        public static void AddMonsterDrops(int dropAmount)
        {
            lock (monsterDropLock)
            {
                monsterDrop += dropAmount;
            }
        }

    }
}
