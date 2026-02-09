using System;

namespace BankProgram
{
    public class BankAccount
    {
        private decimal _balance;

        public void WithDraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");
        }

    }
}