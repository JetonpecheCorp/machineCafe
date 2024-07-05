using Services.CarteBleus;
using Services.Hardwares;

namespace Services;

public class MachineCafe
{
    public IHardware Hardware { get; private init; }
    public IHardwareCarteBleu? HardwareCarteBleu { get; private init; } = null;

    public byte PrixCafe { get; }
    public uint ArgentTotal { get; private set; }

    ICarteBleu? carteBleu;

    public MachineCafe(IHardware _hardware, IHardwareCarteBleu? _hardwareCarteBleu = null)
    {
        Hardware = _hardware;
        Hardware.CallbackInsertionPiece = Inserer;

        HardwareCarteBleu = _hardwareCarteBleu;

        if(HardwareCarteBleu is not null)
            HardwareCarteBleu.CallbackEnregistrerCarteBleu = PayerSansContact;

        PrixCafe = (byte)EPiece._50Centime;
    }

    void Inserer(EPiece _piece)
    {
        ArgentTotal += (byte)_piece;

        if ((byte)ArgentTotal < PrixCafe)
            return;

        Hardware.CoulerCafe();
    }

    void PayerSansContact(ICarteBleu _carteBleu)
    {
        if (ArgentTotal > 0)
            ArgentTotal = 0;

        carteBleu = _carteBleu;

        bool payementValider = carteBleu!.Prelevement((int)EPiece._50Centime);

        if (!payementValider)
            return;

        ArgentTotal += (byte)EPiece._50Centime;

        _carteBleu.Prelevement((byte)EPiece._50Centime);
        Hardware.CoulerCafe();
    }
}
