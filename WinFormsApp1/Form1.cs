using System.Runtime.InteropServices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
                    StartTimer("17-5", TimeSpan.FromMinutes(1));
                }
                if(GetAsyncKeyState(0x72) < 0) // F3
                {
                    ResetTimers();
                }
                await Task.Delay(20);
            }

            void StartTimer(string message, TimeSpan duration)
            {
                if(!IsTimerRunning(message))
                {
                    Console.WriteLine($"Start {message}");
                    TimerState timerState = new TimerState(message, DateTime.Now.Add(duration));
                    timerState.Timer = new System.Threading.Timer(TimerCallback, timerState, 0, 1000);
                    runningTimers.Add(timerState);
                }
            }

            void TimerCallback(object state)
            {
                var timerState = (TimerState)state;
                TimeSpan remainingTime = timerState.EndTime - DateTime.Now;
                label1.BeginInvoke((MethodInvoker)delegate ()
                {
                    string secondsRemaining = remainingTime.Seconds.ToString("D2");
                    label1.Text = secondsRemaining; 
                });

                if(remainingTime.TotalSeconds <= 0)
                {
                    Console.Beep();
                    timerState.Timer.Change(Timeout.Infinite, Timeout.Infinite); // Parar o temporizador
                    timerState.Timer.Dispose(); // Liberar recursos do temporizador
                    runningTimers.Remove(timerState); // Remover o temporizador da lista de temporizadores em execução
                    label1.BeginInvoke((MethodInvoker)delegate ()
                    {
                        label1.Text = "R";
                    });
                }
            }

            void ResetTimers()
            {
                foreach(var timerState in runningTimers)
                {
                    timerState.Timer.Change(Timeout.Infinite, Timeout.Infinite);
                    timerState.Timer.Dispose();
                }
                runningTimers.Clear();

                label1.BeginInvoke((MethodInvoker)delegate ()
                {
                    label1.Text = "R";
                });
            }
        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }
        class TimerState
        {
            public string Message { get; }
            public DateTime EndTime { get; }
            public System.Threading.Timer Timer { get; set; }

            public TimerState(string message, DateTime endTime)
            {
                Message = message;
                EndTime = endTime;
            }
        }

        static List<TimerState> runningTimers = new List<TimerState>();
        static bool IsTimerRunning(string message)
        {
            return runningTimers.Any(t => t.Message == message);
        }

    }
}
