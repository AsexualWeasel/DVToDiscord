using DV.Logic.Job;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DV;
using UnityModManagerNet;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Collections;

namespace DVToDiscord
{
    public class Main
    {
        public static System.Timers.Timer StopWatchTimer = new System.Timers.Timer();
        public static Stopwatch sw = new Stopwatch();
        public static int intervallum = 3000;
        public static UnityModManager.ModEntry mod;
        public static int launchbot = 0;
        public static Job currentJob; // Highest-paying active job.
        public static bool bonusOver; // Whether the bonus time is up.
        public static int numActiveJobs;
        public static Trainset lastTrain;
        public static int lastCarsCount;
        public static bool wasDerailed;
        public static float lastLength;
        public static float lastWeight;
        public static bool updateActivity;
        public static string activityState;
        public static string smallImageKey;
        public static string smallImageText;
        public static string activityDetails;
        public static long jobtime = 0;
        public static List<TaskData> Jobdata;
        //locostats
        public static TrainCar loco;
        public static Trainset train;
        public static TrainCar car;
        public static TrainCarType carType;

        //de6stats
        public static string de6id;
        public static float de6fuelamount;
        public static float de6sandamount;
        public static float de6oilamount;
        public static float de6RPM;
        public static float de6Speed;
        public static float de6temp;
        public static bool de6engineison;
        public static bool de6sanding;
        public static float de6throttle;
        public static float de6independent;
        public static float de6brake;
        public static bool de6remote;
        public static bool de6wheelslip;
        public static string de6reversymbol;

        //shunterstats
        public static string shunterid;
        public static float shunterfuelamount;
        public static float shuntersandamount;
        public static float shunteroilamount;
        public static float shunterRPM;
        public static float shunterspeed;
        public static float shunterengtemp;
        public static bool shunterengisOn;
        public static bool shunterSanding;
        public static float shunterthrottle;
        public static float shunterindependent;
        public static float shunterbrake;
        public static bool shunterremote;
        public static bool shunterwheelslip;
        public static string shunterreversersymbol;
        //shunterarraylists
        public static List<string> shunterIds = new List<string>();
        public static List<float> shunterFuelLevels = new List<float>();
        public static List<float> shunterSandLevels = new List<float>();
        public static List<float> shunterOilLevels = new List<float>();
        public static List<float> shunterRPMs = new List<float>();
        public static List<float> shunterSpeeds = new List<float>();
        public static List<float> shunterTemperatures = new List<float>();
        public static List<string> shunterengineisons = new List<string>();
        public static List<string> shuntersandings = new List<string>();
        public static List<float> shunterthrottlelevels = new List<float>();
        public static List<float> shunterindependentbrakestates = new List<float>();
        public static List<float> shunterbrakestates = new List<float>();
        public static List<string> shunterisremotecontrolleds = new List<string>();
        public static List<string> shunterwheelslips = new List<string>();
        public static List<string> shunterreversersymbols = new List<string>();
        //de6arraylists   de6
        public static List<string> de6Ids = new List<string>();
        public static List<float> de6FuelLevels = new List<float>();
        public static List<float> de6SandLevels = new List<float>();
        public static List<float> de6OilLevels = new List<float>();
        public static List<float> de6RPMs = new List<float>();
        public static List<float> de6Speeds = new List<float>();
        public static List<float> de6Temperatures = new List<float>();
        public static List<string> de6engineisons = new List<string>();
        public static List<string> de6sandings = new List<string>();
        public static List<float> de6throttlelevels = new List<float>();
        public static List<float> de6independentbrakestates = new List<float>();
        public static List<float> de6brakestates = new List<float>();
        public static List<string> de6isremotecontrolleds = new List<string>();
        public static List<string> de6wheelslips = new List<string>();
        public static List<string> de6reversersymbols = new List<string>();
        //steamer
        public static List<string> steamerids = new List<string>();
        public static List<float> steamersandlevels = new List<float>();
        public static List<float> tenderwaters = new List<float>();
        public static List<float> steamerthrottles = new List<float>();
        public static List<float> steamerspeeds = new List<float>();
        public static List<float> firetemps = new List<float>();
        public static List<string> steamerreversersymbolss = new List<string>();
        public static List<float> steamerindependents = new List<float>();
        public static List<float> steamerbrakes = new List<float>();
        public static List<float> steamerboilers = new List<float>();
        public static List<float> steamercoalinchamber = new List<float>();
        //traincarinfos
        public static List<CargoType> loadedcargos = new List<CargoType>();
        public static List<string> carids = new List<string>();
        public static List<string> traincarderailedbools = new List<string>();
        public static List<float> loadedcargoamount = new List<float>();
        public static List<float> traincarmass = new List<float>();


        static bool Load(UnityModManager.ModEntry modEntry)
        {
            mod = modEntry;
            mod.OnUnload = OnUnload;
            mod.OnUpdate = OnUpdate;
            bonusOver = false;
            currentJob = null;
            numActiveJobs = 0;
            updateActivity = true;
            
            timer();
            // Train Status Trackers
            lastCarsCount = -1;
            wasDerailed = false;
            lastLength = -1;
            lastWeight = -1;

            return true;
        }

