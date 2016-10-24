using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasinoSimulator
{
    // This class is supposed to simulate a classic slot machine.
    // The slot machine has three "dials"; each dial can show:
    // 7 : "Seven"
    // # : "Sharp"
    // @ : "At"
    //
    // The player can enter "credits" into the machine.
    // Each spin on the machine costs one credit.
    // Certain combinations of the three dials gives the player
    // a winning.
    //
    class SlotMachineSimulator
    {
        // INSTANCE VARIABLES

        // The numbers of credits left in the machine
        private int credits;

        // The three dials on the slot machine
        private string dial1;
        private string dial2;
        private string dial3;

        // This instance variable is used for generating random numbers
        private Random generator;

        private bool silentModeOn;

        private int gotStar3;
        private int gotSeven3;
        private int gotSharp3;
        private int gotAt3;
        private int gotSeven2;
        private int gotSharp2;
        private int gotNothing;


        // CONSTANTS

        // The probabilities that a dial shows a certain symbol:
        // 10 % for showing "7"
        // 30 % for showing "#"
        // 60 % for showing "@"
        //
        private const int probstar = 2;
        private const int probSeven = 10;
        private const int probSharp = 30;
        private const int probAt = 60;

        // The winnings paid to the player for certain dial combinations
        // * * * pasy 1000
        // 7 7 7 pays 150
        // # # # pays 10
        // @ @ @ pays 1
        // any two 7 pays 5
        // any two # pays 1
        //
        private const int winningStar3 = 1000;
        private const int winningSeven3 = 150;
        private const int winningSharp3 = 10;
        private const int winningAt3 = 1;
        private const int winningSeven2 = 5;
        private const int winningSharp2 = 1;
    


        // CONSTRUCTOR
        //
        public SlotMachineSimulator()
        {
            silentModeOn = false;
            generator = new Random();
            Reset();
        }
 

        // PUBLIC METHODS

        // Sets the simulator back to its starting state
        public void Reset()
        {
            credits = 0;

            gotStar3 = 0;
            gotSeven3 = 0;
            gotSharp3 = 0;
            gotAt3 = 0;
            gotSeven2 = 0;
            gotSharp2 = 0;
            gotNothing = 0;
        }
        public void SetSilentMode(bool silentModeOn)
        {
            this.silentModeOn = silentModeOn;
        }

        public bool GetSilentMode()
        {
            return silentModeOn;
        }

        // Returns the number of credits left in the machine
        public int GetCredits()
        {
            return credits;
        }
    
        // Adds a number of credits to the machine
       
        public void PrintMessageBeforeSpin()
        {
            Console.WriteLine();
            Console.WriteLine("Credits left : {0}, now spinning...", credits);
        }
       
        public void PrintOutcomeOfSpin(int winnings)
        {
            WriteLineConditional("You got : " + dial1 + " " + dial2 + "" + dial3);
            if (winnings == 0)
            {
                WriteLineConditional("Sorry you did not win anything");
            }
            else if (winnings == 1)
            {
                WriteLineConditional("You won 1 credit");
            }
            else
            {
                WriteLineConditional("You won " + winnings + "credits, congratulations");
            }

            Console.WriteLine("Credits left : {0}", credits);
        }
        // Perform one spin on the machine
        public void Spin()
        {
            // Inform the player how many credits are left before spinning

            PrintMessageBeforeSpin();

            // One spin costs one credit
            credits--;

            // Spin the dials

            SpinAllDials();
        
            // Find the winnings, and update the credits left
            int winnings = CalculateWinnings(dial1,dial2,dial3);
            credits = credits + winnings;

            // Report the outcome of the spin to the player
            PrintOutcomeOfSpin(winnings);
           
        }
        public void AddCredits(int moreCredits)
        {
            credits = credits + moreCredits;
        }
        public void SpinAllDials()
        {
            dial1 = SpinDial();
            dial2 = SpinDial();
            dial3 = SpinDial();
        }

        // Print the complete winning table
        public void PrintWinningTable()
        {
            WriteLineConditional("----------- Winning table for slot machine --------------");
            WriteLineConditional(" * * * pays      " + winningStar3);
            WriteLineConditional(" 7 7 7 pays      " + winningSeven3);
            WriteLineConditional(" # # # pays      " + winningSharp3);
            WriteLineConditional(" @ @ @ pays      " + winningAt3);
            WriteLineConditional(" any two 7 pays  " + winningSeven2);
            WriteLineConditional(" any two # pays  " + winningSharp2);
            WriteLineConditional("---------------------------------------------------------");
            WriteLineConditional();
        }
        public void PrintStatistics()
        {
            WriteLineConditional("----------- Statistics for slot machine -----------------");
            WriteLineConditional(" * * * seen      " + gotStar3 + " times");
            WriteLineConditional(" 7 7 7 seen      " + gotSeven3 + " times");
            WriteLineConditional(" # # # seen      " + gotSharp3 + " times");
            WriteLineConditional(" @ @ @ seen      " + gotAt3 + " times");
            WriteLineConditional(" any two 7 seen  " + gotSeven2 + " times");
            WriteLineConditional(" any two # seen  " + gotSharp2 + " times");
            WriteLineConditional(" No win    seen  " + gotNothing + " times");
            WriteLineConditional("---------------------------------------------------------");
        }

        // PRIVATE METHODS

        // Generate a "percentage", i.e. a number between 0 and 100 (100 not included)
        private int GeneratePercentage()
        {
            int result = generator.Next(100);
            return result;
        }

        // Spin a dial, using the defined percentages
        private string SpinDial()
        {
            // Generate a random percentage
            int percentage = GeneratePercentage();

            // Given the random percentage, find out what the dial should show
            //
            if (percentage < probstar)
            {
                return "*";
            }
            if (percentage < probSeven)
            {
                return "7";
            }
            else if (percentage < (probSeven + probSharp))
            {
                return "#";
            }
            else
            {
                return "@";
            }
        }

        // Calculate the winnings corresponding to the given dial combination
        private int CalculateWinnings(string dial1, string dial2, string dial3)
        {
            if (CountSymbols("*", dial1, dial2, dial3) == 3)
            {
                gotStar3++;
                return winningStar3;
            }
            if (CountSymbols("7", dial1, dial2, dial3) == 3)
            {
                return winningSeven3;
            }
            else if (CountSymbols("#", dial1, dial2, dial3) == 3)
            {
                return winningSharp3;
            }
            else if (CountSymbols("@", dial1, dial2, dial3) == 3)
            {
                return winningAt3;
            }
            else if (CountSymbols("7", dial1, dial2, dial3) == 2)
            {
                return winningSeven2;
            }
            else if (CountSymbols("#", dial1, dial2, dial3) == 2)
            {
                return winningSharp2;
            }
            else
            {
                return 0;
            }
        }

        // A helper method for counting how many of the three strings that
        // are equal to the "target" string
        private int CountSymbols(string target, string c1, string c2, string c3)
        {
            int count = 0;

            if (target == c1) count++;
            if (target == c2) count++;
            if (target == c3) count++;

            return count;
        }
        private void WriteLineConditional(string message)
        {
            if (!silentModeOn)
            {
                Console.WriteLine(message);
            }
        }
        private void WriteLineConditional()
        {
            WriteLineConditional("");
        }
    }
}
