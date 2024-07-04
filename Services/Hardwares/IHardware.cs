namespace Services.Hardwares;

public interface IHardware
{
    Action<EPiece> CallbackInsertionPiece { get; init; }

    bool CoulerCafe();
    void SimulerInsertionPiece(EPiece _piece);
}
