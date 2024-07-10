using Services.BadgeNFCs;

namespace Services.HardwareNFCs;

public interface IHardwareNfcFaker: IHardwareNfc
{
    void SimulerPresenterBage(IBadgeNFC _badgeNfc);
    void SimulerRetirerBage();
}
