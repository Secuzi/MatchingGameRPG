using Matching_RPG.Implementation_Classes;
using Matching_RPG.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Matching_RPG.ClickBehaviors
{
    internal class ClickEasyGameBehavior : IClickable
    {

        public bool IsClickable { get; set; }
        public Game EasyGame { get; set; }
        public DrawText Text { get; set; }
        GameLevels GameLevels { get; set; }
        public ClickEasyGameBehavior(Game easyGame, GameLevels levels, DrawText text)
        {
            EasyGame = easyGame;
            GameLevels  = levels;
            Text = text;
        }
        
        
        public void Click(object sender, MouseEventArgs e)
        {
            if (Text.TextBox.IntersectsWith(new System.Drawing.Rectangle(e.X, e.Y, 32, 32)))
            {
                GameLevels.Hide();
                if (EasyGame.IsDisposed)
                {
                    EasyGame = new Game(instanceOfSkulls: 1, numberOfCubies: 9, gameLevelsForm: GameLevels);
                    EasyGame.Cursor = new Cursor(Resources.defaultIcon.Handle);
                    EasyGame.ShowDialog();
                }
                else
                {
                    EasyGame.Cursor = new Cursor(Resources.defaultIcon.Handle);
                    EasyGame.ShowDialog();

                }
                if (!GameLevels.IsDisposed)
                {
                    GameLevels.Show();
                }
            }


        }
    }
}
