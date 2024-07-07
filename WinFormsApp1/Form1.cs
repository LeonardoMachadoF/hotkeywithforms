using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        async private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            [DllImport("user32.dll")]
            static extern short GetAsyncKeyState(int vKey);
            while(true)
            {
                if(GetAsyncKeyState(0x11) < 0 || GetAsyncKeyState(0x70) < 0) // Ctrl e F1
                {
                    StartTimer("17-5", TimeSpan.FromMinutes(1), label1);
                }
                if(GetAsyncKeyState(0x35) < 0) // 5
                {
                    StartTimer("15-5", TimeSpan.FromSeconds(30), label2);
                }
                if(GetAsyncKeyState(0x72) < 0) // F3
                {
                    ResetTimers("17-5");
                }
                if(GetAsyncKeyState(0x73) < 0) // F4
                {
                    ResetTimers("15-5");
                }
                await Task.Delay(20);
            }

            void StartTimer(string message, TimeSpan duration, Label label)
            {
                if(!IsTimerRunning(message))
                {
                    TimerState timerState = new TimerState(message, DateTime.Now.Add(duration), label);
                    timerState.Timer = new System.Threading.Timer(TimerCallback, timerState, 0, 1000);
                    runningTimers.Add(timerState);
                }
            }

            void TimerCallback(object state)
            {
                var timerState = (TimerState)state;
                TimeSpan remainingTime = timerState.EndTime - DateTime.Now;
                timerState.Label.BeginInvoke((MethodInvoker)delegate ()
                {
                    string secondsRemaining = ((int)remainingTime.TotalSeconds).ToString("D");
                    timerState.Label.Text = secondsRemaining;
                });

                if(remainingTime.TotalSeconds <= 0)
                {
                    Console.Beep();
                    timerState.Timer.Change(Timeout.Infinite, Timeout.Infinite); // Parar o temporizador
                    timerState.Timer.Dispose(); // Liberar recursos do temporizador
                    runningTimers.Remove(timerState); // Remover o temporizador da lista de temporizadores em execução
                    timerState.Label.BeginInvoke((MethodInvoker)delegate ()
                    {
                        timerState.Label.Text = "R";
                    });
                }
            }

            void ResetTimers(string message)
            {
                var timersToReset = runningTimers.Where(t => t.Message == message).ToList();
                foreach(var timerState in timersToReset)
                {
                    timerState.Timer.Change(Timeout.Infinite, Timeout.Infinite);
                    timerState.Timer.Dispose();
                    runningTimers.Remove(timerState);

                    timerState.Label.BeginInvoke((MethodInvoker)delegate ()
                    {
                        timerState.Label.Text = "R";
                    });
                }
            }
        }

        class TimerState
        {
            public string Message { get; }
            public DateTime EndTime { get; }
            public System.Threading.Timer Timer { get; set; }
            public Label Label { get; }

            public TimerState(string message, DateTime endTime, Label label)
            {
                Message = message;
                EndTime = endTime;
                Label = label;
            }
        }

        static List<TimerState> runningTimers = new List<TimerState>();
        static bool IsTimerRunning(string message)
        {
            return runningTimers.Any(t => t.Message == message);
        }
    }
}
