namespace Banks
{
    internal class Program
    {
        private static CentralBank _centralBank;

        public void CreateCentralBank(string bankName, decimal percentDebitScore, decimal percentCreditScore, decimal percentDepositScore)
        {
            _centralBank = CentralBank.GetInstance(bankName, percentDebitScore, percentCreditScore, percentDepositScore);
        }

        private static void Main()
        {
        }
    }
}
