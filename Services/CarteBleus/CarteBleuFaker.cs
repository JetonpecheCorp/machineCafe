namespace Services.CarteBleus;

public class CarteBleuFaker : ICarteBleu
{
    public double NbArgent { get; set; }

    public CarteBleuFaker(double _nbArgent)
    {
        NbArgent = _nbArgent * 100;
    }

    public bool Prelevement(double _montant)
    {
        if (NbArgent < _montant)
            return false;

        NbArgent -= _montant;

        return true;
    }

    public void Remboursement(double _montant) => NbArgent += _montant;
}
