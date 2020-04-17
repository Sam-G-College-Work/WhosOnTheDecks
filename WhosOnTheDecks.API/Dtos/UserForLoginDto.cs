namespace WhosOnTheDecks.API.Dtos
{
    //Login Data transfer object is used so a comparable 
    //user can be created in oreder to validate the credintials
    //in the database
    public class UserForLoginDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}