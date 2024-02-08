using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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
        private GuiField fighterSpawnSquare;

        private Button buyChefBtn;
        private Button buyMinerBtn;
        private Button buyFigtherBtn;
        private Button quitGameBtn;

        public void InitUI()
        {
            InitTextFields();
            InitSpawnSquares();
            InitSpawnSquareText();
            InitButtons();

        }

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

            chefSpawnSquareText = new GuiField("Chefs", chefBottomMidPos); // Change text gui field sprite, smaller.


            SceneData.gameObjectsToAdd.Add(chefSpawnSquareText);
        }

        private void InitButtons()
        {
            buyChefBtn = new Button("Buy Chef", BuyChef, AnimNames.MediumButtonClick, new Vector2(foodField.position.X, foodField.position.Y + 80), 2);
            buyChefBtn.SetCollisionBox(65, 30);

            buyMinerBtn = new Button("Buy Miner", BuyMiner, AnimNames.MediumButtonClick, new Vector2(moneyField.position.X, moneyField.position.Y + 80), 2);
            buyMinerBtn.SetCollisionBox(65, 30);

            buyFigtherBtn = new Button("Buy Figther", BuyFighter, AnimNames.MediumButtonClick, new Vector2(dropsField.position.X, dropsField.position.Y + 80), 2);
            buyFigtherBtn.SetCollisionBox(65, 30);

            quitGameBtn = new Button("Quit Game", () => GameWorld.Instance.Exit(), AnimNames.MediumButtonClick, GameWorld.Instance.uiCam.BottomRight + new Vector2(-80, -50), 2);
            quitGameBtn.SetCollisionBox(65, 30);

            SceneData.gameObjectsToAdd.Add(buyChefBtn);
            SceneData.gameObjectsToAdd.Add(buyMinerBtn);
            SceneData.gameObjectsToAdd.Add(buyFigtherBtn);
            SceneData.gameObjectsToAdd.Add(quitGameBtn);
        }

        private void BuyChef()
        {

        }

        private void BuyMiner()
        {

        }

        private void BuyFighter()
        {

        }

        private Vector2 ReturnValidSpawnPos(Rectangle spawnRec)
        {
            Vector2 tempPos = Vector2.Zero;



            return tempPos;
        }

        public void Update()
        {
            UpdateTextFields();
        }

        private void UpdateTextFields()
        {
            foodField.text = $"Current Food: {Ressources.food}";
            moneyField.text = $"Current Money: {Ressources.money}";
            dropsField.text = $"Current Drops: {Ressources.monsterDrop}";

            int populationCount = SceneData.persons.Where(x => !x.isDying).Count();
            populationField.text = $"Population\n {populationCount} / 100";
        }
    }
}
