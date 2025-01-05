using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.ApplicationModel.ConversationalAgent;
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
    public sealed partial class SignUp : Page
    {
        public SignUp()
        {
            this.InitializeComponent();
            rpOutput.Visibility = Visibility.Collapsed;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //validation with exception
            MessageDialog msg;
            string fName = txtFirstName.Text;
            string lName = txtLast.Text;
            string phone = txtPhone.Text;

            try
            {

                if (string.IsNullOrWhiteSpace(fName))
                    throw new Exception("Please Input First Name");

                if (string.IsNullOrWhiteSpace(lName))
                    throw new Exception("Please Input Last Name");


                if (string.IsNullOrWhiteSpace(phone))
                    throw new Exception("Please Input Phone Number");
                int forPhhone;
                bool success = int.TryParse(phone, out forPhhone);
                if (!success && forPhhone < 11)
                {
                    throw new Exception("Please Input the right format for Phone number must be 11-digits");
                }
                if (cboActivity.SelectedIndex == 0)
                {
                    throw new Exception("Please select a Activity to continue.");

                }
                if (cboAge.SelectedIndex == 0)
                {
                    throw new Exception("Please select a appropriate Age .");

                }
                //Prices
                double actPrice = 0;
                switch(cboActivity.SelectedIndex)
                {
                    case 1:
                    case 2:
                        actPrice = 20;
                        break;
                    case 3:
                        actPrice = 25;
                        break;
                        case 4:
                        actPrice = 27;
                        break;
                }
                double ageTotal = 0;

                switch(cboAge.SelectedIndex)
                {
                    case 1:
                        ageTotal = 10; break;
                    case 2:
                        ageTotal = 13;break; 
                        case 3:
                        ageTotal = 15;break;
                }

                Random combination= new Random();
                string randomCombo = "";
                    for(int i = 0; i < 10; i++)
                {
                    randomCombo += combination.Next(10);
                }




                double total = actPrice + ageTotal;
                double taxRate = 0.13;
                double subTotal = total * (1 * taxRate);
                double grandTotal = subTotal + total;

                ComboBoxItem selected = (ComboBoxItem)cboActivity.SelectedItem;
                //output + grandtotals
                lblOutput.Text = $"{fName}, {lName.Substring(0, 1)} Welcome to our club and your chosen activity is {selected.Content.ToString()} ,your total is ${grandTotal} ,confirmation number is {randomCombo} ";
                ////when submitted the panel will be collasped
                rpSignUp.Visibility = Visibility.Collapsed;
                rpOutput.Visibility = Visibility.Visible;
            }


            catch (Exception ex)
            {
                msg = new MessageDialog($"The Following exception has Occured:\n\t->{ex.Message}");
                msg.ShowAsync();
                return;
            }


        }
        ///reset/cancel button
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = "";
            txtLast.Text = "";
            txtPhone.Text = "";
            cboActivity.SelectedIndex = 0;
            lblOutput.Text = "";
        }
        //when clicked in goes back to Signup page resetting everything
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUp));
        }
    }
}

    
    

