namespace Services.Hardwares;

public class HardwareFaker : IHardware
{
    public Action<EPiece> CallbackInsertionPiece { get; set; } = null!;
    public Action CallbackAnnuler { get; set; } = null!;
    public Action CallbackAccepter { get; set; } = null!;

    public uint NbCafeFabrique { get; set; }

    public void SimulerInsertionPiece(EPiece _piece) => CallbackInsertionPiece(_piece);


    public bool CoulerCafe()
    {
        NbCafeFabrique++;
        return true;
    }

    public void AccepterArgent()
    {
        CallbackAccepter();
    }

    public void RendreArgent()
    {
        CallbackAnnuler();
    }
}
