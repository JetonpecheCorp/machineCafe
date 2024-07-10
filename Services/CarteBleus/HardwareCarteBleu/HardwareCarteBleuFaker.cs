namespace Services.CarteBleus.HardwareCarteBleu;

public class HardwareCarteBleuFaker : IHardwareCarteBleuFaker
{
    public Action<ICarteBleu> CallbackEnregistrerCarteBleu { get; set; } = null!;

    public void SimulerPayementSansContact(ICarteBleu _carteBleu)
        => CallbackEnregistrerCarteBleu(_carteBleu);
}
