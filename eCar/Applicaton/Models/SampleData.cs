using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using eCar.Applicaton.Models.Service.Entities;

namespace eCar.Applicaton.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<ECarContext>
    {
        protected override void Seed(ECarContext context)
        {
            #region Departments
            //обрати внимание, ключевое поле инициализировать не нужно
            var departments = new List<Department>
                                  {
                                      new Department {Name = "Passenger cars", Description = "Road car where passengers ride"},
                                      new Department {Name = "Sport vehicles", Description = "Those cars with significant high performance features"},
                                      new Department {Name = "Trucks", Description = "A motor vehicle designed to transport cargo."},
                                      new Department {Name = "Military vehicles", Description = "A vehicle that includes all land combat and transportation vehicles, which are designed for or are significantly used by military forces"}
                                  };
            departments.ForEach(d => context.Departments.Add(d));
            context.SaveChanges();
            #endregion
            
            #region Categories
            //обрати внимание, ключевое поле инициализировать не нужно, а внешний ключ DepartmentID установится через Department
            var categories = new List<Category>
                                  {
                                      //Passenger cars
                                      new Category {Name = "Light", Department = departments.Single(d=>d.Name=="Passenger cars"),
                                          Description = "2,000–2,499 lb (907–1,134 kg)"},
                                      new Category {Name = "Mini",Department = departments.Single(d=>d.Name=="Passenger cars"), 
                                          Description = "1,500–1,999 lb (680–907 kg)"},
                                      new Category {Name = "Compact", Department = departments.Single(d=>d.Name=="Passenger cars"),
                                          Description = "2,500–2,999 lb (1,134–1,360 kg)"},
                                      new Category {Name = "Medium", Department = departments.Single(d=>d.Name=="Passenger cars"),
                                          Description = "3,000–3,499 lb (1,361–1,587 kg)"},
                                      new Category {Name = "Heavy", Department = departments.Single(d=>d.Name=="Passenger cars"),
                                          Description = "3,500 lb (1,588 kg) and over"},

                                      //Sport veshicles
                                      new Category {Name = "Rally", Department = departments.Single(d=>d.Name=="Sport vehicles"),
                                          Description = "A racing automobile built to the specification set by the FIA, the international motorsports governing body and compete in the outright class of the World Rally Championship (WRC)"},
                                      new Category {Name = "F-1", Department = departments.Single(d=>d.Name=="Sport vehicles"),
                                          Description = "A single-seat, open cockpit, open-wheel racing car with substantial front and rear wings, and an engine positioned behind the driver, intended to be used in competition at Formula One racing events"},
                                      
                                      //Trucks
                                      new Category {Name = "Trailer", Department = departments.Single(d=>d.Name=="Trucks"),
                                          Description = "Generally an unpowered vehicle pulled by a powered vehicle. Commonly, the term trailer refers to such vehicles used for transport of goods and materials"},
                                      new Category {Name = "Van", Department = departments.Single(d=>d.Name=="Trucks"),
                                          Description = "A van is a kind of vehicle used for transporting goods or people."},
                                      new Category {Name = "Other trucks", Department = departments.Single(d=>d.Name=="Trucks"),
                                          Description = "A motor vehicles designed to perform different tasks"},
                                      
                                      //Military vehicles
                                      new Category {Name = "Military trailers", Department = departments.Single(d=>d.Name=="Military vehicles"),
                                          Description = "Vehicles used for transport of different ammos and weapons."},
                                      new Category {Name = "Tanks", Department = departments.Single(d=>d.Name=="Military vehicles"),
                                          Description = "A tank is a tracked, armoured fighting vehicle designed for front-line combat which combines operational mobility and tactical offensive and defensive capabilities."},
                                      new Category {Name = "MICV", Department = departments.Single(d=>d.Name=="Military vehicles"),
                                          Description = "An infantry fighting vehicle (IFV), also known as a mechanized infantry combat vehicle (MICV), is a type of armoured fighting vehicle used to carry infantry into battle and provide fire support"},
                                      new Category {Name = "APC", Department = departments.Single(d=>d.Name=="Military vehicles"),
                                          Description = "An armoured personnel carrier (APC) is an armoured fighting vehicle designed to transport infantry to the battlefield. APCs are sometimes known to troops as 'battle taxis' or 'battle buses'."}  
                                  };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();
            #endregion

            #region Autos
            var autos = new List<Auto>
                            {
                                    #region Passenger cars
		                            new Auto()
                                    {
                                        Name = "Acura MDX",
                                        Description = "We found the MDX a very welcome long-weekend companion, with enough room for the occasional back-seat wardrobe change, space for plenty of gear, decent performance and fuel economy, and generally comfortable driving.",
                                        Image = "~/Content/images/Passenger cars/acura_mdx.jpg",
                                        Price = 20000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Light")
                                        
                                    },
                                    new Auto()
                                    {
                                        Name = "Buick Enclave",
                                        Description = "Still one of the best-looking big crossovers out there.",
                                        Image = "~/Content/images/Passenger cars/buick_enclave.jpg",
                                        Price = 15000.58M,
                                        PromoFront = false,
                                        PromoDept = true,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Medium")
                                    },
                                    new Auto()
                                    {
                                        Name = "Cadillac Escalade",
                                        Description = "Though this is a large vehicle, Escalades are stable and confident in low-to-moderate speed changes of direction.",
                                        Image = "~/Content/images/Passenger cars/cadillac_escalade.png",
                                        Price = 42000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Compact")
                                    },
                                    new Auto()
                                    {
                                        Name = "Lexus GX",
                                        Description = "Although it sports some modern touches, the 2010 Lexus GX 460 is an old-style large SUV that can hold many passengers and tow heavy loads, but it burns a lot of gas.",
                                        Image = "~/Content/images/Passenger cars/lexus_gx.PNG",
                                        Price = 55000.50M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Compact")
                                    },
                                    new Auto()
                                    {
                                        Name = "Toyota Highlander",
                                        Description = "Highlander shares its basic design with the more-expensive RX SUV from Toyota's premium Lexus division, and like the RX, offers gas and gas/electric hybrid models. Highlander seats up to seven and comes with front-wheel drive or with all-wheel drive that lacks low-range gearing.",
                                        Image = "~/Content/images/Passenger cars/toyota_highlander.PNG",
                                        Price = 33000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Medium")
                                    },
                                    new Auto()
                                    {
                                        Name = "Mazda CX-9",
                                        Description = "The ride is a bit stiff, and the navigation system isn't the easiest to use. Still, this crossover merits our Best Buy nod because it does many things well.",
                                        Image = "~/Content/images/Passenger cars/mazda_cx9.PNG",
                                        Price = 29020.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Medium")
                                    },
                                    new Auto()
                                    {
                                        Name = "Nissan Armada",
                                        Description = "The first car is from the Nissan. The car is the 2012 Nissan Armada. This car has a powerful performance. Under the bonnet, you can find that there is a powerful engine with 5.6 liters V8 engine capacity. That engine can produce power up to 317 Hp at 5,200 rpm. It is a powerful performance and it also a very good power to contain up to eight passengers. That engine of this car is also combined with 5 speeds automatic transmission. By that combination of transmission and engine capacity, this kind of 8 passenger vehicles list can run from 0–60 mph only in 7.8 seconds. It is a very good performance of a car.",
                                        Image = "~/Content/images/Passenger cars/nissan_armada.PNG",
                                        Price = 20000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Heavy")
                                    },
                                    new Auto()
                                    {
                                        Name = "GMC Yukon",
                                        Description = "If you prefer GMC to be your car, this car can be the best solution for your eight passenger vehicle need. That is the 2012 GMC Yukon. This is a very amazing car because there is large engine capacity in affordable price. Under the bonnet, you can find that there is a 6.2 liters V8 engine capacity. By that capacity, this car can produce 403 HP at 5700 rpm. This is a very good power for this car. Besides that, this kind of 8 passenger vehicles list is also equipped with 6 speeds automatic transmission.",
                                        Image = "~/Content/images/Passenger cars/gmc_yokon.PNG",
                                        Price = 42000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Heavy")
                                    },
                                    new Auto()
                                    {
                                        Name = "Mini Cooper",
                                        Description = "When I was sitting in the back of a taxi the other night, the driver turned and asked what line of business I was in. I said I worked at the Observer and wrote about cars. At this point I normally get asked two questions. The first is: 'Really?' The second is: 'What's the best car you've ever driven then?' This always sounds like a test. Is there a correct answer? Will they nod approvingly? To avoid these complications, I usually say: 'A Nissan Micra…' You know, to be funny, to mix it up. Anyway, true to form, the taxi driver said: 'Really?' But then he mixed it up. He said: 'What's the best car joke you've ever heard then?'",
                                        Image = "~/Content/images/Passenger cars/mini_cooper.PNG",
                                        Price = 12000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Mini")
                                    }, 
	#endregion

                                    #region Sport cars
                                    //rally
                                    new Auto()
                                    {
                                        Name = "Renault Clio",
                                        Description = "Renault Clio RAGNOTTI TOP N",
                                        Image = "~/Content/images/Sport cars/Renault_Clio.PNG",
                                        Price = 19000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Rally")
                                    },
                                    //rally
                                    new Auto()
                                    {
                                        Name = "Subaru sti",
                                        Description = "Subaru sti 2008 GP N 15",
                                        Image = "~/Content/images/Sport cars/subary_sti.PNG",
                                        Price = 56000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Rally")
                                    },
                                    //rally
                                    new Auto()
                                    {
                                        Name = "Ford escort mk2",
                                        Description = "Country: Romania,Wheel side: Left,Class: H clas,Shock absorbers: profle 3way,Power: 300",
                                        Image = "~/Content/images/Sport cars/ford_escord.PNG",
                                        Price = 20000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Rally")
                                    },
                                    //f-1
                                    new Auto()
                                    {
                                        Name = "1961 Vanwall VW14",
                                        Description = "The last racing Vanwall was an “unwieldy” rear engined machine produced for the 1961 3.0 litre Intercontinental Formula. Although showing promise when campaigned by John Surtees in two races, development was stopped short when the formula did not find success in Europe. The engine was enlarged to 2,605 cc (159 cu in) (96.0 mm × 90.0 mm (3.78 in × 3.54 in)), rated at 290 bhp (220 kW) on 100 octane petrol",
                                        Image = "~/Content/images/Sport cars/vanwall.PNG",
                                        Price = 28000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="F-1")
                                    },
                                    //f-1
                                    new Auto()
                                    {
                                        Name = "1974 Shadow DN3",
                                        Description = "This is the second model in Shadow’s coveted series of F1 cars. Driven by Tom Pryce in 1974, its best finish was 6th in the German Grand Prix at Nurburgring.Condition, Fully restored and its currently located in CA, USA",
                                        Image = "~/Content/images/Sport cars/shadow.PNG",
                                        Price = 39000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="F-1")
                                    },
                                    //f-1
                                    new Auto()
                                    {
                                        Name = "1977 Hesketh 308E",
                                        Description = "Hesketh 308E as driven during 77 & 78 seasons. Frank Dernie design which remained competitive in British F1 for several years. This car is in great condition, has HTP and necessary certificates.",
                                        Image = "~/Content/images/Sport cars/heskesh.PNG",
                                        Price = 20000.99M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="F-1")
                                    }, 
                                    #endregion

                                    #region Trucks
                                    new Auto()
                                    {
                                        //other truck
                                        Name = "Isuzu Elf NPR300 Flat Deck 1996",
                                        Description = "Year:1996,Industry:Trucks, Type:EPV, Gross vehicle mass:6200kg,COF Expiry 19/12/2012",
                                        Image = "~/Content/images/Trucks/isuzu_elf.PNG",
                                        Price = 7000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Other trucks")
                                    },
                                    new Auto()
                                    {
                                        //other truck
                                        Name = "Sakai TW502-1",
                                        Description = " Category:Other Construction Mobile Plant, Sub Category: Roller",
                                        Image = "~/Content/images/Trucks/sakai.PNG",
                                        Price = 5000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Other trucks")
                                    },
                                    new Auto()
                                    {
                                        //trailer
                                        Name = "Homebuilt Roadrunner 1991",
                                        Description = "Year 1991 Industry Trailers	Category Truck Trailers	Sub Category Curtainsider Trailer	Odometer 238,531 km",
                                        Image = "~/Content/images/Trucks/homebuilt.PNG",
                                        Price = 10070.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Trailer")
                                    },
                                    new Auto()
                                    {
                                        //van
                                        Name = "2013 Chevrolet Express diesel",
                                        Description = "not assinged",
                                        Image = "~/Content/images/Trucks/ch_express_diesel.jpg",
                                        Price = 19000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Van")
                                    },
                                    new Auto()
                                    {
                                        //van
                                        Name = "2009 Chevrolet Express passenger van",
                                        Description = "not assighed...",
                                        Image = "~/Content/images/Trucks/ch_express_passenger_van.jpg",
                                        Price = 19000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Van")
                                    },
                                    new Auto()
                                    {
                                        //van
                                        Name = "2010 Ford E-150",
                                        Description = "not assigned...",
                                        Image = "~/Content/images/Trucks/ford_e150.jpg",
                                        Price = 19000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Van")
                                    },
                                    new Auto()
                                    {
                                        //van
                                        Name = "2014 Ford Transit Connect",
                                        Description = "no...",
                                        Image = "~/Content/images/Trucks/ford_transit_connect.jpg",
                                        Price = 19000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Van")
                                    },
                                    new Auto()
                                    {
                                        //van
                                        Name = "2009 GMC Savana cargo van",
                                        Description = "no...",
                                        Image = "~/Content/images/Trucks/savana_cargo.jpg",
                                        Price = 33000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Van")
                                    },
                                    #endregion

                                    #region Military vehicles
                                    //MICV
                                    new Auto()
                                    {
                                        Name = "BMP-1",
                                        Description = "The Soviet military was first major military power to widely adopt the concept of an 'armored fighting infantry carrier' thro...",
                                        Image = "~/Content/images/Military/bmp-1-infantry-fighting-vehicle.jpg",
                                        Price = 8100.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="MICV")
                                        
                                    },
                                    new Auto()
                                    {
                                        Name = "BMP-2",
                                        Description = "The Soviet Union introduced the concept of the Infantry Fighting Vehicle (IFV) with the adoption of its BMP-1 in 1966. The ty...",
                                        Image = "~/Content/images/Military/bmp-2-infantry-fighting-vehicle.jpg",
                                        Price = 7000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="MICV")
                                    },
                                    new Auto()
                                    {
                                        Name = "BMP-3",
                                        Description = "Soviet Union engineers and factories garnered much experience in the massive armor battles against German and Axis forces tha...",
                                        Image = "~/Content/images/Military/bmp-3-infantry-fighting-vehicle.jpg",
                                        Price = 6700.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="MICV")
                                    },
                                    //APC
                                    new Auto()
                                    {
                                        Name = "BTR-60",
                                        Description = "The armored 8x8 wheeled BTR-60 of 1960 was developed as a direct replacement for the 6x6 wheeled BTR-152 series Armored Perso...",
                                        Image = "~/Content/images/Military/btr-60-armored-personnel-carrier.jpg",
                                        Price = 17000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="APC")
                                    },
                                    new Auto()
                                    {
                                        Name = "BTR-70",
                                        Description = "The BTR-70 represents a further development of the successful line of BTR-60 armored personnel carriers of Soviet design. The...",
                                        Image = "~/Content/images/Military/btr70.jpg",
                                        Price = 7000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="APC")
                                    },
                                    new Auto()
                                    {
                                        Name = "BTR-80",
                                        Description = "The BTR-80 was the logical evolution of the wheeled armored personnel carrier (APC) BTR series that more or less hit its stri...",
                                        Image = "~/Content/images/Military/btr80.jpg",
                                        Price = 7000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="APC")
                                    },
                                    new Auto()
                                    {
                                        Name = "BTR-90",
                                        Description = "The BTR-90 represents a further development of the BTR series of 8x8 armored personnel carriers that include the BTR-60, BTR-...",
                                        Image = "~/Content/images/Military/btr90.jpg",
                                        Price = 7000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="APC")
                                    },
                                    //tanks
                                    new Auto()
                                    {
                                        Name = "Challenger-1-MBT",
                                        Description = "The original combat 'tank' was born out of the fighting that was World War 1. However, these were hardly representative of th...",
                                        Image = "~/Content/images/Military/challenger-1-mbt.jpg",
                                        Price = 9700.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Tanks")
                                    },
                                    new Auto()
                                    {
                                        Name = "M1-Abrams",
                                        Description = "The M1 Abrams was designed by Chrysler Defense and produced under the General Dynamics brand. The tank was introduced in 1979...",
                                        Image = "~/Content/images/Military/m1-abrams.jpg",
                                        Price = 2200.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Tanks")
                                    },
                                    new Auto()
                                    {
                                        Name = "T-84",
                                        Description = "The nation of Ukraine was under the Soviet sphere of influence up to 1990 when the Empire was dissolved amidst a move to a mo...",
                                        Image = "~/Content/images/Military/t84-main-battle-tank.jpg",
                                        Price = 7000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Tanks")
                                    },
                                    new Auto()
                                    {
                                        Name = "Type-80",
                                        Description = "The NORINCO Type 80 is based on the Type 69 main battle tank albeit with an entirely new hull design making the Type 80 an en...",
                                        Image = "~/Content/images/Military/type80.jpg",
                                        Price = 7000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Tanks")
                                    },
                                    //mil trucks
                                    new Auto()
                                    {
                                        Name = "Mercedes-Benz UNIMOG",
                                        Description = "Mercedes-Benz UNIMOG (UNIversal-MOtor-Gerat)",
                                        Image = "~/Content/images/Military/mercedes-benz-unimog-truck.jpg",
                                        Price = 8000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Military trailers")
                                    },
                                    new Auto()
                                    {
                                        Name = "Oshkosh Medium Tactical Vehicle Replacement (MTVR)",
                                        Description = "The Medium Tactical Vehicle Replacement (or 'MTVR') is a heavy-duty utility vehicle produced by Oshkosh Truck Corporation in...",
                                        Image = "~/Content/images/Military/mtvr.jpg",
                                        Price = 7000.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Military trailers")
                                    },
                                    
                                    new Auto()
                                    {
                                        Name = "GMC CCKW 353 (Jimmy / Deuce-and-a-Half)",
                                        Description = "Before America's involvement in World War 2 began in late 1941, the US Army sent out a requirement to interested American man...",
                                        Image = "~/Content/images/Military/gmc-cckw-353.jpg",
                                        Price = 2900.00M,
                                        PromoFront = true,
                                        PromoDept = false,
                                        Thumbnail = "#",
                                        Category = categories.Single(c=>c.Name=="Military trailers")
                                    }
                                    #endregion
                               };
            autos.ForEach(a => context.Autos.Add(a));
            context.SaveChanges();
            #endregion
        }
    }
}
