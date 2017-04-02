using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.bank
{
    public class Account : IAccount
    {
        double _amount;

        public Account()
        {

        }

        public double Amount
        {
            get
            {
                return _amount;
            }
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new InsufficientFundsException();
            }
            if (double.IsNaN(amount) || double.IsNegativeInfinity(amount) || double.IsPositiveInfinity(Amount))
            {
                throw new IllegalAmountException();
            }
            _amount = amount;
        }

        public void TransferFunds(IAccount destination, double amount)
        {
            if (destination == null)
            {
                throw new OperationNotPermittedException();
            }

            Withdraw(amount);
            destination.Deposit(amount);
        }

        public void Withdraw(double amount)
        {
            if (amount > _amount)
            {
                throw new InsufficientFundsException();
            }

            if (double.IsNaN(amount) || double.IsPositiveInfinity(amount) || double.IsNegativeInfinity(amount))
            {
                throw new IllegalAmountException();
            }


            _amount -= amount;
        }

    }
}