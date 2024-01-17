class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tomyn\OneDrive\Documents\03 - M2i Exercices\03 - Exercices AdoCSharp\AdoCSharp\Exercice02Commande\GestionCommandesDB.mdf;Integrated Security=True;Connect Timeout=30"; ;

            var connexionBaseDeDonnees = new ConnexionBaseDeDonnees(connectionString);

            var clientService = new ClientService(connexionBaseDeDonnees);
            var commandeService = new CommandeService(connexionBaseDeDonnees);

            var serviceIHM = new ServiceIHM(clientService, commandeService);

            serviceIHM.LancerMenu();
        }
    }