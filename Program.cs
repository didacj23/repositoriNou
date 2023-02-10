using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Xml;

namespace botiga_cistella
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //variables i taules de botiga
            int nElemBotiga = 10;
            bool pTrobat = true;
            string producte = "";
            int posicioProdute = 0;
            double preuFinal = 0;


            string[] productesBotiga = new string[10];
            productesBotiga[0] = "camiseta";
            productesBotiga[1] = "sudadera";
            productesBotiga[2] = "pantalo";


            double[] preusProductes = new double[10];
            preusProductes[0] = 20.00;
            preusProductes[1] = 25.00;
            preusProductes[2] = 30.00;

            //variables i taules de botiga

            //variables i taules de cistella

            string[] productesCistella = new string[10];
            productesCistella[0] = "camiseta";
            productesCistella[1] = "sudadera";
            productesCistella[2] = "pantalo";

            int[] quantitat = new int[10];
            quantitat[0] = 2;
            quantitat[1] = 1;
            quantitat[2] = 3;

            int nElemCistella = 3;
            double diners = 20.00;

            string tiquet;

            //variables i taules de cistella



            //info: Hem de tenir en compte que per a cada producte tindrem la seva quantitat, per tant quan inserim un
            //producte hem d’inserir una quantitat i actualitzar nElements, no podrem posar un producte a la cistella si no
            //tenim suficient diners.

            tiquet = toString(productesCistella, quantitat, preusProductes, preuFinal, nElemCistella);

            Console.WriteLine(tiquet);

        }

        static string preguntarProductes(string producte)
        {
            Console.WriteLine("Introdueix el nom d'un producte: ");
            producte = Console.ReadLine();

            return (producte);
        }

        static void buscarProducte(ref bool pTrobat, int nElemBotiga, string producte, string[] productesBotiga, ref int posicioProdute)
        {

            pTrobat = true; //producte trobat
            bool semafor = true;

            for (int i = 0; i < nElemBotiga && semafor; i++)
            {
                if (producte == productesBotiga[i])
                {
                    pTrobat = true;
                    posicioProdute = i;
                    semafor = false;
                }
                else
                {
                    pTrobat = false;
                    
                }
            }


        }

        //cistella

        static void comprar(string[] productesBotiga, ref int[] quantitat, int nElementsCistella, ref double diners, double[] preusProductes, string producte, int nElemBotiga, ref bool pTrobat, ref int posicioProdute, ref double preuFinal, ref string[] productesCistella)
        {

            producte = preguntarProductes(producte);
            buscarProducte(ref pTrobat, nElemBotiga, producte, productesBotiga, ref posicioProdute);

            bool cResposta = true;
            int quantitatProducte;
            char resposta;


            while (cResposta)
            {
                if (pTrobat == false)
                {
                    Console.WriteLine("El producte no s'ha trobat");
                    Console.WriteLine("Vols introduir un altre? (S/N)");
                    resposta = char.Parse(Console.ReadLine());

                    if (resposta == 'S' || resposta == 's')
                    {
                        producte = preguntarProductes(producte);
                        buscarProducte(ref pTrobat, nElemBotiga, producte, productesBotiga, ref posicioProdute);
                    }
                    else
                        cResposta = false;

                }
                else
                {
                    Console.WriteLine("Quants/es" + producte + "vols afegir a la cistella ?");
                    quantitatProducte = Convert.ToInt32(Console.ReadLine());

                    quantitat[posicioProdute] = quantitatProducte;


                    preuFinal = preuFinal + preusProductes[posicioProdute] * quantitat[posicioProdute];

                    if (diners < preuFinal)
                    {
                        Console.WriteLine("No tens suficients diners per realitzar la compra");
                        Console.WriteLine("Vols augmentar el teu crèdit ? ");
                        resposta = Convert.ToChar(Console.ReadLine());


                        if (resposta == 'S' || resposta == 's')
                        {
                            double augment;

                            Console.WriteLine("Amb quants diners vols augmentar el crèdit ? ");
                            augment = Convert.ToDouble(Console.ReadLine());

                            diners = diners + augment;

                            Console.WriteLine("Torna a realitzar la compra");

                            preuFinal = 0;

                            comprar(productesBotiga, ref quantitat, nElementsCistella, ref diners, preusProductes, producte, nElemBotiga, ref pTrobat, ref posicioProdute, ref preuFinal, ref productesCistella);

                            
                        }
                        else
                            cResposta = false;

                    }
                    else
                    {

                        //no comprovo si hi ha espai ja que de la manera que ho he fet es imposible que es passi ja que per passar-se
                        //hauria de haber mes productes a la botiga que espai a la cistella i si augmentem la botiga automaticament també augmentem la cistella

                        productesCistella[posicioProdute] = producte;
                        quantitat[posicioProdute] = quantitatProducte;
                        cResposta = false;

                    }


                }

            }




        }

        static void ordenarCistella(ref string[] productesCistella, ref int[] quantitat, int nElemCistella)
        {
            bool canvis = true;

            for (int nVolta = 0; nVolta < nElemCistella - 1 && canvis; nVolta++)
            {
                canvis = false;

                for(int i = 0; i < nElemCistella - 1- nVolta; i++)
                {
                    if (productesCistella[i].CompareTo(productesCistella[i + 1]) > 0)
                    {
                        string temp = productesCistella[i];
                        productesCistella[i] = productesCistella[i + 1];
                        productesCistella[i + 1] = temp;

                        //qTemp (quantitat temporal)

                        int qTemp = quantitat[i];
                        quantitat[i] = quantitat[i + 1];
                        quantitat[i + 1] = qTemp;

                        canvis = true;


                    }
                }
            }
        }

        static void mostrar(string[] productesCistella, int[] quantitat, double[] preusProductes, double preuFinal, int nElemCistella)
        {
            for (int i = 0; i < nElemCistella; i++)
            {
                Console.WriteLine("Producte: " + productesCistella[i] + " Quantitat: " + quantitat[i] + " Preu Unitat " + preusProductes[i]);

            }

            Console.WriteLine("Preu Final: " + preuFinal);
        }

        static string toString(string[] productesCistella, int[] quantitat, double[] preusProductes, double preuFinal, int nElemCistella)
        {
            string tiquet = "";

            for (int i = 0; i < nElemCistella; i++)
            {
                tiquet =tiquet + "Producte: " + productesCistella[i] + " Quantitat: " + quantitat[i] + " Preu Unitat " + preusProductes[i] + '\n';
            }

            tiquet = tiquet + '\n' + "Preu Final: " + preuFinal;

            return tiquet;
        }
    }  
         
}