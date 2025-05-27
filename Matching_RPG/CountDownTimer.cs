using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel.Design;

namespace Matching_RPG
{
	internal class CountDownTimer
	{
		Timer _countDownTimer;
        Game _form;
        public int Minute { get; set; }
        public double Seconds { get; set; }
        public bool isGameOver { get; set; }    
        public Action CallBackFunction { get; set; }

        public CountDownTimer(Timer timer, Game form)
        {
            this._countDownTimer = timer;     
            this._form = form;
            this._countDownTimer.Interval = 1000;
            isGameOver = false;
        }

        public void Start(float seconds)
        {

            if (this._form == null) { return; }
			this._countDownTimer.Tick += OnCountDownTick;
            if (seconds >= 60)
            {
				this.Minute = (int)seconds / 60; // Integer division directly calculates minutes
				this.Seconds = (int)((seconds % 60) * 100 / 60); // Calculate seconds fraction as an integer
			}
            else
            {
                this.Minute = 0;
                this.Seconds = seconds;
            }
            this._countDownTimer.Start();

        }
        
        public void OnCountDownTick(object sender, EventArgs e)
		{
            if (Minute == 0 && Seconds == 0)
            {
                isGameOver = true;
                CallBackFunction?.Invoke();
                Stop();
                return;
            }

            if (Seconds <= 0 && isGameOver == false)
            {
                Seconds = 60;
                Minute--;
            }
            Seconds--;
		}

		public void Stop()
        {
            this._countDownTimer.Stop();
        }
        
    }
}
