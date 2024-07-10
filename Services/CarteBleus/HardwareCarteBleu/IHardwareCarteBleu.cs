namespace Services.CarteBleus.HardwareCarteBleu;

public interface IHardwareCarteBleu
{
    /// <summary>
    /// Enregistre la CB
    /// </summary>
    Action<ICarteBleu> CallbackEnregistrerCarteBleu { get; set; }
}
