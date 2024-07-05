namespace Services.CarteBleus;

public class HardwareCarteBleuFaker : IHardwareCarteBleu
{
    public Action<ICarteBleu> CallbackEnregistrerCarteBleu { get; set; } = null!;

    public void SimulerPayementSansContact(ICarteBleu _carteBleu) 
        => CallbackEnregistrerCarteBleu(_carteBleu);
}
