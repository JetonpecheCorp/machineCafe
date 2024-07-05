using Services.Hardwares;

namespace Services;

public class MachineCafe
{
    public IHardware Hardware { get; init; }

    public byte PrixCafe { get; }
    public uint ArgentTotal { get; set; }

    public MachineCafe()
    {
        Hardware = new Hardware();
        Hardware.CallbackInsertionPiece = Inserer;
        PrixCafe = (byte)EPiece._50Centime;
    }

    public void Inserer(EPiece _piece)
    {
        if ((byte)_piece < PrixCafe)
            return;

        Hardware.CoulerCafe();
        ArgentTotal += (byte)_piece;
    }
}
