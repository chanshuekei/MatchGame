using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecElapsed = 0;
        int matchedFound = 0;
        public MainWindow()
        {
            InitializeComponent();
            // 设置定时器
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            // 设置游戏
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            tenthsOfSecElapsed++;
            timeTextBlock.Text = (tenthsOfSecElapsed / 10F).ToString("0.0s");
            if (matchedFound >= 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play Again?";
            }
        }

        private void SetUpGame()
        {
            //throw new NotImplementedException();
            List<string> aninmalEmoji = new List<string>()
            {
                "🐼","🐼",
                "🐘","🐘",
                "🦀","🦀",
                "🐋","🐋",
                "🦖","🦖",
                "🐍","🐍",
                "🦋","🦋",
                "🦥","🦥",
                //"🐼","🐵","🐘","🐔","🐸",
                //"🐬","🦞","🐋","🐢","🐯",
                //"🦥","🦖","🦀","🐍","🦋",
            };
            Random random = new Random();
            foreach (TextBlock tb in mainGrid.Children.OfType<TextBlock>())
            {
                if (tb.Name != "timeTextBlock")
                {
                    int index = random.Next(aninmalEmoji.Count);
                    string nextEmoji = aninmalEmoji[index];
                    tb.Text = nextEmoji;
                    aninmalEmoji.RemoveAt(index);
                }
                tb.Visibility = Visibility.Visible;
            }

            tenthsOfSecElapsed = 0;
            matchedFound = 0;
            findingMatch = false;
            timeTextBlock.Text = "Reset Animal";
        }

        // MEMBER
        public TextBlock lastTbClicked;
        public bool findingMatch = false;
        private void doing_Match_Game(ref TextBlock tb)
        {
            if (findingMatch == false)
            {
                tb.Visibility = Visibility.Hidden;
                lastTbClicked = tb;
                findingMatch = true;
                // 启动定时器
                if (0 == matchedFound)
                {
                    timer.Start();
                }

            }
            else if (tb.Text == lastTbClicked.Text)
            {
                tb.Visibility = Visibility.Hidden;
                findingMatch = false;
                matchedFound++;
            }
            else
            {
                lastTbClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
        private void textBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Console.WriteLine("this is a test action!");
            TextBlock tb = sender as TextBlock;
            if (null != tb)
            {
                doing_Match_Game(ref tb);
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (timer.IsEnabled)
            {
                return;
            }
            SetUpGame();
            timeTextBlock.Text = "Reset Animal";
        }
    }
}