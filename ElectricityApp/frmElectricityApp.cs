using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectricApplication
{
    public partial class frmMain : Form
    {
        private ElectricityAccount[] data;  // Array for storing ElectricAccount objects
        private const int MAX = 5;  // Maximum number of ElectricAccount objects
        private int nextRef = 1;    // Used for assigning the next ElectricAccount reference number
        private int count = 0;      // To keep track of number of ElectricAccount

        public frmMain()
        {
            data = new ElectricityAccount[MAX]; // initialise storage for ElectricAccount objects
            InitializeComponent();
        }
        //code for list box, array to store customer objects and for loop to run through customers.
        private void listAccounts()
        {
            lstAccounts.Items.Clear();
            for(int index = 0; index < count; index++)
            {
                ElectricityAccount temp = data[index];
                lstAccounts.Items.Add(temp.getAccRefNo() + " " + temp.getName());
            }
        }
        //Code to clear Account fields when a transaction has been completed
        private void clearAcctFields()
        {
            txtAdd.Text = "";
            txtBalance.Text = "";
            txtName.Text = "";
            txtPayment.Text = "";
            txtRef.Text = "";
            txtSetUnits.Text = "";
            txtUnitCost.Text = "";
            txtUnitsUsed.Text = "";
        }
        //Code behind add button from UI form
        private void btnAdd_Click(object sender, EventArgs e) 
        {
            String name = txtName.Text; 
            String address = txtAdd.Text;
           

          

            //Code which allows 5 customer objects to be created.
            if ( count < MAX )
            {
                ElectricityAccount temp = new ElectricityAccount(nextRef, name, address);
                data[count] = temp;
                lstAccounts.Items.Add(nextRef+" "+name);
                count++;
                nextRef++;
                clearAcctFields();
                lblOutput.Text = "New account added.";
            }
            else
            {
                lblOutput.Text = "Can't add anymore Customers!!";

            }
        }

        private void sortAccounts()
        {
            // code for sorting accounts 
            if(rdoBalance.Checked)
            {
                //data array contains Account objects
                //examine balance 
                //bubble sort to sort customers by balance
                ElectricityAccount swap;
                bool swapped = true;


                while(swapped)
                {
                    swapped = false;

                    //inner loop
                    for(int index = 0; index < (count-1); index++)
                    {

                        if(data[index].getBalance() > data[index + 1].getBalance())
                        {
                            swap = data[index];
                            data[index] = data[index + 1];
                            data[index + 1] = swap;

                            swapped = true;
                        }


                    }//for



                }//while

            }else
            {
                ElectricityAccount swap;
                bool swapped = true;

                //bubble sort to sort customers by REf 
                while (swapped)
                {
                    swapped = false;

                    //inner loop
                    for (int index = 0; index < (count - 1); index++)
                    {

                        if (data[index].getAccRefNo() > data[index + 1].getAccRefNo())
                        {
                            swap = data[index];
                            data[index] = data[index + 1];
                            data[index + 1] = swap;

                            swapped = true;
                        }


                    }//for



                }//while
            }
        }
        // code for recording units used by customers
        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                lblOutput.Text = "No accounts avialable";

                return;
            }

            if(lstAccounts.SelectedIndex == -1)
            {
                lblOutput.Text = "Please select an electric account!";
                return;
            }

            int index = lstAccounts.SelectedIndex;
            ElectricityAccount temp = data[index];
            double units = 0;
            try
            {
                units = Convert.ToDouble(txtRecUnits.Text);
            }
            catch (Exception ex)
            {
                lblOutput.Text = "Invalid Number";
                return;
            }
            temp.recordUnits(units);

            txtBalance.Text = temp.getBalance().ToString("C");
            txtUnitsUsed.Text = temp.getUnits().ToString();





        }
        //Code behind payment button from form
        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                lblOutput.Text = "No accounts avialable";

                return;
            }

            if (lstAccounts.SelectedIndex == -1)
            {
                lblOutput.Text = "Please select an electric account!";
                return;
            }

            int index = lstAccounts.SelectedIndex;
            ElectricityAccount temp = data[index];
            double money = 0;
            try
            {
                money = Convert.ToDouble(txtPayment.Text);
            }
            catch (Exception ex)
            {
                lblOutput.Text = "Invalid Number";
                return;
            }
            temp.deposit(money);

            txtBalance.Text = temp.getBalance().ToString("C");
            


            //lblOutput.Text = "Pressing this button should record a payment";
        }
        //Code behind set units button, which allows users to adjust the unit cost
        private void btnSetUnits_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                lblOutput.Text = "No accounts avialable";

                return;
            }

            if (lstAccounts.SelectedIndex == -1)
            {
                lblOutput.Text = "Please select an electric account!";
                return;
            }

            int index = lstAccounts.SelectedIndex;
            ElectricityAccount temp = data[index];
            double unitCost = 0;
            try
            {
                unitCost = Convert.ToDouble(txtSetUnits.Text);
            }
            catch(Exception ex)
            {
                lblOutput.Text = "Invalid Number";
                return;
            }
            temp.updateUnitCost(unitCost);

            txtUnitCost.Text = temp.getUnitCost().ToString("C");

           //lblOutput.Text = "Pressing this button should set price per unit";
        }
        //code behind sort button from form. Calls previous sorting methods from above
        private void btnSort_Click(object sender, EventArgs e)
        {
            // code for sort button
            if(count < 2)
            {
                lblOutput.Text = "Already sorted!";
            }
            
            
            sortAccounts();
            listAccounts();
        }
        // code for Close account button 
        private void btnClose_Click(object sender, EventArgs e)
        {
            

            if(lstAccounts.SelectedIndex == -1)
            {
                return;
            }

            int index = lstAccounts.SelectedIndex;

            ElectricityAccount deleteAcc = data[index];

            if(deleteAcc.closeAccount())
            {
                count--;
                ElectricityAccount[] tempArray = new ElectricityAccount[MAX];


                int elementTemp = 0;


                for(int element = 0; element < data.Length; element++)
                {
                    if(data[element] != null && data[element].isActive())
                    {

                        tempArray[elementTemp] = data[element];
                        elementTemp++;


                    }



                }//for

                data = tempArray;

                clearAcctFields();
                listAccounts();

            }//if
            
           // lblOutput.Text = "Press this button to close an account";
        }
        //Code for list box, which show customers details when selected..
        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int index = lstAccounts.SelectedIndex;
            if (index >= 0)
            {
                ElectricityAccount temp = data[index];
                txtRef.Text = temp.getAccRefNo() + "";
                txtName.Text = temp.getName() + "";
                txtAdd.Text = temp.getAddress();
                txtBalance.Text = temp.getBalance().ToString("C");
                txtUnitsUsed.Text = temp.getUnits().ToString();
                txtUnitCost.Text = temp.getUnitCost().ToString();

             
            }
        }
    }
}
