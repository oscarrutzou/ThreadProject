using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ThreadGame
{
    public class UIOverlay
    {

        private GuiField foodField;
        private GuiField moneyField;
        private GuiField dropsField;
        private GuiField populationField;

        private GuiField chefSpawnSquare;
        private GuiField chefSpawnSquareText;

        private GuiField minerSpawnSquare;
        private GuiField minerSpawnSquareText;

        private GuiField fighterSpawnSquare;
        private GuiField fighterSpawnSquareText;


        private int costWorker = 2;
        private int maxCostPerWorker = 15;
        private Button buyChefBtn;
        private Button buyMinerBtn;
        private Button buyFigtherBtn;
        private Button quitGameBtn;

        private Thread foodThread;
        private Thread moneyThread;
        private Thread costWorkerScaleThread;

        private Random rand = new Random();
        public void InitUI()
        {
            InitTextFields();
            InitSpawnSquares();
            InitSpawnSquareText();
            InitButtons();

            moneyThread = new Thread(PassiveMoneyGen);
            moneyThread.IsBackground = true;
            moneyThread.Start();

            foodThread = new Thread(PassiveFoodGen);
            foodThread.IsBackground = true;
            foodThread.Start();

            costWorkerScaleThread = new Thread(ScaleCost);
            costWorkerScaleThread.IsBackground = true;
            costWorkerScaleThread.Start();

        }

        private void PassiveMoneyGen()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Ressources.AddMoney(1);
            }
        }
        private void PassiveFoodGen()
        {
            while (true)
            {
                Thread.Sleep(4000);
                Ressources.AddFood(1);
            }
        }

        private void ScaleCost()
        {
            while (true)
            {
                Thread.Sleep(10000);
                costWorker++;
                if (costWorker == maxCostPerWorker) break;
            }
        }

        #region SpawnGuiFields
        private void InitTextFields()
        {
            Vector2 topLeftPos = GameWorld.Instance.uiCam.TopLeft + new Vector2(120, 70);

            foodField = new GuiField("Current Food: 0", topLeftPos);
            topLeftPos += new Vector2(200, 0);

            moneyField = new GuiField("Current Money: 0", topLeftPos);
            topLeftPos += new Vector2(200, 0);

            dropsField = new GuiField("Current Drops: 0", topLeftPos);

            populationField = new GuiField("Population 0 / 100", GameWorld.Instance.uiCam.TopRight + new Vector2(-120, 70));

            SceneData.gameObjectsToAdd.Add(foodField);
            SceneData.gameObjectsToAdd.Add(moneyField);
            SceneData.gameObjectsToAdd.Add(dropsField);
            SceneData.gameObjectsToAdd.Add(populationField);

        }

        private void InitSpawnSquares()
        {

            Vector2 offset = new Vector2(50, 200);
            Vector2 pos = GameWorld.Instance.uiCam.TopLeft + offset;

            int amountOfSqures = 3;
            int wFiller = 70;
            int hFiller = 100;
            int width = (int)(GameWorld.width - offset.X - (wFiller * 3)) / amountOfSqures;
            int height = (int)(GameWorld.height - offset.Y - hFiller);

            chefSpawnSquare = new GuiField(pos, TextureNames.PixelEmpty, false, 1);
            chefSpawnSquare.SetCollisionBox(width, height);

            pos += new Vector2(width + wFiller, 0);
            minerSpawnSquare = new GuiField(pos, TextureNames.PixelEmpty, false, 1);
            minerSpawnSquare.SetCollisionBox(width, height);

            pos += new Vector2(width + wFiller, 0);
            fighterSpawnSquare = new GuiField(pos, TextureNames.PixelEmpty, false, 1);
            fighterSpawnSquare.SetCollisionBox(width, height);

            SceneData.gameObjectsToAdd.Add(chefSpawnSquare);
            SceneData.gameObjectsToAdd.Add(minerSpawnSquare);
            SceneData.gameObjectsToAdd.Add(fighterSpawnSquare);
        }

        private void InitSpawnSquareText()
        {
            Vector2 chefBottomMidPos = new Vector2(chefSpawnSquare.position.X + (chefSpawnSquare.collisionBox.Width) / 2,
                chefSpawnSquare.position.Y + chefSpawnSquare.collisionBox.Height);

            chefSpawnSquareText = new GuiField("Chefs", chefBottomMidPos, TextureNames.TextField2); // Change text gui field sprite, smaller.

            Vector2 minerBottomMidPos = new Vector2(minerSpawnSquare.position.X + (minerSpawnSquare.collisionBox.Width) / 2,
                minerSpawnSquare.position.Y + minerSpawnSquare.collisionBox.Height);

            minerSpawnSquareText = new GuiField("Miners", minerBottomMidPos, TextureNames.TextField2); // Change text gui field sprite, smaller.

            Vector2 fighterBottomMidPos = new Vector2(fighterSpawnSquare.position.X + (fighterSpawnSquare.collisionBox.Width) / 2,
                fighterSpawnSquare.position.Y + fighterSpawnSquare.collisionBox.Height);

            fighterSpawnSquareText = new GuiField("Figthers", fighterBottomMidPos, TextureNames.TextField2); // Change text gui field sprite, smaller.

            SceneData.gameObjectsToAdd.Add(chefSpawnSquareText);
            SceneData.gameObjectsToAdd.Add(minerSpawnSquareText);
            SceneData.gameObjectsToAdd.Add(fighterSpawnSquareText);
        }
        #endregion

        #region Buttons
        private void InitButtons()
        {
            buyChefBtn = new Button($"Buy Chef: {costWorker}", BuyChef, AnimNames.MediumButtonClick, new Vector2(foodField.position.X, foodField.position.Y + 80), 2);
            buyChefBtn.SetCollisionBox(65, 30);

            buyMinerBtn = new Button($"Buy Miner: {costWorker}", BuyMiner, AnimNames.MediumButtonClick, new Vector2(moneyField.position.X, moneyField.position.Y + 80), 2);
            buyMinerBtn.SetCollisionBox(65, 30);

            buyFigtherBtn = new Button($"Buy Figther: {costWorker}", BuyFighter, AnimNames.MediumButtonClick, new Vector2(dropsField.position.X, dropsField.position.Y + 80), 2);
            buyFigtherBtn.SetCollisionBox(65, 30);

            quitGameBtn = new Button("Quit Game", () => GameWorld.Instance.Exit(), AnimNames.MediumButtonClick, GameWorld.Instance.uiCam.BottomRight + new Vector2(-80, -50), 2);
            quitGameBtn.SetCollisionBox(65, 30);

            SceneData.gameObjectsToAdd.Add(buyChefBtn);
            SceneData.gameObjectsToAdd.Add(buyMinerBtn);
            SceneData.gameObjectsToAdd.Add(buyFigtherBtn);
            SceneData.gameObjectsToAdd.Add(quitGameBtn);
        }
        private bool CheckIfCanBuyWorker()
        {
            if (SceneData.workers.Count >= 100)
            {
                WinGame();
                return false;
            }
            return Ressources.TryUseMoneyCheckFood(Worker.foodEatAmount, costWorker);
        }

        private void WinGame()
        {
            foreach (Worker worker in SceneData.workers)
            {
                worker.WorkerDie();
            }
        }

        private void BuyChef()
        {
            if (!CheckIfCanBuyWorker()) return;

            Chef tempF = new Chef(Vector2.Zero);
            tempF.position = ReturnValidSpawnPos(chefSpawnSquare.collisionBox, tempF);
            tempF.workRessource.position += tempF.position;
            SceneData.gameObjectsToAdd.Add(tempF);
        }

        private void BuyMiner()
        {
            if (!CheckIfCanBuyWorker()) return;

            Miner tempF = new Miner(Vector2.Zero);
            tempF.position = ReturnValidSpawnPos(minerSpawnSquare.collisionBox, tempF);
            tempF.workRessource.position += tempF.position;
            SceneData.gameObjectsToAdd.Add(tempF);
        }

        private void BuyFighter()
        {
            if (!CheckIfCanBuyWorker()) return;

            Fighter tempF = new Fighter(Vector2.Zero);
            tempF.position = ReturnValidSpawnPos(fighterSpawnSquare.collisionBox, tempF);
            tempF.workRessource.position += tempF.position;
            SceneData.gameObjectsToAdd.Add(tempF);
        }

        private Vector2 ReturnValidSpawnPos(Rectangle spawnRec, Worker gmWorker)
        {
            Rectangle gameObjectRec = gmWorker.collisionBox;
            Vector2 tempPos = Vector2.Zero;
            bool found = false;

            while (!found)
            {
                // Pick random pos inside rec, by also getting the gmRec and making it so it always only can spawn inside the spawnRec.
                tempPos = new Vector2(
                    spawnRec.X + gameObjectRec.Width + rand.Next(spawnRec.Width - gameObjectRec.Width),
                    spawnRec.Y + rand.Next(spawnRec.Height - gameObjectRec.Height)
                );

                Rectangle tempWorkerCol = new Rectangle((int)tempPos.X, (int)tempPos.Y, gameObjectRec.Width, gameObjectRec.Height);
                Rectangle tempWorkRessouceCol = new Rectangle((int)tempPos.X, (int)tempPos.Y, gmWorker.workRessource.collisionBox.Width, gmWorker.workRessource.collisionBox.Height);

                // Check if the list "SceneData.persons" rectangle called "collisionBox" is not intersecting with the current position.
                found = true;
                foreach (var person in SceneData.workers)
                {
                    if (person.collisionBox.Intersects(tempWorkerCol) || person.collisionBox.Intersects(tempWorkRessouceCol))
                    {
                        found = false; // If the pos that it found then try again.
                        break;
                    }
                }
            }

            return tempPos;
        }
        #endregion

        public void Update()
        {
            UpdateTextFields();
        }

        private void UpdateTextFields()
        {
            foodField.text = $"Current Food: {Ressources.food}";
            moneyField.text = $"Current Money: {Ressources.money}";
            dropsField.text = $"Current Drops: {Ressources.monsterDrop}";

            buyChefBtn.text = $"Buy Chef: {costWorker}";
            buyMinerBtn.text = $"Buy Miner: {costWorker}";
            buyFigtherBtn.text = $"Buy Figther: {costWorker}";
            int populationCount = SceneData.workers.Where(x => !x.isDying).Count();
            populationField.text = $"Population\n {populationCount} / 100";
        }
    }
}
