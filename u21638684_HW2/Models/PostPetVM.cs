using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace u21638684_HW2.Models
{
    public class PostPetVM
    {
        public int SelectedUserID { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
        public int SelectedPhoneNumber { get; set; }
        public IEnumerable<SelectListItem> PhoneList { get; set; }
        public IEnumerable<SelectListItem> BreedList { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }
        public IEnumerable<SelectListItem> LocationList { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Status { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public decimal Weight { get; set; }
        public string PetStory { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }


        public string LastName { get; set; }
        public List<PostPetVM> PetList { get; set; }

        public decimal Amount { get; set; }
        public decimal TotalDonatedAmount { get; set; }
        public decimal DonationGoal { get; set; }
        public bool GoalReached { get; set; }

        public string UserName { get; set; }
        public int AdoptedPetsCount { get; set; }
        public List<PostPetVM> AdoptedPetsList { get; set; }
    }
    }