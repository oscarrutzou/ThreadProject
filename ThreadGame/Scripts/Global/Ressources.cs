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
            food = 10;
            money = 0;
            monsterDrop = 0;
        }

        public static bool GetFood(int foodAmount)
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

        public static bool GetMoney(int moneyAmount)
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

        public static bool GetMonsterDrop(int dropAmount)
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
