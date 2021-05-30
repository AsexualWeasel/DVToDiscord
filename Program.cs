//Discord bots code
using Discord; 
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace DiscordBotOnly
{
    public class Program
    {
        public static int olvasos = 0;
        public static int intervallum = 4000;
        
        //shunterarraylists
        public static List<string> shunterIds = new List<string>();
        public static List<string> shunterFuelLevels = new List<string>();
        public static List<string> shunterSandLevels = new List<string>();
        public static List<string> shunterOilLevels = new List<string>();
        public static List<string> shunterRPMs = new List<string>();
        public static List<string> shunterSpeeds = new List<string>();
        public static List<string> shunterTemperatures = new List<string>();
        public static List<string> shunterengineisons = new List<string>();
        public static List<string> shuntersandings = new List<string>();
        public static List<string> shunterthrottlelevels = new List<string>();
        public static List<string> shunterindependentbrakestates = new List<string>();
        public static List<string> shunterbrakestates = new List<string>();
        public static List<string> shunterisremotecontrolleds = new List<string>();
        public static List<string> shunterwheelslips = new List<string>();
        public static List<string> shunterreversersymbols = new List<string>();
        //de6arraylists   de6
        public static List<string> de6Ids = new List<string>();
        public static List<string> de6FuelLevels = new List<string>();
        public static List<string> de6SandLevels = new List<string>();
        public static List<string> de6OilLevels = new List<string>();
        public static List<string> de6RPMs = new List<string>();
        public static List<string> de6Speeds = new List<string>();
        public static List<string> de6Temperatures = new List<string>();
        public static List<string> de6engineisons = new List<string>();
        public static List<string> de6sandings = new List<string>();
        public static List<string> de6throttlelevels = new List<string>();
        public static List<string> de6independentbrakestates = new List<string>();
        public static List<string> de6brakestates = new List<string>();
        public static List<string> de6isremotecontrolleds = new List<string>();
        public static List<string> de6wheelslips = new List<string>();
        public static List<string> de6reversersymbols = new List<string>();
        //steamer
        public static List<string> steamerids = new List<string>();
        public static List<string> steamersandlevels = new List<string>();
        public static List<string> tenderwaters = new List<string>();
        public static List<string> steamerthrottles = new List<string>();
        public static List<string> steamerspeeds = new List<string>();
        public static List<string> firetemps = new List<string>();
        public static List<string> steamerreversersymbolss = new List<string>();
        public static List<string> steamerindependents = new List<string>();
        public static List<string> steamerbrakes = new List<string>();
        public static List<string> steamerboilers = new List<string>();
        public static List<string> steamercoalinchamber = new List<string>();
        //traincarinfos
        public static List<string> loadedcargos = new List<string>();
        public static List<string> carids = new List<string>();
        public static List<string> traincarderailedbools = new List<string>();
        public static List<string> loadedcargoamount = new List<string>();
        public static List<string> traincarmass = new List<string>();
        //traininfos
        public static List<string> trainlength = new List<string>();
        public static List<string> trainweight = new List<string>();
        public static List<string> carscount = new List<string>();

        

        public static System.Timers.Timer StopWatchTimer = new System.Timers.Timer();
        public static Stopwatch sw = new Stopwatch();
        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        public static DiscordSocketClient _client;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler;
            _client.Log += Log;

            var token = "yourtokenhere";
            timer();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        
        

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public static void timer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = intervallum;
            aTimer.Enabled = true;
            aTimer.Start();
        }
       public static  string text = System.IO.File.ReadAllText("GamePath.txt");
       public static string playername = File.ReadAllText(text + "/DerailValleyStatusInfos/Configs/Playername.txt");
        public static string txtchannel = File.ReadAllText(text + "/DerailValleyStatusInfos/Configs/ServerId.txt");
        public static string guild = File.ReadAllText(text + "/DerailValleyStatusInfos/Configs/TextChannelId.txt");

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            
            shunterIds.Clear();
            shunterFuelLevels.Clear();
            shunterSandLevels.Clear();
            shunterOilLevels.Clear();
            shunterRPMs.Clear();
            shunterSpeeds.Clear();
            shunterengineisons.Clear();
            shunterTemperatures.Clear();
            shuntersandings.Clear();
            shunterthrottlelevels.Clear();
            shunterindependentbrakestates.Clear();
            shunterbrakestates.Clear();
            shunterisremotecontrolleds.Clear();
            shunterwheelslips.Clear();
            shunterreversersymbols.Clear();

            //de6arraylists   de6
            de6Ids.Clear();
            de6FuelLevels.Clear();
            de6SandLevels.Clear();
            de6OilLevels.Clear();
            de6RPMs.Clear();
            de6Speeds.Clear();
            de6Temperatures.Clear();
            de6engineisons.Clear();
            de6sandings.Clear();
            de6throttlelevels.Clear();
            de6independentbrakestates.Clear();
            de6brakestates.Clear();
            de6isremotecontrolleds.Clear();
            de6wheelslips.Clear();
            de6reversersymbols.Clear();

            //steamer
            steamerids.Clear();
            steamersandlevels.Clear();
            tenderwaters.Clear();
            steamerthrottles.Clear();
            steamerspeeds.Clear();
            firetemps.Clear();
            steamerreversersymbolss.Clear();
            steamerindependents.Clear();
            steamerbrakes.Clear();
            steamerboilers.Clear();
            steamercoalinchamber.Clear();

            //traincarinfos
            loadedcargos.Clear();
            carids.Clear();
            traincarderailedbools.Clear();
            loadedcargoamount.Clear();
            traincarmass.Clear();
            //generaltraininfos
            trainlength.Clear();
            trainweight.Clear();
            carscount.Clear();
        ///////

        List<string> shunterid = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterIds.txt").ToList();
            List<string> shunterfuel = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterFuelLevels.txt").ToList();
            List<string> shuntersand = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterSandLevels.txt").ToList();
            List<string> shunteroil = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterOilLevels.txt").ToList();
            List<string> shunterrpm = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterRPMLevels.txt").ToList();
            List<string> shunterspeed = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterSpeeds.txt").ToList();
            List<string> shunterengi = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterEngineStates.txt").ToList();
            List<string> shuntertemps = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterEngineTemps.txt").ToList();
            List<string> shuntersanding = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterIsSandingBools.txt").ToList();
            List<string> shunterthrottle = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterThrottleLevels.txt").ToList();
            List<string> shunterindep = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterIndependentBrakeStates.txt").ToList();
            List<string> shuntermain = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterMainBrakeStates.txt").ToList();
            List<string> shunterwheel = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterWheelslips.txt").ToList();
            List<string> shunterreversym = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterReverserSymbols.txt").ToList();
            List<string> shunterremote = File.ReadAllLines(text + "/DerailValleyStatusInfos/ShunterInformations/ShunterRemotes.txt").ToList();

            //manipulate data here
            foreach (string line1 in shunterid)
            {
                shunterIds.Add(line1);
            }
            foreach (string line2 in shunterfuel)
            {
                shunterFuelLevels.Add(line2);
            }
            foreach (string line3 in shuntersand)
            {
                shunterSandLevels.Add(line3);
            }
            foreach (string line4 in shunteroil)
            {
                shunterOilLevels.Add(line4);
            }
            foreach (string line5 in shunterrpm)
            {
                shunterRPMs.Add(line5);
            }
            foreach (string line6 in shunterspeed)
            {
                shunterSpeeds.Add(line6);
            }
            foreach (string line7 in shunterengi)
            {
                shunterengineisons.Add(line7);
            }
            foreach (string line8 in shuntertemps)
            {
                shunterTemperatures.Add(line8);
            }
            foreach (string line9 in shuntersanding)
            {
                shuntersandings.Add(line9);
            }
            foreach (string line10 in shunterthrottle)
            {
                shunterthrottlelevels.Add(line10);
            }
            foreach (string line11 in shunterindep)
            {
                shunterindependentbrakestates.Add(line11);
            }
            foreach (string line12 in shuntermain)
            {
                shunterbrakestates.Add(line12);
            }

            foreach (string line14 in shunterreversym)
            {
                shunterreversersymbols.Add(line14);
            }

            foreach (string line15 in shunterwheel)
            {
                shunterwheelslips.Add(line15);
            }
            foreach (string line16 in shunterremote)
            {
                shunterisremotecontrolleds.Add(line16);
            }
            //de6
            List<string> de6id = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6Ids.txt").ToList();
            List<string> de6fuel = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6FuelLevels.txt").ToList();
            List<string> de6sand = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6SandLevels.txt").ToList();
            List<string> de6oil = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6OilLevels.txt").ToList();
            List<string> de6rpm = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6RPMs.txt").ToList();
            List<string> de6speed = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6Speeds.txt").ToList();
            List<string> de6engi = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6EngineIsOnStates.txt").ToList();
            List<string> de6temp = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6Temperatures.txt").ToList();
            List<string> de6sanding = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6Sandingstates.txt").ToList();
            List<string> de6throttle = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6ThrottleLevels.txt").ToList();
            List<string> de6indep = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6IndependentBrakeStates.txt").ToList();
            List<string> de6main = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6MainBrakeStates.txt").ToList();
            List<string> de6wheel = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6WheelslipBools.txt").ToList();
            List<string> de6reversym = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6ReverserSymbols.txt").ToList();
            List<string> de6remote = File.ReadAllLines(text + "/DerailValleyStatusInfos/DieselElectric6Informations/De6IsRemoteControlledBools.txt").ToList();
            foreach (string sor1 in de6id)
            {
                de6Ids.Add(sor1);
            }
            foreach (string sor2 in de6fuel)
            {
                de6FuelLevels.Add(sor2);
            }
            foreach (string sor3 in de6sand)
            {
                de6SandLevels.Add(sor3);
            }
            foreach (string sor4 in de6oil)
            {
                de6OilLevels.Add(sor4);
            }
            foreach (string sor5 in de6rpm)
            {
                de6RPMs.Add(sor5);
            }
            foreach (string sor6 in de6speed)
            {
                de6Speeds.Add(sor6);
            }
            foreach (string sor7 in de6engi)
            {
                de6engineisons.Add(sor7);
            }
            foreach (string sor8 in de6temp)
            {
                de6Temperatures.Add(sor8);
            }
            foreach (string sor9 in de6sanding)
            {
                de6sandings.Add(sor9);
            }
            foreach (string sor10 in de6throttle)
            {
                de6throttlelevels.Add(sor10);
            }
            foreach (string sor11 in de6indep)
            {
                de6independentbrakestates.Add(sor11);
            }
            foreach (string sor12 in de6main)
            {
                de6brakestates.Add(sor12);
            }
            foreach (string sor13 in de6wheel)
            {
                de6wheelslips.Add(sor13);
            }
            foreach (string sor14 in de6reversym)
            {
                de6reversersymbols.Add(sor14);
            }
            foreach (string sor15 in de6remote)
            {
                de6isremotecontrolleds.Add(sor15);
            }
            //steamer

            List<string> steamids = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerIds.txt").ToList();
            List<string> steamsand = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerSandLevels.txt").ToList();
            List<string> tenderwat = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerWaterLevels.txt").ToList();
            List<string> steamerthr = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerThrottlelevels.txt").ToList();
            List<string> steamerspeed = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerSpeeds.txt").ToList();
            List<string> steamerfiret = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerFireTemperatures.txt").ToList();
            List<string> steamerreverse = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerReverserSymbols.txt").ToList();
            List<string> steamerindep = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerIndependentBrakeStates.txt").ToList();
            List<string> steamermainb = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerMainBrakeStates.txt").ToList();
            List<string> steamerboilerp = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerBoilerPressures.txt").ToList();
            List<string> steamercoalinch = File.ReadAllLines(text + "/DerailValleyStatusInfos/SteamerInformations/SteamerCoalInChamerLevels.txt").ToList();
            
            foreach (string lin1 in steamids)
            {
                steamerids.Add(lin1);
            }
            foreach (string lin2 in steamsand)
            {
                steamersandlevels.Add(lin2);
            }
            foreach (string lin3 in tenderwat)
            {
                tenderwaters.Add(lin3);

            }
            foreach (string lin4 in steamerthr)
            {
                steamerthrottles.Add(lin4);
            }
           foreach (string lin5 in steamerspeed)
            {
                steamerspeeds.Add(lin5);
             }
            foreach (string lin6 in steamerfiret)
            {
                firetemps.Add(lin6);
              }
             foreach (string lin7 in steamerreverse)
            {
                steamerreversersymbolss.Add(lin7);
            }
            foreach (string lin8 in steamerindep)
               {
                steamerindependents.Add(lin8);
              }
              foreach (string lin9 in steamermainb)
                {
                steamerbrakes.Add(lin9);
                }
                foreach (string lin10 in steamerboilerp)
                {
                steamerboilers.Add(lin10);
                }
                foreach (string lin11 in steamercoalinch)
                {
                steamercoalinchamber.Add(lin11);
                }
            //traincarinfos
            List<string> traincarids = File.ReadAllLines(text + "/DerailValleyStatusInfos/TrainCarInformations/TrainCarIds.txt").ToList();
            List<string> traincarcontents = File.ReadAllLines(text + "/DerailValleyStatusInfos/TrainCarInformations/TrainCarContents.txt").ToList();
            List<string> traincarcontentamounts = File.ReadAllLines(text + "/DerailValleyStatusInfos/TrainCarInformations/TrainCarCargoAmount.txt").ToList();
            List<string> traincarisderaileds = File.ReadAllLines(text + "/DerailValleyStatusInfos/TrainCarInformations/TrainCarDerailStatus.txt").ToList();
            List<string> traincarmasses = File.ReadAllLines(text + "/DerailValleyStatusInfos/TrainCarInformations/TrainCarMassValues.txt").ToList();
            
            foreach (string linee1 in traincarids)
            {
                carids.Add(linee1);
            }
            foreach (string linee2 in traincarcontents)
            {
                loadedcargos.Add(linee2);
            }
            foreach (string linee3 in traincarcontentamounts)
            {
                loadedcargoamount.Add(linee3);
            }
            foreach (string linee4 in traincarisderaileds)
            {
                traincarderailedbools.Add(linee4);
            }
            foreach (string linee5 in traincarmasses)
            {
                traincarmass.Add(linee5);
            }
            //Generaltraininfo
            List<string> trainlengths = File.ReadAllLines(text+"/DerailValleyStatusInfos/GeneralTrainInfos/TrainLength.txt").ToList();
            List<string> trainweights = File.ReadAllLines(text+"/DerailValleyStatusInfos/GeneralTrainInfos/TrainWeight.txt").ToList();
            List<string> carscounts = File.ReadAllLines(text+"/DerailValleyStatusInfos/GeneralTrainInfos/TrainCarCount.txt").ToList();
         
            foreach (string li1 in trainlengths)
            {
                trainlength.Add(li1);
            }
            foreach (string li2 in trainweights)
            {
                trainweight.Add(li2);
            }
            foreach (string li3 in carscounts)
            {
                carscount.Add(li3);
            }

            //readytosenddata
            olvasos++;
                if (olvasos == 1)
                {
                string serveridfajl = File.ReadAllText(text + "/DerailValleyStatusInfos/Configs/ServerId.txt");
                ulong serverid = (ulong)long.Parse(serveridfajl);
                string textchannelidfajl = File.ReadAllText(text + "/DerailValleyStatusInfos/Configs/TextChannelId.txt");
                ulong textchannelid = (ulong)long.Parse(textchannelidfajl);
                
                    ITextChannel channel = (ITextChannel)_client.GetGuild(serverid).GetChannel(textchannelid);
                    channel.SendMessageAsync("Data Read From files for : **"+playername+"**!");
                }


            }
            
            private Task CommandHandler(SocketMessage message)
            {
                string text = System.IO.File.ReadAllText("GamePath.txt");
                
               
                
                //int lengthofcommand = -1;
                if (!message.Content.StartsWith('!'))
                    return Task.CompletedTask;
                if (message.Author.IsBot)
                    return Task.CompletedTask;

                
                if (message.Content.Equals("!shunterinfo " + playername))
                {
                    if (shunterIds.Count == 0)
                    {
                        message.Channel.SendMessageAsync("There is no **shunters** in your train!");
                    }
                    else
                    {
                        for (int x = 0; x < shunterIds.Count; x++)
                        {
                            //Console.WriteLine(shunterIds.ElementAt(x));
                            message.Channel.SendMessageAsync("The Shunter with: **" + shunterIds.ElementAt(x) + "** ID has **" + shunterFuelLevels.ElementAt(x) + "L** Fuel, **" + shunterSandLevels.ElementAt(x) + "m³** Sand, **" +
                                shunterOilLevels.ElementAt(x) + "L** Oil, Engine:**" + shunterengineisons.ElementAt(x) + "**,  Engine Temperature: **" + shunterTemperatures.ElementAt(x) + "°C**, Engine RPM: **" + shunterRPMs.ElementAt(x) + " RPM **, Notch:** " + shunterthrottlelevels.ElementAt(x)
                                + " **, Independent Brake Applied : **" + shunterindependentbrakestates.ElementAt(x) + "**, Main Brake Applied:  **" + shunterbrakestates.ElementAt(x) + "**, Wheelslip :**" +
                                shunterwheelslips.ElementAt(x) + "**, Reverser: **" + shunterreversersymbols.ElementAt(x) + "**, Speed: **" + shunterSpeeds.ElementAt(x) + " Km/H**, " +
                                "Sanding: **" + shuntersandings.ElementAt(x) + "**");

                        }
                    }
                }
                else if (message.Content.Contains("!shunterinfo " + playername) && message.Content.Any(char.IsDigit))
                {
                    string a = message.Content;
                    string b = string.Empty;
                    int val = 0;
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (Char.IsDigit(a[i]))
                            b += a[i];
                    }

                    if (b.Length > 0)
                    {
                        val = int.Parse(b);
                    }
                    if (shunterIds.Count == 0)
                    {
                        message.Channel.SendMessageAsync("There is no **shunters** in your train!");
                    }
                    else
                    {
                        int shuntercount = shunterIds.Count - 1;
                        if (val > shunterIds.Count - 1)
                        {
                            message.Channel.SendMessageAsync("You only have **" + shunterIds.Count + "** shunters in you train! Please use numbers in this interval: **0-" + shuntercount + "**. Thank you! ");
                        }
                        else
                        {
                            //Console.WriteLine(shunterIds.ElementAt(x));
                            message.Channel.SendMessageAsync("The Shunter with: **" + shunterIds.ElementAt(val) + "** ID has **" + shunterFuelLevels.ElementAt(val) + "L** Fuel, **" + shunterSandLevels.ElementAt(val) + "m³** Sand, **" +
                                    shunterOilLevels.ElementAt(val) + "L** Oil, Engine:**" + shunterengineisons.ElementAt(val) + "**,  Engine Temperature: **" + shunterTemperatures.ElementAt(val) + "°C**, Engine RPM: **" + shunterRPMs.ElementAt(val) + " RPM **, Notch:** " + shunterthrottlelevels.ElementAt(val)
                                    + " **, Independent Brake Applied: **" + shunterindependentbrakestates.ElementAt(val) + "**, Main Brake Applied:**" + shunterbrakestates.ElementAt(val) + "**, Wheelslip :**" +
                                    shunterwheelslips.ElementAt(val) + "**, Reverser: **" + shunterreversersymbols.ElementAt(val) + "**, Speed: **" + shunterSpeeds.ElementAt(val) + " Km/H**, " +
                                    "Sanding: **" + shuntersandings.ElementAt(val) + "**");
                        }
                    }
                }
            else if (message.Content.Equals("!de6info " + playername))
            {
                if (de6Ids.Count == 0)
                {
                    message.Channel.SendMessageAsync("There is no **Diesel Electric 6** in your train!");
                }
                else
                {
                    for (int y = 0; y < de6Ids.Count; y++)
                    {
                        //Console.WriteLine(shunterIds.ElementAt(x));
                        message.Channel.SendMessageAsync("The De6 with: **" + de6Ids.ElementAt(y) + "** ID has **" + de6FuelLevels.ElementAt(y) + "L** Fuel, **" + de6SandLevels.ElementAt(y) + "m³** Sand, **" +
                            de6OilLevels.ElementAt(y) + "L** Oil, Engine:**" + de6engineisons.ElementAt(y) + "**,  Engine Temperature: **" + de6Temperatures.ElementAt(y) + "°C**, Engine RPM: **" + de6RPMs.ElementAt(y) + " RPM **, Notch:** " + de6throttlelevels.ElementAt(y)
                            + " **, Independent Brake Applied : **" + de6independentbrakestates.ElementAt(y) + "**, Main Brake Applied:  **" + de6brakestates.ElementAt(y) + "**, Wheelslip :**" +
                            de6wheelslips.ElementAt(y) + "**, Reverser: **" + de6reversersymbols.ElementAt(y) + "**, Speed: **" +de6Speeds.ElementAt(y) + " Km/H**, " +
                            "Sanding: **" + de6sandings.ElementAt(y) + "**");

                    }
                }
            }
            else if (message.Content.Contains("!de6info " + playername) && message.Content.Any(char.IsDigit))
            {
                string a = message.Content;
                string b = string.Empty;
                int val1 = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    if (Char.IsDigit(a[i]))
                        b += a[i];
                }

                if (b.Length > 0)
                {
                    val1 = int.Parse(b);
                }
                if (de6Ids.Count == 0)
                {
                    message.Channel.SendMessageAsync("There is no **Diesel Electric 6** in your train!");
                }
                else
                {
                    int de6count = de6Ids.Count - 1;
                    if (val1 > de6Ids.Count - 1)
                    {
                        message.Channel.SendMessageAsync("You only have **" + de6Ids.Count + "** shunters in you train! Please use numbers in this interval: **0-" + de6count + "**. Thank you! ");
                    }
                    else
                    {
                        //Console.WriteLine(shunterIds.ElementAt(x));
                        message.Channel.SendMessageAsync("The De6 with: **" + de6Ids.ElementAt(val1) + "** ID has **" + de6FuelLevels.ElementAt(val1) + "L** Fuel, **" + de6SandLevels.ElementAt(val1) + "m³** Sand, **" +
                            de6OilLevels.ElementAt(val1) + "L** Oil, Engine:**" + de6engineisons.ElementAt(val1) + "**,  Engine Temperature: **" + de6Temperatures.ElementAt(val1) + "°C**, Engine RPM: **" + de6RPMs.ElementAt(val1) + " RPM **, Notch:** " + de6throttlelevels.ElementAt(val1)
                            + " **, Independent Brake Applied : **" + de6independentbrakestates.ElementAt(val1) + "**, Main Brake Applied:  **" + de6brakestates.ElementAt(val1) + "**, Wheelslip :**" +
                            de6wheelslips.ElementAt(val1) + "**, Reverser: **" + de6reversersymbols.ElementAt(val1) + "**, Speed: **" + de6Speeds.ElementAt(val1) + " Km/H**, " +
                            "Sanding: **" + de6sandings.ElementAt(val1) + "**");
                    }
                }
            }
            else if (message.Content.Equals("!steamerinfo " + playername))
            {
                if (steamerids.Count == 0)
                {
                    message.Channel.SendMessageAsync("There is no **SH-2-8-2** in your train!");
                }
                else
                {
                    for (int v = 0; v < shunterIds.Count; v++)
                    {
                        //Console.WriteLine(shunterIds.ElementAt(x));
                        message.Channel.SendMessageAsync("The SH-2-8-2 with: **" + steamerids.ElementAt(v) + "** ID has **" + steamercoalinchamber.ElementAt(v) + "Units** Coal, **" + steamersandlevels.ElementAt(v) + "m³** Sand, **" +
                            "** Fire Temperature: **" + firetemps.ElementAt(v) + "°C**, Notch:** " + steamerthrottles.ElementAt(v)
                            + " **, Independent Brake Applied : **" + steamerindependents.ElementAt(v) + "**, Main Brake Applied:  **" + steamerbrakes.ElementAt(v) + "**, Reverser: **" + steamerreversersymbolss.ElementAt(v) + "**, Speed: **" + steamerspeeds.ElementAt(v) + " Km/H**, " +
                            "Boiler Pressure: **" + steamerboilers.ElementAt(v) + "**, Tender waterlevel: **"+tenderwaters.ElementAt(v)+"**");

                    }
                }
            }
            else if (message.Content.Contains("!steamerinfo " + playername) && message.Content.Any(char.IsDigit))
            {
                string a = message.Content;
                string b = string.Empty;
                int val2 = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    if (Char.IsDigit(a[i]))
                        b += a[i];
                }

                if (b.Length > 0)
                {
                    val2 = int.Parse(b);
                }
                if (steamerids.Count == 0)
                {
                    message.Channel.SendMessageAsync("There is no **SH-2-8-2** in your train!");
                }
                else
                {
                    int steamercount = steamerids.Count - 1;
                    if (val2 > steamerids.Count - 1)
                    {
                        message.Channel.SendMessageAsync("You only have **" + steamerids.Count + "** shunters in you train! Please use numbers in this interval: **0-" + steamercount + "**. Thank you! ");
                    }
                    else
                    {
                        //Console.WriteLine(shunterIds.ElementAt(x));
                        message.Channel.SendMessageAsync("The SH-2-8-2 with: **" + steamerids.ElementAt(val2) + "** ID has **" + steamercoalinchamber.ElementAt(val2) + "Units** Coal, **" + steamersandlevels.ElementAt(val2) + "m³** Sand, **" +
                            "** Fire Temperature: **" + firetemps.ElementAt(val2) + "°C**, Notch:** " + steamerthrottles.ElementAt(val2)
                            + " **, Independent Brake Applied : **" + steamerindependents.ElementAt(val2) + "**, Main Brake Applied:  **" + steamerbrakes.ElementAt(val2) + "**, Reverser: **" + steamerreversersymbolss.ElementAt(val2) + "**, Speed: **" + steamerspeeds.ElementAt(val2) + " Km/H**, " +
                            "Boiler Pressure: **" + steamerboilers.ElementAt(val2) + "**, Tender waterlevel: **" + tenderwaters.ElementAt(val2)+"**");
                    }
                }
            }
            else if (message.Content.Equals("!traincarinfo " + playername))
            {
                if (carids.Count == 0)
                {
                    message.Channel.SendMessageAsync("There is no **traincar** in your train!");
                }
                else
                {
                    for (int b = 0; b < carids.Count; b++)
                    {
                        //Console.WriteLine(shunterIds.ElementAt(x));
                        message.Channel.SendMessageAsync("The TrainCar with: **"+ carids.ElementAt(b) + "** ID has **" + loadedcargos.ElementAt(b) + "** Product, Amount: **"+loadedcargoamount.ElementAt(b)+"** Traincar's weight: **" + traincarmass.ElementAt(b) + "Kg**, Traincar is derailed: **" +
                            traincarderailedbools.ElementAt(b) + "**");

                    }
                }
            }
            else if (message.Content.Contains("!traincarinfo " + playername) && message.Content.Any(char.IsDigit))
            {
                string a = message.Content;
                string b = string.Empty;
                int val3 = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    if (Char.IsDigit(a[i]))
                        b += a[i];
                }

                if (b.Length > 0)
                {
                    val3 = int.Parse(b);
                }
                if (carids.Count == 0)
                {
                    message.Channel.SendMessageAsync("There is no **traincar** in your train!");
                }
                else
                {
                    int carcounts = carids.Count - 1;
                    if (val3 > carids.Count - 1)
                    {
                        message.Channel.SendMessageAsync("You only have **" + carids.Count + "** shunters in you train! Please use numbers in this interval: **0-" + carcounts + "**. Thank you! ");
                    }
                    else
                    {
                        //Console.WriteLine(shunterIds.ElementAt(x));
                        message.Channel.SendMessageAsync("The TrainCar with: **" + carids.ElementAt(val3) + "** ID has **" + loadedcargos.ElementAt(val3) + "** Product, Amount: **" + loadedcargoamount.ElementAt(val3) + "** Traincar's weight: **" + traincarmass.ElementAt(val3) + "Kg**, Traincar is derailed: **" +
                           traincarderailedbools.ElementAt(val3) + "**");
                    }
                }
            }
            else if (message.Content.Equals("!traininfo " + playername))
            {
                //Console.WriteLine(shunterIds.ElementAt(x));
                message.Channel.SendMessageAsync("Your train's length: **"+trainlength.ElementAt(0)+"** Meter , Weight: **"+trainweight.ElementAt(0)+"** T, TrainCar Count: **"+carscount.ElementAt(0)+"**.");
            }
            return Task.CompletedTask;
            }
        }
    }
