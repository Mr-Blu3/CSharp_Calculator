using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NewCalculator.Interfaces;
using System.Data;

namespace NewCalculator
{
    public partial class MainWindow : Window
    {
        private bool CheckInputEmpty = false;
        private bool BoolSetResetAndKeepToZero = false;
        private bool BoolCheckCalculate = false;

        private string StrCollectCalcVal = null;
        private string StrStoreContent = null;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Pontus Pettersson Calculator";
            this.DataContext = this;
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Button GetSender = (Button)sender;
            string StrSender = Convert.ToString(GetSender.Content);
            
            string last = GetValue.Text[GetValue.Text.Length - 1].ToString();

            this.SetResetAndKeepToZero(StrSender);

            if (BoolSetResetAndKeepToZero)
            {
                this.BoolSetResetAndKeepToZero = false;
                return;
            }

            if (StrSender == "±")
            {
                this.SetChangePlusMinusValue();
                return;
            }

            if (this.CheckInputEmpty && StrSender != "+" && StrSender != "-" 
                && StrSender != "*" && StrSender != "/" && StrSender != "%" && StrSender != "=" && StrSender != "->" && StrSender != "±")
            {
                if (StrSender == ".") GetValue.Text = "0.";
                else GetValue.Text = StrSender;

                this.CheckInputEmpty = false;
                return;
            }

            if (((last == "." && StrSender == ".") || GetValue.Text == "0" && StrSender == "."))
            {
                GetValue.Text = "0.";
                return;
            }

            if (StrSender == "->")
            {
                if (GetValue.Text.Length < 2) GetValue.Text = "0";
                else GetValue.Text = this.removeLastValueOfString(GetValue.Text);
                return;    
            }

            if (GetValue.Text == "0" && StrSender != "+" && StrSender != "-" 
                && StrSender != "*" && StrSender != "/" && StrSender != "%" && StrSender != "=" && StrSender != "->" && StrSender!= "±")
                GetValue.Text = StrSender;
            else
                this.setValue(StrSender);

           
        }

        private void setValue(string sender)
        {
            string[] Assigneds = new string[] { "=", "->", "+", "-", "*", "/", "±"};
            string StrCalc = this.StrCollectCalcVal + this.StrStoreContent + GetValue.Text;
           
            foreach (string Assigned in Assigneds)
            {
                if(Assigned == sender)
                {
                    if (Convert.ToString(LabelCollectVal.Content) != " ")
                    {
                        LabelCollectVal.Content += GetValue.Text + sender;

                        if (sender == "=")
                        {
                            GetValue.Text = this.SetCalculateValue(
                                StrCalc
                                );

                            LabelCollectVal.Content = " ";
                            this.CheckInputEmpty = true;
                            this.BoolCheckCalculate = false;
                            return;
                        }

                        if (!this.BoolCheckCalculate)
                        {
                            GetValue.Text = this.SetCalculateValue(this.removeLastValueOfString(
                                Convert.ToString(LabelCollectVal.Content))
                            );

                        } else {
                            GetValue.Text = this.SetCalculateValue(StrCalc);
                        }
                        

                    } else {

                        if (sender == "=")
                        {
                            GetValue.Text = "0";
                            LabelCollectVal.Content = " ";
                            this.CheckInputEmpty = true;
                            return;
                        }

                        LabelCollectVal.Content = GetValue.Text + sender;
                    }

                    this.StrStoreContent = sender;

                    this.StrCollectCalcVal = GetValue.Text;

                    this.CheckInputEmpty = true;

                    return;

                }
            }

            
            GetValue.Text += sender;
        }

        private string SetCalculateValue(string removeLastChar)
        {
            this.BoolCheckCalculate = true;

            return new DataTable().Compute(
                Convert.ToString(removeLastChar
            ), null).ToString();
        }

        private string removeLastValueOfString(string sender)
        {
            string StrLabelContent = Convert.ToString(sender);
            int CountStrLabelContent = StrLabelContent.Length;

            return StrLabelContent.Remove(CountStrLabelContent - 1);
        }

        private void SetChangePlusMinusValue()
        {
            GetValue.Text = (GetValue.Text.StartsWith("-")) ? GetValue.Text.Substring(1) : "-" + GetValue.Text;
        }

        private void SetResetAndKeepToZero(string sender)
        {
            if (sender == "C")
            {
                LabelCollectVal.Content = " ";
                GetValue.Text = "0";
                this.BoolSetResetAndKeepToZero = true;

            } else if (sender == "CE") {
                this.BoolSetResetAndKeepToZero = true;
                GetValue.Text = "0";
            }

        }
    }

}
