using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using u21638684_HW2.Models;

namespace u21638684_HW2.Controllers
{
    public class HomeController : Controller
    {
        private PetsRescueEntities db = new PetsRescueEntities();
        public ActionResult Index()
        {
            
            int adoptedPetsCount = db.Adoptions.Count();

            var adoptedPets = db.Adoptions
                .Include(a => a.Pet)
                .Include(a => a.User)
                .Select(a => new PostPetVM
                {
                    UserName = a.User.FirstName + " " + a.User.LastName,
                    PetName = a.Pet.PetName
                })
                .ToList();

            var viewModel = new PostPetVM
            {
                AdoptedPetsCount = adoptedPetsCount,
                AdoptedPetsList = adoptedPets
            };

            return View(viewModel);
        }
// Add contoller action----------------------------------------------------------------------------------------------------------------
        //GET
        public ActionResult Add()
        {
            var userList = db.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FirstName + " " + u.LastName
            }).ToList();

            var model = new PostPetVM
            {
                UserList = userList,
                BreedList = new List<SelectListItem>(),
                TypeList = GetPetTypeList(), 
                LocationList = GetLocationList(),
                GenderList = GetGenderList(),
                PhoneList = new List<SelectListItem>(),

            };

            return View(model);
        }

        // Controller action to get the breed list based on the selected pet type
        public ActionResult GetBreedsForType(string petType)
        {
            var breedList = GetBreedListForType(petType);

            var breedItems = breedList.Select(breed => new SelectListItem
            {
                Value = breed.Value,
                Text = breed.Text
            }).ToList();

            return Json(breedList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPhoneNumbers(int userId)
        {
            var phoneList = db.Users
                .Where(u => u.Id == userId)
                .Select(u => new SelectListItem
                {
                    Value = u.PhoneNo.ToString(),
                    Text = u.PhoneNo.ToString(),
                })
                .ToList();

            return Json(phoneList, JsonRequestBehavior.AllowGet);
        }


        // POST:Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PostPetVM model)
        {
            if (ModelState.IsValid)
            {

                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    model.ImageFile.SaveAs(path);

                    model.Image = "/Images/" + fileName;
                }


                var newPet = new Pet
                {

                    PetName = model.PetName,
                    UserId = model.SelectedUserID,
                    Type = model.Type,
                    Breed = model.Breed,
                    Location = model.Location,
                    Age = model.Age,
                    Gender = model.Gender,
                    Weight = model.Weight,
                    PetStory = model.PetStory,
                    Status = "Available",
                    Image = model.Image
                };

                db.Pets.Add(newPet);
                db.SaveChanges();

                return RedirectToAction("Pets", "Home");
            }

            // If model validation fails, return to the posting form with errors
            model.UserList = db.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FirstName + " " + u.LastName
            }).ToList();

            model.TypeList = GetPetTypeList();
            model.LocationList = GetLocationList();
            model.GenderList = GetGenderList();
            model.BreedList = GetBreedListForType(model.Type);
            return View(model);
        }
        private List<SelectListItem> GetPetTypeList()
        {

            var petTypes = new List<SelectListItem>
    {
        new SelectListItem { Value = "Dog", Text = "Dog" },
        new SelectListItem { Value = "Cat", Text = "Cat" },
         new SelectListItem { Value = "Bird", Text = "Bird" },

    };
            return petTypes;
        }
        private List<SelectListItem> GetLocationList()
        {

            var petLocations = new List<SelectListItem>
    {
        new SelectListItem { Value = "Gauteng", Text = "Gauteng" },
        new SelectListItem { Value = "Free State", Text = "Free State" },
        new SelectListItem { Value = "Kwazul Natal", Text = "Kwazul Natal" },
        new SelectListItem { Value = "Eastern Cape", Text = "Eastern Cape" },
        new SelectListItem { Value = "Limpopo", Text = "Limpopo" },
        new SelectListItem { Value = "Northern Cape", Text = "Northern Cape" },
        new SelectListItem { Value = "Western Cape", Text = "Western Cape" },
        new SelectListItem { Value = "Mpumalanga", Text = "Mpumalanga" },
        new SelectListItem { Value = "North West", Text = "North West" },
    };
            return petLocations;
        }

        private List<SelectListItem> GetGenderList()
        {

            var petGenders = new List<SelectListItem>
    {
        new SelectListItem { Value = "Male", Text = "Male" },
        new SelectListItem { Value = "Female", Text = "Female" },

    };
            return petGenders;
        }

        private List<SelectListItem> GetBreedListForType(string petType)
        {
            var petBreeds = new List<SelectListItem>();

            if (petType == "Dog")
            {
                petBreeds.Add(new SelectListItem { Value = "Swiss", Text = "Swiss" });
                petBreeds.Add(new SelectListItem { Value = "French", Text = "French" });
                petBreeds.Add(new SelectListItem { Value = "Bulldog", Text = "Bulldog" });

            }
            else if (petType == "Cat")
            {
                petBreeds.Add(new SelectListItem { Value = "Abyssinian", Text = "Abyssinian" });
                petBreeds.Add(new SelectListItem { Value = "Aegean", Text = "Aegean" });
                petBreeds.Add(new SelectListItem { Value = "AmericanS", Text = "American Shorthair" });

            }
            else if (petType == "Bird")
            {
                petBreeds.Add(new SelectListItem { Value = "AbyssinianL", Text = "Abyssinian Lovebird" });
                petBreeds.Add(new SelectListItem { Value = "BorderC", Text = "Border Canary" });
                petBreeds.Add(new SelectListItem { Value = "AmazonP", Text = "Amazon Parrots" });

            }
            return petBreeds;
        }
        public ActionResult Pets(string petType, string petBreed, string location)
        {

            var pets = db.Pets.Where(p => p.Status == "Available");

            if (!string.IsNullOrEmpty(petType) && !string.IsNullOrEmpty(petBreed))
            {
                pets = pets.Where(p => p.Type == petType && p.Breed == petBreed);
            }
            else if (!string.IsNullOrEmpty(location))
            {
                pets = pets.Where(p => p.Location == location);
            }
            var viewModel = pets.Select(p => new PostPetVM
            {
                PetId = p.Id,
                PetName = p.PetName,
                Type = p.Type,
                Breed = p.Breed,
                Location = p.Location,
                Age = p.Age,
                Gender = p.Gender,
                Weight = p.Weight,
                PetStory = p.PetStory,
                Status = p.Status,
                LastName = p.User.LastName,
                Image = p.Image
            }).ToList();

            // Populate dropdown lists for filtering
            var filteringModel = new PostPetVM
            {
                PetList = viewModel, 
                TypeList = GetPetTypeList(),
                LocationList = GetLocationList(),
                BreedList = GetBreedListForType(petType)
            };

            return View(filteringModel);
        }

