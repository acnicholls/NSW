// represents the C# object that is passed back by BFF
// var user = new
// {
//     cookiename = User.Identity.Name,
//     Id = response.Claims.FirstOrDefault(x => x.Type == "sub").Value,
//     Name = response.Claims.FirstOrDefault(x => x.Type == "name").Value,
//     FirstName = response.Claims.FirstOrDefault(x => x.Type == "given_name").Value,
//     LastName = response.Claims.FirstOrDefault(x => x.Type == "family_name").Value,
//     Username = response.Claims.FirstOrDefault(x => x.Type == "preferred_username").Value,
//     Website = response.Claims.FirstOrDefault(x => x.Type == "website").Value,
//     IsAuthenticated = true,
// };

const initialUser = {
  cookiename: "Initial User",
  id: -1,
  name: "Initial User",
  firstName: "Initial",
  lastName: "User",
  userName: "initialUser",
  website: "",
  isAuthenticated: false,
};

export default initialUser;
