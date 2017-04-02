using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using HemtentaTdd2017.bank;

namespace HemtentaTest2017
{
    public class TestForBank
    {
        IAccount account = new Account();
        public TestForBank()
        {

        }

        [Fact]
        public void Deposit_Success()
        {
            account.Deposit(50);
            var amount = 25;
            var total = account.Amount;

            Assert.Equal(amount, total);

        }

        [Fact]
        public void Deposit_Fail()
        {
            Assert.Throws<IllegalAmountException>(() => account.Deposit(double.NaN));
        }

        [Fact]
        public void WithDraw_Success()
        {
            account.Deposit(50);
            account.Withdraw(25);

            var amount = 25;
            var total = account.Amount;

            Assert.Equal(amount, total);
        }
        [Fact]
        public void WithDraw_Fail()
        {
            Assert.Throws<InsufficientFundsException>(() => account.Withdraw(15));
        }

        [Fact]
        public void Transfer_Success()
        {
            IAccount destination = new Account();
            destination.Deposit(500);
            destination.TransferFunds(account, 500);

            var amount = account.Amount;
            var total = 500;

            Assert.Equal(amount, total);
        }

        [Fact]
        public void Transfer_Fail()
        {
            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(null, 0));
        }

        [Fact]
        public void Tranfer_Insufficient_Funds()
        {
            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(account, 0));
        }

        [Fact]
        public void Transfer_Illigal_Amount()
        {
            Assert.Throws<IllegalAmountException>(() => account.TransferFunds(account, double.NaN));
        }


    }
}
