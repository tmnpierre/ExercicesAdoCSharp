namespace ConsoleAppEtudiant
{
    public class IHM
    {
        public static void GererMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Ajouter un étudiant");
                Console.WriteLine("2. Afficher tous les étudiants");
                Console.WriteLine("3. Supprimer un étudiant");
                Console.WriteLine("4. Quitter");
                Console.Write("Entrez votre choix : ");
                string choix = Console.ReadLine();

                try
                {
                    switch (choix)
                    {
                        case "1":
                            AjouterEtudiant();
                            break;
                        case "2":
                            Etudiant.AfficherEtudiants();
                            break;
                        case "3":
                            SupprimerEtudiant();
                            break;
                        case "4":
                            Console.WriteLine("Fin du programme.");
                            Console.ReadKey();
                            return;
                        default:
                            Console.WriteLine("Choix non valide.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Une erreur est survenue : {ex.Message}");
                }

                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }

        private static void AjouterEtudiant()
        {
            Console.Write("Entrez le nom : ");
            string nom = Console.ReadLine();

            Console.Write("Entrez le prénom : ");
            string prenom = Console.ReadLine();

            Console.Write("Entrez le numéro de classe : ");
            int numeroClasse = int.Parse(Console.ReadLine());

            Console.Write("Entrez la date de diplôme (YYYY-MM-DD) : ");
            DateTime dateDiplome = DateTime.Parse(Console.ReadLine());

            Etudiant.AjouterEtudiant(nom, prenom, numeroClasse, dateDiplome);
            Console.WriteLine("Étudiant ajouté avec succès.");
        }

        private static void SupprimerEtudiant()
        {
            Console.Write("Entrez l'ID de l'étudiant à supprimer : ");
            int id = int.Parse(Console.ReadLine());

            Etudiant.SupprimerEtudiant(id);
        }
    }
}
