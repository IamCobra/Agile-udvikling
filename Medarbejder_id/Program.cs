using System.Text.Json;
using System.Text.Json.Serialization;

namespace Medarbejder_id
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Indtast fornavn:");
                string fornavn = Console.ReadLine();

                Console.Write("Indtast efternavn:");
                string efternavn = Console.ReadLine();

                bool validation = Validate(fornavn, efternavn);

                if (validation == true)
                {
                    string ID = Generate_ID(fornavn, efternavn);

                    Console.WriteLine(ID);

                    Upload_Json(fornavn, efternavn, ID);


                    break;
                }
                else
                {
                    Console.WriteLine("Hvordan kan dit navn indeholde et tal!?");
                    Console.WriteLine("Prøv igen");
                }
            }

        }


        static bool Validate(string fornavn, string efternavn)
        {
            int number;
            bool fornavn_Test = int.TryParse(fornavn, out number);
            bool efternavn_Test = int.TryParse(efternavn, out number);

            if (fornavn_Test == true || efternavn_Test == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static string Generate_ID(string fornavn, string efternavn)
        {
            Random rand = new Random();
            int number = rand.Next();

            if (fornavn.Length < 4)
            {
                int X_amount = 4 - fornavn.Length;

                for (int i = 0; i < X_amount; i++)
                {
                    fornavn = fornavn + "X";
                }
            }

            if (efternavn.Length < 4)
            {
                int Y_amount = 4 - efternavn.Length;

                for (int i = 0; i < Y_amount; i++)
                {
                    efternavn = efternavn + "Y";
                }
            }

            return (fornavn.Substring(0, 4) + efternavn.Substring(0, 4) + number);
        }



        static void Upload_Json(string fornavn, string efternavn, string ID)
        {
            List<data> _data = new List<data>();
            _data.Add(new data()
            {
                fornavn = fornavn,
                efternavn = efternavn,
                ID = ID
            });

            using FileStream createStream = File.Create(@"C:\Users\HFGF\source\repos\Medarbejder_id\Medarbejder_id\est.json");
            JsonSerializer.SerializeAsync(createStream, _data);

        }


    }
}