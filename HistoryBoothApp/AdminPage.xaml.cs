﻿using System;
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
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HistoryBoothApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        // -----------------------------------------
        //      HU HISTORY BOOTH GMAIL ACCOUNT   
        // -----------------------------------------
        // username: hu.history.booth@gmail.com    
        // password: HistoryBoothHU!               
        // TODO: change phone number and recovery
        //       email address on google account
        // -----------------------------------------

        // TODO: get Brackett Library email
        private string adminEmail = "spamcason@gmail.com";

        // TODO: load custom/default admin password from settings
        private string adminPassword = "password";

        public AdminPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(800, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;        
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: save any changes

            // TODO: confirm that user wants to log out

            // "logging out" is just returning to the main page
            // the title bar back button also "logs out"
            Frame.GoBack();
            Frame.GoBack();
        }

        private void SendEmail(string emailTo, string emailSubject, string emailBody)
        {
            // SMTP Guide Used --> https://mailtrap.io/blog/csharp-send-email-gmail/
            string emailFrom = "hu.history.booth@gmail.com";

            // this is an app specific SMTP password
            string emailFromPassword = "vakkcvucemwnoukp";
            // the actual login password located at the top of this file

            #region Setup+SendEmail

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("HU History Booth", emailFrom));
            email.To.Add(new MailboxAddress("Admin", emailTo));

            email.Subject = emailSubject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = emailBody
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);
                smtp.Authenticate(emailFrom, emailFromPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }

            #endregion
        }

        private async void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var passwordBox = new PasswordBox()
            {
                PlaceholderText = "Password...",
                MaxLength = 25,
                Height = 33
            };

            ContentDialog dialogBox = new ContentDialog()
            {
                Title = "Change Admin Password",
                Content = passwordBox,
                PrimaryButtonText = "OK",
                SecondaryButtonText = "Cancel"
            };                        

            dialogBox.Content = passwordBox;
            await dialogBox.ShowAsync();

            if (passwordBox.Password != "")
            { 
                // TODO: update admin password in local settings
                adminPassword = passwordBox.Password;
                string emailSubject = "Admin Password Updated!";
                string emailBody = "Your administrative password for the HU History Booth has just been reset to: \n\n\t\t" + passwordBox.Password;
                SendEmail(adminEmail, emailSubject, emailBody);
            }                        
        }
    }
}
