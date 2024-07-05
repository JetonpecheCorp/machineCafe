using Services.CarteBleus;
using Services.Hardwares;

namespace Services;

public class MachineCafe
{
    public IHardware Hardware { get; private set; }
    public IHardwareCarteBleu? HardwareCarteBleu { get; private set; } = null;

    public byte PrixCafe { get; }
    public uint ArgentTotal { get; private set; }

    ICarteBleu? carteBleu;

    public MachineCafe()
    {
        PrixCafe = (byte)EPiece._50Centime;
    }

    public MachineCafe AjouterHadwareCarteBleu(IHardwareCarteBleu _hardwareCarteBleu)
    {
        HardwareCarteBleu = _hardwareCarteBleu;
        HardwareCarteBleu.CallbackEnregistrerCarteBleu = PayerSansContact;

        return this;
    }

    public MachineCafe ModifierHardware(IHardware _hardware)
    {
        Hardware = _hardware;
        Hardware.CallbackInsertionPiece = Inserer;

        return this;
    }

    public MachineCafe Build()
    {
        if(Hardware is null)
            ModifierHardware(new HardwareFaker());

        return this;
    }

    void Inserer(EPiece _piece)
    {
        ArgentTotal += (byte)_piece;

        if ((byte)ArgentTotal < PrixCafe)
            return;

        Hardware.CoulerCafe();
    }

    void PayerSansContact(ICarteBleu? _carteBleu)
    {
        if (_carteBleu is null)
            return;

        if (ArgentTotal > 0)
            ArgentTotal = 0;

        carteBleu = _carteBleu;

        bool payementValider = carteBleu!.Prelevement((byte)EPiece._50Centime);

        if (!payementValider)
            return;

        ArgentTotal += (byte)EPiece._50Centime;

        _carteBleu.Prelevement((byte)EPiece._50Centime);
        bool cafeTerminer = Hardware.CoulerCafe();

        if (!cafeTerminer)
            _carteBleu.Remboursement((byte)EPiece._50Centime);
    }
}
