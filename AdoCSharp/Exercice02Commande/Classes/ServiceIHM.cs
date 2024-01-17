using System.Data;

public class ServiceIHM : IServiceIHM
{
    private readonly IClientService _clientService;
    private readonly ICommandeService _commandeService;

    public ServiceIHM(IClientService clientService, ICommandeService commandeService)
    {
        _clientService = clientService;
        _commandeService = commandeService;
    }

    public void LancerMenu()
    {
        bool continuer = true;
        while (continuer)
        {
            Console.Clear();
            AfficherMenu();
            string choix = Console.ReadLine();
            switch (choix)
            {
                case "1":
                    AfficherClients();
                    break;
                case "2":
                    AjouterClient();
                    break;
                case "3":
                    ModifierClient();
                    break;
                case "4":
                    SupprimerClient();
                    break;
                case "5":
                    AfficherCommandes();
                    break;
                case "6":
                    AjouterCommande();
                    break;
                case "7":
                    ModifierCommande();
                    break;
                case "8":
                    SupprimerCommande();
                    break;
                case "0":
                    continuer = false;
                    break;
                default:
                    Console.WriteLine("Choix invalide, veuillez réessayer.");
                    break;
            }
        }

        Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter...");
        Console.ReadKey();
    }

    private void AfficherMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Afficher les clients");
        Console.WriteLine("2. Ajouter un client");
        Console.WriteLine("3. Modifier un client");
        Console.WriteLine("4. Supprimer un client");
        Console.WriteLine("5. Afficher les commandes");
        Console.WriteLine("6. Ajouter une commande");
        Console.WriteLine("7. Modifier une commande");
        Console.WriteLine("8. Supprimer une commande");
        Console.WriteLine("0. Quitter");
    }

    private void AfficherClients()
    {
        Console.Clear();
        DataTable clients = _clientService.AfficherTousLesClients();
    }

    private void AjouterClient()
    {
        Console.Clear();
        Console.WriteLine("Entrez le nom du client :");
        string nom = Console.ReadLine();
        Console.WriteLine("Entrez le prénom du client :");
        string prenom = Console.ReadLine();
        Console.WriteLine("Entrez l'adresse du client :");
        string adresse = Console.ReadLine();
        Console.WriteLine("Entrez le code postal du client :");
        string codePostal = Console.ReadLine();
        Console.WriteLine("Entrez la ville du client :");
        string ville = Console.ReadLine();
        Console.WriteLine("Entrez le numéro de téléphone du client :");
        string telephone = Console.ReadLine();

        Client nouveauClient = new(0, nom, prenom, adresse, codePostal, ville, telephone);

        _clientService.Ajouter(nouveauClient);

        Console.WriteLine("Client ajouté avec succès !");
    }

    private void ModifierClient()
    {
        Console.Clear();
        Console.WriteLine("Entrez l'ID du client à modifier :");
        int id = int.Parse(Console.ReadLine());

        Client clientExistant = _clientService.Obtenir(id);

        if (clientExistant == null)
        {
            Console.WriteLine("Client non trouvé.");
            return;
        }

        Console.WriteLine("Entrez le nouveau nom du client :");
        string nom = Console.ReadLine();
        Console.WriteLine("Entrez le nouveau prénom du client :");
        string prenom = Console.ReadLine();
        Console.WriteLine("Entrez la nouvelle adresse du client :");
        string adresse = Console.ReadLine();
        Console.WriteLine("Entrez le nouveau code postal du client :");
        string codePostal = Console.ReadLine();
        Console.WriteLine("Entrez la nouvelle ville du client :");
        string ville = Console.ReadLine();
        Console.WriteLine("Entrez le nouveau numéro de téléphone du client :");
        string telephone = Console.ReadLine();

        clientExistant.Nom = nom;
        clientExistant.Prenom = prenom;
        clientExistant.Adresse = adresse;
        clientExistant.CodePostal = codePostal;
        clientExistant.Ville = ville;
        clientExistant.Telephone = telephone;

        _clientService.Modifier(clientExistant);

        Console.WriteLine("Client modifié avec succès !");
    }

    private void SupprimerClient()
    {
        Console.Clear();
        Console.WriteLine("Entrez l'ID du client à supprimer :");
        int id = int.Parse(Console.ReadLine());

        _clientService.Supprimer(id);

        Console.WriteLine("Client supprimé avec succès !");
    }

    private void AfficherCommandes()
    {
        Console.Clear();
        DataTable commandes = _commandeService.AfficherToutesLesCommandes();

        Console.WriteLine("Liste des commandes :");
        Console.WriteLine("ID\tClient ID\tDate\tTotal");

        foreach (DataRow row in commandes.Rows)
        {
            int id = Convert.ToInt32(row["ID"]);
            int clientId = Convert.ToInt32(row["ClientId"]);
            DateTime dateCommande = Convert.ToDateTime(row["DateCommande"]);
            decimal total = Convert.ToDecimal(row["Total"]);

            Console.WriteLine($"{id}\t{clientId}\t{dateCommande}\t{total}");
        }
    }

    private void AjouterCommande()
    {
        Console.Clear();
        Console.WriteLine("Entrez l'ID du client pour lequel vous souhaitez ajouter une commande :");
        int clientId = int.Parse(Console.ReadLine());

        Client client = _clientService.Obtenir(clientId);

        if (client == null)
        {
            Console.WriteLine("Client non trouvé.");
            return;
        }

        Console.WriteLine("Entrez la date de la commande (au format yyyy-MM-dd) :");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dateCommande) == false)
        {
            Console.WriteLine("Date invalide.");
            return;
        }

        Console.WriteLine("Entrez le montant total de la commande :");
        if (decimal.TryParse(Console.ReadLine(), out decimal total) == false)
        {
            Console.WriteLine("Montant invalide.");
            return;
        }

        Commande nouvelleCommande = new(0, clientId, dateCommande, total);

        _commandeService.Ajouter(nouvelleCommande);

        Console.WriteLine("Commande ajoutée avec succès !");
    }

    private void ModifierCommande()
    {
        Console.Clear();
        Console.WriteLine("Entrez l'ID de la commande à modifier :");
        int id = int.Parse(Console.ReadLine());

        Commande commandeExistant = _commandeService.Obtenir(id);

        if (commandeExistant == null)
        {
            Console.WriteLine("Commande non trouvée.");
            return;
        }

        Console.WriteLine("Entrez la nouvelle date de la commande (au format yyyy-MM-dd) :");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime nouvelleDateCommande) == false)
        {
            Console.WriteLine("Date invalide.");
            return;
        }

        Console.WriteLine("Entrez le nouveau montant total de la commande :");
        if (decimal.TryParse(Console.ReadLine(), out decimal nouveauTotal) == false)
        {
            Console.WriteLine("Montant invalide.");
            return;
        }

        commandeExistant.DateCommande = nouvelleDateCommande;
        commandeExistant.Total = nouveauTotal;

        _commandeService.Modifier(commandeExistant);

        Console.WriteLine("Commande modifiée avec succès !");
    }

    private void SupprimerCommande()
    {
        Console.Clear();
        Console.WriteLine("Entrez l'ID de la commande à supprimer :");
        int id = int.Parse(Console.ReadLine());

        _commandeService.Supprimer(id);

        Console.WriteLine("Commande supprimée avec succès !");
    }
}
