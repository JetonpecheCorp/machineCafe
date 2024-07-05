using Services.CarteBleus;
using Services.Hardwares;

namespace Services;

public class MachineCafe
{
    public IHardware Hardware { get; init; }

    public byte PrixCafe { get; }
    public uint ArgentTotal { get; private set; }

    public MachineCafe()
    {
        Hardware = new Hardware();
        Hardware.CallbackInsertionPiece = Inserer;
        PrixCafe = (byte)EPiece._50Centime;
    }

    public void Inserer(EPiece _piece)
    {
        if ((byte)ArgentTotal < PrixCafe)
            return;

        Hardware.CoulerCafe();
        ArgentTotal += (byte)_piece;
    }

    public void SansContact(ICarteBleu _carteBleu)
    {
        if (ArgentTotal > 0)
        {
            ArgentTotal = 0;
            return;
        }

        bool payementValider = _carteBleu.Debiter(EPiece._50Centime);

        if (!payementValider)
            return;

        ArgentTotal += (byte)EPiece._50Centime;
        Hardware.CoulerCafe();
    }
}
