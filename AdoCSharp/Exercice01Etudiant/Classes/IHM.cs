using System;

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
                Console.WriteLine("4. Modifier un étudiant");
                Console.WriteLine("5. Quitter");
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
                            AfficherEtudiants();
                            break;
                        case "3":
                            SupprimerEtudiant();
                            break;
                        case "4":
                            ModifierEtudiant();
                            break;
                        case "5":
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
            var etudiant = LireInformationsEtudiant();
            if (etudiant.Save())
            {
                Console.WriteLine("Étudiant ajouté avec succès.");
            }
            else
            {
                Console.WriteLine("Erreur lors de l'ajout de l'étudiant.");
            }
        }

        private static void AfficherEtudiants()
        {
            var etudiants = Etudiant.GetEtudiants();
            foreach (var etudiant in etudiants)
            {
                Console.WriteLine($"ID: {etudiant.Id}, Nom: {etudiant.Nom}, Prénom: {etudiant.Prenom}, Numéro de Classe: {etudiant.NumeroClasse}, Date de Diplôme: {etudiant.DateDiplome}");
            }
        }

        private static void SupprimerEtudiant()
        {
            Console.Write("Entrez l'ID de l'étudiant à supprimer : ");
            int id = int.Parse(Console.ReadLine());
            var etudiant = Etudiant.GetById(id);

            if (etudiant != null && etudiant.Delete())
            {
                Console.WriteLine("Étudiant supprimé avec succès.");
            }
            else
            {
                Console.WriteLine("Erreur lors de la suppression de l'étudiant.");
            }
        }

        private static void ModifierEtudiant()
        {
            Console.Write("Entrez l'ID de l'étudiant à modifier : ");
            int id = int.Parse(Console.ReadLine());
            var etudiant = Etudiant.GetById(id);

            if (etudiant != null)
            {
                var etudiantModifie = LireInformationsEtudiant();
                etudiantModifie.Id = id;

                if (Etudiant.EditEtudiant(id, etudiantModifie))
                {
                    Console.WriteLine("Étudiant modifié avec succès.");
                }
                else
                {
                    Console.WriteLine("Erreur lors de la modification de l'étudiant.");
                }
            }
            else
            {
                Console.WriteLine("Aucun étudiant trouvé avec cet ID.");
            }
        }

        private static Etudiant LireInformationsEtudiant()
        {
            Console.Write("Entrez le nom : ");
            string nom = Console.ReadLine();

            Console.Write("Entrez le prénom : ");
            string prenom = Console.ReadLine();

            Console.Write("Entrez le numéro de classe : ");
            int numeroClasse;
            while (!int.TryParse(Console.ReadLine(), out numeroClasse))
            {
                Console.Write("Veuillez entrer un numéro de classe valide : ");
            }

            Console.Write("Entrez la date de diplôme (format YYYY-MM-DD) : ");
            DateTime dateDiplome;
            while (!DateTime.TryParse(Console.ReadLine(), out dateDiplome))
            {
                Console.Write("Veuillez entrer une date valide (format YYYY-MM-DD) : ");
            }

            return new Etudiant(nom, prenom, numeroClasse, dateDiplome);
        }
    }
}
