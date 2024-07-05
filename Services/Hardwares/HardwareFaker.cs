namespace Services.Hardwares;

public class HardwareFaker : IHardware
{
    public Action<EPiece> CallbackInsertionPiece { get; set; } = null!;

    public uint NbCafeFabrique { get; set; }

    public void SimulerInsertionPiece(EPiece _piece) => CallbackInsertionPiece(_piece);

    public bool CoulerCafe()
    {
        NbCafeFabrique++;
        return true;
    }
}
