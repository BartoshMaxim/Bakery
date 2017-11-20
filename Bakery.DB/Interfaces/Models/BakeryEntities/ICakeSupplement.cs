namespace Bakery.DB.Interfaces
{
    public interface ICakeSupplement
    {
        int CakeSupplementId { get; set; }

        int CakeId { get; set; }

        int SupplementId { get; set; }
    }
}