        static void ReadyCallback()
        {
            mod.Logger.Log("Got ready callback.");
        }
        static void DisconnectedCallback(int errorCode, string message)
        {
            mod.Logger.Log(string.Format("Got disconnect {0}: {1}", errorCode, message));
        }
        static void ErrorCallback(int errorCode, string message)
        {
            mod.Logger.Log(string.Format("Got error {0}: {1}", errorCode, message));
        }
        static bool OnUnload(UnityModManager.ModEntry modEntry)
        {
            return true;
        }

        static Dictionary<string, string> generalCarNames = new Dictionary<string, string>
        {
            {
                "FlatbedMilitary",
                "Military Flatbed"
            },
            {
                "Flatbed",
                "Flatbed"
            },
            {
                "Autorack",
                "Autorack"
            },
            {
                "Tank",
                "Tank Car"
            },
            {
                "Boxcar",
                "Boxcar"
            },
            {
                "Hopper",
                "Hopper"
            },
            {
                "Passenger",
                "Passenger Car"
            },
            {
                "Nuclear",
                "Nuclear Flask"
            }
        };
        static string GetEmptyCarName(TrainCarType carType, bool multiple)
        {
            string name = CarTypes.DisplayName(carType).Split(' ')[0];
            // Oh boy here we go being YandereDev again!
            if (generalCarNames.ContainsKey(name))
                generalCarNames.TryGetValue(name, out name);
            return string.Format("Empty {0}{1}", name, multiple ? "s" : "");
        }
        static bool UpdateTrainStatus()
        {
            TrainCar car = PlayerManager.Car;
            Trainset train = car?.trainset;
            loco = null; // Current train has loco?
            bool changed = train != lastTrain;

            // If no current train, use last train if job is active.
            if (train == null)
            {
                // If no active job, switch to idle.
                if (currentJob == null)
                {
                    lastTrain = null;
                    lastCarsCount = -1;
                    lastLength = -1;
                    lastWeight = -1;
                    return changed;
                }
                // Otherwise keep using last train.
            }
            // Switch current train if player entered locomotive.
            else if (train == PlayerManager.LastLoco?.trainset)
            {
                lastTrain = train;
                loco = PlayerManager.LastLoco;
            }
            // Switch current train if player entered caboose.
            else if (car != null && CarTypes.IsCaboose(car.carType))
            {
                lastTrain = train;
                loco = car;
            }
            // No changes if otherwise, and we need to check the last train itself.
            if (lastTrain == null)
                return false;

            // Update status based on lastTrain.
            carType = TrainCarType.NotSet; // First found consist car type.
            CargoType cargo = CargoType.None; // First found cargo type.
            int consist = 0; // Number of cars in consist.
            int caboose = 0; // Consist has caboose?
            bool derailed = false; // Any car derailed?
            bool mixed = false; // Mixed cargo types?
            float length = 0; // Length of consist, excluding locos.
            float weight = 0; // Weight of consist, excluding locos.

            foreach (TrainCar c in lastTrain.cars)
            {


                derailed = derailed || c.derailed;
                if (CarTypes.IsAnyLocomotiveOrTender(c.carType))
                {
                    if (loco == null || CarTypes.IsCaboose(loco.carType))
                        loco = c;
                }
                else if (CarTypes.IsCaboose(c.carType))
                {
                    if (loco == null)
                        loco = c;
                    caboose++;
                    length += c.logicCar.length;
                    weight += c.logicCar.carOnlyMass;
                }
                else
                {
                    cargo = cargo == CargoType.None ? c.logicCar.CurrentCargoTypeInCar : cargo;
                    mixed = mixed || cargo != CargoType.None && cargo != c.logicCar.CurrentCargoTypeInCar;
                    carType = carType == TrainCarType.NotSet ? c.carType : carType;
                    consist++;
                    length += c.logicCar.length;
                    // weight += c.totalMass;
                    weight += c.logicCar.carOnlyMass + c.logicCar.LoadedCargoAmount * CargoTypes.GetCargoUnitMass(c.logicCar.CurrentCargoTypeInCar);
                }
            }

            changed = changed || lastCarsCount != lastTrain.cars.Count || lastLength != length || lastWeight != weight || wasDerailed != derailed;
            lastCarsCount = lastTrain.cars.Count;
            lastLength = length;
            lastWeight = weight;
            wasDerailed = derailed;

            if (changed)
            {
                if (loco != null)
                {
                    switch (loco.carType)
                    {
                        case TrainCarType.LocoShunter:
                            smallImageKey = "locoshunteryellow";
                            smallImageText = "DE2 Shunter";
                            break;
                        case TrainCarType.LocoSteamHeavy:
                        case TrainCarType.Tender:
                            smallImageKey = "locosteamgray";
                            smallImageText = "SH 2-8-2";
                            break;
                        case TrainCarType.LocoDiesel:
                            smallImageKey = "locodiesel";
                            smallImageText = "DE6 Diesel";
                            break;
                        case TrainCarType.CabooseRed:
                            smallImageKey = "carcaboosered";
                            smallImageText = "Caboose";
                            break;
                        default:
                            smallImageKey = "";
                            smallImageText = "";
                            break;
                    }
                }

                if (consist > 0)
                {
                    string emptyCarType = GetEmptyCarName(carType, consist > 1);
                    string cabooseState = caboose > 0 ? string.Format(", Caboose{0}", caboose > 1 ? "s" : "") : "";
                    string cargoName = cargo == CargoType.None ? string.Format("{0}{1}", emptyCarType, cabooseState) : string.Format("{0}{1}{2}", cargo.GetCargoName(), cabooseState, mixed ? ", etc." : "");
                    activityState = string.Format("{0}: {1:0.00} tons; {2:0.00} meters{3}", cargoName, weight / 1000f, length, derailed ? "; derailed" : "");
                }
                else // activityState = derailed ? "Derailed" : STATE_NO_CARGO;
                    activityState = string.Format("{0}{1}", "No Cargo", derailed ? "; derailed" : "");
            }

            return changed;
        }
        private static StationInfo ExtractStationInfoWithYardID(string yardId)
        {
            StationController stationController;
            if (IdGenerator.Instance != null && LogicController.Instance.YardIdToStationController != null && LogicController.Instance.YardIdToStationController.TryGetValue(yardId, out stationController))
            {
                return stationController.stationInfo;
            }
            return null;
        }
        static bool UpdateJobStatus()
        {
            bool changed = false;

            int curActiveJobs = PlayerJobs.Instance.currentJobs.Count;

            if (numActiveJobs != curActiveJobs)
            {
                Job highest = null;

                foreach (Job j in PlayerJobs.Instance.currentJobs)
                    if (highest == null || highest.GetBasePaymentForTheJob() < j.GetBasePaymentForTheJob())
                        highest = j;

                changed = currentJob != highest;
                currentJob = highest;
                // TODO: Determine if this actually happens.
                if (currentJob == null && numActiveJobs > 0)
                    numActiveJobs = -1; // Flag for checking this again.
                else
                    numActiveJobs = curActiveJobs;
            }

            if (currentJob != null)
            {
                Jobdata = currentJob.GetJobData();
            }

            if (changed)
            {
                if (currentJob == null)
                {
                    Jobdata.Clear();
                    Jobdata.Add(null);
                }
                else
                {
                    StationInfo srcStation = ExtractStationInfoWithYardID(currentJob.chainData.chainOriginYardId);
                    StationInfo stationInfo = ExtractStationInfoWithYardID(currentJob.chainData.chainDestinationYardId);
                    string jobTypeString;
                    string preposition;
                    switch (currentJob.jobType)
                    {
                        case JobType.ShuntingLoad:
                            stationInfo = srcStation;
                            jobTypeString = "Loading Cars";
                            preposition = "in";
                            break;
                        case JobType.ShuntingUnload:
                            jobTypeString = "Unloading Cars";
                            preposition = "in";
                            break;
                        case JobType.Transport:
                            jobTypeString = "Freight Haul";
                            preposition = "to";
                            break;
                        case JobType.EmptyHaul:
                            jobTypeString = "Logistical Haul";
                            preposition = "to";
                            //Power Move Jobs Integration

                            EmptyHaulJobData jobData = JobDataExtractor.ExtractEmptyHaulJobData(currentJob);
                            TrainCar car = PlayerManager.Car;
                            Trainset train = car?.trainset;
                            if (jobData.transportingCars.All(c => CarTypes.IsAnyLocomotiveOrTender(c.carType)))
                            {
                                jobTypeString = "Power Move";
                            }
                            break;
                        default:
                            stationInfo = srcStation;
                            jobTypeString = "Unknown Job";
                            preposition = "from";
                            break;
                    }
                    if (stationInfo != null)
                        activityDetails = string.Format("{0} {1} {2}", jobTypeString, preposition, stationInfo.Name);
                    else
                        activityDetails = jobTypeString;

                }
            }
            return changed;
        }
        public static bool updated = false;
        public static void timer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = intervallum;
            aTimer.Enabled = true;
            aTimer.Start();
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            updated = true;
            
        }



