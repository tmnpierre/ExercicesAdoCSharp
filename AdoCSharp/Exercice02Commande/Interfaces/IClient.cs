using System.Data;

public interface IClientService
{
    void Ajouter(Client client);
    Client Obtenir(int id);
    void Modifier(Client client);
    void Supprimer(int id);
    DataTable AfficherTousLesClients();
}
