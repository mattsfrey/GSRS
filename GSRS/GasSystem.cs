using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GSRS
{
    public class GasSystem
    {
        /******************* Variables *********************************/



        /* The following boolean variables are used to control the data flow
         * through the app as data is entered and various processes are run.
         * They control whether buttons are grayed out or not, ensuring that 
         * the user enters data and runs functions in the sequential 
         * manner in which the app is designed to run
         */
        public bool m_bInputFilled;           //are all the input fields filled out?
        public bool m_bGasCalcRan;            //have the gas calculations been run?
        public bool m_bPrintRan;              //has the print sheet function been run?

        //following variables will be loaded from XML at application start

     
        public double m_dGasPrevRegGal;       //Previous shift regular gallons
        public double m_dGasPrevRegDol;       //Previous shift regular dollars
        public double m_dGasPrevPremGal;      //Previous shift premium gallons
        public double m_dGasPrevPremDol;      //Previous shift premium dollars
        public double m_dGasPrevPlusGal;      //Previous shift plus gallons
        public double m_dGasPrevPlusDol;      //Previous shift plus dollars

        public int    m_iGasPrevRegTotal;     //Previous shift regular total (tank total)
        public int    m_iGasPrevPremTotal;    //Previous shift premium total (tank total)

        public double m_dGasPriceRegCash;     //gas price for regular cash
        public double m_dGasPriceRegCredit;   //gas price for regular credit
        public double m_dGasPricePremCash;    //gas price for premium cash
        public double m_dGasPricePremCredit;  //gas price for premium credit
        public double m_dGasPricePlusCash;    //gas price for plus cash
        public double m_dGasPricePlusCredit;  //gas price for plus credit

        public List<int> _sticks;             //list of stick data


        //following variables will be entered by user into forms

        public double m_dGasCurRegGal;        //Current shift regular gallons
        public double m_dGasCurRegDol;        //Current shift regular dollars
        public double m_dGasCurPremGal;       //Current shift premium gallons
        public double m_dGasCurPremDol;       //Current shift premium dollars
        public double m_dGasCurPlusGal;       //Current shift plus gallons
        public double m_dGasCurPlusDol;       //Current shift plus dollars

        public int m_iBatchNumber;            //batch number (day of year # + 001)
        public double m_dBatchTotal;          //batch total / host total / credit cards

        public int m_iGasDeliveryReg;         //gallons of regular gas delivered
        public int m_iGasDeliveryPrem;        //gallons of premium gas delivered
        public int m_iGasBOLNumber;           //BOL number for gas Delivery

        public double m_dStickRegOne;         //stick height in 1/8ths of inches for regular tank 1
        public double m_dStickRegTwo;         //stick height in 1/8ths of inches for regular tank 2
        public double m_dStickPrem;           //stick height in 1/8ths of inches for premium tank

        public string m_sDatePrint;
        public string m_sDateFileName;

        public DateTime m_dDate;

        //following variables will be calculated

        public double m_dRegSalesGal;         //sales for regular gas (gallons) (curreggal - prevreggal)
        public double m_dRegSalesDol;         //sales for regular gas (dollars) (curregdol - prevregdol)
        public double m_dPremSalesGal;        //sales for premium gas (gallons) (curpremgal - prevpremgal)
        public double m_dPremSalesDol;        //sales for premium gas (dollars) (curpremdol - prevpremdol)
        public double m_dPlusSalesGal;        //sales for plus gas (gallons) (curplusgal - prevplusgal)
        public double m_dPlusSalesDol;        //sales for plus gas (dollars) (curplusdol - prevplusol)

        public double m_dTotalSales;          //total gas sales, calculated from gas numbers (regsalesdol + premsalesdol + plussalesdol)
        public double m_dBankDeposit;         //bank deposit amount, calculated from (total sales - batch total)  

        public double m_dPlusSixty;           //60% of the plus sales (gallons) (added to regsales to get regular tank total)
        public double m_dPlusForty;           //40% of the plus sales (gallons) (added to premsales to get premium tank total)

        public int m_iGasSalesRegTotal;       //total regular sales (gallons) rounded
        public int m_iGasSalesPremTotal;      //total premium sales (gallons) rounded

        public int m_iGasBookRegTotal;        //Current shift book value regular total (tank total) rounded to integer
        public int m_iGasBookPremTotal;       //Current shift book value premium total (tank total) rounded to integer

        public int m_iGasStickRegTotal;       //Current shift stick value regular total (tank total) rounded to integer
        public int m_iGasStickPremTotal;      //Current shift stick value premium total (tank total) rounded to integer

        public int m_iIdealStickRegOne;       //ideal stick height in 1/8ths of inches for regular tank 1
        public int m_iIdealStickRegTwo;       //ideal stick height in 1/8ths of inches for regular tank 2
        public int m_iIdealStickPrem;         //ideal stick height in 1/8ths of inches for premium tank

        public int m_iIdealStickRegOneIndex;
        public int m_iIdealStickRegTwoIndex;
        public int m_iIdealStickPremIndex;

        public int m_iRegVariation;           //variation for stick / book for regular tanks
        public int m_iPremVariation;          //variation for stick / book for premium tank  

        public int m_iTotalStartReg;
        public int m_iTotalStartPrem;


        /**********************  settings *******************************************/

        //gas sheet settings

        public string m_sStationNumber;

        public double m_dToleranceRegDol;
        public double m_dTolerancePremDol;
        public double m_dTolerancePlusDol;

        public double m_dToleranceRegGal;
        public double m_dTolerancePremGal;
        public double m_dTolerancePlusGal;

        //web sira
        public string m_sSIRALogin;
        public string m_sSIRAPass;

        public string m_sSIRAComp1;
        public string m_sSIRAComp2;
        public string m_sSIRAComp3;

        public string m_sSIRAComp1RegCash;
        public string m_sSIRAComp1RegCred;
        public string m_sSIRAComp1PremCash;
        public string m_sSIRAComp1PremCred;
        public string m_sSIRAComp1PlusCash;
        public string m_sSIRAComp1PlusCred;

        public string m_sSIRAComp2RegCash;
        public string m_sSIRAComp2RegCred;
        public string m_sSIRAComp2PremCash;
        public string m_sSIRAComp2PremCred;
        public string m_sSIRAComp2PlusCash;
        public string m_sSIRAComp2PlusCred;

        public string m_sSIRAComp3RegCash;
        public string m_sSIRAComp3RegCred;
        public string m_sSIRAComp3PremCash;
        public string m_sSIRAComp3PremCred;
        public string m_sSIRAComp3PlusCash;
        public string m_sSIRAComp3PlusCred;



        /****************************** Functions *********************************/

        //constructor
        public GasSystem()
        {
             _sticks = new List<int>();
        }


        //function to load gas data from XML file
        public void LoadGasData()
        {
            string[] lines = System.IO.File.ReadAllLines(@"data\gas.dat");
            string[] entries;
            foreach (string line in lines)
            {

                //check for previous gas data
                if (line.Contains("GAS_PREV_REG_GAL"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPrevRegGal = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PREV_REG_DOL"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPrevRegDol = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PREV_PREM_GAL"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPrevPremGal = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PREV_PREM_DOL"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPrevPremDol = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PREV_PLUS_GAL"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPrevPlusGal = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PREV_PLUS_DOL"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPrevPlusDol = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PREV_REG_TOTAL"))
                {
                    entries = line.Split(' ');
                    this.m_iGasPrevRegTotal = int.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PREV_PREM_TOTAL"))
                {
                    entries = line.Split(' ');
                    this.m_iGasPrevPremTotal = int.Parse(entries[1]);
                }

                //check for gas prices

                else if (line.Contains("GAS_PRICE_REG_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPriceRegCash = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PRICE_REG_CREDIT"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPriceRegCredit = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PRICE_PREM_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPricePremCash = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PRICE_PREM_CREDIT"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPricePremCredit = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PRICE_PLUS_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPricePlusCash = double.Parse(entries[1]);
                }
                else if (line.Contains("GAS_PRICE_PLUS_CREDIT"))
                {
                    entries = line.Split(' ');
                    this.m_dGasPricePlusCredit = double.Parse(entries[1]);
                }
            }

        }

        //function to load gas data from XML file
        public void LoadSettings()
        {
            string[] lines = System.IO.File.ReadAllLines(@"data\settings.dat");
            string[] entries;
            foreach (string line in lines)
            {


                if (line.Contains("STATION_NUMBER"))
                {
                    entries = line.Split(' ');
                    this.m_sStationNumber = int.Parse(entries[1]).ToString();
                }
                else if (line.Contains("SIRA_LOGIN"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRALogin = entries[1];
                }
                else if (line.Contains("SIRA_PASS"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAPass = entries[1];
                }
                else if (line.Contains("SIRA_COMP1_NAME"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp1 = entries[1];
                }
                else if (line.Contains("SIRA_COMP2_NAME"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp2 = entries[1];
                }
                else if (line.Contains("SIRA_COMP3_NAME"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp3 = entries[1];
                }
                else if (line.Contains("SIRA_COMP1_REG_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp1RegCash = entries[1];
                }
                else if (line.Contains("SIRA_COMP1_REG_CRED"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp1RegCred = entries[1];
                }
                else if (line.Contains("SIRA_COMP1_PREM_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp1PremCash = entries[1];
                }
                else if (line.Contains("SIRA_COMP1_PREM_CRED"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp1PremCred = entries[1];
                }
                else if (line.Contains("SIRA_COMP1_PLUS_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp1PlusCash = entries[1];
                }
                else if (line.Contains("SIRA_COMP1_PLUS_CRED"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp1PlusCred = entries[1];
                }
                else if (line.Contains("SIRA_COMP2_REG_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp2RegCash = entries[1];
                }
                else if (line.Contains("SIRA_COMP2_REG_CRED"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp2RegCred = entries[1];
                }
                else if (line.Contains("SIRA_COMP2_PREM_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp2PremCash = entries[1];
                }
                else if (line.Contains("SIRA_COMP2_PREM_CRED"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp2PremCred = entries[1];
                }
                else if (line.Contains("SIRA_COMP2_PLUS_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp2PlusCash = entries[1];
                }
                else if (line.Contains("SIRA_COMP2_PLUS_CRED"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp2PlusCred = entries[1];
                }
                else if (line.Contains("SIRA_COMP3_REG_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp3RegCash = entries[1];
                }
                else if (line.Contains("SIRA_COMP3_REG_CRED"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp3RegCred = entries[1];
                }
                else if (line.Contains("SIRA_COMP3_PREM_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp3PremCash = entries[1];
                }
                else if (line.Contains("SIRA_COMP3_PREM_CRED"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp3PremCred = entries[1];
                }
                else if (line.Contains("SIRA_COMP3_PLUS_CASH"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp3PlusCash = entries[1];
                }
                else if (line.Contains("SIRA_COMP3_PLUS_CRED"))
                {
                    entries = line.Split(' ');
                    this.m_sSIRAComp3PlusCred = entries[1];
                }
                else if (line.Contains("SALES_TOLERANCE_REG_DOL"))
                {
                    entries = line.Split(' ');
                    this.m_dToleranceRegDol = double.Parse(entries[1]);
                }
                else if (line.Contains("SALES_TOLERANCE_PREM_DOL"))
                {
                    entries = line.Split(' ');
                    this.m_dTolerancePremDol = double.Parse(entries[1]);
                }
                else if (line.Contains("SALES_TOLERANCE_PLUS_DOL"))
                {
                    entries = line.Split(' ');
                    this.m_dTolerancePlusDol = double.Parse(entries[1]);
                }
                else if (line.Contains("SALES_TOLERANCE_REG_GAL"))
                {
                    entries = line.Split(' ');
                    this.m_dToleranceRegGal = double.Parse(entries[1]);
                }
                else if (line.Contains("SALES_TOLERANCE_PREM_GAL"))
                {
                    entries = line.Split(' ');
                    this.m_dTolerancePremGal = double.Parse(entries[1]);
                }
                else if (line.Contains("SALES_TOLERANCE_PLUS_GAL"))
                {
                    entries = line.Split(' ');
                    this.m_dTolerancePlusGal = double.Parse(entries[1]);
                }

            } 

        }

        public void LoadSticks()
        {
            string[] lines = System.IO.File.ReadAllLines(@"data\sticks.dat");

            foreach (string line in lines)
            {
                _sticks.Add(int.Parse(line));
            }
        }

        //function to save gas data to XML file
        public void SaveGasData()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"data\gas.dat"))
            {
                file.WriteLine("GAS_PREV_REG_GAL " + m_dGasCurRegGal);
                file.WriteLine("GAS_PREV_REG_DOL " + m_dGasCurRegDol);
                file.WriteLine("GAS_PREV_PREM_GAL " + m_dGasCurPremGal);
                file.WriteLine("GAS_PREV_PREM_DOL " + m_dGasCurPremDol);
                file.WriteLine("GAS_PREV_PLUS_GAL " + m_dGasCurPlusGal);
                file.WriteLine("GAS_PREV_PLUS_DOL " + m_dGasCurPlusDol);
                file.WriteLine("GAS_PREV_REG_TOTAL " + m_iGasStickRegTotal);
                file.WriteLine("GAS_PREV_PREM_TOTAL " + m_iGasStickPremTotal);
                file.WriteLine("GAS_PRICE_REG_CASH " + m_dGasPriceRegCash);
                file.WriteLine("GAS_PRICE_REG_CREDIT " + m_dGasPriceRegCredit);
                file.WriteLine("GAS_PRICE_PREM_CASH " + m_dGasPricePremCash);
                file.WriteLine("GAS_PRICE_PREM_CREDIT " + m_dGasPricePremCredit);
                file.WriteLine("GAS_PRICE_PLUS_CASH " + m_dGasPricePlusCash);
                file.WriteLine("GAS_PRICE_PLUS_CREDIT " + m_dGasPricePlusCredit);
            }  
        }

        //function to save gas data to XML file
        public void SaveSettings()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"data\settings.dat"))
            {
                file.WriteLine("STATION_NUMBER " + m_sStationNumber);
                file.WriteLine("SIRA_LOGIN " + m_sSIRALogin);
                file.WriteLine("SIRA_PASS " + m_sSIRAPass);
                
                file.WriteLine("SIRA_COMP1_NAME " + m_sSIRAComp1);
                file.WriteLine("SIRA_COMP2_NAME " + m_sSIRAComp2);
                file.WriteLine("SIRA_COMP3_NAME " + m_sSIRAComp3);
                
                file.WriteLine("SIRA_COMP1_REG_CASH " + m_sSIRAComp1RegCash);
                file.WriteLine("SIRA_COMP1_REG_CRED " + m_sSIRAComp1RegCred);
                file.WriteLine("SIRA_COMP1_PREM_CASH " + m_sSIRAComp1PremCash);
                file.WriteLine("SIRA_COMP1_PREM_CRED " + m_sSIRAComp1PremCred);
                file.WriteLine("SIRA_COMP1_PLUS_CASH " + m_sSIRAComp1PlusCash);
                file.WriteLine("SIRA_COMP1_PLUS_CRED " + m_sSIRAComp1PlusCred);

                file.WriteLine("SIRA_COMP2_REG_CASH " + m_sSIRAComp2RegCash);
                file.WriteLine("SIRA_COMP2_REG_CRED " + m_sSIRAComp2RegCred);
                file.WriteLine("SIRA_COMP2_PREM_CASH " + m_sSIRAComp2PremCash);
                file.WriteLine("SIRA_COMP2_PREM_CRED " + m_sSIRAComp2PremCred);
                file.WriteLine("SIRA_COMP2_PLUS_CASH " + m_sSIRAComp2PlusCash);
                file.WriteLine("SIRA_COMP2_PLUS_CRED " + m_sSIRAComp2PlusCred);

                file.WriteLine("SIRA_COMP3_REG_CASH " + m_sSIRAComp3RegCash);
                file.WriteLine("SIRA_COMP3_REG_CRED " + m_sSIRAComp3RegCred);
                file.WriteLine("SIRA_COMP3_PREM_CASH " + m_sSIRAComp3PremCash);
                file.WriteLine("SIRA_COMP3_PREM_CRED " + m_sSIRAComp3PremCred);
                file.WriteLine("SIRA_COMP3_PLUS_CASH " + m_sSIRAComp3PlusCash);
                file.WriteLine("SIRA_COMP3_PLUS_CRED " + m_sSIRAComp3PlusCred);

                file.WriteLine("SALES_TOLERANCE_REG_GAL " + m_dToleranceRegGal);
                file.WriteLine("SALES_TOLERANCE_REG_DOL " + m_dToleranceRegDol);
                file.WriteLine("SALES_TOLERANCE_PREM_GAL " + m_dTolerancePremGal);
                file.WriteLine("SALES_TOLERANCE_PREM_DOL " + m_dTolerancePremDol);
                file.WriteLine("SALES_TOLERANCE_PLUS_GAL " + m_dTolerancePlusGal);
                file.WriteLine("SALES_TOLERANCE_PLUS_DOL " + m_dTolerancePlusDol);
            }
        }

        public void CalculateIdealSticks()
        {
            int i = 0;

            int j = 0, k = 0;

            int regBook = this.m_iGasBookRegTotal;
            int premBook = this.m_iGasBookPremTotal;

            int close;
            int close1 = _sticks[0];
            int close2 = _sticks[0];

            int check;

            close = -999999;

            while (i <= 734)
            {
                check = _sticks[i] + _sticks[i];

                if (Math.Abs(regBook - check) < Math.Abs(regBook - close))
                {
                    close1 = _sticks[i];
                    close2 = _sticks[i];
                    close = check;
                    j = i;
                    k = i;

                }
                else if (Math.Abs(regBook - check) == Math.Abs(regBook - close))
                {
                    if (Math.Abs(i - i) < Math.Abs(j - k))
                    {
                        close1 = _sticks[i];
                        close2 = _sticks[i];
                        close = check;
                        j = i;
                        k = i;
                    }
                }

                check = _sticks[i] + _sticks[i + 1];

                if (Math.Abs(regBook - check) < Math.Abs(regBook - close))
                {
                    close1 = _sticks[i];
                    close2 = _sticks[i+1];
                    close = check;
                    j = i;
                    k = i + 1;
                }
                else if (Math.Abs(regBook - check) == Math.Abs(regBook - close))
                {
                    if (Math.Abs(i - (i+1)) < Math.Abs(j - k))
                    {
                        close1 = _sticks[i];
                        close2 = _sticks[i];
                        close = check;
                        j = i;
                        k = i + 1;
                    }
                }

                check = _sticks[i] + _sticks[i+2];

                if (Math.Abs(regBook - check) < Math.Abs(regBook - close))
                {
                    close1 = _sticks[i];
                    close2 = _sticks[i+2];
                    close = check;
                    j = i;
                    k = i + 2;
                }
                else if (Math.Abs(regBook - check) == Math.Abs(regBook - close))
                {
                    if (Math.Abs(i - (i+2)) < Math.Abs(j - k))
                    {
                        close1 = _sticks[i];
                        close2 = _sticks[i];
                        close = check;
                        j = i;
                        k = i+2;
                    }
                }

                i++;
            }

            this.m_iIdealStickRegOne = close1;
            this.m_iIdealStickRegTwo = close2;

            this.m_iIdealStickRegOneIndex = j;
            this.m_iIdealStickRegTwoIndex = k;

            //calculate premium

            i = 0;
            j = 0;
            close = -9999999;
            close1 = _sticks[0];

            while (i <= 736)
            {
                if (Math.Abs(premBook - _sticks[i]) < Math.Abs(premBook - close1))
                {
                    close1 = _sticks[i];
                    j = i;
                }
                i++;
            }
            this.m_iIdealStickPrem = close1;
            this.m_iIdealStickPremIndex = j;



          
        }




        //calculate gas sheet values
        public void CalculateGasValues()
        {
            //calcuate sales for reg, prem, plus

            //regular
            m_dRegSalesGal = Math.Round((m_dGasCurRegGal - m_dGasPrevRegGal), 3);
            m_dRegSalesDol = Math.Round((m_dGasCurRegDol - m_dGasPrevRegDol), 2);

            //premium
            m_dPremSalesGal = Math.Round((m_dGasCurPremGal - m_dGasPrevPremGal), 3);
            m_dPremSalesDol = Math.Round((m_dGasCurPremDol - m_dGasPrevPremDol), 2);
 
            //plus
            m_dPlusSalesGal = Math.Round((m_dGasCurPlusGal - m_dGasPrevPlusGal), 3);
            m_dPlusSalesDol = Math.Round((m_dGasCurPlusDol - m_dGasPrevPlusDol), 2);

            //calculate 60 / 40
            m_dPlusSixty = Math.Round((m_dPlusSalesGal * 0.60), 3);
            m_dPlusForty = Math.Round((m_dPlusSalesGal * 0.40), 3);


            //calculate total sales for regular / premium
            m_iGasSalesRegTotal = (int)Math.Round((m_dRegSalesGal + m_dPlusSixty), 0);
            m_iGasSalesPremTotal = (int)Math.Round((m_dPremSalesGal + m_dPlusForty), 0);

            m_dTotalSales = Math.Round(m_dRegSalesDol + m_dPremSalesDol + m_dPlusSalesDol, 2);

            m_dBankDeposit = Math.Round(m_dTotalSales - m_dBatchTotal, 2);

            //calculate book values for tanks


            m_iTotalStartReg = m_iGasPrevRegTotal + m_iGasDeliveryReg;
            m_iTotalStartPrem = m_iGasPrevPremTotal + m_iGasDeliveryPrem;

            m_iGasBookRegTotal  = m_iGasPrevRegTotal + m_iGasDeliveryReg - m_iGasSalesRegTotal;
            m_iGasBookPremTotal = m_iGasPrevPremTotal + m_iGasDeliveryPrem - m_iGasSalesPremTotal;


        }

        public void SwapShiftData()
        {

            m_dGasPrevRegGal = m_dGasCurRegGal;
            m_dGasPrevRegDol = m_dGasCurRegDol;
            m_dGasPrevPremGal = m_dGasCurPremGal;
            m_dGasPrevPremDol = m_dGasCurPremDol;
            m_dGasPrevPlusGal = m_dGasCurPlusGal;
            m_dGasPrevPlusDol = m_dGasCurPlusDol;

            m_iGasPrevRegTotal = m_iGasStickRegTotal;
            m_iGasPrevPremTotal = m_iGasStickRegTotal;


        }


        public void DrawGasImg()
        {
            //previous shift data points
            PointF PREV_REG_GAL = new PointF(992f, 284f);
            PointF PREV_REG_DOL = new PointF(805, 284);
            PointF PREV_PREM_GAL = new PointF(1363, 284);
            PointF PREV_PREM_DOL = new PointF(1176, 284);
            PointF PREV_PLUS_DOL = new PointF(805, 1007);
            PointF PREV_PLUS_GAL = new PointF(992, 1007);

            //current shift data points
            PointF CUR_REG_DOL = new PointF(805, 253);
            PointF CUR_REG_GAL = new PointF(992, 253);
            PointF CUR_PREM_DOL = new PointF(1176, 253);
            PointF CUR_PREM_GAL = new PointF(1363, 253);
            PointF CUR_PLUS_DOL = new PointF(805, 977);
            PointF CUR_PLUS_GAL = new PointF(992, 977);

            //sales data points
            PointF SALES_REG_DOL = new PointF(805, 344);
            PointF SALES_REG_GAL = new PointF(992, 344);
            PointF SALES_PREM_DOL = new PointF(1176, 344);
            PointF SALES_PREM_GAL = new PointF(1363, 344);
            PointF SALES_PLUS_DOL = new PointF(805, 1067);
            PointF SALES_PLUS_GAL = new PointF(992, 1067);

            //price data points
            PointF PRICE_REG_CASH = new PointF(805, 374);
            PointF PRICE_REG_CREDIT = new PointF(805, 405);
            PointF PRICE_PREM_CASH = new PointF(1176, 374);
            PointF PRICE_PREM_CREDIT = new PointF(1176, 405);
            PointF PRICE_PLUS_CASH = new PointF(805, 1097);
            PointF PRICE_PLUS_CREDIT = new PointF(805, 1128);

            //point data for 60 / 40
            PointF LOCATION_60 = new PointF(1176, 977);
            PointF LOCATION_40 = new PointF(1176, 1007);


            //point data for left column


            //sticks
            PointF STICK_REG_ONE = new PointF(165, 141);
            PointF STICK_REG_TWO = new PointF(165, 184);
            PointF STICK_PREM = new PointF(293, 141);

            //totals

            PointF PREV_REG_TOTAL = new PointF(157, 721);
            PointF PREV_PREM_TOTAL = new PointF(286, 721);

            PointF DELIVERY_REG = new PointF(157, 763);
            PointF DELIVERY_PREM = new PointF(286, 763);

            PointF REG_TANK_TOTAL = new PointF(157, 886);
            PointF PREM_TANK_TOTAL = new PointF(286, 886);

            PointF REG_SALES_TOTAL = new PointF(157, 927);
            PointF PREM_SALES_TOTAL = new PointF(286, 927);

            PointF REG_BOOK_INV = new PointF(157, 968);
            PointF PREM_BOOK_INV = new PointF(286, 968);

            PointF REG_STICK_INV = new PointF(157, 1009);
            PointF PREM_STICK_INV = new PointF(286, 1009);

            PointF REG_VARIATION = new PointF(157, 1050);
            PointF PREM_VARIATION = new PointF(286, 1050);

            PointF LOCATION_BOL = new PointF(283, 1132);

            PointF STATION_NUMBER = new PointF(1148, 12);

            PointF LOCATION_DATE = new PointF(1585, 12);

            PointF TOTAL_REG_SELF = new PointF(1833, 166);
            PointF TOTAL_PREM_SELF = new PointF(1833, 277);
            PointF TOTAL_PLUS_SELF = new PointF(1833, 388);
            PointF TOTAL_SALES = new PointF(1833, 658);
            PointF BANK_DEPOSIT = new PointF(1833, 768);
            PointF BATCH_TOTAL = new PointF(1833, 878);
            PointF BATCH_NUMBER = new PointF(1673,878);

           // Bitmap bitmap = (Bitmap)Image.FromFile(@"data\GSR_Template2.bmp");//load the image file

            Image bitmap = Image.FromFile(@"data\GSR_Template2.jpg");//load the image file

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font TNR20 = new Font("Times New Roman", 27))
                {

                    //draw previous shift data
                    graphics.DrawString(this.m_dGasPrevRegGal.ToString(), TNR20, Brushes.Black, PREV_REG_GAL);
                    graphics.DrawString(this.m_dGasPrevRegDol.ToString(), TNR20, Brushes.Black, PREV_REG_DOL);
                    graphics.DrawString(this.m_dGasPrevPremGal.ToString(), TNR20, Brushes.Black, PREV_PREM_GAL);
                    graphics.DrawString(this.m_dGasPrevPremDol.ToString(), TNR20, Brushes.Black, PREV_PREM_DOL);
                    graphics.DrawString(this.m_dGasPrevPlusDol.ToString(), TNR20, Brushes.Black, PREV_PLUS_DOL);
                    graphics.DrawString(this.m_dGasPrevPlusGal.ToString(), TNR20, Brushes.Black, PREV_PLUS_GAL);

                    //draw current shift data
                    graphics.DrawString(this.m_dGasCurRegDol.ToString(), TNR20, Brushes.Black, CUR_REG_DOL);
                    graphics.DrawString(this.m_dGasCurRegGal.ToString(), TNR20, Brushes.Black, CUR_REG_GAL);
                    graphics.DrawString(this.m_dGasCurPremDol.ToString(), TNR20, Brushes.Black, CUR_PREM_DOL);
                    graphics.DrawString(this.m_dGasCurPremGal.ToString(), TNR20, Brushes.Black, CUR_PREM_GAL);
                    graphics.DrawString(this.m_dGasCurPlusGal.ToString(), TNR20, Brushes.Black, CUR_PLUS_GAL);
                    graphics.DrawString(this.m_dGasCurPlusDol.ToString(), TNR20, Brushes.Black, CUR_PLUS_DOL);

                    //draw sales data
                    graphics.DrawString("$ " + this.m_dRegSalesDol.ToString(), TNR20, Brushes.Black, SALES_REG_DOL);
                    graphics.DrawString("$ " + this.m_dPremSalesDol.ToString(), TNR20, Brushes.Black, SALES_PREM_DOL);
                    graphics.DrawString("$ " + this.m_dPlusSalesDol.ToString(), TNR20, Brushes.Black, SALES_PLUS_DOL);

                    graphics.DrawString(this.m_dRegSalesGal.ToString(), TNR20, Brushes.Black, SALES_REG_GAL);
                    graphics.DrawString(this.m_dPremSalesGal.ToString(), TNR20, Brushes.Black, SALES_PREM_GAL);
                    graphics.DrawString(this.m_dPlusSalesGal.ToString(), TNR20, Brushes.Black, SALES_PLUS_GAL);


                    //draw prices
                    graphics.DrawString("CA $" + this.m_dGasPriceRegCash, TNR20, Brushes.Black, PRICE_REG_CASH);
                    graphics.DrawString("CA $" + this.m_dGasPricePremCash, TNR20, Brushes.Black, PRICE_PREM_CASH);
                    graphics.DrawString("CA $" + this.m_dGasPricePlusCash, TNR20, Brushes.Black, PRICE_PLUS_CASH);

                    graphics.DrawString("CR $" + this.m_dGasPriceRegCredit, TNR20, Brushes.Black, PRICE_REG_CREDIT);
                    graphics.DrawString("CR $" + this.m_dGasPricePremCredit, TNR20, Brushes.Black, PRICE_PREM_CREDIT);
                    graphics.DrawString("CR $" + this.m_dGasPricePlusCredit, TNR20, Brushes.Black, PRICE_PLUS_CREDIT);

                    //draw 60 / 40
                    graphics.DrawString("60% = " + this.m_dPlusSixty, TNR20, Brushes.Black, LOCATION_60);
                    graphics.DrawString("40% = " + this.m_dPlusForty, TNR20, Brushes.Black, LOCATION_40);

                }  //end middle area, times new roman 20 font


                using (Font TNR26 = new Font("Times New Roman", 33))
                {
                    //draw sticks
                    graphics.DrawString(m_dStickRegOne.ToString(), TNR26, Brushes.Black, STICK_REG_ONE);
                    graphics.DrawString(m_dStickRegTwo.ToString(), TNR26, Brushes.Black, STICK_REG_TWO);
                    graphics.DrawString(m_dStickPrem.ToString(), TNR26, Brushes.Black, STICK_PREM);

                    //totals
                    graphics.DrawString(m_iGasPrevRegTotal.ToString(), TNR26, Brushes.Black, PREV_REG_TOTAL);
                    graphics.DrawString(m_iGasPrevPremTotal.ToString(), TNR26, Brushes.Black, PREV_PREM_TOTAL);

                    if (m_iGasDeliveryReg > 0)
                    {
                        graphics.DrawString(m_iGasDeliveryReg.ToString(), TNR26, Brushes.Black, DELIVERY_REG);
                    }

                    if (m_iGasDeliveryPrem > 0)
                    {
                        graphics.DrawString(m_iGasDeliveryPrem.ToString(), TNR26, Brushes.Black, DELIVERY_PREM);
                    }

                    graphics.DrawString(m_iTotalStartReg.ToString(), TNR26, Brushes.Black, REG_TANK_TOTAL);
                    graphics.DrawString(m_iTotalStartPrem.ToString(), TNR26, Brushes.Black, PREM_TANK_TOTAL);

                    graphics.DrawString(m_iGasSalesRegTotal.ToString(), TNR26, Brushes.Black, REG_SALES_TOTAL);
                    graphics.DrawString(m_iGasSalesPremTotal.ToString(), TNR26, Brushes.Black, PREM_SALES_TOTAL);

                    graphics.DrawString(m_iGasBookRegTotal.ToString(), TNR26, Brushes.Black, REG_BOOK_INV);
                    graphics.DrawString(m_iGasBookPremTotal.ToString(), TNR26, Brushes.Black, PREM_BOOK_INV);

                    graphics.DrawString(m_iGasStickRegTotal.ToString(), TNR26, Brushes.Black, REG_STICK_INV);
                    graphics.DrawString(m_iGasStickPremTotal.ToString(), TNR26, Brushes.Black, PREM_STICK_INV);

                    graphics.DrawString(m_iRegVariation.ToString(), TNR26, Brushes.Black, REG_VARIATION);
                    graphics.DrawString(m_iPremVariation.ToString(), TNR26, Brushes.Black, PREM_VARIATION);

                    if (m_iGasBOLNumber > 0)
                    {
                        graphics.DrawString(m_iGasBOLNumber.ToString(), TNR26, Brushes.Black, LOCATION_BOL);
                    }

                    graphics.DrawString("58218", TNR26, Brushes.Black, STATION_NUMBER);
                    graphics.DrawString(m_sDatePrint, TNR26, Brushes.Black, LOCATION_DATE);


                    //draw totals

                    graphics.DrawString("$ " + m_dRegSalesDol.ToString(), TNR26, Brushes.Black, TOTAL_REG_SELF);
                    graphics.DrawString("$ " + m_dPremSalesDol.ToString(), TNR26, Brushes.Black, TOTAL_PREM_SELF);
                    graphics.DrawString("$ " + m_dPlusSalesDol.ToString(), TNR26, Brushes.Black, TOTAL_PLUS_SELF);

                    graphics.DrawString("$ " + m_dTotalSales.ToString(), TNR26, Brushes.Black, TOTAL_SALES);
                    graphics.DrawString("$ " + m_dBankDeposit.ToString(), TNR26, Brushes.Black, BANK_DEPOSIT);
                    graphics.DrawString("$ " + m_dBatchTotal.ToString(), TNR26, Brushes.Black, BATCH_TOTAL);
                    graphics.DrawString(m_iBatchNumber.ToString(), TNR26, Brushes.Black, BATCH_NUMBER);



                }
            }

            //bitmap.Save(@"imglog\" + m_sDateFileName + ".bmp");//save the image file
            bitmap.Save(@"imglog\" + m_sDateFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += (s, ev) =>
            {
                Image image = bitmap;

                float quotient = 1;
                float margin = 20;

                float page_w = ev.PageBounds.Width - (2 * margin);
                float page_h = ev.PageBounds.Height - (2 * margin);

                if (image.Width >= image.Height)
                {
                    quotient = page_w / image.Width;
                }
                if (image.Width < image.Height)
                {
                    quotient = image.Height / page_h;
                }

                float w = page_w;
                float h = image.Height * quotient;

                ev.Graphics.DrawImage(image, margin, margin, w, h);
                ev.HasMorePages = false;
            };
            doc.DefaultPageSettings.Landscape = true;
            

            doc.Print();   
        }
           
    }
}
