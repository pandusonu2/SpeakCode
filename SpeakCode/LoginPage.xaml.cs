using System;
using System.Data;
using System.Data.SqlClient;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;
using SpeakCode.SqlAccess;

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
            try
            {
                Service1Client client = new Service1Client();
                Boolean result = await client.AddUserAsync(uname2.Text, pass2.Password);
                if (result)
                    await new MessageDialog("User has been added").ShowAsync();
                else await new MessageDialog("Something seems to be wrong").ShowAsync();
            }
            catch(Exception err)
            {
                await new MessageDialog("Something seems to be wrong").ShowAsync();
            }
        }

        private async void LoginFunc(object sender, RoutedEventArgs e)
        {
            try
            {
                Service1Client client = new Service1Client();
                IList<User> userList = await client.QueryUserAsync();

                foreach (User u in userList)
                {
                    if (u.user.Equals(uname.Text) && u.password.Equals(pass.Password))
                    {
                        App.currUser = u;
                        this.Frame.Navigate(typeof(Landing));
                        return;
                    }
                }
                await new MessageDialog("No such user exists").ShowAsync();
            }
            catch(Exception err)
            {
                await new MessageDialog("Something seems to be wrong").ShowAsync();
            }
        }
    }
}
