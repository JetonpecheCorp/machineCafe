using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Hardwares;

public class Hardware : IHardware
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
