using Matching_RPG.Implementation_Classes;
using Matching_RPG.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG.ClickBehaviors
{
    internal class ClickNormalGameBehavior : IClickable
    {

        public bool IsClickable { get; set; }
        public Game NormalGame { get; set; }
        public DrawText Text { get; set; }
        GameLevels GameLevels { get; set; }
        public ClickNormalGameBehavior(Game normalGame, GameLevels levels, DrawText text)
        {
            NormalGame = normalGame;
            GameLevels = levels;
            Text = text;
        }


        public void Click(object sender, MouseEventArgs e)
        {
            if (Text.TextBox.IntersectsWith(new System.Drawing.Rectangle(e.X, e.Y, 32, 32)))
            {
                GameLevels.Hide();
                if (NormalGame.IsDisposed)
                {
                    NormalGame = new Game(instanceOfSkulls: 3, numberOfCubies: 9, gameLevelsForm: GameLevels);
                    NormalGame.Cursor = new Cursor(Resources.defaultIcon.Handle);

                    NormalGame.ShowDialog();
                }
                else
                {
                    NormalGame.Cursor = new Cursor(Resources.defaultIcon.Handle);

                    NormalGame.ShowDialog();

                }
                if (!GameLevels.IsDisposed)
                {
                    GameLevels.Show();
                }
            }


        }

    }
}
