using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
