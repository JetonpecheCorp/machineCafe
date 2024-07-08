using Services.CarteBleus;
using Services.HardwareNFCs;
using Services.Hardwares;

namespace Services;

public class MachineCafeBuilder
{
    MachineCafe MachineCafe { get; set; } = new();

    public MachineCafeBuilder AjouterHadwareNfc(IHardwareNfc _hardwareNfc)
    {
        MachineCafe.HardwareNfc = _hardwareNfc;
        MachineCafe.HardwareNfc.CallbackBadgePresenter = MachineCafe.BadgePresenter;
        MachineCafe.HardwareNfc.CallbackBadgeRetirer = MachineCafe.BadgeRetirer;

        return this;
    }

    public MachineCafeBuilder AjouterHadwareCarteBleu(IHardwareCarteBleu _hardwareCarteBleu)
    {
        MachineCafe.HardwareCarteBleu = _hardwareCarteBleu;
        MachineCafe.HardwareCarteBleu.CallbackEnregistrerCarteBleu = MachineCafe.PayerSansContact;

        return this;
    }

    public MachineCafeBuilder ModifierHardware(IHardware _hardware)
    {
        MachineCafe.Hardware = _hardware;
        MachineCafe.Hardware.CallbackInsertionPiece = MachineCafe.Inserer;

        return this;
    }

    public MachineCafe Build()
    {
        if (MachineCafe.Hardware is null)
            ModifierHardware(new HardwareFaker());

        return MachineCafe;
    }
}
