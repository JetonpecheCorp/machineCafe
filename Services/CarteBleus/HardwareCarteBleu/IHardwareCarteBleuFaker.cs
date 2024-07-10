namespace Services.CarteBleus.HardwareCarteBleu;

public interface IHardwareCarteBleuFaker : IHardwareCarteBleu
{
    void SimulerPayementSansContact(ICarteBleu _carteBleu);
}
