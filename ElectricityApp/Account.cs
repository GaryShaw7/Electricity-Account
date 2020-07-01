using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricApplication
{
    class ElectricityAccount
    {
        private int intAccRefNo; 	//  - the account reference number
        private String strName;     //  - the name of the account holder
        private String strAddress;  //  - the address of the account holder
        private double dblBalance;  //	- the balance of the account(in £)
        private double dblUnits; 	//  - the current quantity of units used
        private double dblUnitCost=0.02; // 	- the price per unit[initialised = 0.02]
        private Boolean boolActive; //  - indicates if the account is active

        // Constructor which i have used to initialize the data members of new objects.
        public ElectricityAccount(int intNewAccRefNo, String strNewName, String strNewAddress)
        {
            intAccRefNo = intNewAccRefNo;
            strName = strNewName;
            strAddress = strNewAddress;
            dblBalance = 0.0;
            dblUnits = 0.0;
            boolActive = true;
        }
        // Constructor which i have used to initialize the data members of new objects.
        public ElectricityAccount(int intNewAccRefNo, String strNewName, String strNewAddress, double dblNewUnits)
        {
            intAccRefNo = intNewAccRefNo;
            strName = strNewName;
            strAddress = strNewAddress;
            dblBalance = dblUnitCost * dblNewUnits; 
            dblUnits = 0.0;
            boolActive = true;

            
        }
        //method that i have created to deduct the deposit amount from the customers balance.
        public void deposit(double dblDepositAmount)
        {
            
            if (dblDepositAmount > 0)
            {
                dblBalance -= dblDepositAmount;
            }
        }
        //method i have created that will be used to record the units a customer has used to calculate the cost.
        public string recordUnits(double dblUnitsUsed)
        {
            
            
            double amount = dblUnitsUsed * dblUnitCost;

            dblBalance += amount;

            dblUnits += dblUnitsUsed;

            return "Transaction Successful!!";

            

        }
        //method to allow usres to record Account reference number
        public int getAccRefNo()
        {
            return intAccRefNo;
        }
        //method to allow users to record customers names.
        public String getName()
        {
            return strName;
        }
        //method to allow users to record customers address
        public String getAddress()
        {
            return strAddress;
        }
        //method which returns the customers balance
        public double getBalance()
        {
            return dblBalance;
        }
        //method which returns the unit cost
        public double getUnitCost()
        {
            return dblUnitCost;
        }
        //Method which returns units used 
        public double getUnits()
        {
            return dblUnits;
        }
        // method to calculate new unit cost 
        public void updateUnitCost(double dblNewUnitCost)
        {
            
            if(dblNewUnitCost > 0)
            {
                dblUnitCost = dblNewUnitCost;
            }
            
           
        }
        // method to close customers account when balance has been paid
        public Boolean closeAccount()
        {
            if(dblBalance == 0)
            {
                boolActive = false;
                return true;
            }

            
            return false;
        }
        //method which returns true if a customers account is still active
        public Boolean isActive()
        {
            return boolActive;
        }
    }
}
