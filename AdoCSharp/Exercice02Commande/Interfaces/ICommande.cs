using System.Data;

public interface ICommandeService
{
    void Ajouter(Commande commande);
    Commande Obtenir(int id);
    void Modifier(Commande commande);
    void Supprimer(int id);
    DataTable AfficherToutesLesCommandes();
}
