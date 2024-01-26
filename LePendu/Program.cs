// Le jeu du pendu
// On a un tableau rempli de mots
// 1 mot est choisit aleatoirement
// On donne un nombre de chance à l'utilisateur
// Le joueur peut jouer une lettre par tour
// On stocke la lettre jouée
// Si la lettre se trouve dans le mot, on l affiche au bon endroit
// Si la lettre ne se trouve pas dans le mot on diminue la chance de 1
// On affiche le nombre de chance restant
// On affiche les lettres jouées
// Fin de partie
// On supprime le mot de la liste
// Si le joueur trouve le mot, on le félicite
// Si le joueur à perdu on l'informe
// On demande pour une nouvelle partie
// Si nouvelle partie RE tant qu'il y a des mots dans la liste
// Sinon au revoir

List<String> listeDeMot = new List<string>();
List<char> listeLettreJouee = new List<char>();
List<int> listeIndexLettre = new List<int>();

int nbVie=10;
bool wordFind = false;
bool letterFind = false;
char lettre;
bool rejouer=false;
String mot="";
Random rand = new Random();
int indexDuMot;
char[] motTableFormat;
char[] devinette;



// initialisation de la liste des mots
listeDeMot.Add("mayo");
listeDeMot.Add("saucisse");
listeDeMot.Add("sandwich");
listeDeMot.Add("carotte");
listeDeMot.Add("poule");


    do
    {
        wordFind = false;
        rejouer = false;
        // Choix du mot à retrouver
        indexDuMot = rand.Next(0, listeDeMot.Count());

        // Le mot est sous format tableau de caracteres
        motTableFormat = listeDeMot.ElementAt(indexDuMot).ToCharArray();

        // On clone le mot a rechercher
        devinette = new char[motTableFormat.Length];

        // Appelle a la fonction
        RemplirLeCloneDeTirets(devinette);

        // Debut de partie
        do
        {

            AfficherLeMotAdeviner(devinette);
            Console.WriteLine("Entrez une lettre : ");
            lettre = Char.Parse(Console.ReadLine().ToLower());



            //Verification de la lettre
            VerifierLettre(motTableFormat);

            // Verifier si on a deja joué la lettre
            if (!LettreDejaJouee(lettre, listeLettreJouee))
            {
                listeLettreJouee.Add(lettre);
                if (letterFind)
                {
                    RemplacementDesLettres(listeIndexLettre);

                }
                else nbVie--;
            }


            letterFind = false;
            listeIndexLettre.Clear();

            //Verifier si le mot a ete trouvé
            if (!devinette.Contains('_'))
            {
                int nbBonneLettre = 0;
                // comparer le clone et le mot pour verifier si c est trouvé
                for (int i = 0; i < devinette.Length; i++)
                {
                    if (devinette[i].Equals(motTableFormat[i]))
                    {
                        nbBonneLettre++;
                    }
                }
                if (nbBonneLettre == motTableFormat.Length) wordFind = true;
                else wordFind = false;
            }

            Console.WriteLine();
            Console.Write("Lettres jouees : ");
            AfficherLesLettresJouees(listeLettreJouee);

            Console.WriteLine("Nombre de chance restante: " + nbVie);


        } while (nbVie > 0 && !wordFind);
        // Fin de partie

        if (nbVie == 0) Console.WriteLine("Dommage! Vous avez perdu...");
        else if (wordFind) Console.WriteLine("Bravo! Vous avez gagné!");

        rejouer = RejouerUnePartie();
        // suppression du mot de la liste
        listeDeMot.RemoveAt(indexDuMot);
        listeLettreJouee.Clear();

    } while (listeDeMot.Count > 0 && rejouer);


if (listeDeMot.Count == 0)
{
    Console.WriteLine("Vous etes trop fort il n y a plus de mots");
}

Console.WriteLine("Au revoir!");



// Fonctions
// Afficher le mot sous forme de tirets
void RemplirLeCloneDeTirets( char[] t)
{
    for (int i = 0; i < t.Length; i++)
    {
        devinette[i] = '_';
    }
}

//Afficher les lettres jouees
void AfficherLesLettresJouees(List<char> l)
{
    for (int i = 0; i < l.Count; i++)
    {
        Console.Write(l[i]+" , ");
    }
    Console.WriteLine();
}

//Verifier si la lettre est dans le mot
void VerifierLettre(char[] t)
{
    for (int i = 0; i < t.Length; i++)
    {
        if (t[i] == lettre)
        {
            letterFind = true;
            //Ajout de l index de la lettre trouvee dans la liste
            listeIndexLettre.Add(i);
        }

    }
}

// Remplacer les lettres dans le clone
void RemplacementDesLettres(List<int> l)
{
    // Modifier la devinette
    for (int i = 0; i < l.Count; i++)
    {
        //Remplacement des lettres aux bons index
        devinette[l.ElementAt(i)] = lettre;
    }
}

// Rejouer une partie ?
bool RejouerUnePartie()
{
    bool re=false;
    Console.WriteLine("Voulez-vous commencer une nouvelle partie? O/N ");
    char newParty;
    do
    {
        newParty = Char.Parse(Console.ReadLine()?.ToLower() ?? "");
        

        switch (newParty)
        {
            case 'o':
                 re = true;
                 break;
                
            case 'n':
                 re= false;
                break;
               
            default:
                newParty = '\0';
                Console.WriteLine("Veuillez entrer O ou N");
                break;
        }

    } while (newParty.Equals('\0'));
    return re;
}

// Afficher le mot devinette
void AfficherLeMotAdeviner(char[] t)
{
    for (int i = 0; i < t.Length; i++)
    {
        Console.Write(t[i]+" ");
    }
    Console.WriteLine();
}

// Verifier si on a deja joué la lettre
bool LettreDejaJouee(char lettre,List<char> l)
{
    if (l.Contains(lettre))
    {

        Console.WriteLine("Vous avez déjà joué cette lettre, essayez une autre lettre");
        return true;
    }
    return false;
}
