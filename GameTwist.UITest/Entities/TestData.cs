using Bogus;

namespace GTAutomation.Entities
{
    public class User
    {
        public string Email { get; set; }
        public string NickName { get; set; }       
        public string Password { get; set; }
        public string Day { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }

        public static Faker<User> GetDetails { get; } =
            new Faker<User>()
                .RuleFor(user => user.NickName, faker => faker.Person.FirstName + faker.Person.Random.AlphaNumeric(5))
                .RuleFor(user => user.Email, faker => faker.Person.FirstName + "@mailinator.com")
                .RuleFor(user => user.Password, faker => "At1!" + faker.Person.Random.AlphaNumeric(6))
                .RuleFor(user => user.Day, "15")
                .RuleFor(user => user.Year, "2005")
                .RuleFor(user => user.Month, "March");          
    }

    public class APIPetData
    {
        public string PetName { get; set; }

        public static Faker<APIPetData> GetDetails { get; } =
            new Faker<APIPetData>()
                .RuleFor(apipetdata => apipetdata.PetName, faker => faker.Person.UserName);
    }

}

