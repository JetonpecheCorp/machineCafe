namespace Services.Hardwares;

public interface IHardware
{
    Action<EPiece> CallbackInsertionPiece { get; set; }
    Action CallbackAnnuler { get; set; }
    Action CallbackAccepter { get; set; }

    uint NbCafeFabriquer { get; set; }

    bool CoulerCafe();
    void SimulerInsertionPiece(EPiece _piece);

    void RendreArgent();
    void AccepterArgent();
}
