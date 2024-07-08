namespace Services.BadgeNFCs;
public class BadgeNFC : IBadgeNFC
{
    public int Id { get; init; }

    public BadgeNFC(int _id)
    {
        Id = _id;
    }
}
