namespace Services.Hardwares;

public interface IHardware
{
    Action<EPiece> CallbackInsertionPiece { get; set; }
    uint NbCafeFabriquer { get; set; }

    bool CoulerCafe();
    void SimulerInsertionPiece(EPiece _piece);
}
