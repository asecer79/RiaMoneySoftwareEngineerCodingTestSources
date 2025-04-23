namespace Task1_DenominationRoutine
{
    /// <summary>
    /// Calculates all possible combinations of denominations (10, 50, 100 EUR) 
    /// for a given payout amount from the ATM.
    /// The ATM has three cartridges:
    /// - 10 EUR cartridge
    /// - 50 EUR cartridge
    /// - 100 EUR cartridge
    /// 
    /// Example payouts and expected combinations:
    /// - 100 EUR => 10 x 10 EUR, 1 x 50 EUR + 5 x 10 EUR, 2 x 50 EUR, 1 x 100 EUR.
    /// </summary>
    /// <param name="amount">The payout amount to calculate denomination combinations for.</param>
    /// <returns>A list of possible combinations where each combination specifies the count of 10, 50, and 100 EUR notes.</returns>


    //Solution:
    // The code calculates all possible combinations of denominations (10, 50, 100 EUR)

    class Program
    {
        static void Main()
        {
            var payments = new int[] { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

            foreach (var payment in payments)
            {
                Console.WriteLine($"For {payment}:");

                var combinations = GetCombinations(payment);

                foreach (var combination in combinations)
                {
                    Console.WriteLine(combination);
                }

                Console.WriteLine("***********************");
            }
        }

        static List<string> GetCombinations(int amount)
        {
            var combinations = new List<string>();

            for (int i = 0; i <= amount / 100; i++)
            {
                for (int j = 0; j <= amount / 50; j++)
                {
                    for (int k = 0; k <= amount / 10; k++)
                    {
                        int sum = (i * 100) + (j * 50) + (k * 10);

                        if (sum == amount)
                        {
                            combinations.Add($"{i}x100 + {j}x50 + {k}x10");
                        }
                    }
                }
            }

            return combinations;
        }
    }

}