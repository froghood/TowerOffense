using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Objects.Towers;

namespace TowerOffense.Scenes.Gameplay.Objects.Shop {
    public class ShopButton : Button {

        private int _price;
        private string _name;
        private string[] _lines;
        private Color _color;

        public ShopButton(
            Shop window,
            EntityManager entityManager,
            Rectangle bounds,
            Type towerType,
            int price,
            string name,
            string[] lines,
            Color color) : base(
            window,
            bounds) {

            _price = price;
            _name = name;
            _lines = lines;
            _color = color;

            Clicked += (_, _) => {
                if (TOGame.Player.Money < _price) return;
                System.Console.WriteLine(towerType);

                if (towerType == typeof(GravityTower)) {

                    System.Console.WriteLine(window.Scene);
                    System.Console.WriteLine(entityManager);

                    window.Scene.AddObject(entityManager.CreateTower<GravityTower>(window.PurchaseOffset));
                } else if (towerType == typeof(ElectroTower)) {
                    window.Scene.AddObject(entityManager.CreateTower<ElectroTower>(window.PurchaseOffset));
                } else if (towerType == typeof(NuclearTower)) {
                    window.Scene.AddObject(entityManager.CreateTower<NuclearTower>(window.PurchaseOffset));
                }

                window.PurchaseOffset += Vector2.One * 24;
                TOGame.Player.Charge(_price);
                var sound = TOGame.Assets.Sounds["Sounds/Purchase"].CreateInstance();
                sound.Volume = TOGame.Settings.Volume;
                sound.Play();
            };


        }

        public ShopButton(TowerOffense.Objects.Base.SceneWindow window, Point position, Point size) : base(window, position, size) {


        }

        public override void Render(GameTime gameTime) {

            if (Texture != null) {
                TOGame.SpriteBatch.Draw(
                    Texture,
                    Bounds,
                    Texture.Bounds,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    0f);

                if (Hovering) {
                    TOGame.SpriteBatch.Draw(SceneWindow.Pixel,
                        Bounds,
                        (_price <= TOGame.Player.Money ? Color.White : Color.Red) * 0.3f);
                }
            }

            var spriteFont = TOGame.Instance.Content.Load<SpriteFont>("Fonts/Daydream");
            string text = _name;
            Vector2 textSize = spriteFont.MeasureString(text);

            TOGame.SpriteBatch.DrawString(
                spriteFont,
                text,
                Bounds.Location.ToVector2() + new Vector2(Bounds.Size.X / 2f, Bounds.Size.Y) + Vector2.UnitY * 5,
                _color,
                0f,
                textSize * Vector2.UnitX / 2f,
                0.25f,
                SpriteEffects.None,
                0f);



            //base.Render(gameTime);
        }
    }
}