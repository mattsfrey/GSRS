using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSRS
{
    public partial class Form1 : Form
    {
        GasSystem GS;


        public Form1()
        {
            InitializeComponent();

            GS = new GasSystem();
        }

        List<string> _items1 = new List<string>();
        List<string> _items2 = new List<string>();
        List<string> _items3 = new List<string>();



        /* SetBatchToJulian
         * 
         * This function will obtain the DayOfYear value from the datetimepicker,
         * and will format it into the form of XXX001, filling in leading zeros
         * if the day of year is less than 3 digits long. This vlaue will then be 
         * set to the text box for the batch number
         */
        private void SetBatchToJulian()
        {
            string doy = "";

            doy = dateTimePicker1.Value.DayOfYear.ToString();

            if (doy.Length == 2)
            {
                doy = "0" + doy;
            }
            else if (doy.Length == 1)
            {
                doy = "00" + doy;
            }

            batchNum.Text = doy + "001";
            GS.m_iBatchNumber = int.Parse(batchNum.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //set flow handlers to false

            GS.m_bInputFilled = false;
            GS.m_bGasCalcRan  = false;
            GS.m_bPrintRan    = false;

            //load settings
            GS.LoadSettings();

            //load gas data from file
            GS.LoadGasData();

            //load stick data
            GS.LoadSticks();

            //load license info
            string[] licstrs = System.IO.File.ReadAllLines(@"data\license.dat");
            licLabel.Text = "" + licstrs[0] + "\n" + licstrs[1];


            //set previous shifts gas data to text boxes

            gasPrevRegGal.Text    = System.Convert.ToString(GS.m_dGasPrevRegGal);
            gasPrevRegDol.Text    = System.Convert.ToString(GS.m_dGasPrevRegDol);
            gasPrevPremGal.Text   = System.Convert.ToString(GS.m_dGasPrevPremGal);
            gasPrevPremDol.Text   = System.Convert.ToString(GS.m_dGasPrevPremDol);
            gasPrevPlusGal.Text   = System.Convert.ToString(GS.m_dGasPrevPlusGal);
            gasPrevPlusDol.Text   = System.Convert.ToString(GS.m_dGasPrevPlusDol);
            gasPrevRegTotal.Text  = System.Convert.ToString(GS.m_iGasPrevRegTotal);
            gasPrevPremTotal.Text = System.Convert.ToString(GS.m_iGasPrevPremTotal);

            //set delivery numbers to zero
            gasDeliveryReg.Text = "";
            gasDeliveryPrem.Text = "";
            gasBOLNumber.Text = "";

            //set gas prices to text boxes

            gasPriceRegCash.Text    = System.Convert.ToString(GS.m_dGasPriceRegCash);
            gasPriceRegCredit.Text  = System.Convert.ToString(GS.m_dGasPriceRegCredit);
            gasPricePremCash.Text   = System.Convert.ToString(GS.m_dGasPricePremCash);
            gasPricePremCredit.Text = System.Convert.ToString(GS.m_dGasPricePremCredit);
            gasPricePlusCash.Text   = System.Convert.ToString(GS.m_dGasPricePlusCash);
            gasPricePlusCredit.Text = System.Convert.ToString(GS.m_dGasPricePlusCredit);


            //load data into list boxes

            for (double i = 0.0; i <= 92.125; i += 0.125)
            {
                string s = string.Format("{0:N3}", i);
                _items1.Add(s);
                _items2.Add(s);
                _items3.Add(s);
            }

            regList1.DataSource = _items1;
            regList2.DataSource = _items2;
            premList.DataSource = _items3;

            SetBatchToJulian();


            /***********************  settings tab   **************************************/

            //load settings into text boxes

            stationNumberBox.Text = GS.m_sStationNumber;

            toleranceRegGal.Text  = GS.m_dToleranceRegGal.ToString();
            toleranceRegDol.Text  = GS.m_dToleranceRegDol.ToString();
            tolerancePremGal.Text = GS.m_dTolerancePremGal.ToString();
            tolerancePremDol.Text = GS.m_dTolerancePremDol.ToString();
            tolerancePlusGal.Text = GS.m_dTolerancePlusGal.ToString();
            tolerancePlusDol.Text = GS.m_dTolerancePlusDol.ToString();

            siraLogin.Text = GS.m_sSIRALogin;
            siraPass.Text = GS.m_sSIRAPass;

            siraComp1Label.Text = GS.m_sSIRAComp1;
            siraComp2Label.Text = GS.m_sSIRAComp2;
            siraComp3Label.Text = GS.m_sSIRAComp3;

            siraComp1.Text = GS.m_sSIRAComp1;
            siraComp2.Text = GS.m_sSIRAComp2;
            siraComp3.Text = GS.m_sSIRAComp3;

            siraShiftNum.Text = "1";

            cp1RegCash.Text  = GS.m_sSIRAComp1RegCash;
            cp1RegCred.Text  = GS.m_sSIRAComp1RegCred;
            cp1PremCash.Text = GS.m_sSIRAComp1PremCash;
            cp1PremCred.Text = GS.m_sSIRAComp1PremCred;
            cp1PlusCash.Text = GS.m_sSIRAComp1PlusCash;
            cp1PlusCred.Text = GS.m_sSIRAComp1PlusCred;

            cp2RegCash.Text  = GS.m_sSIRAComp2RegCash;
            cp2RegCred.Text  = GS.m_sSIRAComp2RegCred;
            cp2PremCash.Text = GS.m_sSIRAComp2PremCash;
            cp2PremCred.Text = GS.m_sSIRAComp2PremCred;
            cp2PlusCash.Text = GS.m_sSIRAComp2PlusCash;
            cp2PlusCred.Text = GS.m_sSIRAComp2PlusCred;

            cp3RegCash.Text  = GS.m_sSIRAComp3RegCash;
            cp3RegCred.Text  = GS.m_sSIRAComp3RegCred;
            cp3PremCash.Text = GS.m_sSIRAComp3PremCash;
            cp3PremCred.Text = GS.m_sSIRAComp3PremCred;
            cp3PlusCash.Text = GS.m_sSIRAComp3PlusCash;
            cp3PlusCred.Text = GS.m_sSIRAComp3PlusCred;

            /* Handle Links to personal and software sites on about tab
             * 
             * 
             */

            // Add an event handler to do something when the links are clicked.
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);

        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://www.solidresolve.com");
        }

        private void linkLabel2_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://www.gassalessoftware.com");
        }

        /* GasCalcButton_Click
         * 
         * This is a major function that is ran when the user clicks the 'Calculate Gas Values' Button
         * It will first attempt to read in data properly from all the required input boxes, throwing 
         * error messages accordingly if invalid data is encountered. It will then calculate the gas 
         * values and set the stick values to their ideal placements. Finally the function will check
         * the calculated gas values to determine if any of them are either negative or outside of
         * their respective tolerances. If so, error messages are displayed accordingly.
         */

        private void GasCalcButton_Click(object sender, EventArgs e)
        {
            /**************check all textbox data and enter values to gas system class ***********************************/

            //previous shift data

            if (!Double.TryParse(gasPrevRegGal.Text, out GS.m_dGasPrevRegGal))
            {
                MessageBox.Show("Bad data entered for Previous Shift - Regular Gallons",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPrevRegDol.Text, out GS.m_dGasPrevRegDol))
            {
                MessageBox.Show("Bad data entered for Previous Shift - Regular Dollars",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPrevPremGal.Text, out GS.m_dGasPrevPremGal))
            {
                MessageBox.Show("Bad data entered for Previous Shift - Premium Gallons",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPrevPremDol.Text, out GS.m_dGasPrevPremDol))
            {
                MessageBox.Show("Bad data entered for Previous Shift - Premium Dollars",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPrevPlusGal.Text, out GS.m_dGasPrevPlusGal))
            {
                MessageBox.Show("Bad data entered for Previous Shift - Plus Gallons",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPrevPlusDol.Text, out GS.m_dGasPrevPlusDol))
            {
                MessageBox.Show("Bad data entered for Previous Shift - Plus Dollars",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!int.TryParse(gasPrevRegTotal.Text, out GS.m_iGasPrevRegTotal))
            {
                MessageBox.Show("Bad data entered for Previous Shift - Regular Total",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!int.TryParse(gasPrevPremTotal.Text, out GS.m_iGasPrevPremTotal))
            {
                MessageBox.Show("Bad data entered for Previous Shift - Premium Total",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            //current shift "today's shift"

            if (!Double.TryParse(gasCurRegGal.Text, out GS.m_dGasCurRegGal))
            {
                MessageBox.Show("Bad data entered for Today's Shift - Regular Gallons",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasCurRegDol.Text, out GS.m_dGasCurRegDol))
            {
                MessageBox.Show("Bad data entered for Today's Shift - Regular Dollars",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasCurPremGal.Text, out GS.m_dGasCurPremGal))
            {
                MessageBox.Show("Bad data entered for Today's Shift - Premium Gallons",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasCurPremDol.Text, out GS.m_dGasCurPremDol))
            {
                MessageBox.Show("Bad data entered for Today's Shift - Premium Dollars",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasCurPlusGal.Text, out GS.m_dGasCurPlusGal))
            {
                MessageBox.Show("Bad data entered for Today's Shift - Plus Gallons",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasCurPlusDol.Text, out GS.m_dGasCurPlusDol))
            {
                MessageBox.Show("Bad data entered for Today's Shift - Plus Dollars",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!int.TryParse(batchNum.Text, out GS.m_iBatchNumber))
            {
                MessageBox.Show("Bad data entered for Today's Shift - Batch Number",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(batchTotal.Text, out GS.m_dBatchTotal))
            {
                MessageBox.Show("Bad data entered for Today's Shift - Batch Total",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }


            //gas prices
            if (!Double.TryParse(gasPriceRegCash.Text, out GS.m_dGasPriceRegCash))
            {
                MessageBox.Show("Bad data entered for Gas Price - Regular Cash ",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPriceRegCredit.Text, out GS.m_dGasPriceRegCredit))
            {
                MessageBox.Show("Bad data entered for Gas Price - Regular Credit",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPricePremCash.Text, out GS.m_dGasPricePremCash))
            {
                MessageBox.Show("Bad data entered for Gas Price - Premium Cash",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPricePremCredit.Text, out GS.m_dGasPricePremCredit))
            {
                MessageBox.Show("Bad data entered for Gas Price - Premium Credit",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPricePlusCash.Text, out GS.m_dGasPricePlusCash))
            {
                MessageBox.Show("Bad data entered for Gas Price - Plus Cash",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (!Double.TryParse(gasPricePlusCredit.Text, out GS.m_dGasPricePlusCredit))
            {
                MessageBox.Show("Bad data entered for Gas Price - Plus Credit",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            /* delivery and BOL number
             * 
             * This segment of code will check if any of the delivery text boxes or the BOL number
             * text box contain any data. If so, it will be made sure that if there is a delivery entered,
             * there is also a BOL number and vice verse
             */

            GS.m_iGasBOLNumber = 0;
            GS.m_iGasDeliveryReg = 0;
            GS.m_iGasDeliveryPrem = 0;


            if (gasDeliveryPrem.Text != "" || gasDeliveryReg.Text != "" || gasBOLNumber.Text != "")
            {

                if (gasDeliveryReg.Text != "")
                {


                    if (!int.TryParse(gasDeliveryReg.Text, out GS.m_iGasDeliveryReg))
                    {
                        MessageBox.Show("Bad data entered for Gas Deliveries - Regular",
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (gasBOLNumber.Text == "")
                    {
                        MessageBox.Show("You must enter a BOL number",
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                if (gasDeliveryPrem.Text != "")
                {
                    if (!int.TryParse(gasDeliveryPrem.Text, out GS.m_iGasDeliveryPrem))
                    {
                        MessageBox.Show("Bad data entered for Gas Deliveries - Premium",
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;

                    }
                    if (gasBOLNumber.Text == "")
                    {
                        MessageBox.Show("You must enter a BOL number",
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                if (gasBOLNumber.Text != "")
                {
                    if (!int.TryParse(gasBOLNumber.Text, out GS.m_iGasBOLNumber))
                    {
                        MessageBox.Show("Bad data entered for Gas Deliveries - BOL Number",
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    if (gasDeliveryPrem.Text == "" && gasDeliveryReg.Text == "")
                    {
                        MessageBox.Show("You entered a BOL number but no delivery!",
                            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                }


            }

            

 
           
            /********* At this point, all vlaues are valid, so do calculations *********************************/



            GS.CalculateGasValues();

            GS.CalculateIdealSticks();

            regList1.SelectedIndex = GS.m_iIdealStickRegOneIndex;
            regList2.SelectedIndex = GS.m_iIdealStickRegTwoIndex;
            premList.SelectedIndex = GS.m_iIdealStickPremIndex;


            //handle warnings

            string warnmsg = "";


            if ( GS.m_dRegSalesGal < 0 || GS.m_dRegSalesDol < 0 || GS.m_dPremSalesGal < 0 || GS.m_dPremSalesDol < 0 || 
                 GS.m_dPlusSalesGal < 0 || GS.m_dPlusSalesDol < 0)
            {

                if (GS.m_dRegSalesGal < 0)
                {
                    warnmsg += "Negative value calculated for Gas Sales - Regular Gallons.\n";
                }
                if (GS.m_dRegSalesDol < 0)
                {
                    warnmsg += "Negative value calculated for Gas Sales - Regular Dollars.\n";
                }
                if (GS.m_dPremSalesGal < 0)
                {
                    warnmsg += "Negative value calculated for Gas Sales - Premium Gallons.\n";
                }
                if (GS.m_dPremSalesDol < 0)
                {
                    warnmsg += "Negative value calculated for Gas Sales - Premium Dollars.\n";
                }
                if (GS.m_dPlusSalesGal < 0)
                {
                    warnmsg += "Negative value calculated for Gas Sales - Plus Gallons.\n";
                }
                if (GS.m_dPlusSalesDol < 0)
                {
                    warnmsg += "Negative value calculated for Gas Sales - Plus Dollars.";
                }
                    
                MessageBox.Show(warnmsg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

            warnmsg = "";


            if (GS.m_dRegSalesGal > GS.m_dToleranceRegGal || GS.m_dRegSalesDol > GS.m_dToleranceRegDol || GS.m_dPremSalesGal > GS.m_dTolerancePremGal ||
                GS.m_dPremSalesDol > GS.m_dTolerancePremDol || GS.m_dPlusSalesGal > GS.m_dTolerancePlusGal || GS.m_dPlusSalesDol > GS.m_dTolerancePlusDol)
            {
                if (GS.m_dRegSalesGal > GS.m_dToleranceRegGal)
                {
                    warnmsg += "Value exceeds tolerance for Gas Sales - Regular Gallons.\n";
                }
                if (GS.m_dRegSalesDol > GS.m_dToleranceRegDol)
                {
                    warnmsg += "Value exceeds tolerance for Gas Sales - Regular Dollars.\n";
                }
                if (GS.m_dPremSalesGal > GS.m_dTolerancePremGal)
                {
                    warnmsg += "Value exceeds tolerance for Gas Sales - Premium Gallons.\n";
                }
                if (GS.m_dPremSalesDol > GS.m_dTolerancePremDol)
                {
                    warnmsg += "Value exceeds tolerance for Gas Sales - Premium Dollars.\n";
                }
                if (GS.m_dPlusSalesGal > GS.m_dTolerancePlusGal)
                {
                    warnmsg += "Value exceeds tolerance for Gas Sales - Plus Gallons.\n";
                }
                if (GS.m_dPlusSalesDol > GS.m_dTolerancePlusDol)
                {
                    warnmsg += "Value exceeds tolerance for Gas Sales - Plus Dollars.";
                }

                MessageBox.Show(warnmsg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

        }

        private void GasSubmitButton_Click(object sender, EventArgs e)
        {

            //get date strings
            GS.m_dDate = dateTimePicker1.Value;
            GS.m_sDatePrint = dateTimePicker1.Value.ToString("MM/dd/yyyy");
            GS.m_sDateFileName = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            //set stick data from forms

            GS.m_dStickRegOne =  double.Parse(regList1.SelectedValue.ToString());
            GS.m_dStickRegTwo = double.Parse(regList2.SelectedValue.ToString());
            GS.m_dStickPrem   = double.Parse(premList.SelectedValue.ToString());


            GS.m_iGasStickRegTotal = GS._sticks[regList1.SelectedIndex] + GS._sticks[regList2.SelectedIndex];
            GS.m_iGasStickPremTotal = GS._sticks[premList.SelectedIndex];

            //calculate daily variations
            GS.m_iRegVariation = GS.m_iGasStickRegTotal - GS.m_iGasBookRegTotal;
            GS.m_iPremVariation = GS.m_iGasStickPremTotal - GS.m_iGasBookPremTotal; 

            GS.DrawGasImg();

        }

        private void changeShiftButton_Click(object sender, EventArgs e)
        {
            //change all previous shift values to current ones
            gasPrevRegGal.Text    = GS.m_dGasCurRegGal.ToString();
            gasPrevRegDol.Text    = GS.m_dGasCurRegDol.ToString();

            gasPrevPremGal.Text   = GS.m_dGasCurPremGal.ToString();
            gasPrevPremDol.Text   = GS.m_dGasCurPremDol.ToString();

            gasPrevPlusGal.Text   = GS.m_dGasCurPlusGal.ToString();
            gasPrevPlusDol.Text   = GS.m_dGasCurPlusDol.ToString();

            gasPrevRegTotal.Text  = GS.m_iGasStickRegTotal.ToString();
            gasPrevPremTotal.Text = GS.m_iGasStickPremTotal.ToString();


            //clear out other fields

            gasCurRegGal.Text = "";
            gasCurRegDol.Text = "";
            gasCurPremGal.Text = "";
            gasCurPremDol.Text = "";
            gasCurPlusGal.Text = "";
            gasCurPlusDol.Text = "";

            gasBOLNumber.Text = "";
            gasDeliveryReg.Text = "";
            gasDeliveryPrem.Text = "";

            batchNum.Text = "";
            batchTotal.Text = "";

            regList1.SelectedIndex = 0;
            regList2.SelectedIndex = 0;
            premList.SelectedIndex = 0;

            //save new data

            GS.SaveGasData();

        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            if (!Double.TryParse(toleranceRegGal.Text, out GS.m_dToleranceRegGal))
            {
                MessageBox.Show("Bad data entered for Tolerance - Regular Gallons",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            if (!Double.TryParse(toleranceRegDol.Text, out GS.m_dToleranceRegDol))
            {
                MessageBox.Show("Bad data entered for Tolerance - Regular Dollars",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            if (!Double.TryParse(tolerancePremGal.Text, out GS.m_dTolerancePremGal))
            {
                MessageBox.Show("Bad data entered for Tolerance - Premium Gallons",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            if (!Double.TryParse(tolerancePremDol.Text, out GS.m_dTolerancePremDol))
            {
                MessageBox.Show("Bad data entered for Tolerance - Premium Dollars",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            if (!Double.TryParse(tolerancePlusGal.Text, out GS.m_dTolerancePlusGal))
            {
                MessageBox.Show("Bad data entered for Tolerance - Plus Gallons",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            if (!Double.TryParse(tolerancePlusDol.Text, out GS.m_dTolerancePlusDol))
            {
                MessageBox.Show("Bad data entered for Tolerance - Plus Dollars",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (stationNumberBox.Text.Length > 0)
            {
                GS.m_sStationNumber = stationNumberBox.Text;
            }
            else
            {
                MessageBox.Show("Nothing entered for Station Number!",
                   "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (siraLogin.Text.Length > 0)
            {
                GS.m_sSIRALogin = siraLogin.Text;
            }
            else
            {
                MessageBox.Show("Nothing entered for SIRA Login!",
                   "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            if (siraPass.Text.Length > 0)
            {
                GS.m_sSIRAPass = siraPass.Text;
            }
            else
            {
                MessageBox.Show("Nothing entered for SIRA Pass!",
                   "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            //values were ok, save to file

            GS.SaveSettings();


        }

       


        private void gasPrevRegGal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasPrevRegDol_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasPrevPremGal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasPrevPremDol_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasPrevPlusGal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasPrevPlusDol_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasPrevRegTotal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasPrevPremTotal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasCurRegGal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasCurRegDol_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasCurPremGal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasCurPremDol_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasCurPlusGal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasCurPlusDol_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void batchNum_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void batchTotal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gasPriceRegCash_TextChanged(object sender, EventArgs e)
        {
            cpThisRegCash.Text = gasPriceRegCash.Text;
        }

        private void gasPriceRegCredit_TextChanged(object sender, EventArgs e)
        {
            cpThisRegCred.Text = gasPriceRegCredit.Text;
        }

        private void gasPricePremCash_TextChanged(object sender, EventArgs e)
        {
            cpThisPremCash.Text = gasPricePremCash.Text;
        }

        private void gasPricePremCredit_TextChanged(object sender, EventArgs e)
        {
            cpThisPremCred.Text = gasPricePremCredit.Text;
        }

        private void gasPricePlusCash_TextChanged(object sender, EventArgs e)
        {
            cpThisPlusCash.Text = gasPricePlusCash.Text;
        }

        private void gasPricePlusCredit_TextChanged(object sender, EventArgs e)
        {
            cpThisPlusCred.Text = gasPricePlusCredit.Text;
        }

        private void regList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void regList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void premList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            SetBatchToJulian();
        }

        private void stationNumberBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }


    } //end Form1

    
}
