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
        ArgentTotal += (byte)_piece;

        if ((byte)ArgentTotal < PrixCafe)
            return;

        Hardware.CoulerCafe();
    }

    public void Annuler() => ArgentTotal = 0;
    public void Valider() => Hardware.CoulerCafe();

    public void SansContact(ICarteBleu _carteBleu)
    {
        if (ArgentTotal > 0)
        {
            Hardware.RendreArgent();
            return;
        }

        bool payementValider = _carteBleu.Debiter(EPiece._50Centime);

        if (!payementValider)
            return;

        ArgentTotal += (byte)EPiece._50Centime;
        Hardware.AccepterArgent();
    }
}