        public static void OnUpdate(UnityModManager.ModEntry _, float delta)
        {
            if (updateActivity)
            {
                
                
                if (lastTrain != null)
                {
                    List<TrainCar> onlycars = new List<TrainCar>();
                    List<TrainCar> locomotives = new List<TrainCar>();
                    locomotives.Clear();
                    onlycars.Clear();
                    foreach (var car in lastTrain.cars)
                    {
                        if (CarTypes.IsLocomotive(car.carType))
                        {
                            locomotives.Add(car);
                        }
                        else if (!CarTypes.IsLocomotive(car.carType))
                        {
                            onlycars.Add(car);
                        }

                    }

                    //lastTrain.cars.Where(c => CarTypes.IsLocomotive(c.carType));
                    string path2 = "DerailValleyStatusInfos/DieselElectric6Informations";
                    bool letezik = System.IO.Directory.Exists(path2);
                    string path3 = "DerailValleyStatusInfos/SteamerInformations";
                    bool vane = System.IO.Directory.Exists(path3);
                    string path4 = "DerailValleyStatusInfos/TrainCarInformations";
                    bool exisssst = System.IO.Directory.Exists(path4);
                    string path5 = "DerailValleyStatusInfos/GeneralTrainInfos";
                    bool letezikkkk = System.IO.Directory.Exists(path5);
                    string path10 = "DerailValleyStatusInfos/Configs";
                    bool path100 = System.IO.Directory.Exists(path10);
                    string path = "DerailValleyStatusInfos/ShunterInformations";
                    bool exists = System.IO.Directory.Exists(path);
                    if (exists == false)
                    {
                        System.IO.Directory.CreateDirectory(path);
                        System.IO.File.CreateText(path + "/ShunterIds.txt");
                        System.IO.File.CreateText(path + "/ShunterFuelLevels.txt");
                        System.IO.File.CreateText(path + "/ShunterSandLevels.txt");
                        System.IO.File.CreateText(path + "/ShunterOilLevels.txt");
                        System.IO.File.CreateText(path + "/ShunterRPMLevels.txt");
                        System.IO.File.CreateText(path + "/ShunterSpeeds.txt");
                        System.IO.File.CreateText(path + "/ShunterEngineTemps.txt");
                        System.IO.File.CreateText(path + "/ShunterEngineStates.txt");
                        System.IO.File.CreateText(path + "/ShunterIsSandingBools.txt");
                        System.IO.File.CreateText(path + "/ShunterThrottleLevels.txt");
                        System.IO.File.CreateText(path + "/ShunterIndependentBrakeStates.txt");
                        System.IO.File.CreateText(path + "/ShunterMainBrakeStates.txt");
                        System.IO.File.CreateText(path + "/ShunterRemotes.txt");
                        System.IO.File.CreateText(path + "/ShunterWheelSlips.txt");
                        System.IO.File.CreateText(path + "/ShunterReverserSymbols.txt");
                    }
                    else
                    if (letezik == false)
                    {
                        System.IO.Directory.CreateDirectory(path2);
                        System.IO.File.CreateText(path2 + "/De6Ids.txt");
                        System.IO.File.CreateText(path2 + "/De6FuelLevels.txt");
                        System.IO.File.CreateText(path2 + "/De6SandLevels.txt");
                        System.IO.File.CreateText(path2 + "/De6OilLevels.txt");
                        System.IO.File.CreateText(path2 + "/De6RPMs.txt");
                        System.IO.File.CreateText(path2 + "/De6Speeds.txt");
                        System.IO.File.CreateText(path2 + "/De6Temperatures.txt");
                        System.IO.File.CreateText(path2 + "/De6EngineIsOnStates.txt");
                        System.IO.File.CreateText(path2 + "/De6Sandingstates.txt");
                        System.IO.File.CreateText(path2 + "/De6ThrottleLevels.txt");
                        System.IO.File.CreateText(path2 + "/De6IndependentBrakeStates.txt");
                        System.IO.File.CreateText(path2 + "/De6MainBrakeStates.txt");
                        System.IO.File.CreateText(path2 + "/De6IsRemoteControlledBools.txt");
                        System.IO.File.CreateText(path2 + "/De6WheelslipBools.txt");
                        System.IO.File.CreateText(path2 + "/De6ReverserSymbols.txt");
                    }
                    else
                    if (vane == false)
                    {
                        System.IO.Directory.CreateDirectory(path3);
                        System.IO.File.CreateText(path3 + "/SteamerIds.txt");
                        System.IO.File.CreateText(path3 + "/SteamerSandLevels.txt");
                        System.IO.File.CreateText(path3 + "/SteamerWaterLevels.txt");
                        System.IO.File.CreateText(path3 + "/SteamerThrottlelevels.txt");
                        System.IO.File.CreateText(path3 + "/SteamerSpeeds.txt");
                        System.IO.File.CreateText(path3 + "/SteamerFireTemperatures.txt");
                        System.IO.File.CreateText(path3 + "/SteamerReverserSymbols.txt");
                        System.IO.File.CreateText(path3 + "/SteamerIndependentBrakeStates.txt");
                        System.IO.File.CreateText(path3 + "/SteamerMainBrakeStates.txt");
                        System.IO.File.CreateText(path3 + "/SteamerBoilerPressures.txt");
                        System.IO.File.CreateText(path3 + "/SteamerCoalInChamerLevels.txt");
                    }
                    else
                    if (exisssst == false)
                    {
                        System.IO.Directory.CreateDirectory(path4);
                        System.IO.File.CreateText(path4 + "/TrainCarContents.txt");
                        System.IO.File.CreateText(path4 + "/TrainCarCargoAmount.txt");
                        System.IO.File.CreateText(path4 + "/TrainCarDerailStatus.txt");
                        System.IO.File.CreateText(path4 + "/TrainCarMassValues.txt");
                        System.IO.File.CreateText(path4 + "/TrainCarIds.txt");
                    }
                    else
                    if (letezikkkk == false)
                    {
                        System.IO.Directory.CreateDirectory(path5);
                        System.IO.File.CreateText(path5 + "/TrainLength.txt");
                        System.IO.File.CreateText(path5 + "/TrainWeight.txt");
                        System.IO.File.CreateText(path5 + "/TrainCarCount.txt");
                    }
                    else
                    if (path100 == false)
                    {
                        System.IO.Directory.CreateDirectory(path10);
                        if (System.IO.Directory.Exists(path10 + "/Playername.txt") == false)
                        {
                            System.IO.File.CreateText(path10 + "/Playername.txt");
                        }
                        if (System.IO.Directory.Exists(path10 + "/ServerId.txt") == false)
                        {
                            System.IO.File.CreateText(path10 + "/ServerId.txt");
                        }
                        if (System.IO.Directory.Exists(path10 + "/TextChannelId.txt") == false)
                        {
                            System.IO.File.CreateText(path10 + "/TextChannelId.txt");
                        }
                    }
                    else
                    {
                        shunterIds.Clear();
                        shunterFuelLevels.Clear();
                        shunterSandLevels.Clear();
                        shunterOilLevels.Clear();
                        shunterRPMs.Clear();
                        shunterSpeeds.Clear();
                        shunterTemperatures.Clear();
                        shunterengineisons.Clear();
                        shuntersandings.Clear();
                        shunterthrottlelevels.Clear();
                        shunterindependentbrakestates.Clear();
                        shunterbrakestates.Clear();
                        shunterisremotecontrolleds.Clear();
                        shunterwheelslips.Clear();
                        shunterreversersymbols.Clear();
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

                        for (int x = 0; x < locomotives.Count; x++)
                        {
                            TrainCar Locos = locomotives.ElementAt(x);
                            if (Locos != null)
                            {
                                switch (Locos.carType)
                                {
                                    case TrainCarType.LocoShunter:
                                        LocoControllerShunter shuntercontoller = Locos.GetComponent<LocoControllerShunter>();
                                        shunterid = Locos.ID;
                                        shunterfuelamount = shuntercontoller.GetFuelAmount();
                                        shuntersandamount = shuntercontoller.GetSandAmount() / 100;
                                        shunteroilamount = shuntercontoller.GetOilAmount();
                                        shunterRPM = shuntercontoller.GetEngineRPM();
                                        shunterspeed = shuntercontoller.GetSpeedKmH();
                                        shunterengtemp = shuntercontoller.GetEngineTemp();
                                        shunterengisOn = shuntercontoller.GetEngineRunning();
                                        shunterSanding = shuntercontoller.IsSandOn();
                                        shunterthrottle = shuntercontoller.GetTargetThrottle();
                                        shunterindependent = shuntercontoller.GetTargetIndependentBrake();
                                        shunterbrake = shuntercontoller.GetTargetBrake();
                                        shunterremote = shuntercontoller.IsRemoteControlled();
                                        shunterwheelslip = shuntercontoller.IsWheelslipping();
                                        shunterreversersymbol = shuntercontoller.GetReverserSymbol();
                                        shunterIds.Add(shunterid);
                                        shunterFuelLevels.Add(shunterfuelamount);
                                        shunterSandLevels.Add(shuntersandamount);
                                        shunterOilLevels.Add(shunteroilamount);
                                        shunterRPMs.Add(shunterRPM);
                                        shunterSpeeds.Add(shunterspeed);
                                        shunterTemperatures.Add(shunterengtemp);
                                        if (shunterengisOn)
                                        {
                                            shunterengineisons.Add("Running");
                                        }
                                        else
                                        {
                                            shunterengineisons.Add("Not Running");
                                        }
                                        if (shunterSanding)
                                        {
                                            shuntersandings.Add("On");
                                        }
                                        else
                                        {
                                            shuntersandings.Add("Off");
                                        }
                                        shunterthrottlelevels.Add(shunterthrottle);
                                        shunterindependentbrakestates.Add(shunterindependent);
                                        shunterbrakestates.Add(shunterbrake);
                                        if (shunterremote)
                                        {
                                            shunterisremotecontrolleds.Add("Controlled Remotely");
                                        }
                                        else
                                        {
                                            shunterisremotecontrolleds.Add("Not Controlled Remotely");
                                        }
                                        if (shunterwheelslip)
                                        {
                                            shunterwheelslips.Add("Wheelslip detected");
                                        }
                                        else
                                        {
                                            shunterwheelslips.Add("No wheelslip detected");
                                        }
                                        shunterreversersymbols.Add(shunterreversersymbol);

                                        break;
                                    case TrainCarType.LocoSteamHeavy:
                                    case TrainCarType.Tender:
                                        LocoControllerSteam steamer = Locos.GetComponent<LocoControllerSteam>();
                                        string steamerid = Locos.ID;
                                        float steamercoalinchaamber = steamer.GetCoalInFirebox();
                                        float steamsandamount = steamer.GetSandAmount() / 100;
                                        float tenderwater = steamer.GetTenderWater();
                                        float steamerthrottle = steamer.GetTargetThrottle();
                                        float steamerspeed = steamer.GetSpeedKmH();
                                        float firetemp = steamer.GetFireTemperature();
                                        string steamerreversersymbols = steamer.GetReverserSymbol();
                                        float steamerindependent = steamer.GetTargetIndependentBrake();
                                        float steamerbrake = steamer.GetTargetBrake();
                                        float steamerboiler = steamer.GetBoilerPressure();
                                        steamerids.Add(steamerid);
                                        steamersandlevels.Add(steamsandamount);
                                        tenderwaters.Add(tenderwater);
                                        steamerthrottles.Add(steamerthrottle);
                                        steamerspeeds.Add(steamerspeed);
                                        firetemps.Add(firetemp);
                                        steamerreversersymbolss.Add(steamerreversersymbols);
                                        steamerindependents.Add(steamerindependent);
                                        steamerbrakes.Add(steamerbrake);
                                        steamerboilers.Add(steamerboiler);
                                        steamercoalinchamber.Add(steamercoalinchaamber);
                                        break;
                                    case TrainCarType.LocoDiesel:
                                        LocoControllerDiesel de6 = Locos.GetComponent<LocoControllerDiesel>();
                                        de6id = Locos.ID;
                                        de6fuelamount = de6.GetFuelAmount();
                                        de6sandamount = de6.GetSandAmount() / 100;
                                        de6oilamount = de6.GetOilAmount();
                                        de6RPM = de6.GetEngineRPM();
                                        de6Speed = de6.GetSpeedKmH();
                                        de6temp = de6.GetEngineTemp();
                                        de6engineison = de6.GetEngineRunning();
                                        de6sanding = de6.IsSandOn();
                                        de6throttle = de6.GetTargetThrottle();
                                        de6independent = de6.GetTargetIndependentBrake();
                                        de6brake = de6.GetTargetBrake();
                                        de6remote = de6.IsRemoteControlled();
                                        de6wheelslip = de6.IsWheelslipping();
                                        de6reversymbol = de6.GetReverserSymbol();
                                        de6Ids.Add(de6id);
                                        de6FuelLevels.Add(de6fuelamount);
                                        de6SandLevels.Add(de6sandamount);
                                        de6OilLevels.Add(de6oilamount);
                                        de6RPMs.Add(de6RPM);
                                        de6Speeds.Add(de6Speed);
                                        de6Temperatures.Add(de6temp);
                                        if (de6engineison)
                                        {
                                            de6engineisons.Add("Running");
                                        }
                                        else
                                        {
                                            de6engineisons.Add("Not Running");
                                        }
                                        if (de6sanding)
                                        {
                                            de6sandings.Add("On");
                                        }
                                        else
                                        {
                                            de6sandings.Add("Off");
                                        }

                                        de6throttlelevels.Add(de6throttle);
                                        de6independentbrakestates.Add(de6independent);
                                        de6brakestates.Add(de6brake);
                                        if (de6remote)
                                        {
                                            de6isremotecontrolleds.Add("Controlled Remotely");
                                        }
                                        else
                                        {
                                            de6isremotecontrolleds.Add("Not Controlled Remotely");
                                        }
                                        if (de6wheelslip)
                                        {
                                            de6wheelslips.Add("Wheelslip detected");
                                        }
                                        else
                                        {
                                            de6wheelslips.Add("No Wheelslip detected");
                                        }

                                        de6reversersymbols.Add(de6reversymbol);
                                        break;
                                    case TrainCarType.CabooseRed:

                                        break;
                                    default:
                                        smallImageKey = "";
                                        smallImageText = "";
                                        break;
                                }
                            }
                        }
                        carids.Clear();
                        loadedcargos.Clear();
                        loadedcargoamount.Clear();
                        traincarderailedbools.Clear();
                        for (int m = 0; m < onlycars.Count; m++)
                        {
                            TrainCar caratm = onlycars.ElementAt(m);
                            carids.Add(caratm.ID);
                            loadedcargos.Add(caratm.LoadedCargo);
                            loadedcargoamount.Add(caratm.LoadedCargoAmount);
                            if (caratm.derailed)
                            {
                                traincarderailedbools.Add("Derailed");
                            }
                            else
                            {
                                traincarderailedbools.Add("On the track");
                            }

                            traincarmass.Add(caratm.totalMass);
                        }

                        StreamWriter shunteridk = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterIds.txt");
                        StreamWriter shunterfuellevelsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterFuelLevels.txt");
                        StreamWriter shuntersandlevelsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterSandLevels.txt");
                        StreamWriter shunteroillevelsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterOilLevels.txt");
                        StreamWriter shunterRPMsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterRPMLevels.txt");
                        StreamWriter shunterSpeedsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterSpeeds.txt");
                        StreamWriter shunterTemperaturesf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterEngineTemps.txt");
                        StreamWriter shunterengineisonsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterEngineStates.txt");
                        StreamWriter shuntersandingsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterIsSandingBools.txt");
                        StreamWriter shunterthrottlelevelsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterThrottleLevels.txt");
                        StreamWriter shunterindependentsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterIndependentBrakeStates.txt");
                        StreamWriter shunterbrakesf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterMainBrakeStates.txt");
                        StreamWriter shunterremotesf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterRemotes.txt");
                        StreamWriter shunterwheelslipsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterWheelSlips.txt");
                        StreamWriter shunterreversersymbolsf = File.CreateText("DerailValleyStatusInfos/ShunterInformations/ShunterReverserSymbols.txt");
                        StreamWriter de6idk = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6Ids.txt");
                        StreamWriter de6fuel = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6FuelLevels.txt");
                        StreamWriter de6sand = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6SandLevels.txt");
                        StreamWriter de6oil = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6OilLevels.txt");
                        StreamWriter de6rpm = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6RPMs.txt");
                        StreamWriter de6speed = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6Speeds.txt");
                        StreamWriter de6tempf = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6Temperatures.txt");
                        StreamWriter de6engine = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6EngineIsOnStates.txt");
                        StreamWriter de6sandf = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6Sandingstates.txt");
                        StreamWriter de6throttlelev = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6ThrottleLevels.txt");
                        StreamWriter de6independ = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6IndependentBrakeStates.txt");
                        StreamWriter de6mainbrake = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6MainBrakeStates.txt");
                        StreamWriter de6isremotec = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6IsRemoteControlledBools.txt");
                        StreamWriter de6wheels = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6WheelslipBools.txt");
                        StreamWriter de6reverssymb = File.CreateText("DerailValleyStatusInfos/DieselElectric6Informations/De6ReverserSymbols.txt");
                        StreamWriter steameridk = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerIds.txt");
                        StreamWriter steamersandlevelsf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerSandLevels.txt");
                        StreamWriter tenderwatersf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerWaterLevels.txt");
                        StreamWriter steamerthrottlesf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerThrottlelevels.txt");
                        StreamWriter steamerspeedsf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerSpeeds.txt");
                        StreamWriter firetempsf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerFireTemperatures.txt");
                        StreamWriter steamerreversersymbolssf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerReverserSymbols.txt");
                        StreamWriter steamerindependentsf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerIndependentBrakeStates.txt");
                        StreamWriter steamerbrakesf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerMainBrakeStates.txt");
                        StreamWriter steamerboilersf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerBoilerPressures.txt");
                        StreamWriter steamercoalinchamberf = File.CreateText("DerailValleyStatusInfos/SteamerInformations/SteamerCoalInChamerLevels.txt");
                        StreamWriter caridk = File.CreateText("DerailValleyStatusInfos/TrainCarInformations/TrainCarIds.txt");
                        StreamWriter carcontents = File.CreateText("DerailValleyStatusInfos/TrainCarInformations/TrainCarContents.txt");
                        StreamWriter carcontentamounts = File.CreateText("DerailValleyStatusInfos/TrainCarInformations/TrainCarCargoAmount.txt");
                        StreamWriter isderailedfile = File.CreateText("DerailValleyStatusInfos/TrainCarInformations/TrainCarDerailStatus.txt");
                        StreamWriter traincarmassf = File.CreateText("DerailValleyStatusInfos/TrainCarInformations/TrainCarMassValues.txt");

                        StreamWriter trainlength = File.CreateText("DerailValleyStatusInfos/GeneralTrainInfos/TrainLength.txt");
                        StreamWriter trainweight = File.CreateText("DerailValleyStatusInfos/GeneralTrainInfos/TrainWeight.txt");
                        StreamWriter carscount = File.CreateText("DerailValleyStatusInfos/GeneralTrainInfos/TrainCarCount.txt");
                        for (int o = 0; o < shunterIds.Count; o++)
                        {
                            shunteridk.WriteLine(shunterIds.ElementAt(o));
                            shunterfuellevelsf.WriteLine(shunterFuelLevels.ElementAt(o));
                            shuntersandlevelsf.WriteLine(shunterSandLevels.ElementAt(o));
                            shunteroillevelsf.WriteLine(shunterOilLevels.ElementAt(o));
                            shunterRPMsf.WriteLine(shunterRPMs.ElementAt(o));
                            shunterSpeedsf.WriteLine(shunterSpeeds.ElementAt(o));
                            shunterTemperaturesf.WriteLine(shunterTemperatures.ElementAt(o));
                            shunterengineisonsf.WriteLine(shunterengineisons.ElementAt(o));
                            shuntersandingsf.WriteLine(shuntersandings.ElementAt(o));
                            shunterthrottlelevelsf.WriteLine(shunterthrottlelevels.ElementAt(o)*7);
                            shunterindependentsf.WriteLine(shunterindependentbrakestates.ElementAt(o).ToString("P0"));
                            shunterbrakesf.WriteLine(shunterbrakestates.ElementAt(o).ToString("P0"));
                            shunterremotesf.WriteLine(shunterisremotecontrolleds.ElementAt(o));
                            shunterwheelslipsf.WriteLine(shunterwheelslips.ElementAt(o));
                            shunterreversersymbolsf.WriteLine(shunterreversersymbols.ElementAt(o));
                        }
                        for (int b = 0; b < de6Ids.Count; b++)
                        {
                            de6idk.WriteLine(de6Ids.ElementAt(b));
                            de6fuel.WriteLine(de6FuelLevels.ElementAt(b));
                            de6sand.WriteLine(de6SandLevels.ElementAt(b));
                            de6oil.WriteLine(de6OilLevels.ElementAt(b));
                            de6rpm.WriteLine(de6RPMs.ElementAt(b));
                            de6speed.WriteLine(de6Speeds.ElementAt(b));
                            de6tempf.WriteLine(de6Temperatures.ElementAt(b));
                            de6engine.WriteLine(de6engineisons.ElementAt(b));
                            de6sandf.WriteLine(de6sandings.ElementAt(b));
                            de6throttlelev.WriteLine(de6throttlelevels.ElementAt(b)*7);
                            de6independ.WriteLine(de6independentbrakestates.ElementAt(b).ToString("P0"));
                            de6mainbrake.WriteLine(de6brakestates.ElementAt(b).ToString("P0"));
                            de6isremotec.WriteLine(de6isremotecontrolleds.ElementAt(b));
                            de6wheels.WriteLine(de6wheelslips.ElementAt(b));
                            de6reverssymb.WriteLine(de6reversersymbols.ElementAt(b));
                        }
                        for (int v = 0; v < steamerids.Count; v++)
                        {
                            steameridk.WriteLine(steamerids.ElementAt(v));
                            steamersandlevelsf.WriteLine(steamersandlevels.ElementAt(v));
                            tenderwatersf.WriteLine(tenderwaters.ElementAt(v));
                            steamerthrottlesf.WriteLine(steamerthrottles.ElementAt(v)*7);
                            steamerspeedsf.WriteLine(steamerspeeds.ElementAt(v));
                            firetempsf.WriteLine(firetemps.ElementAt(v));
                            steamerreversersymbolssf.WriteLine(steamerreversersymbolss.ElementAt(v));
                            steamerindependentsf.WriteLine(steamerindependents.ElementAt(v).ToString("P0"));
                            steamerbrakesf.WriteLine(steamerbrakes.ElementAt(v).ToString("P0"));
                            steamerboilersf.WriteLine(steamerboilers.ElementAt(v));
                            steamercoalinchamberf.WriteLine(steamercoalinchamber.ElementAt(v));
                        }

                        int de;

                        for (de = 0; de < carids.Count; de++)
                        {
                            caridk.WriteLine(carids.ElementAt(de));
                            carcontents.WriteLine(loadedcargos.ElementAt(de));
                            carcontentamounts.WriteLine(loadedcargoamount.ElementAt(de));
                            isderailedfile.WriteLine(traincarderailedbools.ElementAt(de));
                            traincarmassf.WriteLine(traincarmass.ElementAt(de));
                        }
                        carscount.WriteLine(lastCarsCount);
                        trainlength.WriteLine((int)lastLength);
                        trainweight.WriteLine(lastWeight / 1000);
                        traincarmassf.Close();
                        caridk.Close();
                        carcontents.Close();
                        carcontentamounts.Close();
                        isderailedfile.Close();
                        shunteridk.Close();
                        shunterfuellevelsf.Close();
                        shuntersandlevelsf.Close();
                        shunteroillevelsf.Close();
                        shunterRPMsf.Close();
                        shunterSpeedsf.Close();
                        shunterTemperaturesf.Close();
                        shunterengineisonsf.Close();
                        shuntersandingsf.Close();
                        shunterthrottlelevelsf.Close();
                        shunterindependentsf.Close();
                        shunterbrakesf.Close();
                        shunterremotesf.Close();
                        shunterwheelslipsf.Close();
                        shunterreversersymbolsf.Close();
                        de6idk.Close();
                        de6fuel.Close();
                        de6sand.Close();
                        de6oil.Close();
                        de6rpm.Close();
                        de6speed.Close();
                        de6tempf.Close();
                        de6engine.Close();
                        de6sandf.Close();
                        de6throttlelev.Close();
                        de6independ.Close();
                        de6mainbrake.Close();
                        de6isremotec.Close();
                        de6wheels.Close();
                        de6reverssymb.Close();
                        steameridk.Close();
                        steamersandlevelsf.Close();
                        tenderwatersf.Close();
                        steamerthrottlesf.Close();
                        steamerspeedsf.Close();
                        firetempsf.Close();
                        steamerreversersymbolssf.Close();
                        steamerindependentsf.Close();
                        steamerbrakesf.Close();
                        steamerboilersf.Close();
                        steamercoalinchamberf.Close();
                        trainlength.Close();
                        trainweight.Close();
                        carscount.Close();
                        if (launchbot == 1)
                        {
                            //string text = System.IO.File.ReadAllText("Mods/DVToDiscord/GamePath.txt");
                            System.Diagnostics.Process.Start("Mods/DVToDiscord/DiscordBot/DiscordBotOnly.exe");
                        }
                        launchbot++;
                    }


                }
                updated = false;
                updateActivity = false;
            }


            else
            {
                bool jobChanged = UpdateJobStatus();

                bool trainChanged = UpdateTrainStatus();
                updateActivity = jobChanged || trainChanged || updated;

            }
        }
    }
}


