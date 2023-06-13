public class Tusa
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public ulong CreatorId { get; set; }
    public User Creator { get; set; }
    public DateTime Date { get; set; }

    public uint Money { get; set; }
    public string Order { get; set; }

    public string Comment { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}