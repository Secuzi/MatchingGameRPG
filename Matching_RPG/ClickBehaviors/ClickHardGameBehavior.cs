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
    internal class ClickHardGameBehavior : IClickable
    {
        public bool IsClickable { get; set; }
        public Game HardGame { get; set; }
        public DrawText Text { get; set; }
        GameLevels GameLevels { get; set; }
        public ClickHardGameBehavior(Game hardGame, GameLevels levels, DrawText text)
        {
            HardGame = hardGame;
            GameLevels = levels;
            Text = text;
        }

        public void Click(object sender, MouseEventArgs e)
        {
            if (Text.TextBox.IntersectsWith(new System.Drawing.Rectangle(e.X, e.Y, 32, 32)))
            {
                GameLevels.Hide();
                if (HardGame.IsDisposed)
                {
                    HardGame = new Game(instanceOfSkulls: 6, numberOfNiglets: 9, gameLevelsForm: GameLevels);
                    HardGame.Cursor = new Cursor(Resources.defaultIcon.Handle);

                    HardGame.ShowDialog();
                }
                else
                {
                    HardGame.Cursor = new Cursor(Resources.defaultIcon.Handle);
                    HardGame.ShowDialog();

                }
                if (!GameLevels.IsDisposed)
                {
                    GameLevels.Show();
                }
            }


        }
    }
}
