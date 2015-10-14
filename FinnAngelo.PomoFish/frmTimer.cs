﻿using FinnAngelo.PomoFish.Properties;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FinnAngelo.PomoFish
{
    partial class frmTimer : Form
    {
        DateTime _countdownEnd;
        readonly Timer _timer;

        public frmTimer(Timer timer)
        {
            InitializeComponent();

            _timer = timer;
            _timer.Interval = 1000;// Timer will tick every 1 second
            _timer.Tick += timer_Tick;

            _countdownEnd = DateTime.Now.AddMinutes(Settings.Default.DurationInMinutes);
            _timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;

            if (now > _countdownEnd)
            {
                NativeMethods.LockWorkStation();
                Application.Exit();
            }
            else
            {
                var countDownSpan = _countdownEnd - now;
                int countDown;
                if (countDownSpan.Minutes == 0)
                {
                    if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
                    countDown = countDownSpan.Seconds;
                }
                else
                {
                    countDown = countDownSpan.Minutes;
                };
                lblCountDown.Text = countDown.ToString(CultureInfo.CurrentCulture);
                this.Text = countDown.ToString(CultureInfo.CurrentCulture);
            }
        }
    }
}