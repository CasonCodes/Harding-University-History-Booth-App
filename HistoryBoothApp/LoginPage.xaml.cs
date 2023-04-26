using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HistoryBoothApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {

        // TODO: load custom/default admin password from settings
        // TODO: email password reset (forgot password)
        string adminPassword = "password";

        public LoginPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private async void displayWrongPasswordDialog()
        {
            ContentDialog dialogBox = new ContentDialog();
            dialogBox.Title = "Incorrect Password";
            dialogBox.Content = "The password you entered does not match our records.\n" +
                "Please try again or click 'Forgot Password' to reset.";

            dialogBox.IsPrimaryButtonEnabled = true;
            dialogBox.PrimaryButtonText = "OK";

            await dialogBox.ShowAsync();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            // if correct password, navigate to admin page
            if (passwordBox.Password == adminPassword)
            {
                AdminPage adminPage = new AdminPage();
                Frame.Navigate(typeof(AdminPage), adminPage);
            }
            else
            {
                passwordBox.Password = "";
                passwordBox.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                displayWrongPasswordDialog();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void SendEmail(string emailTo, string emailSubject, string emailBody)
        {
            // TODO: Implement code to send email here
            // You can use a third-party library or an email service provider to send the email
            // Alternatively, you can prompt the user to send the email manually
        }

        private async void forgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: password reset for admin

            // ask if user is sure they want to reset password "an email will be sent to ____" (y/n)?


            // generate random 4 digit number
            Random random = new Random();
            int resetCode = random.Next(1000, 9999);

            // send email of the random number

            // TODO: get Brackett Library email
            string emailTo = "klaing@harding.edu";

            string emailSubject = "Password Reset Code";
            string emailBody = ("Your password reset code is " + resetCode + ".");
            SendEmail(emailTo, emailSubject, emailBody);


            // prompt user to enter the reset code
            int userCode = 0;
            var dialog = new ContentDialog()
            {
                Title = "Password Reset",
                Content = "Please enter the reset code you received by email:",
                PrimaryButtonText = "Submit",
                SecondaryButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };

            var codeBox = new TextBox();
            dialog.Content = codeBox;
            await dialog.ShowAsync();


            if (userCode == resetCode)
            {
                // Enforce one-time use of the code
                resetCode = 0;

                // TODO: Reset the admin's password here
                // You can display a dialog or navigate to a password reset page where the admin can enter a new password
            }
            else
            {
                //MessageBox.Show("Invalid reset code. Please try again.", "Password Reset");
            }


        }

        private void passwordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                loginButton_Click(sender, e);
            }
        }
    }
}
