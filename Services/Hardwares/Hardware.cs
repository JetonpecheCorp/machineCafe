namespace Services.Hardwares;

public class Hardware : IHardware
{
    public Action<EPiece> CallbackInsertionPiece { get; set; } = null!;
    public uint NbCafeFabriquer { get; set; }

    public bool CoulerCafe()
    {
        NbCafeFabriquer++;
        return true;
    }

    public void SimulerInsertionPiece(EPiece _piece) => CallbackInsertionPiece(_piece);


}
