namespace Services.Hardwares;

public interface IHardware
{
    Action<EPiece> CallbackInsertionPiece { get; set; }

    uint NbCafeFabrique { get; set; }

    bool CoulerCafe();
}
