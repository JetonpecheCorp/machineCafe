namespace Services.CarteBleus;

public interface ICarteBleu
{
    string Numero { get; init; }
    double NbArgent { get; }

    bool Debiter(EPiece _piece);
}
