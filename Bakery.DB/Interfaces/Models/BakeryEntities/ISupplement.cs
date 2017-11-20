namespace Bakery.DB.Interfaces
{
    public interface ISupplement
    {
        int SupplementId { get; set; }

        string SupplementName { get; set; }

        string SupplementDescription { get; set; }

        int SupplementPrice { get; set; }
    }
}
