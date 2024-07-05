namespace Services.Hardwares;

public class Hardware : IHardware
{
    public Action<EPiece> CallbackInsertionPiece { get; set; } = null!;
    public Action CallbackAnnuler { get; set; } = null!;
    public Action CallbackAccepter { get; set; } = null!;
    public uint NbCafeFabrique { get; set; }

    public void AccepterArgent()
    {
        throw new NotImplementedException();
    }

    public bool CoulerCafe()
    {
        NbCafeFabrique++;
        return true;
    }

    public void RendreArgent()
    {
        throw new NotImplementedException();
    }

    public void SimulerInsertionPiece(EPiece _piece) => CallbackInsertionPiece(_piece);


}
