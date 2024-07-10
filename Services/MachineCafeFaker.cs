using Services.BadgeNFCs;
using Services.CarteBleus;
using Services.CarteBleus.HardwareCarteBleu;
using Services.HardwareNFCs;
using Services.Hardwares;

namespace Services;

public class MachineCafeFaker
{
    public IHardwareFaker Hardware { get; set; }
    public IHardwareCarteBleuFaker? HardwareCarteBleu { get; set; }
    public IHardwareNfcFaker? HardwareNfc { get; set; }

    public byte PrixCafe { get; }
    public uint ArgentTotal { get; private set; }

    public List<Compte> ListeCompte { get; private set; } = [];

    ICarteBleu? carteBleu;
    IBadgeNFC? badgeNFC;

    bool badgeEstPresent = false;
    ushort prelevement = 1_000;

    public MachineCafeFaker()
    {
        PrixCafe = (byte)EPiece._50Centime;

        ListeCompte.Add(new Compte(1, new BadgeNFC(1), 100));
    }

    public void Inserer(EPiece _piece)
    {
        ArgentTotal += (byte)_piece;

        if (badgeEstPresent && ArgentTotal > 0)
            return;

        if ((byte)ArgentTotal < PrixCafe)
            return;

        Hardware.CoulerCafe();
    }

    public void PayerSansContact(ICarteBleu? _carteBleu)
    {
        if (_carteBleu is null)
            return;

        if(badgeEstPresent)
        {
            var compte = ListeCompte
                .Where(x => x.BadgeNFC.Id == badgeNFC?.Id)
                .First();

            ArgentTotal = prelevement;

            return;
        }

        if (ArgentTotal > 0)
            ArgentTotal = 0;

        carteBleu = _carteBleu;

        bool payementValider = carteBleu!.Prelevement((byte)EPiece._50Centime);

        if (!payementValider)
            return;

        ArgentTotal += (byte)EPiece._50Centime;

        _carteBleu.Prelevement((byte)EPiece._50Centime);
        bool cafeTerminer = Hardware.CoulerCafe();

        if (!cafeTerminer)
            _carteBleu.Remboursement((byte)EPiece._50Centime);
    }

    public void PayerParBadge()
    {
        var compte = ListeCompte.Where(x => x.BadgeNFC.Id == badgeNFC?.Id).First();

        if (compte.Argent <= 0 || compte.Argent - (byte)EPiece._50Centime < 0)
            return;

        compte.Argent -= (byte)EPiece._50Centime;
    }

    public void BadgePresenter(IBadgeNFC _badgeNfc)
    {
        var compte = ListeCompte.Where(x => x.BadgeNFC.Id == _badgeNfc.Id).FirstOrDefault();

        if(compte is null)
        {
            ListeCompte.Add(new Compte(
                Random.Shared.Next(1, 100_000), 
                _badgeNfc, 
                0)
            );
        }

        badgeNFC = _badgeNfc;
        badgeEstPresent = true;
    }

    public void BadgeRetirer()
    {
        badgeEstPresent = false;

        var compte = ListeCompte.Where(x => x.BadgeNFC.Id == badgeNFC!.Id).First();

        if (ArgentTotal > 0)
        {
            if (!carteBleu?.Prelevement(prelevement) ?? false)
            {
                ArgentTotal = 0;
                return;
            }

            compte.Argent += ArgentTotal;
            ArgentTotal = 0;
            return;
        }

        if (compte.Argent < PrixCafe)
            return;

        badgeNFC = null;
        Hardware.CoulerCafe();
    }
}
