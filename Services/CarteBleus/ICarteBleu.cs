namespace Services.CarteBleus;

public interface ICarteBleu
{
   /// <summary>
   /// Tente de prelever le montant sur la cart bleu
   /// </summary>
   /// <param name="_montant"></param>
   /// <returns>true => OK / false => erreur</returns>
    bool Prelevement(double _montant);

    /// <summary>
    /// Rembourse une somme sur la carte
    /// </summary>
    /// <param name="_montant">Montant a rembourser</param>
    void Remboursement(double _montant);
}
