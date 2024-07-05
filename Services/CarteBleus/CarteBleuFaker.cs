namespace Services.CarteBleus;

public class CarteBleuFaker : ICarteBleu
{
    public required string Numero { get; init; }
    public required double NbArgent { get; set; }

    public bool Debiter(EPiece _piece)
    {
        if (NbArgent < (double)_piece)
            return false;

        NbArgent -= (double)_piece;

        return true;
    }
}
