namespace CW10.DTOs;

public class NewClientDto
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }
    public DateTime? PaymentDate { get; set; }
}

public class ClientShortDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}