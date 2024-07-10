namespace Services.Hardwares;

public class Hardware : IHardware
{
    public Action<EPiece> CallbackInsertionPiece { get; set; } = null!;
    public uint NbCafeFabrique { get; set; }

    public bool CoulerCafe()
    {
        NbCafeFabrique++;
        return true;
    }
}
