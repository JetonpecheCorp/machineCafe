namespace Services.Hardwares;

public class HardwareFaker : IHardware
{
    public Action<EPiece> CallbackInsertionPiece { get; init; } = null!;

    private int NbCafeFabriquer = 0;

    public bool CoulerCafe()
    {
        NbCafeFabriquer++;
        return true;
    }

    public void SimulerInsertionPiece(EPiece _piece) => CallbackInsertionPiece(_piece);
}
