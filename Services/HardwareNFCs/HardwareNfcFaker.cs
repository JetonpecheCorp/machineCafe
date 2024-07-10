
using Services.BadgeNFCs;

namespace Services.HardwareNFCs;
public class HardwareNfcFaker : IHardwareNfcFaker
{
    public Action<IBadgeNFC> CallbackBadgePresenter { get; set; } = null!;
    public Action CallbackBadgeRetirer { get; set; } = null!;

    public void SimulerPresenterBage(IBadgeNFC _badgeNfc) => CallbackBadgePresenter(_badgeNfc);
    public void SimulerRetirerBage() => CallbackBadgeRetirer();
}
