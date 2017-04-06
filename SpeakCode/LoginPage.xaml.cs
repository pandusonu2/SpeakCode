using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void OpenFreePlay(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Playground), "()");
        }

        private async void SignUpFunc(object sender, RoutedEventArgs e)
        {
            foreach (char c in uname2.Text.ToCharArray())
                if (c < 'a' || c > 'z')
                {
                    await new MessageDialog("Username can contain only small case alphabets").ShowAsync();
                    return;
                }
            foreach (char c in pass2.Password.ToCharArray())
                if (c < 'a' || c > 'z')
                {
                    await new MessageDialog("Password can contain only small case alphabets").ShowAsync();
                    return;
                }
            if (pass2.Password != pass3.Password)
            {
                await new MessageDialog("Enter the same password both the times").ShowAsync();
                return;
            }
            string strConn = "Server=tcp:imagine13.database.windows.net,1433;Initial Catalog=users;" + 
                "Persist Security Info=False;User ID=speakcode;Password=Imagine@13;" +
                "MultipleActiveResultSets=False;Encrypt=True;" + 
                "TrustServerCertificate=False;Connection Timeout=30;";
            //try
            //{
                using (var connection = new SqlConnection(strConn))
                {
                    connection.Open();

                    SqlCommand insertCommand = new SqlCommand("", connection)
                    {
                        CommandType = CommandType.Text,

                        CommandText = $"insert into usertable values ('{uname.Text}',  0, 0, '0000000000', '{pass2.Password}');"
                    };
                    int newRows = insertCommand.ExecuteNonQuery();
                    if (newRows != 1)
                        await new MessageDialog("Registration unsuccessful").ShowAsync();
                    else
                        await new MessageDialog("Registration successful").ShowAsync();
                }
            //}
            /*catch(Exception err)
            {
                //await new MessageDialog(err.ToString()).ShowAsync();
                await new MessageDialog("There was an error, please use free play.").ShowAsync();
            }*/
        }

        private async void LoginFunc(object sender, RoutedEventArgs e)
        {
            foreach(char c in uname.Text.ToCharArray())
                if (c < 'a' || c > 'z')
                {
                    await new MessageDialog("Wrong Entry").ShowAsync();
                    return;
                }
            foreach(char c in pass.Password.ToCharArray())
                if (c < 'a' || c > 'z')
                {
                    await new MessageDialog("Wrong Entry").ShowAsync();
                    return;
                }
            await new MessageDialog("There was an error, please use free play.").ShowAsync();
        }
    }
}
