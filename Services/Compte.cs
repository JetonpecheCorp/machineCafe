using Services.BadgeNFCs;

namespace Services;
public sealed class Compte
{
    public int Id { get; init; }
    public uint Argent { get; set; }
    public IBadgeNFC BadgeNFC { get; set; }

    public Compte(int _id, IBadgeNFC _badgeNFC, uint _argent)
    {
        Id = _id;
        BadgeNFC = _badgeNFC;
        Argent = _argent * 100;
    }
}
