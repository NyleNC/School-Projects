using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Devices.Lights;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Final_Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class ClubLeader : Page
    {
        public ClubLeader()
        {
            this.InitializeComponent();
            rpForm.Visibility = Visibility.Collapsed;
        }

        private void bntContact_Click(object sender, RoutedEventArgs e)
        {
            if (rpForm.Visibility == Visibility.Visible)
            {
                rpForm.Visibility = Visibility.Collapsed;
            }
            else
            {
                rpForm.Visibility = Visibility.Visible;
            }
        }
        MessageDialog msg;
        private void btnSubmit_Click(object sender, RoutedEventArgs e)

        { string fName = txtFirst.Text;
            string email = txtEmail.Text;
            string message = txtMessage.Text;
            //validation
            try
            {

                if (string.IsNullOrWhiteSpace(fName))
                    throw new Exception("Please Input A Name");

                if (string.IsNullOrWhiteSpace(email))
                    throw new Exception("Please put you email so we can contact you back");
            }
            catch (Exception ex)
            {
                msg = new MessageDialog($"The Following exception has Occured:\n\t->{ex.Message}");
                msg.ShowAsync();
                return;
            }
            //takes the output to another page
            Frame.Navigate(typeof(ContactVerify),"Thank you"+ " "+fName+" "+"our club leader will answer in about 24 hours");
      

        }

    
    } 
}
    
