using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] procedureCodes = new String[] { "D0130", "D0120", "D0140", "20304", "34567" }; //Array of given Procedure Codes

            int arrLength = procedureCodes.Length;  //Length of the Array of given Procedure Codes

            float[] fees = new float[] { 85, 126, 200, 10, 99 };    //Array of the fees of the given Procedure Codes

            float[] discountArray = new float[] { 0, 0, 0, 0, 0 };  //Array of discount values of each of the given Procedure Codes

            float credit = 200;                    //User Credit Initially
            int userInput = 1;                     //For User choice to run the program again
            int delCode;

            do
            {
                Console.WriteLine("\n \n Following are the Available Procedure Codes:");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                DisplayMenu(arrLength, procedureCodes, fees);                          //Displays Initial Menu of Procedure Codes and Fees 

                for (int i = 0; i < arrLength; i++)
                    FindProcedureCodeType(procedureCodes, fees, discountArray, i);     //Finds the Procedure code type and does related operations

                DisplayDisc(arrLength, procedureCodes, discountArray, fees);       //Displays Procedure Codes and their corresponding discounted rates

                ShowCreditInfo(arrLength, credit, discountArray, fees);            //Calculates and Displays the information related to the credit                   

                Console.WriteLine("Press 1 if you accept the following procedure codes : ");
                userInput = Convert.ToInt32(Console.ReadLine());


                if (userInput != 1)                                                     //To Remove one code at a time for the set of proposed codes
                {
                    Console.WriteLine("\n \n Which code would you like to remove ? ");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    DisplayMenu(arrLength, procedureCodes, fees);
                    delCode = Convert.ToInt32(Console.ReadLine()) - 1;                  //Read the index of the code to be removed from the proposed
                    if ((delCode >= arrLength)||(delCode <= 0))
                        Console.WriteLine("Invalid Input. Enter the Slno of the code to be removed."); 

                    procedureCodes = procedureCodes.Where((source, index) => index != delCode).ToArray();   //Remove the code and its linked values from all 3 arrays
                    fees = fees.Where((source, index) => index != delCode).ToArray();
                    discountArray = discountArray.Where((source, index) => index != delCode).ToArray();
                    arrLength = procedureCodes.Length;

                    if (arrLength == 0)                                                 //If there are no more codes to remove
                        Console.WriteLine("Transaction Terminated! ");
                }

            } while ((userInput != 1) && (arrLength != 0));
            Console.ReadLine();
        }

        //Displays Initial Menu of Procedure Codes and Fees       
        public static void DisplayMenu(int arrLength, string[] procedureCodes, float[] fees)
        {
            string line1 = string.Format("{0,-5} {1,-7} {2,-10}", "Slno", "Codes", "Fees");
            Console.WriteLine(line1);
            Console.WriteLine("=======================");
            for (int i = 0; i < arrLength; i++)
            {
                string line2 = string.Format("{0,-5} {1,-7} $ {2,-10}", i + 1, procedureCodes[i], fees[i]);
                Console.WriteLine(line2);
            }
        }

        //Displays Procedure Codes and their corresponding discounted rates
        public static void DisplayDisc(int arrLength, string[] procedureCodes, float[] discArray, float[] fees)
        {
            Console.WriteLine("\n \n The payment details are:");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            string line3 = string.Format("{0,-5} {1,-7} {2,-10}", "Slno", "Codes", "Discount Applied Cost");
            Console.WriteLine(line3);
            Console.WriteLine("=====================================");
            for (int i = 0; i < arrLength; i++)
            {
                string line2 = string.Format("{0,-5} {1,-7} $ {2,-10}", i + 1, procedureCodes[i], (fees[i] - discArray[i]));
                Console.WriteLine(line2);
            }
        }

        //Displays the information related to the credit after calculation
        public static void ShowCreditInfo(int arrLength, float credit, float[] discountArray, float[] fees)
        {
            float creditPayable = 0, creditRemaining = 0, extraPay = 0;
            float feeTotal = fees.Sum();
            float discountTotal = discountArray.Sum();
            Console.Write("\n The total amount to be payed from credit :");
            if ((feeTotal - discountTotal) >= 200)
            {
                creditPayable = 200;
                creditRemaining = 0;
                extraPay = (feeTotal - discountTotal) - 200;
            }
            else
            {
                creditPayable = feeTotal - discountTotal;
                creditRemaining = 200 - (feeTotal - discountTotal);
                extraPay = 0;
            }
            Console.Write("${0}", creditPayable);

            Console.WriteLine("\n\n The amount of credit remaining : ${0}", creditRemaining);

            Console.WriteLine("\n Total discount available : ${0}", discountTotal);

            Console.WriteLine("\n The amount to be paid extra : ${0} \n \n", extraPay);
        }


        //Finds the Procedure code type and calls a function to calculate corresponding discount
        private static void FindProcedureCodeType(string[] ProcCode, float[] Fees, float[] discArray, int counter)
        {
            char[] charArray = ProcCode[counter].ToCharArray();
            if (charArray[0] == 'D')
            {
                if (ProcCode[counter] != "D0120")
                {
                    discArray[counter] = CalcDiscount(1, counter, Fees[counter]);
                }
                else
                {
                    discArray[counter] = CalcDiscount(2, counter, Fees[counter]);
                }
            }
            else
            {
                discArray[counter] = CalcDiscount(3, counter, Fees[counter]);
            }
        }


        //Calculates Discount when called from FindProcedureCodeType()
        private static float CalcDiscount(int codeFlag, int counter, float indiFees)
        {
            float disc = 0;
            if (codeFlag == 1)
            {
                disc = (float)0.33 * indiFees;
            }
            else if (codeFlag == 2)
            {
                disc = 0;
            }
            else if (codeFlag == 3)
            {
                disc = (float)0.58 * indiFees;      //Try Convert.ToFloat
            }
            else
                Console.WriteLine("Invalid");
            return disc;
        }

    }
}

/*   Alternate for Truncating string size as per index value 
 *   public string[] RemoveAt(string[] stringArray, int index)
        {
            if (index < 0 || index >= stringArray.Length)
                return stringArray;
            var newArray = new string[stringArray.Length - 1];
            int j = 0;
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (i == index) continue;
                newArray[j] = stringArray[i];
                j++;
            }
            return newArray;
        }

        */
