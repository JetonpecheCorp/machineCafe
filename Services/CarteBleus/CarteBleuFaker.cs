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

    public bool Prelevement(double _montant)
    {
        if (NbArgent < _montant)
            return false;

        NbArgent -= _montant;

        return true;
    }

    public void Remboursement(double _montant) => NbArgent += _montant;
}