//Viewpet action ------------------------------------------------------------------------------------------------------------------------
        public ActionResult Viewpet(int id)
        {
            var pet = db.Pets.FirstOrDefault(p => p.Id == id);
            if (pet == null)
            {
                return RedirectToAction("Pets");
            }
            var userList = db.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FirstName + " " + u.LastName
            }).ToList();

            var viewModel = new PostPetVM
            {
                PetId = pet.Id,
                Image = pet.Image,
                PetName = pet.PetName,
                Location = pet.Location,
                Age = pet.Age,
                Gender = pet.Gender,
                Weight = pet.Weight,
                PetStory = pet.PetStory,
                UserList = userList,
                PhoneList = new List<SelectListItem>(),
            };

            return View(viewModel);
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Viewpet(PostPetVM model)
        {
            if (ModelState.IsValid)
            {
               
                var adoption = new Adoption
                {
                    PetId = model.PetId,
                    UserId = model.SelectedUserID,
                };

                db.Adoptions.Add(adoption);
                db.SaveChanges();

                var pet = db.Pets.FirstOrDefault(p => p.Id == model.PetId);
                if (pet != null)
                {
                    pet.Status = "Adopted";
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Donate action-----------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult Donate()
        {
            decimal totalDonatedAmount = db.Donations.Sum(d => d.Amount) ?? 0;

        
            decimal donationGoal = 100000;
            var viewModel = new PostPetVM
            {
                TotalDonatedAmount = totalDonatedAmount,
                DonationGoal = donationGoal,
                GoalReached = totalDonatedAmount >= donationGoal,
                UserList = db.Users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.FirstName + " " + u.LastName
                }).ToList(),
                PhoneList = new List<SelectListItem>(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Donate(PostPetVM model)
        {
            if (ModelState.IsValid)
            {
                var donation = new Donation
                {
                    UserId = model.SelectedUserID,
                    Amount = model.Amount
                };

                db.Donations.Add(donation);
                db.SaveChanges();
            }

            decimal totalDonatedAmount = db.Donations.Sum(d => d.Amount) ?? 0;

            var viewModel = new PostPetVM
            {
                TotalDonatedAmount = totalDonatedAmount,
                DonationGoal = 100000,
                GoalReached = totalDonatedAmount >= model.DonationGoal,
                UserList = db.Users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.FirstName + " " + u.LastName
                }).ToList(),
                PhoneList = new List<SelectListItem>(),
            };

            return View(viewModel);
        }
    }
}