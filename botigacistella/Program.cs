using System;

namespace botiga_cistella
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //variables i taules de botiga
            string[] productesBotiga = { "b", "a", "c", "e", "d", "f", "AA" };

            double[] preusProductes = { 2, 1, 3, 5, 4, 6, 9 };

            int nElemBotiga = 7;

            //variables i taules de cistella
            string[] productesCistella = { };
            string[] quantitat = { };
            int nElemCistella = productesCistella.Length;
            decimal diners = 0;

            //info: Hem de tenir en compte que per a cada producte tindrem la seva quantitat, per tant quan inserim un
            //producte hem d’inserir una quantitat i actualitzar nElements, no podrem posar un producte a la cistella si no
            //tenim suficient diners.

            int opcio = 0;


            MostrarMenuBotiga(ref opcio);
            DetectaOpcio(opcio, nElemBotiga, ref productesBotiga, ref preusProductes);          

        }

        static void MostrarMenuBotiga(ref int opcio)
        {
            do
            {
                Console.Clear();

                Console.WriteLine("BOTIGA - CISTELLA");

                Console.WriteLine("1. Afegir producte\n" +
                                  "2. Ampliar tenda\n" +
                                  "3. Modificar preu\n" +
                                  "4. Modificar producte\n" +
                                  "5. Ordenar producte\n" +
                                  "6. Ordenar preus\n" +
                                  "7. Mostrar botiga\n" +
                                  "8. ToString botiga\n" +
                                  "9. Sortir");

                Console.Write("Escriu una opcio: ");
                opcio = Convert.ToInt32(Console.ReadLine());
            } while (opcio > 9 || opcio < 1);

        }

        static void DetectaOpcio(int opcio, int nElemBotiga, ref string[] productesBotiga, ref double[] preusProductes)
        {
            string nomNouProducteUsuari = "", producte = "";
            double preuUsuari = 0;
            bool a;
            int numProducte = 1;

            switch (opcio)
            {
                case 1: //AFEGIR PRODUCTE
                    {
                        if (productesBotiga.Length == nElemBotiga)
                        {
                            char confirmacioAmpliar;
                            Console.Write("La botiga està plena. Vols ampliar la botiga? (s/n) ");
                            confirmacioAmpliar = Convert.ToChar(Console.ReadLine());
                            if (confirmacioAmpliar == 's' || confirmacioAmpliar == 'S')
                            {
                                AmpliarTenda(ref productesBotiga, ref preusProductes);

                                //OBTENIR NOM DEL PRODUCTE
                                a = true;
                                producte = IntroduirProducteValid(productesBotiga, nElemBotiga, a, numProducte);

                                //OBTENIR PREU DEL PRODUCTE
                                preuUsuari = IntroduirPreuValid();

                                //AFEGIR PRODUCTE   
                                AfegirProducte(ref productesBotiga, ref preusProductes, nElemBotiga, producte, preuUsuari);

                            }
                            
                        }
                        else
                        {
                            //OBTENIR NOM DEL PRODUCTE
                            a = true;
                            producte = IntroduirProducteValid(productesBotiga, nElemBotiga, a, numProducte);

                            //OBTENIR PREU DEL PRODUCTE
                            preuUsuari = IntroduirPreuValid();

                            //AFEGIR PRODUCTE 
                            AfegirProducte(ref productesBotiga, ref preusProductes, nElemBotiga, producte, preuUsuari);
                        }

                        break;
                    }

                case 2: //AMPLIAR TENDA
                    {
                        AmpliarTenda(ref productesBotiga, ref preusProductes);
                        break;
                    }

                case 3: //MODIFICAR PREU
                    {
                        a = false;
                        producte = IntroduirProducteValid(productesBotiga, nElemBotiga, a, numProducte);

                        preuUsuari = IntroduirPreuValid();

                        ModificarPreu(producte, preuUsuari, nElemBotiga, ref productesBotiga, ref preusProductes);
                        break;
                    }

                case 4: //MODIFICAR PRODUCTE 
                    {
                        //NOM DEL PRODUCTE QUE A MODIFICAR
                        a = false;
                        producte = IntroduirProducteValid(productesBotiga, nElemBotiga, a, numProducte);

                        //NOM NOU DEL PRODUCTE A MODIFICAR
                        a = true;
                        nomNouProducteUsuari = IntroduirProducteValid(productesBotiga, nElemBotiga, a, numProducte = 2);


                        ModificarProducte(nElemBotiga, producte, nomNouProducteUsuari, ref productesBotiga);
                        break;
                    }

                case 5: //ORDENAR PRODUCTE
                    {
                        OrdenarProducte(nElemBotiga, ref productesBotiga, ref preusProductes);
                        break;
                    }

                case 6: //ORDENAR PREUS
                    {
                        OrdenarPreu(nElemBotiga, ref productesBotiga, ref preusProductes);
                        break;
                    }

                case 7: //MOSTRAR BOTIGA
                    {
                        MostrarBotiga(nElemBotiga, ref productesBotiga, ref preusProductes);
                        break;
                    }

                case 8: //TO STRING BOTIGA
                    {
                        ToString(nElemBotiga, ref productesBotiga, ref preusProductes);
                        break;
                    }

                case 9: //SORTIR
                    {
                        Environment.Exit(0);
                        break;
                    }

            }
        }

        static void MostrarMenuCistella()
        {
            int opcio = 0;

            Console.Clear();

            do
            {
                Console.WriteLine("CISTELLA");

                Console.WriteLine("1. Comprar producte" +
                                  "2. Ordenar cistella\n" +
                                  "3. Mostra\n" +
                                  "4. ToString\n");

                Console.Write("Escriu una opcio: ");
                opcio = Convert.ToInt32(Console.ReadLine());
            } while (opcio > 6 || opcio < 1);

        }

        static string DemanarProducte(string producte, int numProducte)
        {
            Console.Write($"Introdueix el nom del producte {numProducte}: ");
            producte = Console.ReadLine();

            return (producte);
        }

        static double IntroduirPreuValid()
        {
            double preu;
            do
            {
                Console.Write("Introdueix el nou preu del producte: ");
                preu = Convert.ToDouble(Console.ReadLine());
            } while (preu < 1);

            return preu;
        }
        static string IntroduirProducteValid(string[] productesBotiga, int nElemBotiga, bool a, int numProducte)
        {
            string producte = "";
            producte = DemanarProducte(producte, numProducte);

            if (ExistirProducte(producte, productesBotiga, nElemBotiga) == a)
            //si el producte no existeix, demana'l fins que usuari introdueixi un que existeix
            {
                do
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    if (a) Console.WriteLine("El producte ja existeix");
                    else Console.WriteLine("El producte no existeix");
                    Console.BackgroundColor = ConsoleColor.Black;
                    producte = DemanarProducte(producte, numProducte);
                } while (ExistirProducte(producte, productesBotiga, nElemBotiga) == a);
            }

            return producte;
        }

        static void AfegirProducte(ref string[] productesBotiga, ref double[] preusProductes, int nElemBotiga, string producte, double preu)
        {
            productesBotiga[nElemBotiga] = producte;

            preusProductes[nElemBotiga] = preu;

            nElemBotiga++;

        }

        static bool ExistirProducte(string producte, string[] productesBotiga, int nElemBotiga)
        {
            bool existeix = false;

            for (int i = 0; i < nElemBotiga && !existeix; i++)
            {
                if (productesBotiga[i] == producte) existeix = true;
            }

            return existeix;
        }

        static void AmpliarTenda(ref string[] productesBotiga, ref double[] preusProductes)
        {
            int num = -1;

            do
            {
                Console.Write("Introduir el valor per ampliar: ");
                num = Convert.ToInt32(Console.ReadLine());
            } while (num < 1);

            Array.Resize(ref productesBotiga, productesBotiga.Length + num);
            Array.Resize(ref preusProductes, preusProductes.Length + num);
        }

        static void ModificarPreu(string producte, double preu, int nElemBotiga, ref string[] productesBotiga, ref double[] preusProductes)
        {
            int i = 0;

            while (i < nElemBotiga - 1 && producte != productesBotiga[i])
            {
                i++;
            }

            preusProductes[i] = preu;

        }

        static void ModificarProducte(int nElemBotiga, string producte, string nomNou, ref string[] productesBotiga)
        {
            int i = 0;

            while (i < nElemBotiga - 1 && producte != productesBotiga[i])
            {
                i++;
            }

            productesBotiga[i] = nomNou;

        }

        static void OrdenarProducte(int nElemBotiga, ref string[] productesBotiga, ref double[] preusProducte)
        {
            bool canvis = true;

            for (int i = 0; i < nElemBotiga - 1 && canvis; i++)
            {
                canvis = false;
                for (int j = 0; j < nElemBotiga - 1 - i; j++)
                {
                    if (productesBotiga[j].CompareTo(productesBotiga[j + 1]) > 0) //si el primer mes gran que el segon
                    {
                        //productesBotiga
                        string aux = productesBotiga[j];
                        productesBotiga[j] = productesBotiga[j + 1];
                        productesBotiga[j + 1] = aux;

                        //preu
                        double auxPreus = preusProducte[j];
                        preusProducte[j] = preusProducte[j + 1];
                        preusProducte[j + 1] = auxPreus;


                        canvis = true;
                    }

                }
            }

        }

        static void OrdenarPreu(int nElemBotiga, ref string[] productesBotiga, ref double[] preusProducte)
        {
            bool canvis = true;

            for (int i = 0; i < nElemBotiga - 1 && canvis; i++)
            {
                canvis = false;
                for (int j = 0; j < nElemBotiga - 1 - i; j++)
                {
                    if (preusProducte[j].CompareTo(preusProducte[j + 1]) > 0) //si el primer mes gran que el segon
                    {
                        //preu
                        double auxPreus = preusProducte[j];
                        preusProducte[j] = preusProducte[j + 1];
                        preusProducte[j + 1] = auxPreus;


                        //productesBotiga
                        string aux = productesBotiga[j];
                        productesBotiga[j] = productesBotiga[j + 1];
                        productesBotiga[j + 1] = aux;

                        canvis = true;
                    }

                }
            }

        }

        static void MostrarBotiga(int nElemBotiga, ref string[] productesBotiga, ref double[] preusProductes)
        {
            for (int a = 0; a < nElemBotiga; a++)
            {
                if (a % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;

                Console.WriteLine($"Producte: {productesBotiga[a]},  Preu: {preusProductes[a]} euros");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        static void ToString(int nElemBotiga, ref string[] productesBotiga, ref double[] preusProductes)
        {
            MostrarBotiga(nElemBotiga, ref productesBotiga, ref preusProductes);

            Console.WriteLine($"\nNúmero de productes: {nElemBotiga}");
            Console.WriteLine($"Espai restant lliure a la botiga: {productesBotiga.Length - nElemBotiga}");
        }

    }

}