using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpeakCode
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Playground : Page
    {
        public Playground()
        {
            this.InitializeComponent();
            int i = 0;
            foreach(Language l in App.langList)
            {
                langs.Items.Add(l.lang_code);
                if(App.current.id == ++i)
                    langs.SelectedIndex = i - 1;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string fromButton = (string)e.Parameter;
            int start = fromButton.IndexOf("(") + 1;
            int end = fromButton.IndexOf(")");
            string qcode = fromButton.Substring(start, end - start);
            foreach(Question q in App.quesList)
            {
                quesList.Items.Add(q.id + "." + q.pname + " (" + q.pcode + ")");
                if (qcode.Equals(q.pcode))
                {
                    Ques.Text = q.statement;
                }
            }
            if (Ques.Text.Equals(""))
            {
                Ques.Text = "Problems couldn't be loaded or you have opened the free play mode," +
                    "feel free to play with the editor. \n You cannot submit code right now.";
                submitButton.IsEnabled = false;
            }
        }

        private void dragStart(UIElement sender, DragStartingEventArgs args)
        {
            TextBlock block = sender as TextBlock;
            args.Data.SetText(block.Text);
            args.Data.RequestedOperation = DataPackageOperation.Copy;
        }

        private void genCode(object sender, RoutedEventArgs e)
        {
            code.Text = "#include <iostream>\nint main(){\n" + StartGround.genString() + "return 0;\n}";
        }
        private async void submit(object sender, RoutedEventArgs e)
        {
            string codex = code.Text;
            string x = await JsonReq.postReq(App.quesList.ElementAt(quesList.SelectedIndex).pcode, codex);
        }

        private void quesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            submitButton.IsEnabled = true;
            int fromButton = quesList.SelectedIndex;
            Ques.Text = App.quesList.ElementAt(fromButton).statement;
        }

        private void langs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.current = App.langList.ElementAt(langs.SelectedIndex);
            ifelse.Text = App.current.if_else;
            forx.Text = App.current.@for;
            print.Text = App.current.print;
            input.Text = App.current.read;
            breakx.Text = App.current.@break;
            continuex.Text = App.current.@continue;
            integerx.Text = App.current.@int;
            stringx.Text = App.current.@string;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Playground), "()");
        }
    }
}
