using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Data
{
    //Seed class used to seed data into database for showcasing site functionality
    public class Seed
    {
        //SeedData will take all data stored in DataSeeds/ and create the correct object for each
        public static void SeedData(DataContext context)
        {
            //A check to see if any users are in database already
            if (!context.Users.Any())
            {
                //User data location is stored as a string for reference
                var userData = System.IO.File.ReadAllText("Data/DataSeeds/UserSeedData.json");

                //List of user objects created from deserialized user data 
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                //A For loop is performed to iterate through each user in the list of user objects
                foreach (var user in users)
                {
                    //a byte array is declared for the passwordhash and salt
                    byte[] passwordhash, passwordsalt;

                    //Create password hash method is called which is below
                    CreatePasswordHash("password", out passwordhash, out passwordsalt);

                    //Hash is stored as hash
                    user.PasswordHash = passwordhash;

                    //Salt is stored as salt
                    user.PasswordSalt = passwordsalt;

                    //Email is then converted to lower
                    user.Email = user.Email.ToLower();

                    //User object is then written to the database
                    context.Users.Add(user);
                }

                //Databse is then saved
                context.SaveChanges();
            }

            //A check to see if any events are in database already
            if (!context.Events.Any())
            {
                //Event data location is stored as a string for reference
                var eventData = System.IO.File.ReadAllText("Data/DataSeeds/EventSeedData.json");

                //List of event object created from deserialized event data 
                var events = JsonConvert.DeserializeObject<List<Event>>(eventData);

                //A For loop is performed to iterate through each event in the list of event objects
                foreach (Event e in events)
                {
                    //Event object is then written to the database
                    context.Events.Add(e);
                }
                //Databse is then saved
                context.SaveChanges();
            }

            //A check to see if any bookings are in database already
            if (!context.Bookings.Any())
            {
                //Booking data location is stored as a string for reference
                var bookingData = System.IO.File.ReadAllText("Data/DataSeeds/BookingSeedData.json");

                //List of booking objects created from deserialized booking data 
                var bookings = JsonConvert.DeserializeObject<List<Booking>>(bookingData);

                //A For loop is performed to iterate through each booking in the list of booking objects
                foreach (var booking in bookings)
                {
                    //Booking object is then written to the database
                    context.Bookings.Add(booking);
                }
                //Databse is then saved
                context.SaveChanges();
            }

            //A check to see if any payments are in database already
            if (!context.Payments.Any())
            {
                //Payment data location is stored as a string for reference
                var paymentData = System.IO.File.ReadAllText("Data/DataSeeds/PaymentSeedData.json");

                //List of payment objects created from deserialized payment data 
                var payments = JsonConvert.DeserializeObject<List<Payment>>(paymentData);

                //A For loop is performed to iterate through each payment in the list of payment objects
                foreach (var payment in payments)
                {
                    //Payment object is then written to the database
                    context.Payments.Add(payment);
                }
                //Databse is then saved
                context.SaveChanges();
            }
        }

        //Create password hash is called within SeedUsers method
        //This mimics the method in auth repository only so the auth repository can rmeain private
        //The method is void as it will only create a hash and a salt plus hmac key reference 
        //Takes in the password string and the 2 byte arrays created in the register method
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //hmac uses a cryptography library 
            //the library generates a key for the salt to unlock the hash related to it
            //The using statement has been applied so that the IDispose interface the library is 
            //connected to is used at the end of the method call 
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {

                passwordSalt = hmac.Key;

                //Compute hash method within hmac takes a byte array 
                //The string password is converted to a byte array 
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


    }
}