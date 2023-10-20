namespace BankApp.Web.Models.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal AccountNumber { get; set; }
        public int UserId { get; set; }
    }
}
