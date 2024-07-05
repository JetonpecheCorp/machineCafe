namespace Services.CarteBleus;

public interface IHardwareCarteBleu
{
    /// <summary>
    /// Enregistre la CB
    /// </summary>
    Action<ICarteBleu> CallbackEnregistrerCarteBleu { get; set; }

    void SimulerPayementSansContact(ICarteBleu _carteBleu);
}
