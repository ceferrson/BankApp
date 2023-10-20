namespace BankApp.Web.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal AccountNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
