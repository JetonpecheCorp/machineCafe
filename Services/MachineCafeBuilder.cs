using Services.CarteBleus.HardwareCarteBleu;
using Services.HardwareNFCs;
using Services.Hardwares;

namespace Services;

public class MachineCafeBuilder
{
    MachineCafeFaker MachineCafe { get; set; } = new();

    public MachineCafeBuilder AjouterHadwareNfc(IHardwareNfcFaker _hardwareNfc)
    {
        MachineCafe.HardwareNfc = _hardwareNfc;
        MachineCafe.HardwareNfc.CallbackBadgePresenter = MachineCafe.BadgePresenter;
        MachineCafe.HardwareNfc.CallbackBadgeRetirer = MachineCafe.BadgeRetirer;

        return this;
    }

    public MachineCafeBuilder AjouterHadwareCarteBleu(IHardwareCarteBleuFaker _hardwareCarteBleu)
    {
        MachineCafe.HardwareCarteBleu = _hardwareCarteBleu;
        MachineCafe.HardwareCarteBleu.CallbackEnregistrerCarteBleu = MachineCafe.PayerSansContact;

        return this;
    }

    public MachineCafeBuilder ModifierHardware(IHardwareFaker _hardware)
    {
        MachineCafe.Hardware = _hardware;
        MachineCafe.Hardware.CallbackInsertionPiece = MachineCafe.Inserer;

        return this;
    }

    public MachineCafeFaker Build()
    {
        if (MachineCafe.Hardware is null)
            ModifierHardware(new HardwareFaker());

        return MachineCafe;
    }
}
