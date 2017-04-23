using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpeakCode.SqlAccess;
using Windows.UI.Xaml.Media;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SpeakCode
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPage : Page
    {
        public UserPage()
        {
            this.InitializeComponent();
            acc.Text = App.currUser.acc.ToString();
            sol.Text = App.currUser.sol.ToString();
            Details.Text = App.currUser.user;
            if (App.currUser.sol == 0)
                percent.Text = "0";
            else percent.Text = (App.currUser.acc * 100.0 / App.currUser.sol).ToString();
            populateQuestions();
        }
        public void populateQuestions()
        {
            List<Question> questions = App.quesList;
            int i = 0;
            foreach (Question q in questions)
            {
                Button ques = new Button();
                ques.SetValue(Button.ContentProperty, q.id + "." + q.pname + " (" + q.pcode + ")");
                ques.Click += OpenQues;
                if ((App.currUser.ques.ToCharArray())[i] == '1')
                    ques.Background = new SolidColorBrush(Colors.Green);

                RowDefinition defineRow = new RowDefinition();
                defineRow.SetValue(RowDefinition.HeightProperty, (new GridLength(1, GridUnitType.Auto)));
                Question.RowDefinitions.Add(defineRow);

                Question.Children.Add(ques);
                ques.SetValue(Grid.RowProperty, i++);
                ques.SetValue(Grid.ColumnProperty, 0);
            }
        }
        private void OpenQues(object Sender, RoutedEventArgs e)
        {
            string arg = (string)((Button)Sender).Content;
            this.Frame.Navigate(typeof(Playground), arg);
        }
    }
}
