namespace Services.CarteBleus;

public class CarteBleuFaker : ICarteBleu
{
    public string Numero { get; init; }
    public double NbArgent { get; set; }

    public CarteBleuFaker(string _numero, double _nbArgent)
    {
        Numero = _numero;
        NbArgent = _nbArgent * 100;
    }

    public bool Debiter(EPiece _piece)
    {
        if (NbArgent < (double)_piece)
            return false;

        NbArgent -= (double)_piece;

        return true;
    }
}
