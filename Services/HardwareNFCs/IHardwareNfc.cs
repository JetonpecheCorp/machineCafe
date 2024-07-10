using Services.BadgeNFCs;

namespace Services.HardwareNFCs;
public interface IHardwareNfc
{
    Action<IBadgeNFC> CallbackBadgePresenter { get; set; }
    Action CallbackBadgeRetirer { get; set; }
}
