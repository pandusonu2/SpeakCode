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
using SpeakCode.SqlAccess;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpeakCode
{
    public sealed partial class AddLang : ContentDialog
    {
        public AddLang()
        {
            this.InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            UserLang ul = new UserLang()
            {
                user = App.currUser.user,
                lang = lang.Text,
                integer = integer.Text,
                strings = strings.Text,
                ifelse = ifelse.Text,
                looper = looper.Text,
                printer = print.Text,
                input = input.Text,
                breaker = breaker.Text,
                continuex = continuex.Text
            };
            Service1Client client = new Service1Client();
            bool x = await client.AddLangAsync(ul);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
